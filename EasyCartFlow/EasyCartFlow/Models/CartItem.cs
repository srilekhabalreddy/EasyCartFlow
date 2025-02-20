using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyCartFlow.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual Product Product { get; set; }
    }
}