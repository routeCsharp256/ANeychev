using System;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public class ClothingSize : Enumeration
    {
        public static ClothingSize XS = new(1, nameof(XS));
        public static ClothingSize S = new(2, nameof(S));
        public static ClothingSize M = new(3, nameof(M));
        public static ClothingSize L = new(4, nameof(L));
        public static ClothingSize XL = new(5, nameof(XL));
        public static ClothingSize XXL = new(6, nameof(XXL));

        public ClothingSize(int id, string name) : base(id, name)
        {
        }

        public static ClothingSize Parse(string name) => name.ToUpper() switch
        {
            "XS" => XS,
            "S" => S,
            "M" => M,
            "L" => L,
            "XL" => XL,
            "XXL" => XXL,
            _ => throw new Exception("Unknown size")
        };
    }
}