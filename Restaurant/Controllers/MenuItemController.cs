using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models;
using System;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork dbContext;
        private readonly IWebHostEnvironment hostEnvironment;

        public MenuItemController(IUnitOfWork dbContext, IWebHostEnvironment hostEnvironment)
        {
            this.dbContext = dbContext;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { Data = dbContext.MenuItem.GetAll(null, null, "Category,FoodType") });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {

                var ItemTodDelete = dbContext.MenuItem.GetFirstOrDefault(i => i.Id == id);
                if (ItemTodDelete == null)
                {
                    return Json(new { success = false, message = "Menue item is deleted successfuly" });
                }
                string filePath = Path.Combine(hostEnvironment.WebRootPath, ItemTodDelete.Image.TrimStart('\\'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                dbContext.MenuItem.Remove(ItemTodDelete);
                dbContext.Save();
                return Json(new { success = true, message = "Menu item is deleted successfuly" });
            }
            catch (Exception Ex)
            {
                return Json(new { success = false, message = "Menue item is deleted successfuly" });
            }

        }
    }
}