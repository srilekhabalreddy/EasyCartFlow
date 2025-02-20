using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyCartFlow.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CouponCode { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}