using Cart.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories;

public class CartRepository : ICartRepository
{
    private readonly IDistributedCache _redisCache;

    public CartRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task Delete(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }

    public async Task<ShoppingCart?> Get(string userName)
    {
        var cart = await _redisCache.GetStringAsync(userName);

        if (string.IsNullOrEmpty(cart))
            return null;

        return JsonConvert.DeserializeObject<ShoppingCart>(cart);
    }

    public async Task<ShoppingCart?> Update(ShoppingCart cart)
    {
        await _redisCache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));

        return await Get(cart.UserName);
    }
}
