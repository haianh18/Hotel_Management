using BusinessObjects.Models;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LaiVuHaiAnhWPF
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private CustomerService customerRepository;
        public CustomerWindow()
        {
            InitializeComponent();
            customerRepository = new CustomerRepository();
        }
        public Customer Account { get; set; }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow loginWindow = new LogInWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnManageCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerDetail customerDetail = new CustomerDetail
            {
                Title = "Manage Profile",
                InsertOrUpdate = true,
                CustomerInfo = Account
            };

            customerDetail.ShowDialog();
            Account = customerRepository.GetCustomerById(Account.CustomerId);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblWelcome.Content += Account.CustomerFullName;
        }

        private void btnViewHistory_Click(object sender, RoutedEventArgs e)
        {
            BookingWindow booking = new BookingWindow
            {
                Account = Account
            };
            booking.Show();
            this.Close();
        }
    }
}
