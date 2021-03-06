using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Core.Contracts;
using eCom.Core.Models;
using eCom.Core.ViewModels;

namespace eCom.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> Context;
        IRepository<ProductCategory> categoryRepository;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productcategoryRepository)
        {
            Context = productContext;
            categoryRepository = productcategoryRepository;
        }

        public ActionResult Index(string Category=null)
        {
            List<Product> products = Context.Collection().ToList();
            List<ProductCategory> categories = categoryRepository.Collection().ToList();

            if (Category == null)
            {
                products = Context.Collection().ToList();
            }
            else
            {
                products = Context.Collection().Where(p => p.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;

            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Product product = Context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}