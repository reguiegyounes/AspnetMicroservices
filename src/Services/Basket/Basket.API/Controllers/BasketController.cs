using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this._basketRepository = basketRepository?? throw new ArgumentNullException(nameof(basketRepository));
        }

        [HttpGet("{userName}" ,Name =nameof(GetBasket))]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName) 
        {
            var basket =await _basketRepository.GetBasketAsync(userName);
            return Ok(basket?? new ShoppingCart(userName));
        }

        [HttpPut(Name = nameof(UpdateBasket))]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody]ShoppingCart cart)
        {
            var basket = await _basketRepository.UpdateBasketAsync(cart);
            return Ok(basket);
        }

        [HttpDelete("{userName}", Name = nameof(DeleteBasket))]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasketAsync(userName);
            return Ok();
        }

    }
}
