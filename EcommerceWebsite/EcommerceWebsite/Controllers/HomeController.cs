using Newtonsoft.Json;
using EcommerceWebsite.Models;
using EcommerceWebsite.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceWebsite.Controllers
{
    public class HomeController : Controller
    {
        EcommerceEntities ctx = new EcommerceEntities();

        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 4, page));
        }

        [Authorize(Roles = "Customer")]
        public ActionResult Checkout()
        {
            return View();
        }



        [Authorize(Roles = "Customer")]
        public ActionResult CheckoutDetails()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public ActionResult DecreaseQty(int ProductID)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.Product.Find(ProductID);
                foreach (var item in cart)
                {
                    if (item.Product.PruductID == ProductID)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }
        [Authorize(Roles = "Customer")]
        public ActionResult AddToCart(int ProductID, string url)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.Product.Find(ProductID);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var product = ctx.Product.Find(ProductID);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.PruductID == ProductID)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Product.PruductID == ProductID).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect(url);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult RemoveFromCart(int ProductID)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.PruductID == ProductID)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }
    }
}