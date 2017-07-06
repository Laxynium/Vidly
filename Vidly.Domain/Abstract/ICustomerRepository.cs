using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidly.Domain.Entities;

namespace Vidly.Domain.Abstract
{
    public interface  ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();

        Customer GetCustomer(int id);

        void AddCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void RemoveCustomer(int id);

    }
}
