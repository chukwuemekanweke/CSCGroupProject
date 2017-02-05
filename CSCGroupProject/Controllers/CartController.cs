using CSCGroupProject.Infrastructure;
using CSCGroupProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
namespace CSCGroupProject.Controllers
{
    [RoutePrefix("Cart")]
    public class CartController : Controller
    {
        ShoppingCartDBContext db = new ShoppingCartDBContext();
        EmailSettings emailSettings;
        EmailOrderProcessor orderProcessor;

        public CartController()
        {
            emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            emailSettings.Username = "emekanweke604@gmail.com";
            emailSettings.Password = "Live\"4\"Money";
            emailSettings.ServerName = "smtp.gmail.com";
            emailSettings.ServerPort = 587;
            emailSettings.MailFromAddress = "emekanweke604@gmail.com";
            emailSettings.MailToAddress = "godaddy@gmail.com";
            orderProcessor = new EmailOrderProcessor(emailSettings);
        }
        public ViewResult Index(string returnUrl)
        {
            @ViewBag.ReturnUrl = returnUrl;
            return View(GetCart());
        }

        [HttpPost]
        [Route("AddToCart")]
        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Product product = db.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                GetCart().AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        [Route("RemoveFromCart")]
        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = db.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                GetCart().RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        [Authorize]
        public async Task< ViewResult> Checkout()
        {
            if (User.Identity.IsAuthenticated)
            {
                var LoggedInUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                return View(new ShippingDetails() { 
                    Name = LoggedInUser.UserName,
                    
                    City = LoggedInUser.City,
                    Line1 = LoggedInUser.PhoneNumber,
                });
            }
            return null;
        }

        [HttpPost]
        public ViewResult Checkout(ShippingDetails shippingDetails)
        {
            if (GetCart().Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(GetCart(), shippingDetails);
                GetCart().Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }


        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
    }
}