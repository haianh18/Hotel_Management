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
    /// Interaction logic for BookingDetailWindow.xaml
    /// </summary>
    public partial class BookingDetailWindow : Window
    {
        private BookingDetailService bookingDetailService;
        public BookingDetailWindow()
        {
            InitializeComponent();
            bookingDetailService = new BookingDetailRepository();
        }
        public int BookingID { get; set; }
        public Customer Account { get; set; }
        public bool AdminOrCustomer { get; set; }

        private void btnLogout1_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow loginWindow = new LogInWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (AdminOrCustomer == false)
            {
                BookingWindow bookingWindow = new BookingWindow()
                {
                    Account = Account
                };
                bookingWindow.Show();
            }
            else
            {
                ReportWindow reportWindow = new ReportWindow();
                reportWindow.Show();
            }


            this.Close();
        }

        private void LoadBookingDetail()
        {
            try
            {
                List<BookingDetail> bookingDetailList = bookingDetailService.GetBookingDetailsByBookingID(BookingID);
                dgBookingDetails.ItemsSource = bookingDetailList;
            }
            catch (Exception ex)
            {

            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBookingDetail();
        }
    }
}
