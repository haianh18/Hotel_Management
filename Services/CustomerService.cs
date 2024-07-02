using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface CustomerService
    {
        List<Customer> GetCustomers();
        void SaveCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        Customer GetCustomerById(int customerId);

        Customer LogIn(string email, string password);
        List<Customer> FindByName(string fullName);
    }
}
