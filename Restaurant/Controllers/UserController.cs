using Microsoft.AspNetCore.Mvc;
using Restaurant.DataAccess.Data.Repository.IRepository;
using System;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { Data = unitOfWork.ApplicationUser.GetAll() });
        }

        [HttpPost]
        public IActionResult UserAccountLockToggler([FromBody] string id)
        {
            var userFromDb = unitOfWork.ApplicationUser.GetFirstOrDefault(i => i.Id == id);
            if (userFromDb == null)
            {
                return Json(new { success = false, message = "Operation Failed" });
            }
            if (userFromDb.LockoutEnd > DateTime.Now && userFromDb.LockoutEnd != null)
            {
                userFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                userFromDb.LockoutEnd = DateTime.Now.AddYears(200);
            }
            
            unitOfWork.Save();
            return Json(new { success = true, message = "Operation successful" });
        }
    }
}