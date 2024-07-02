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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }
        private void btnManageCustomer_Click(object sender, RoutedEventArgs e)
        {
            ManageCustomerWindow manageCustomer = new ManageCustomerWindow();
            manageCustomer.Show();
            this.Close();
        }

        private void btnManageRoom_Click(object sender, RoutedEventArgs e)
        {
            ManageRoomWindow manageRoom = new ManageRoomWindow();
            manageRoom.Show();
            this.Close();
        }

        private void btnManageBooking_Click(object sender, RoutedEventArgs e)
        {
            ManageBookingWindow manageBooking = new ManageBookingWindow();
            manageBooking.Show();
            this.Close();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow loginWindow = new LogInWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow reportWindow = new ReportWindow();
            reportWindow.Show();
            this.Close();
        }
    }
}
