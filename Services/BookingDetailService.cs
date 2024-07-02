using BusinessObjects.Models;

namespace Services
{
    public interface BookingDetailService
    {
        List<BookingDetail> GetBookingDetails();
        void SaveBookingDetail(BookingDetail bookingDetail);
        void UpdateBookingDetail(BookingDetail bookingDetail);
        void DeleteBookingDetail(BookingDetail bookingDetail);
        BookingDetail GetBookingDetailById(int bookingReservationId, int roomId);

        List<BookingDetail> GetBookingDetailsByBookingID(int bookingId);

        List<BookingDetail> GetBookingDetailsByRoomID(int roomId);

        List<BookingDetail> GetBookingDetailsByCustomerID(int customerId);
    }
}
