using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.Repository.IRepository;

namespace Restaurant.Pages.Admin.FoodType
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork dbContext;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            dbContext = unitOfWork;
        }
        [BindProperty]
        public Models.FoodType FoodTypeObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            FoodTypeObj = new Models.FoodType();
            if (id != null)
            {
            var FoodTypeObj = dbContext.FoodType.GetFirstOrDefault(item => item.Id == id);
            if (FoodTypeObj == null)
            {
                return NotFound();
            }
              }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (FoodTypeObj.Id == 0)
            {
                dbContext.FoodType.Add(FoodTypeObj);
            }
            else
            {
                dbContext.FoodType.Update(FoodTypeObj);
            }
            dbContext.Save();
            return RedirectToPage("./Index");
        }
    }
}
