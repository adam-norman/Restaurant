using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class OrderDetailsCartVM
    {
        public List<ShoppingCart>   ShoppingCarts { get; set; }
        public OrderHeader  OrderHeader { get; set; }
    }
}
