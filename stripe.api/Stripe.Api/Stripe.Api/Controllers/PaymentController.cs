using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace Stripe.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        public PaymentController()
        {
            
        }

        [HttpGet]
        [Route("GetSession")]
        public async Task<IActionResult> GetSessionAsync()
        {
            StripeConfiguration.ApiKey = "sk_test_p9SPI9sPLAegWEXL6fZ5MIDm009gl2HLo5";
        
            var options = new SessionCreateOptions {
                PaymentMethodTypes = new List<string> {
                    "card",
                },
                Mode = "setup",
                SuccessUrl = "http://localhost:3000/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "http://localhost:3000/cancel",
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return Ok(session);
        }
        
        [HttpPost]
        [Route("Buy")]
        public async Task<IActionResult> BuyAsync()
        {
            StripeConfiguration.ApiKey = "sk_test_p9SPI9sPLAegWEXL6fZ5MIDm009gl2HLo5";
        
            var options = new SessionCreateOptions {
                PaymentMethodTypes = new List<string> {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions> {
                    new SessionLineItemOptions {
                        Name = "T-shirt",
                        Description = "Comfortable cotton t-shirt",
                        Amount = 500,
                        Currency = "usd",
                        Quantity = 1,
                    },
                },
                PaymentIntentData = new SessionPaymentIntentDataOptions {
                    CaptureMethod = "manual",
                },
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Ok(session);
        }
    }
}