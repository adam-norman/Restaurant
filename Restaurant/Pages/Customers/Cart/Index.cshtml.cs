using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models;
using Restaurant.Utilities;
using Restaurant.ViewModels;

namespace Restaurant.Pages.Customers.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [BindProperty]
        public OrderDetailsCartVM OrderDetailsCartVM { get; set; }
        public void OnGet()
        {
            OrderDetailsCartVM = new OrderDetailsCartVM
            {
                OrderHeader = new OrderHeader()
            };
            OrderDetailsCartVM.OrderHeader.OrderTotal = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<ShoppingCart> shoppingCarts = unitOfWork.ShoppingCart.GetAll(i => i.ApplicationUserId == UserId);
            if (shoppingCarts != null)
            {
                OrderDetailsCartVM.ShoppingCarts = shoppingCarts.ToList();
                foreach (var cart in OrderDetailsCartVM.ShoppingCarts)
                {
                    cart.MenuItem = unitOfWork.MenuItem.GetFirstOrDefault(i => i.Id == cart.MenuItemId);
                    OrderDetailsCartVM.OrderHeader.OrderTotal += cart.MenuItem.Price * cart.Count;
                }
            }
        }

        public IActionResult OnPostPlus(int cartId)
        {
            var cart = unitOfWork.ShoppingCart.GetFirstOrDefault(s => s.Id == cartId);
            unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            unitOfWork.Save();
            return RedirectToPage("/Customers/Cart/Index");
        }
        public IActionResult OnPostMinus(int cartId)
        {
            var cart = unitOfWork.ShoppingCart.GetFirstOrDefault(s => s.Id == cartId);
            if (cart.Count == 1)
            {
                unitOfWork.ShoppingCart.Remove(cart);
                unitOfWork.Save();
                var scn = unitOfWork.ShoppingCart.GetAll(i => i.ApplicationUserId == cart.ApplicationUserId).Count();
                HttpContext.Session.SetInt32(StaticDetails.ShoppingCart, scn);
            }
            else
            {
                unitOfWork.ShoppingCart.DecrementCount(cart, 1);
                unitOfWork.Save();
            }
            return RedirectToPage("/Customers/Cart/Index");
        }
        public IActionResult OnPostRemove(int cartId)
        {
            var cart = unitOfWork.ShoppingCart.GetFirstOrDefault(s => s.Id == cartId);

            unitOfWork.ShoppingCart.Remove(cart);
            unitOfWork.Save();
            var scn = unitOfWork.ShoppingCart.GetAll(i => i.ApplicationUserId == cart.ApplicationUserId).Count();
            HttpContext.Session.SetInt32(StaticDetails.ShoppingCart, scn);
            return RedirectToPage("/Customers/Cart/Index");
        }
    }
}
