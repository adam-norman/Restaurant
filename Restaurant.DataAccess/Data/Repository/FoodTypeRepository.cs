using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.DataAccess.Data.Repository
{
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public FoodTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<SelectListItem> GetFoodTypeForDropDownList()
        {
            return dbContext.FoodTypes.Select(item => new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
        }

        public void Update(FoodType foodType)
        {
            var foodTypeToUpdate = dbContext.FoodTypes.FirstOrDefault(item => item.Id == foodType.Id);
            if (foodTypeToUpdate != null)
            {
                foodTypeToUpdate.Name = foodType.Name;
                dbContext.FoodTypes.Update(foodTypeToUpdate);
                dbContext.SaveChanges();
            }
        }
    }
}