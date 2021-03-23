using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Utilities;
using Microsoft.AspNetCore.Authorization;
namespace Restaurant.Pages.Admin.Category
{
    [Authorize(Roles = StaticDetails.ManagerRole)]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Models.Category CategoryObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            CategoryObj = new Models.Category();
            if (id != null)
            {
                CategoryObj = unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
                if (CategoryObj == null)
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
            if (CategoryObj.Id == 0)
            {
                unitOfWork.Category.Add(CategoryObj);
            }
            else
            {
                unitOfWork.Category.Update(CategoryObj);
            }
            unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}