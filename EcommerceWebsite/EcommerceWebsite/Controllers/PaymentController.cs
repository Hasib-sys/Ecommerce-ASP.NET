using System;
using System.Web.Mvc;
using System.Collections.Generic;
using Stripe;

namespace EcommerceWebsite.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult ProcessPayment()
        {
            // Retrieve the total amount from the session
            int totalAmount = (int)Session["SesTotal"];
            ViewBag.TotalAmount = totalAmount;

            StripeConfiguration.ApiKey = "sk_test_4eC39HqLyjWDarjtT1zdp7dc"; // Replace with your Stripe secret key

            
            var options = new PaymentIntentCreateOptions
            {
                Amount = totalAmount,
                Currency = "bdt",
                PaymentMethodTypes = new List<string> { "card" },
            };

            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options);

            ViewBag.ClientSecret = paymentIntent.ClientSecret;

            return View();
        }
        public ActionResult PaymentSuccess()
        {
            return View();
        }

        public ActionResult PrintOut()
        {
            return View();
        }

    }
}
