using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Impl
{
    public class CategoryRepositoryImpl : RepositoryImplBase<Category>, ICategoryRepository
    {
        FuflowerBouquetManagementContext _context;
        public CategoryRepositoryImpl(FuflowerBouquetManagementContext Context) : base(Context)
        {
            _context = Context;
        }
    }
}
