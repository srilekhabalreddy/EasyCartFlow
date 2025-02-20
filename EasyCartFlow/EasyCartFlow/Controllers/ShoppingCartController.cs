using EasyCartFlow.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using System.Linq;
using Microsoft.AspNet.Identity;

public class ShoppingCartController : Controller
{
    private readonly ApplicationDbContext _context;

    public ShoppingCartController()
    {
        _context = new ApplicationDbContext();
    }

    // Add a product to the cart
    public ActionResult AddToCart(int productId)
    {
        var product = _context.Products.Find(productId);
        if (product == null)
        {
            return HttpNotFound();
        }

        var cart = GetCart();
        var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
        if (cartItem == null)
        {
            cart.Add(new CartItem { ProductId = productId, Product = product, Quantity = 1, Price = product.Price });
        }
        else
        {
            cartItem.Quantity++;
        }

        SaveCart(cart);
        return RedirectToAction("Cart");
    }

    // Remove a product from the cart
    public ActionResult RemoveFromCart(int productId)
    {
        var cart = GetCart();
        var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
        if (cartItem != null)
        {
            cart.Remove(cartItem);
        }

        SaveCart(cart);
        return RedirectToAction("Cart");
    }

    // View the cart
    public ActionResult Cart()
    {
        var cart = GetCart();
        return View("Cart",cart);
    }

    // Proceed to checkout
    public ActionResult Checkout()
    {
        var cart = GetCart();
        if (!cart.Any())
        {
            return RedirectToAction("Index");
        }

        return View("Checkout",cart);
    }

    // Apply a coupon code
    [HttpPost]
    public ActionResult ApplyCoupon(string couponCode)
    {
        var cart = GetCart();
        // Validate the coupon code (this is a simple example; implement your own validation logic)
        if (couponCode == "DISCOUNT10")
        {
            // Apply a 10% discount
            foreach (var item in cart)
            {
                item.Price *= 0.9m;
            }
        }
        else
        {
            ModelState.AddModelError("CouponCodeError", "Invalid coupon code.");
        }

        SaveCart(cart);
        return View("Checkout", cart);
    }

    // Place an order
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult PlaceOrder(string couponCode)
    {
        var cart = GetCart();
        if (!cart.Any())
        {
            return RedirectToAction("Index");
        }

        var order = new Order
        {
            UserId = User.Identity.GetUserId(),
            OrderDate = DateTime.Now,
            CouponCode = couponCode,
            TotalAmount = cart.Sum(c => c.Price * c.Quantity)
        };

        _context.Orders.Add(order);
        _context.SaveChanges();

        foreach (var cartItem in cart)
        {
            var orderDetail = new OrderDetail
            {
                OrderId = order.OrderId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                Product = _context.Products.FirstOrDefault(c => c.ProductId == cartItem.ProductId),
                Price = cartItem.Price
            };
            _context.OrderDetails.Add(orderDetail);
        }

        _context.SaveChanges();
        ClearCart();
        return View("OrderConfirmation", order);
    }

    // Retrieve the shopping cart from session
    private List<CartItem> GetCart()
    {
        var cart = Session["Cart"] as List<CartItem>;
        if (cart == null)
        {
            cart = new List<CartItem>();
            Session["Cart"] = cart;
        }
        return cart;
    }

    // Save the shopping cart to session
    private void SaveCart(List<CartItem> cart)
    {
        Session["Cart"] = cart;
    }

    // Clear the shopping cart
    private void ClearCart()
    {
        Session["Cart"] = new List<CartItem>();
    }
}
