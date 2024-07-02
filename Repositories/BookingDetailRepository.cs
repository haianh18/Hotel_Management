using BusinessObjects.Models;
using DataAccessLayer;
using Services;

namespace Repositories
{
    public class BookingDetailRepository : BookingDetailService

    {
        public void DeleteBookingDetail(BookingDetail bookingDetail) => BookingDetailDAO.Instance.DeleteBookingDetail(bookingDetail);
       

        public BookingDetail GetBookingDetailById(int bookingReservationId, int roomId) => BookingDetailDAO.Instance.GetById(bookingReservationId, roomId);
       

        public List<BookingDetail> GetBookingDetails() => BookingDetailDAO.Instance.GetBookingDetails();
        public List<BookingDetail> GetBookingDetailsByBookingID(int bookingId) => BookingDetailDAO.Instance.GetBookingDetailsByBookingID(bookingId);
        public List<BookingDetail> GetBookingDetailsByRoomID(int roomId) => BookingDetailDAO.Instance.GetBookingDetailsByRoomID(roomId);

        public void SaveBookingDetail(BookingDetail bookingDetail) => BookingDetailDAO.Instance.SaveBookingDetail(bookingDetail);

        public void UpdateBookingDetail(BookingDetail bookingDetail) => BookingDetailDAO.Instance.UpdateBookingDetail(bookingDetail);

        public List<BookingDetail> GetBookingDetailsByCustomerID(int customerId) => BookingDetailDAO.Instance.GetBookingDetailsByCustomerID(customerId) ;

    }
}
