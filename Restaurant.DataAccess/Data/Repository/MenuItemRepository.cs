using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.DataAccess.Data.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext dbContext;

        public MenuItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<SelectListItem> GetFoodTypeForDropDownList()
        {
            return dbContext.FoodTypes.Select(item => new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
        }

        public void Update(MenuItem menuItem)
        {
            var menuItemToUpdate = dbContext.MenuItems.FirstOrDefault(item => item.Id == menuItem.Id);
            if (menuItemToUpdate != null)
            {
                menuItemToUpdate.Name = menuItem.Name;
                menuItemToUpdate.CategoryId = menuItem.CategoryId;
                menuItemToUpdate.Description = menuItem.Description;
                menuItemToUpdate.FoodType = menuItem.FoodType;
                if (menuItem.Image != null)
                {
                    menuItemToUpdate.Image = menuItem.Image;
                }
                menuItemToUpdate.Price = menuItem.Price;
                dbContext.MenuItems.Update(menuItemToUpdate);
                dbContext.SaveChanges();
            }
        }
    }
}