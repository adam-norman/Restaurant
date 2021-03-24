using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.DataAccess.Data.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OrderDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Update(OrderDetail orderDetail)
        {
            var orderDetailFromDb = dbContext.OrderDetails.FirstOrDefault(item => item.Id == orderDetail.Id);
            if (orderDetailFromDb != null)
            {
                
                orderDetailFromDb.Count = orderDetail.Count;
                orderDetailFromDb.Description = orderDetail.Description;
                orderDetailFromDb.MenuItemId = orderDetail.MenuItemId;
                orderDetailFromDb.Name = orderDetail.Name;
                orderDetailFromDb.OrderId = orderDetail.OrderId;
                orderDetailFromDb.Price = orderDetail.Price;
                dbContext.OrderDetails.Update(orderDetailFromDb);
                dbContext.SaveChanges();
            }
        }
    }
}