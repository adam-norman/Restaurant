using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { Data = unitOfWork.Category.GetAll() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoryToDelete = unitOfWork.Category.GetFirstOrDefault(i => i.Id == id);
            if (categoryToDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            unitOfWork.Category.Remove(categoryToDelete);
            unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }

    }
}
