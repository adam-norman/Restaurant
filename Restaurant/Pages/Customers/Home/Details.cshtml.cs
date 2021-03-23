using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models;
using Restaurant.Utilities;

namespace Restaurant.Pages.Customers.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;


        public DetailsModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }
        public void OnGet(int id)
        {
            ShoppingCart = new ShoppingCart()
            {
                MenuItem = unitOfWork.MenuItem.GetFirstOrDefault(filter: i => i.Id == id, includeProperties: "Category,FoodType"),
                MenuItemId = id
            };
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                ShoppingCart.ApplicationUserId = claim.Value;
                var menuItemFromDb = unitOfWork.ShoppingCart.GetFirstOrDefault(i => i.ApplicationUserId == claim.Value &&
                  i.MenuItemId == ShoppingCart.MenuItemId);
                if (menuItemFromDb == null)
                {
                    unitOfWork.ShoppingCart.Add(ShoppingCart);
                }
                else
                {
                    menuItemFromDb.Count= unitOfWork.ShoppingCart.IncrementCount(menuItemFromDb, ShoppingCart.Count);
                }
                unitOfWork.Save();
                int itemsCount = unitOfWork.ShoppingCart.GetAll(i => i.ApplicationUserId == claim.Value).Count();
                HttpContext.Session.SetInt32(StaticDetails.ShoppingCart, itemsCount);
              return  RedirectToPage("Index");
            }
            else
            {
                ShoppingCart.MenuItem = unitOfWork.MenuItem.GetFirstOrDefault(filter: i => i.Id == ShoppingCart.MenuItemId, includeProperties: "Category,FoodType");
                return Page();
            }
        }
    }
}
