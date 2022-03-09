using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CustomerShoppingCard
    {
        public CustomerShoppingCard()
        {
        }

        public CustomerShoppingCard(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public List<ShoppingCardItem> Items { get; set; } = new List<ShoppingCardItem>();
        public decimal AdditionalCost { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        
    }
}
