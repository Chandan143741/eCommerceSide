using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Core.Contracts;
using eCom.Core.Models;
using eCom.Core.ViewModels;
using eCom.DataAccess.InMemory;

namespace eCom.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> Context;
        IRepository<ProductCategory> categoryRepository;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productcategoryRepository)
        {
            Context = productContext;
            categoryRepository = productcategoryRepository;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = Context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = categoryRepository.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if(file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//Images//") + product.Image);
                }
                Context.Insert(product);
                Context.Commit();

                return RedirectToAction("Index");
            }
           
        }

        public ActionResult Edit(string Id)
        {
            Product product = Context.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = categoryRepository.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product productToEdit = Context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//Images//") + productToEdit.Image);
                }

                productToEdit.Name = product.Name;
                productToEdit.Description = product.Description;
                productToEdit.Price = product.Price;
                productToEdit.Category = product.Category;

                Context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
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

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = Context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                Context.Delete(Id);
                Context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}