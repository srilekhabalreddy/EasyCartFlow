using EasyCartFlow.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyCartFlow.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "EasyCheckout description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "EasyCheckout contact page. contact to  @Srilekha Balreddy.";

            return View();
        }

    
        public ActionResult OrderHistory()
        {
            var userId = User.Identity.GetUserId();
            var orders = _context.Orders
                                 .Where(o => o.UserId == userId)
                                 .OrderByDescending(o => o.OrderDate)
                                 .ToList();

            return View(orders);
        }

        public ActionResult OrderDetails(int orderId)
        {
            var userId = User.Identity.GetUserId();
            var order = _context.Orders
                                .Include("OrderDetails.Product")
                                .FirstOrDefault(o => o.OrderId == orderId && o.UserId == userId);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}