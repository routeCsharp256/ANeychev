using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Configuration;
using OzonEdu.MerchandiseService.Infrastructure.Models;
using OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces;
using OzonEdu.StockApi.Grpc;

namespace OzonEdu.MerchandiseService.Infrastructure.Services
{
    public class StockApiService : IStockApiService, IDisposable
    {
        private readonly GrpcChannel _stockApiGrpcChannel;
        private readonly StockApiGrpc.StockApiGrpcClient _stockApiGrpcClient;

        public StockApiService(IOptions<StockApiGrpcConnectionOptions> options)
        {
            _stockApiGrpcChannel = GrpcChannel.ForAddress(options.Value.ConnectionString);
            _stockApiGrpcClient = new StockApiGrpc.StockApiGrpcClient(_stockApiGrpcChannel);
        }

        public async Task<IReadOnlyList<ItemTypeDto>> GetItemTypesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var itemTypesResult =
                    await _stockApiGrpcClient.GetItemTypesAsync(new Empty(), null, null, cancellationToken);
                return itemTypesResult.Items.Select(it => new ItemTypeDto {Id = (int) it.Id, Name = it.Name}).ToList();
            }
            catch (RpcException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IReadOnlyList<StockItemQuantityDto>> GetStockItemsAvailabilityAsync(
            IEnumerable<MerchRequestItem> items,
            CancellationToken cancellationToken = default)
        {
            var request = new SkusRequest
            {
                Skus =
                {
                    items.Select(it => it.Sku.Value)
                }
            };

            try
            {
                var stockItemsAvailabilityResponse =
                    await _stockApiGrpcClient.GetStockItemsAvailabilityAsync(request, null, null, cancellationToken);

                return stockItemsAvailabilityResponse.Items
                    .Select(it => new StockItemQuantityDto
                    {
                        Quantity = it.Quantity,
                        Sku = it.Sku
                    })
                    .ToList();
            }
            catch (RpcException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IReadOnlyList<StockItemDto>> GetAllStockItemsAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                var stockItems =
                    await _stockApiGrpcClient.GetAllStockItemsAsync(new Empty(), null, null, cancellationToken);
                return stockItems.Items.Select(it => new StockItemDto
                {
                    Sku = it.Sku,
                    ItemTypeId = it.ItemTypeId,
                    Name = it.ItemName,
                    ClothingSizeId = it.SizeId,
                    Quantity = it.Quantity
                }).ToList();
            }
            catch (RpcException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> GiveOutItemsAsync(IReadOnlyList<StockItemQuantityDto> items,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new GiveOutItemsRequest
                {
                    Items =
                    {
                        items.Select(it => new SkuQuantityItem
                        {
                            Sku = it.Sku,
                            Quantity = it.Quantity
                        }).ToList()
                    }
                };
                var response = await _stockApiGrpcClient.GiveOutItemsAsync(request, null, null, cancellationToken);
                if (response is null) throw new Exception("Didn't get answer");

                return response.Result switch
                {
                    GiveOutItemsResponse.Types.Result.Successful => true,
                    GiveOutItemsResponse.Types.Result.OutOfStock => false,
                    _ => throw new Exception("Unknown response")
                };
            }
            catch (RpcException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IReadOnlyList<ItemTypeDto>> GetByItemTypeAsync(int id,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _stockApiGrpcChannel.Dispose();
        }
    }
}