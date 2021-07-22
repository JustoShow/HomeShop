using HomeShop.Core.Contracts;
using HomeShop.Core.Models;
using HomeShop.Core.ViewModels;
using HomeShop.Services;
using HomeShop.WebUI.Controllers;
using HomeShop.WebUI.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HomeShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            // Setup
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();

            var httpContext = new MockHttpContext();

            IBasketService basketService = new BasketService(products, baskets);
            var controller = new BasketController(basketService);
            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);

            // Act
            //basketService.AddToBasket(httpContext, "1");
            controller.AddToBasket("1");

            Basket basket = baskets.Collection().FirstOrDefault();

            // Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ProductId);
        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            // Setup
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();

            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 5.00m });
            products.Insert(new Product() { Id = "3", Price = 20.00m });

            // 5 items at $50 total
            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "3", Quantity = 1 });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(products, baskets);

            var controller = new BasketController(basketService);
            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new HttpCookie("eCommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(5, basketSummary.BasketCount);
            Assert.AreEqual(50.00m, basketSummary.BasketTotal);
        }
    }
}
