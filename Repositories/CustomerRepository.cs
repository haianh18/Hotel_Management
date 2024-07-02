using BusinessObjects.Models;
using DataAccessLayer;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : CustomerService
    {
        public void DeleteCustomer(Customer customer) => CustomerDAO.Instance.DeleteCustomer(customer);

        public List<Customer> FindByName(string fullName) => CustomerDAO.Instance.FindByName(fullName);

        public Customer GetCustomerById(int customerId) => CustomerDAO.Instance.GetCustomerById(customerId);

        public List<Customer> GetCustomers() => CustomerDAO.Instance.GetCustomers();

        public Customer LogIn(string email, string password) => CustomerDAO.Instance.Login(email, password);

        public void SaveCustomer(Customer customer) => CustomerDAO.Instance.SaveCustomer(customer);

        public void UpdateCustomer(Customer customer) => CustomerDAO.Instance.UpdateCustomer(customer);

       
    }
}
