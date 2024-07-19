using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Impl
{
    public class CustomerRepositoryImpl : RepositoryImplBase<Customer>, ICustomerRepository
    {
        FuflowerBouquetManagementContext _context;
        public CustomerRepositoryImpl(FuflowerBouquetManagementContext Context) : base(Context)
        {
            _context = Context;
        }

        public Customer? SignIn(string username, string password)
        {
            return _context.Customers.FirstOrDefault(c => c.Email.Equals(username) && c.Password.Equals(password));
        }
    }
}
