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
    /// Interaction logic for ManageBookingWindow.xaml
    /// </summary>
    public partial class ManageBookingWindow : Window
    {
        private CustomerService customerRepository;
        private RoomInformationService roomInformationRepository;
        private BookingDetailService bookingDetailRepository;
        private BookingReservationService bookingReservationRepository;
        private int selectedCustomerId = -1;
        private int selectedRoomId = -1;
        private int selectedBooking = -1;

        public ManageBookingWindow()
        {
            InitializeComponent();
            customerRepository = new CustomerRepository();
            roomInformationRepository = new RoomInformationRepository();
            bookingDetailRepository = new BookingDetailRepository();
            bookingReservationRepository = new BookingReservationRepository();
        }

        private void LoadRoomList()
        {
            try
            {
                var roomList = roomInformationRepository.GetRoomInformations()
                    .Select(b => new
                    {
                        RoomId = b.RoomId,
                        RoomNumber = b.RoomNumber
                    });
                dgRoomsName.ItemsSource = roomList;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error on load list of rooms");
            }
        }

        public void LoadCustomerList()
        {
            try
            {
                var customerList = customerRepository.GetCustomers()
                    .Select(c => new
                    {
                        CustomerId = c.CustomerId,
                        CustomerName = c.CustomerFullName
                    });
                dgCustomerName.ItemsSource = customerList;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error on load list of customer");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCustomerList();
            LoadRoomList();
        }

        private void dgCustomerName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid? dataGrid = sender as DataGrid;
            DataGridRow? row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dgCustomerName.SelectedIndex);
            DataGridCell? rowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            if (rowColumn != null)
            {
                int id = Int32.Parse(((TextBlock)rowColumn.Content).Text);
                selectedCustomerId = id;
                LoadCustomerBooking();
            }
        }

        private void LoadCustomerBooking()
        {
            var bookingDetails = bookingDetailRepository.GetBookingDetailsByCustomerID(selectedCustomerId)
                    .Select(b => new
                    {
                        BookingReservationId = b.BookingReservationId,
                        RoomNumber = b.Room.RoomNumber,
                        BookingDate = b.BookingReservation.BookingDate,
                        StartDate = b.StartDate,
                        EndDate = b.EndDate,
                        ActualPrice = b.ActualPrice,
                        BookingStatus = b.BookingReservation.BookingStatus
                    });

            dgCustomerBookingDetail.ItemsSource = bookingDetails;
        }

        private void dgRoomsName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid? dataGrid = sender as DataGrid;
            DataGridRow? row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dgRoomsName.SelectedIndex);
            DataGridCell? rowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            if (rowColumn != null)
            {
                int id = Int32.Parse(((TextBlock)rowColumn.Content).Text);
                selectedRoomId = id;
                LoadRoomBooking();
            }
        }

        private void LoadRoomBooking()
        {
            var bookingDetails = bookingDetailRepository.GetBookingDetailsByRoomID(selectedRoomId);
            if (bookingDetails == null) { return; }

           bookingDetails
                    .Select(b => new
                    {
                        BookingReservationId = b.BookingReservationId,
                        RoomNumber = b.Room.RoomNumber,
                        BookingDate = b.BookingReservation.BookingDate,
                        StartDate = b.StartDate,
                        EndDate = b.EndDate,
                        ActualPrice = b.ActualPrice,
                        BookingStatus = b.BookingReservation.BookingStatus
                    });
                dgCustomerBookingDetail.ItemsSource = bookingDetails;
            
        }

        private void btnDeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBooking == -1)
            {
                MessageBox.Show("Please select a booking to delete.", "Generate report");
                return;
            }

            BookingReservation booking = bookingReservationRepository.GetBookingReservationById(selectedBooking);
            MessageBoxResult result = MessageBox
                        .Show($"Do you want to delete booking number {booking.BookingReservationId}?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                bookingReservationRepository.DeleteBookingReservation(booking);
                LoadCustomerBooking();
            }
        }


        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("Please select a customer.", "Generate report");
                return;
            }

            if (selectedRoomId == -1)
            {
                MessageBox.Show("Please select a room.", "Generate report");
                return;
            }

            if (start_date.SelectedDate == null || end_date.SelectedDate == null)
            {
                MessageBox.Show("Please select start date and end date.", "Generate report");
                return;
            }

            var bookingDate = GetBookingDate();
            DateTime startDate = DateTime.Parse(start_date.Text);
            DateTime endDate = DateTime.Parse(end_date.Text);

            if (startDate < bookingDate)
            {
                MessageBox.Show("Start date can not smaller than today");
                return;
            }

            if (endDate < bookingDate)
            {
                MessageBox.Show("End date can not bigger than today");
                return;
            }
            if (startDate > endDate)
            {
                MessageBox.Show("Start date can not bigger than end date");
                return;
            }
            bool roomIsBusy = bookingReservationRepository.IsRoomBooked(selectedRoomId, startDate, endDate);
            if (roomIsBusy)
            {
                MessageBox.Show($"Room is busy between {startDate} till {endDate}!");
                return;
            }

            RoomInformation room = roomInformationRepository.GetRoomInformationById(selectedRoomId);
            
           
            int numberOfDays = (endDate - startDate).Days;
            decimal? totalPrice = numberOfDays * room.RoomPricePerDay;

            BookingReservation booking = new BookingReservation()
            {
                BookingDate = bookingDate,
                CustomerId = selectedCustomerId,
                BookingStatus = 1,
                TotalPrice = totalPrice
            };
            bookingReservationRepository.SaveBookingReservation(booking);

            BookingDetail bookingDetail = new BookingDetail()
            {
                BookingReservationId = booking.BookingReservationId,
                RoomId = selectedRoomId,
                StartDate = startDate,
                EndDate = endDate,
                ActualPrice = room.RoomPricePerDay
            };
            bookingDetailRepository.SaveBookingDetail(bookingDetail);
            LoadCustomerBooking();
            //book_date.Text = string.Empty;
            start_date.Text = string.Empty;
            end_date.Text = string.Empty;
        }

        private DateTime GetBookingDate()
        {
            var date = DateTime.Now;
            return date;
        }

        private void btnCloseBooking_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
        }

        private void dgCustomerBookingDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid? dataGrid = sender as DataGrid;
            DataGridRow? row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dgCustomerBookingDetail.SelectedIndex);
            if (row == null)
            {
                ClearDataGrid();
                return;
            }
            DataGridCell? rowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            if (rowColumn != null)
            {
                int id = Int32.Parse(((TextBlock)rowColumn.Content).Text);
                selectedBooking = id;
            }
        }

        private void ClearDataGrid()
        {
            dgCustomerBookingDetail.ItemsSource = null;
        }
    }
}


