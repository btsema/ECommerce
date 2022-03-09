using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class ShoppingCardRepository : IShoppingCardRepository
    {
        private readonly IDatabase _database;

        public ShoppingCardRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string backetId)
        {
            return await _database.KeyDeleteAsync(backetId);
        }

        public async Task<CustomerShoppingCard> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerShoppingCard>(data);
        }

        public async Task<CustomerShoppingCard> UpdateBasketAsync(CustomerShoppingCard basket)
        {
            var created = await _database.StringSetAsync(basket.Id, 
                JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            if (!created)
            {
                return null;
            }

            return await GetBasketAsync(basket.Id);
        }
    }
}
