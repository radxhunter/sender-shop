using Cart.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Basket.API.Repositories;

namespace Cart.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartRepository _repository;

    public CartController(ICartRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet("{userName}", Name = "GetCart")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> GetCart(string userName)
    {
        var Cart = await _repository.Get(userName);
        return Ok(Cart ?? new ShoppingCart(userName));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateCart([FromBody] ShoppingCart Cart)
    {
        return Ok(await _repository.Update(Cart));
    }

    [HttpDelete("{userName}", Name = "DeleteCart")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteCart(string userName)
    {
        await _repository.Delete(userName);
        return Ok();
    }
}