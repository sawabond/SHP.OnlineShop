using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHP.OnlineShopAPI.Web.Constants;
using SHP.OnlineShopAPI.Web.DTO.Checkout;
using SHP.OnlineShopAPI.Web.Services.Interfaces;

namespace SHP.OnlineShopAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] IEnumerable<ProductInBasketDto> products)
        {
            await _checkoutService.Checkout(products, "");

            return Ok();
        }
    }
}