using HomeShop.Core.Contracts;
using HomeShop.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HomeShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        private readonly IRepository<ProductCategory> _context;

        public ProductCategoryManagerController(IRepository<ProductCategory> context)
        {
            _context = context;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = _context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                _context.Insert(productCategory);
                _context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            ProductCategory productCategory = _context.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string id)
        {
            ProductCategory productCategoryToEdit = _context.Find(id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }

                productCategoryToEdit.Category = productCategory.Category;

                _context.Update(productCategoryToEdit);
                _context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string id)
        {
            ProductCategory productCategoryToDelete = _context.Find(id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory productCategoryToDelete = _context.Find(id);
            if (productCategoryToDelete == null)
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