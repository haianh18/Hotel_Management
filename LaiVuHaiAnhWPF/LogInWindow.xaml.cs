using BusinessObjects.Models;
using Microsoft.Extensions.Configuration;
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
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        private CustomerService customerService;
        public LogInWindow()
        {
            InitializeComponent();
            customerService = new CustomerRepository();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
#if DEBUG
            txtEmail.Text = "admin@FUMiniHotelSystem.com";
            txtPass.Password = "@@abc123@@";

            //txtEmail.Text = "WilliamShakespeare@FUMiniHotel.org";
            //txtPass.Password = "123@";
#endif

            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPass.Password))
            {
                MessageBox.Show("Please input all fields", "Login");
                return;
            }

            Customer customer = customerService.LogIn(txtEmail.Text, txtPass.Password);
            if (isValidLogin(txtEmail.Text, txtPass.Password))
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                this.Close();
            }
            else if (customer != null)
            {
                CustomerWindow customerWindow = new CustomerWindow
                {
                    Account = customer
                };
                customerWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed! Email or password incorrect", "Login");
            }
        }

        private bool isValidLogin(string email, string password)
        {
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", true, true)
               .Build();
            var adminEmail = config["Admin:Email"];
            var adminPass = config["Admin:Password"];
            if (adminEmail == email && adminPass == password)
            {
                return true;
            }
            return false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
