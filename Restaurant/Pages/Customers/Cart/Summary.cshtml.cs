using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.Repository;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models;
using Restaurant.ViewModels;

namespace Restaurant.Pages.Customers.Cart
{
    public class SummaryModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public SummaryModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [BindProperty]
        public OrderDetailsCartVM OrderDetailVM { get; set; }
        public IActionResult OnGet()
        {
            OrderDetailVM = new  OrderDetailsCartVM
            {
                OrderHeader = new OrderHeader()
            };
            OrderDetailVM.OrderHeader.OrderTotal = 0;
            var claims = (ClaimsIdentity)User.Identity;
            var userId = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<ShoppingCart> shoppingCarts = unitOfWork.ShoppingCart.GetAll(i => i.ApplicationUserId == userId);
            if (shoppingCarts != null)
            {
                OrderDetailVM.ShoppingCarts = shoppingCarts.ToList();
                foreach (var cart in OrderDetailVM.ShoppingCarts)
                {
                    cart.MenuItem = unitOfWork.MenuItem.GetFirstOrDefault(i => i.Id == cart.MenuItemId);
                    OrderDetailVM.OrderHeader.OrderTotal += cart.Count * cart.MenuItem.Price;
                }
            }
            ApplicationUser  applicationUser  = unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId);
            OrderDetailVM.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
            OrderDetailVM.OrderHeader.PickUpName = applicationUser.FullName;
            OrderDetailVM.OrderHeader.PickUpTime = DateTime.Now;
            return Page();
        }
    }
}
