using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Impl
{
    public class OrderRepositoryImpl : RepositoryImplBase<Order>, IOrderRepository
    {
        FuflowerBouquetManagementContext _context;
        public OrderRepositoryImpl(FuflowerBouquetManagementContext Context) : base(Context)
        {
            _context = Context;
        }
    }
}
