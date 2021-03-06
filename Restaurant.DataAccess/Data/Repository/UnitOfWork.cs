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
            ApplicationUser = new ApplicationUserRepository(dbContext);
            ShoppingCart  = new ShoppingCartRepository(dbContext);
            OrderHeader  = new OrderHeaderRepository(dbContext);
            OrderDetail  = new OrderDetailRepository(dbContext);
        }
        public ICatrgoryRepository Category {
            get;
            private set;
        }
        
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
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
