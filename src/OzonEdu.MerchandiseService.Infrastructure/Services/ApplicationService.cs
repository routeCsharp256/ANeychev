using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Contracts;
using OzonEdu.MerchandiseService.Infrastructure.Models;
using OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Services
{
    public sealed class ApplicationService : IApplicationService, IDisposable
    {
        private readonly IStockApiService _stockApiService;
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        public ApplicationService(IStockApiService stockApiService,
            IMerchRequestRepository merchRequestRepository,
            IMerchPackRepository merchPackRepository,
            IUnitOfWork unitOfWork,
            INotificationService notificationService)
        {
            _stockApiService = stockApiService ?? throw new ArgumentNullException(nameof(stockApiService));
            _merchRequestRepository =
                merchRequestRepository ?? throw new ArgumentNullException(nameof(merchRequestRepository));
            _merchPackRepository = merchPackRepository ?? throw new ArgumentNullException(nameof(merchPackRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        public async Task<MerchPack> GetMerchPackAsync(int merchTypeId,
            CancellationToken cancellationToken = default)
        {
            var type = MerchPackType.Parse(merchTypeId);
            var merchPack = await _merchPackRepository.FindByTypeAsync(type, cancellationToken);
            if (merchPack is null) throw new ArgumentNullException(nameof(merchPack));
            return merchPack;
        }

        public async Task<bool> IsNotRepeatedMerchRequestAsync(long employeeId, MerchPack merchPack,
            CancellationToken cancellationToken = default)
        {
            if (merchPack is null) throw new ArgumentNullException(nameof(merchPack));
            if (merchPack.CanBeReissued) return true;
            if (employeeId == 0) throw new ArgumentException($"Argument {nameof(employeeId)} mustn't be zero");

            var repeatMerckPack = (await FindMerchRequestsAsync(it =>
                    it.EmployeeId == employeeId &&
                    Equals(it.MerchPackType, merchPack.Type) &&
                    Equals(it.Status, RequestStatus.Done), cancellationToken))
                .Last();

            if (repeatMerckPack is null) return true;

            return merchPack.CanBeReissuedAfterDays.Value <
                   (DateTimeOffset.UtcNow - repeatMerckPack.DateOfCompleted).Days;
        }

        public async Task<MerchRequest> CreateMerchRequestAsync(long employeeId,
            ClothingSize clothingSize,
            Email employeeEmail,
            Email managerEmail,
            MerchPack merchPack,
            CancellationToken cancellationToken = default)
        {
            var merchRequest = new MerchRequest();
            merchRequest.Create(employeeId, clothingSize, employeeEmail, managerEmail);
            var items = new List<MerchRequestItem>();
            var stockItems = await _stockApiService.GetAllStockItemsAsync(cancellationToken);
            foreach (var merchItem in merchPack.Items)
            {
                var stockItem = stockItems.FirstOrDefault(it =>
                    it.ItemTypeId == merchItem.ItemType.Type.Id && it.ClothingSizeId == clothingSize.Id);
                if (stockItem is null)
                    throw new Exception(
                        $"Stock item doesn't exist with type: {merchItem.ItemType.Type.Name} and clothing size{clothingSize.Name}");
                items.Add(new MerchRequestItem(new Sku(stockItem.Sku), new Name(stockItem.Name),
                    new Item(ItemType.Parse((int) stockItem.ItemTypeId)), merchItem.Quantity));
            }

            merchRequest.StartWork(merchPack, items);
            await _unitOfWork.StartTransaction(cancellationToken);
            await _merchRequestRepository.CreateAsync(merchRequest, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return merchRequest;
        }

        public async Task TryGiveOutMerchRequestAsync(MerchRequest merchRequest,
            CancellationToken cancellationToken = default)
        {
            if (await _stockApiService.GiveOutItemsAsync(merchRequest.Items.Select(it => new StockItemQuantityDto
            {
                Sku = it.Sku.Value,
                Quantity = it.Quantity.Value
            }).ToList(), cancellationToken))
            {
                await _notificationService.SendMessageAsync(merchRequest.EmployeeEmail,
                    (MerchType) Enum.Parse(typeof(MerchType), merchRequest.MerchPackType.Name), cancellationToken);
                await _notificationService.SendMessageAsync(merchRequest.ManagerEmail,
                    $"Give out merch request number {merchRequest.RequestNumber}.", cancellationToken);
                merchRequest.Complete();
                await _unitOfWork.StartTransaction(cancellationToken);
                await _merchRequestRepository.UpdateAsync(merchRequest, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IReadOnlyCollection<MerchRequest>> FindMerchRequestsAsync(Func<MerchRequest, bool> predicate,
            CancellationToken cancellationToken = default)
            => (await _merchRequestRepository.FindAsync(predicate, cancellationToken)).OrderBy(it => it.DateOfCompleted)
                .ToList();

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}