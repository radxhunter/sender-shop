using Cart.Api.Entities;

namespace Basket.API.Repositories;

public interface ICartRepository
{
    Task<ShoppingCart?> Get(string userName);
    Task<ShoppingCart?> Update(ShoppingCart cart);
    Task Delete(string userName);
}
