using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace Restaurant.DataAccess.Data.Repository.IRepository
{
    public interface ICatrgoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoriesForDropDownList();
        void Update(Category category);
    }
}
