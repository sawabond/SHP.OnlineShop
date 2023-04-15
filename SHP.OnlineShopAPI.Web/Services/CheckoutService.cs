using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using SHP.OnlineShopAPI.Web.DTO.Checkout;
using SHP.OnlineShopAPI.Web.Services.Interfaces;
using Stripe.Checkout;

namespace SHP.OnlineShopAPI.Web.Services
{
    public sealed class CheckoutService : ICheckoutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CheckoutService> _logger;

        public CheckoutService(IUnitOfWork unitOfWork, ILogger<CheckoutService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        public async Task Checkout(IEnumerable<ProductInBasketDto> productsInBasket, string stripeToken)
        {
            var products = await _unitOfWork.ProductRepository
                .GetProductRangeById(productsInBasket.Select(x => x.ProductId));

            var intentOptions = products
                .OrderBy(x => x.Id)
                .Zip(productsInBasket
                    .OrderBy(x => x.ProductId))
                .Select(x => new SessionLineItemOptions
                {
                    
                });

            var options = new SessionCreateOptions
            {
                LineItems = new()
                {
                    new()
                    {
                        PriceData = new()
                        {
                            Currency = "usd",
                            UnitAmount = 10000,
                            ProductData = new()
                            {
                                Name = "PRODUCT NAME",
                                Description = "PRODUCT DESCR"
                            }
                        },
                        Quantity = 5
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://localhost:44318/swagger/index.html/success",
                CancelUrl = "https://localhost:44318/swagger/index.html/cancel"
            };

            var service = new SessionService();
            var session = service.Create(options);
        }
    }
}