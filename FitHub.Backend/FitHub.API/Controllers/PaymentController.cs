using FitHub.Application.Common.Interfaces;
using FitHub.Application.Abstractions;
using FitHub.Domain.Entities.Store; // Dodano za Payment
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using Microsoft.AspNetCore.Authorization;
using System;

namespace FitHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IAppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAppCurrentUser _currentUser;

        public PaymentController(IAppDbContext context, IConfiguration configuration, IAppCurrentUser currentUser)
        {
            _context = context;
            _configuration = configuration;
            _currentUser = currentUser;
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] int orderId,CancellationToken ct)
        {
            if (_currentUser.UserId is null)
                return Unauthorized("Niste prijavljeni.");

            var order = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.FitnessPlan)
                .FirstOrDefaultAsync(o => o.OrderID == orderId);

            if (order == null || order.UserID != _currentUser.UserId)
            {
                return NotFound("Order not found.");
            }

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var item in order.Items)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "bam",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.FitnessPlan?.Title ?? "Fitness Plan",
                        },
                    },
                    Quantity = item.Quantity,
                });
            }

            var domain = _configuration["FrontendUrl"] ?? "http://localhost:4200";
            
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = $"{domain}/order-success/{order.OrderID}",
                CancelUrl = domain + "/cart",
                Metadata = new Dictionary<string, string>
                {
                    { "order_id", order.OrderID.ToString() }
                }
            };
            
            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            var paymentRecord = new Payment
            {
                OrderID = order.OrderID,
                StripeSessionId = session.Id,
                Amount = order.TotalAmount, 
                Status = "Initiated",
                CreatedAtUtc = DateTime.UtcNow
            };

            _context.Payments.Add(paymentRecord);
            await _context.SaveChangesAsync(ct); // Save to database before redirecting

            return Ok(new { sessionId = session.Id, url = session.Url });
        }
    

        [HttpPost("webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> StripeWebhook(CancellationToken cancellationToken) 
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync(cancellationToken);
            string endpointSecret = _configuration["Stripe:WebhookSecret"];

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], endpointSecret);

                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Session;

                    if (session.Metadata.TryGetValue("order_id", out string orderIdString) && int.TryParse(orderIdString, out int orderId))
                    {
                        var order = await _context.Orders.FindAsync(new object[] { orderId }, cancellationToken);
                        
                        // 2. UPDATE PAYMENT RECORD ("Succeeded")
                        var payment = await _context.Payments
                            .FirstOrDefaultAsync(p => p.StripeSessionId == session.Id, cancellationToken);

                        if (order != null)
                        {
                            order.Status = "Paid";
                            order.PaidAtUtc = DateTime.UtcNow;
                        }

                        if (payment != null)
                        {
                            payment.Status = "Succeeded";
                        }

                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
                else if (stripeEvent.Type == "checkout.session.expired" || stripeEvent.Type == "payment_intent.payment_failed")
                {
                    // 3. UPDATE PAYMENT RECORD IN CASE OF FAILURE ("Failed")
                    // 3. UPDATE PAYMENT RECORD IN CASE OF FAILURE ("Failed")
var session = stripeEvent.Data.Object as Session;

if (session != null)
{
    var payment = await _context.Payments
            .FirstOrDefaultAsync(p => p.StripeSessionId == session.Id, cancellationToken);
    
    if (payment != null)
    {
        payment.Status = "Failed";
        await _context.SaveChangesAsync(cancellationToken);
    }
}
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}
