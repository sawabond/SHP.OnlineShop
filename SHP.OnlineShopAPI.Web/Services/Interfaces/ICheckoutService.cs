using System.Collections.Generic;
using System.Threading.Tasks;
using SHP.OnlineShopAPI.Web.DTO.Checkout;

namespace SHP.OnlineShopAPI.Web.Services.Interfaces
{
    public interface ICheckoutService
    {
        Task Checkout(IEnumerable<ProductInBasketDto> productsInBasket, string stripeToken);
    }
}