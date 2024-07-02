using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface BookingReservationService
    {
        List<BookingReservation> GetBookingReservations();
        void SaveBookingReservation(BookingReservation bookingReservation);
        void UpdateBookingReservation(BookingReservation bookingReservation);
        void DeleteBookingReservation(BookingReservation bookingReservation);
        BookingReservation GetBookingReservationById(int bookingReservationId);
        List<BookingReservation> GetBookingReservationsByCustomerId(int customerId);

        List<BookingReservation> GetBookingByDateRange(DateTime start, DateTime end);

        bool IsRoomBooked(int roomId, DateTime startDate, DateTime endDate);
    }
}
