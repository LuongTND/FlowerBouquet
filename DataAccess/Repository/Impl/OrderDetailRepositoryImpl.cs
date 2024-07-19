using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Impl
{
    public class OrderDetailRepositoryImpl : RepositoryImplBase<OrderDetail>, IOrderDetailRepository
    {
        FuflowerBouquetManagementContext _context;
        public OrderDetailRepositoryImpl(FuflowerBouquetManagementContext Context) : base(Context)
        {
            _context = Context;
        }
    }
}
