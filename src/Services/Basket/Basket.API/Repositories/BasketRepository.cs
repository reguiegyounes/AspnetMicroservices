using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            this._redisCache = redisCache?? throw new ArgumentNullException(nameof(redisCache));
        }
        public async Task DeleteBasketAsync(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async  Task<ShoppingCart> GetBasketAsync(string userName)
        {
            var basket =await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart cart)
        {
            await _redisCache.SetStringAsync(cart.UserName,JsonConvert.SerializeObject(cart));
            return await GetBasketAsync(cart.UserName);
        }
    }
}
