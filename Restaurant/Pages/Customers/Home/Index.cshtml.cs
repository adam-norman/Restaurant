using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models;

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
            MenuItemList = unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType");
            CategoryList = unitOfWork.Category.GetAll(null, i => i.OrderBy( c=>c.DisplayOrder), null);
        }
    }
}
