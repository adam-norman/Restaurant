using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.DataAccess.Data.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OrderHeaderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Update(OrderHeader orderHeader)
        {
            var orderHeaderFromDb = dbContext.OrderHeaders.FirstOrDefault(item => item.Id == orderHeader.Id);
            if (orderHeaderFromDb != null)
            {
                orderHeaderFromDb.Comments = orderHeader.Comments;
                orderHeaderFromDb.OrderDate = orderHeader.OrderDate;
                orderHeaderFromDb.OrderTotal = orderHeader.OrderTotal;
                orderHeaderFromDb.PaymentStatus = orderHeader.PaymentStatus;
                orderHeaderFromDb.PhoneNumber = orderHeader.PhoneNumber;
                orderHeaderFromDb.PickUpDate = orderHeader.PickUpDate;
                orderHeaderFromDb.PickUpName = orderHeader.PickUpName;
                orderHeaderFromDb.PickUpTime = orderHeader.PickUpTime;
                orderHeaderFromDb.Status = orderHeader.Status;
                orderHeaderFromDb.TransactionId = orderHeader.TransactionId;
                orderHeaderFromDb.UserId = orderHeader.UserId;
                dbContext.OrderHeaders.Update(orderHeaderFromDb);
                dbContext.SaveChanges();
            }
        }
    }
}