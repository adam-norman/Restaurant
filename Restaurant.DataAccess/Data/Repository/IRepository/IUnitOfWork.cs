using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.DataAccess.Data.Repository.IRepository
{
   public interface IUnitOfWork:IDisposable
    {
        ICatrgoryRepository Category { get; }
        IFoodTypeRepository  FoodType { get; }
        IMenuItemRepository  MenuItem { get; }
        IApplicationUserRepository  ApplicationUser { get; }
        IShoppingCartRepository ShoppingCart { get; }
        void Save();
    }
}
