using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.ViewModels;

namespace Restaurant.Pages.Admin.MenuItem
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment hostEnvironment;

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            unitOfWork = unitOfWork;
            this.hostEnvironment = hostEnvironment;
        }
        [BindProperty]
        public MenuItemVM MenuItemPbj { get; set; }
        public IActionResult OnGet(int? id)
        {
            MenuItemPbj = new MenuItemVM
            {
                CategoryList = unitOfWork.Category.GetCategoriesForDropDownList(),
                FoodTypeList = unitOfWork.FoodType.GetFoodTypeForDropDownList()
            };
            if (id != null)
            {
                MenuItemPbj.MenuItem = unitOfWork.MenuItem.GetFirstOrDefault(i => i.Id == id);
                if (MenuItemPbj == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            string serverRootPath = hostEnvironment.WebRootPath;
            var uploadedFiles = HttpContext.Request.Form.Files;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (MenuItemPbj.MenuItem.Id == 0)
            {
                string newFileName = Guid.NewGuid().ToString();
                string uploadsPath = Path.Combine(serverRootPath, @"images\menuItems");
                string extention = Path.GetExtension(uploadedFiles[0].Name);
                using (var fileStream = new FileStream(Path.Combine(uploadsPath, newFileName + extention), FileMode.Create))
                {
                    uploadedFiles[0].CopyTo(fileStream);
                }
                MenuItemPbj.MenuItem.Image = uploadsPath + @"\" + newFileName + extention;
                unitOfWork.MenuItem.Add(MenuItemPbj.MenuItem);
            }
            else
            {
                var menuItemFromDb = unitOfWork.MenuItem.GetFirstOrDefault(i => i.Id == MenuItemPbj.MenuItem.Id);
                menuItemFromDb.Name = MenuItemPbj.MenuItem.Name;
                menuItemFromDb.Description = MenuItemPbj.MenuItem.Description;
                menuItemFromDb.FoodTypeId = MenuItemPbj.MenuItem.FoodTypeId;
                menuItemFromDb.CategoryId = MenuItemPbj.MenuItem.CategoryId;
                menuItemFromDb.Price = MenuItemPbj.MenuItem.Price;
                if (MenuItemPbj.MenuItem.Image != null)
                {
                    //delete existed file first
                    if (uploadedFiles.Count > 0)
                    {
                        string existedFilePath = Path.Combine(hostEnvironment.WebRootPath, menuItemFromDb.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(existedFilePath))
                        {
                            System.IO.File.Delete(existedFilePath);
                        }
                    }
                    string newFileName = Guid.NewGuid().ToString();
                    string uploadsPath = Path.Combine(serverRootPath, @"images\menuItems");
                    string extention = Path.GetExtension(uploadedFiles[0].Name);
                    using (var fileStream = new FileStream(Path.Combine(uploadsPath, newFileName + extention), FileMode.Create))
                    {
                        uploadedFiles[0].CopyTo(fileStream);
                    }
                    MenuItemPbj.MenuItem.Image = uploadsPath + @"\" + newFileName + extention;
                }
                else
                {
                    MenuItemPbj.MenuItem.Image = menuItemFromDb.Image;
                }
                unitOfWork.MenuItem.Update(MenuItemPbj.MenuItem);
            }
            unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
