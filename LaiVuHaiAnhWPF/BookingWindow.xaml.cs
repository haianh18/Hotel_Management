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
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        private BookingReservationService bookingReservationService;
        public BookingWindow()
        {
            InitializeComponent();
            bookingReservationService = new BookingReservationRepository();
        }
        public Customer Account { get; set; }

        private void LoadBooking()
        {
            try
            {
                var bookings = bookingReservationService.GetBookingReservationsByCustomerId(Account.CustomerId);
                dgBookingHistory.ItemsSource = bookings;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnLogout1_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow loginWindow = new LogInWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow()
            {
                Account = Account
            };
            this.Close();
            customerWindow.Show();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var booking = button.DataContext as BookingReservation;
            if (booking != null)
            {
                // Show the booking detail pop-up window passing the booking reservation ID
                BookingDetailWindow detailWindow = new BookingDetailWindow
                {
                    BookingID = booking.BookingReservationId,
                    Account = Account,
                    AdminOrCustomer = false
                };
                this.Close();
                detailWindow.ShowDialog();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBooking();
        }
    }
}
