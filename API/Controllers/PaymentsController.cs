using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Order = Core.Entities.OrderAggregate.Order;

namespace API.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _log;
        private readonly string _whSecret;
        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> log,
            IConfiguration config)
        {
            _paymentService = paymentService;
            _log = log;
            _whSecret = config.GetSection("StripeSettings:WhSecret").Value;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerShoppingCard>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null) return BadRequest(new ApiGlobalResponse(400, "Problem with your basket"));

            return basket;
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _whSecret);

            PaymentIntent intent;
            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _log.LogInformation("Succeeded: ", intent.Id);
                    
                    order = await _paymentService.UpdateOrderPaymentCompleted(intent.Id);
                    _log.LogInformation("Payment received: ", order.Id);
                    break;

                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _log.LogInformation("Something went wrong: ", intent.Id);
                    
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _log.LogInformation("Payment problem: ", order.Id);

                    break;

                default:
                    break;
            }

            return new EmptyResult();
        }
    }
}
