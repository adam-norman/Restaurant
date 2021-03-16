using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.DataAccess.Data.Repository.IRepository
{
   public interface IUnitOfWork:IDisposable
    {
        ICatrgoryRepository Category { get; }
        void Save();
    }
}
