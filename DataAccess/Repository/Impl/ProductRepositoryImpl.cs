using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Impl
{
    public class ProductRepositoryImpl : RepositoryImplBase<FlowerBouquet>, IProductRepository
    {
        FuflowerBouquetManagementContext _context;
        public ProductRepositoryImpl(FuflowerBouquetManagementContext Context) : base(Context)
        {
            _context = Context;
        }
    }
}
