using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Customer? SignIn(string username, string password);
    }
}
