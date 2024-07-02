using BusinessObjects.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        private static CustomerDAO instance = null;
        private static readonly object padlock = new object();

        private CustomerDAO() { }
       
        public static CustomerDAO Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                    return instance;
                }
            }

        }

        public List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();
           
            try
            {
                using var db = new FuminiHotelManagementContext();
                customers = db.Customers.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
            return customers;
        }


        public void SaveCustomer(Customer customer)
        {
           try
            {
                using var db = new FuminiHotelManagementContext();
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void UpdateCustomer(Customer customer)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void DeleteCustomer(Customer customer)
        {
            try
            {
                Customer cus;
                using var db = new FuminiHotelManagementContext();
                cus = db.Customers.SingleOrDefault(c => c.CustomerId == customer.CustomerId);
                db.Customers.Remove(cus);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
                
            }
        }


        public Customer GetCustomerById(int customerId)
        {
            using var db = new FuminiHotelManagementContext();
            return db.Customers.FirstOrDefault(c=>c.CustomerId == customerId);
        }

        public Customer Login(string email, string password)
        {
            List<Customer> customers = GetCustomers();
            Customer? customer = customers.FirstOrDefault(c => (c.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase) && c.Password.Equals(password)));
            return customer;
        }

        public List<Customer> FindByName(string fullName)
        {
            List<Customer> customers = GetCustomers();
            List<Customer> customer = customers.Where(c => c.CustomerFullName.ToLower().Contains(fullName.ToLower())).ToList();
            return customer;
        }
    }
}
