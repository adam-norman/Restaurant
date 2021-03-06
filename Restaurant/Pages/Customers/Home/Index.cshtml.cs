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

namespace Restaurant.Pages.Customers.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork )
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<MenuItem>  MenuItemList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                int shoppingCartItemsCount = unitOfWork.ShoppingCart.GetAll(i => i.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(StaticDetails.ShoppingCart, shoppingCartItemsCount);
            }

            MenuItemList = unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType");
            CategoryList = unitOfWork.Category.GetAll(null, i => i.OrderBy( c=>c.DisplayOrder), null);
        }
    }
}
