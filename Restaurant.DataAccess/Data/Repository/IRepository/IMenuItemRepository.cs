using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.DataAccess.Data.Repository.IRepository
{

    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        IEnumerable<SelectListItem> GetFoodTypeForDropDownList();
        void Update(MenuItem   menuItem);
    }
}
