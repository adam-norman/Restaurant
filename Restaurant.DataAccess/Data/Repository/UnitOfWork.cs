using Restaurant.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.DataAccess.Data.Repository
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            Category = new CategoryRepository(dbContext);
            FoodType = new FoodTypeRepository(dbContext);
            MenuItem = new MenuItemRepository(dbContext);
        }
        public ICatrgoryRepository Category {
            get;
            private set;
        }

        public IFoodTypeRepository  FoodType { get; private set; }
        public IMenuItemRepository MenuItem { get; private set; }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
