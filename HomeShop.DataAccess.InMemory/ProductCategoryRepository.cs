using HomeShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace HomeShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> products = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            products = cache["products"] as List<ProductCategory>;
            products = products == null ? new List<ProductCategory>() : products;
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(ProductCategory productCategory)
        {
            products.Add(productCategory);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = products.Find(p => p.Id == productCategory.Id);
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }

        public ProductCategory Find(string id)
        {
            ProductCategory productCategory = products.Find(p => p.Id == id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string id)
        {
            ProductCategory productCategoryToDelete = products.Find(p => p.Id == id);
            if (productCategoryToDelete != null)
            {
                products.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }
    }
}
