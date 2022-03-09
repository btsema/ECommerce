using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public ProductType ProductType { get; set; }
        public ProductBrandInfo BrandInfo { get; set; }
        public int ProductTypeId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageDestination { get; set; }
        public int ProductBrandId { get; set; }
        public bool InStock { get; set; }
        public int GuaranteePeriod { get; set; }
    }
}
