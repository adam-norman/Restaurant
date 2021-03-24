using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace Restaurant.DataAccess.Data.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    { 
        void Update(OrderHeader  orderHeader);
    }
}
