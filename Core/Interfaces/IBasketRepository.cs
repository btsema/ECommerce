using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IShoppingCardRepository
    {
        Task<bool> DeleteBasketAsync(string backetId);
        Task<CustomerShoppingCard> GetBasketAsync(string basketId);
        Task<CustomerShoppingCard> UpdateBasketAsync(CustomerShoppingCard basket);

    }
}
