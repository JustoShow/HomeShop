using HomeShop.Core.Contracts;
using HomeShop.Core.Models;
using HomeShop.Core.ViewModels;
using HomeShop.DataAccess.InMemory;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        private readonly IRepository<Product> _context;
        private readonly IRepository<ProductCategory> _productCategories;

        public ProductManagerController(IRepository<Product> context, IRepository<ProductCategory> productCategories)
        {
            _context = context;
            _productCategories = productCategories;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = _context.Collection().OrderBy(c => c.Category).ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();
            viewModel.ProcductCategories = _productCategories.Collection().OrderBy(c => c.Category);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if(file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }

                _context.Insert(product);
                _context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            Product product = _context.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProcductCategories = _productCategories.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id, HttpPostedFileBase file)
        {
            Product productToEdit = _context.Find(id);
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
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                _context.Update(productToEdit);
                _context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string id)
        {
            Product productToDelete = _context.Find(id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product productToDelete = _context.Find(id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                _context.Delete(id);
                _context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}