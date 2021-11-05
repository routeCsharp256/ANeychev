using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    /// <summary>
    /// Статусы заявки на выдачу мерча сотруднику
    /// </summary>
    public class RequestStatus : Enumeration
    {
        /// <summary>
        /// Черновая заявка
        /// </summary>
        public static RequestStatus Draft = new(1, nameof(Draft));
        
        /// <summary>
        /// Заявка создана
        /// </summary>
        public static RequestStatus Created = new(1, nameof(Created));
        
        /// <summary>
        /// Заявка в процессе обработки
        /// </summary>
        public static RequestStatus InProgress = new(1, nameof(InProgress));
        
        /// <summary>
        /// Заявка завершен
        /// </summary>
        public static RequestStatus Done = new(1, nameof(Done));
            
        public RequestStatus(int id, string name) : base(id, name)
        {
        }
    }
}