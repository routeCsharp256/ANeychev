using System.Collections.Generic;

namespace OzonEdu.MerchandiseService.Infrastructure.Models
{
    public interface IItemsModel<TItemsModel>
        where TItemsModel : class
    {
        IReadOnlyList<TItemsModel> Items { get; set; }
    }
}