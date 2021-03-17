using Microsoft.AspNetCore.Mvc;

using Restaurant.DataAccess.Data.Repository.IRepository;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTypeController : Controller
    {
        private readonly IUnitOfWork dbContext;

        public FoodTypeController(IUnitOfWork dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { Data = dbContext.FoodType.GetAll() });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ItemTodDelete = dbContext.FoodType.GetFirstOrDefault(i => i.Id == id);
            if (ItemTodDelete != null)
            {
                dbContext.FoodType.Remove(ItemTodDelete);
                dbContext.Save();
                return Json(new { success=true, message = "Food type deleted successfuly" });
            }
            else {
                return Json(new { success = false, message = "Food type deleted successfuly" });
            }
             
        }
    }
}