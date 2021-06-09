using HomeShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeShop.Core.ViewModels
{
    public class ProductManagerViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductCategory> ProcductCategories { get; set; }
    }
}
