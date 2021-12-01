using eCom.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCom.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;

        public BasketController(IBasketService BasketService)
        {
            this.basketService = BasketService;
        }

        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketsItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basket = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basket); 
        }
    }
}