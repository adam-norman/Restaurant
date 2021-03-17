using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.DataAccess.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICatrgoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<SelectListItem> GetCategoriesForDropDownList()
        {
            return dbContext.Categories.Select(item => new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
        }

        public void Update(Category category)
        {
            var categoryFromDb = dbContext.Categories.FirstOrDefault(item => item.Id == category.Id);
            if (categoryFromDb != null)
            {
                categoryFromDb.DisplayOrder = category.DisplayOrder;
                categoryFromDb.Name = category.Name;
                dbContext.Categories.Update(categoryFromDb);
                dbContext.SaveChanges();
            }
        }
    }
}