using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Impl
{
    public class SupplierRepositoryImpl : RepositoryImplBase<Supplier>, ISupplierRepository
    {
        FuflowerBouquetManagementContext _context;
        public SupplierRepositoryImpl(FuflowerBouquetManagementContext Context) : base(Context)
        {
            _context = Context;
        }
    }
}
