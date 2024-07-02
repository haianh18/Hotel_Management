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
    public class BookingReservationRepository : BookingReservationService
    {
        public void DeleteBookingReservation(BookingReservation bookingReservation) => BookingReservationDAO.Instance.DeleteBookingReservation(bookingReservation);

        public BookingReservation GetBookingReservationById(int bookingReservationId) => BookingReservationDAO.Instance.GetBookingReservationById(bookingReservationId);

        public List<BookingReservation> GetBookingReservations() => BookingReservationDAO.Instance.GetBookingReservations();

        public void SaveBookingReservation(BookingReservation bookingReservation) => BookingReservationDAO.Instance.SaveBookingReservation(bookingReservation);

        public void UpdateBookingReservation(BookingReservation bookingReservation) => BookingReservationDAO.Instance.UpdateBookingReservation(bookingReservation);
        public List<BookingReservation> GetBookingReservationsByCustomerId(int customerId) => BookingReservationDAO.Instance.GetBookingReservationsByCustomerId((int)customerId);

        public List<BookingReservation> GetBookingByDateRange(DateTime start, DateTime end) => BookingReservationDAO.Instance.GetBookingByDateRange(start, end);

        public bool IsRoomBooked(int roomId, DateTime startDate, DateTime endDate) => BookingReservationDAO.Instance.IsRoomBooked(roomId, startDate, endDate);
    }
}
