using BusinessObjects.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class BookingDetailDAO
    {
        private static BookingDetailDAO instance = null;
        private static readonly object padlock = new object();

        private BookingDetailDAO()
        {}

        public static BookingDetailDAO Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new BookingDetailDAO();
                    }
                    return instance;
                }
            }
        }


        public List<BookingDetail> GetBookingDetails()
        {
            var bookingDetails = new List<BookingDetail>();
            try
            {
                using var db = new FuminiHotelManagementContext();
                bookingDetails = [.. db.BookingDetails];
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return bookingDetails;
        }

        public void SaveBookingDetail(BookingDetail bookingDetail)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.BookingDetails.Add(bookingDetail);
                db.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void UpdateBookingDetail(BookingDetail bookingDetail)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Entry<BookingDetail>(bookingDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void DeleteBookingDetail(BookingDetail bookingDetail)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var bookingDetail1 = context.BookingDetails.SingleOrDefault(c => (c.BookingReservationId == bookingDetail.BookingReservationId && c.RoomId == bookingDetail.RoomId));
                context.BookingDetails.Remove(bookingDetail1);
                context.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public BookingDetail GetById(int bookingId, int roomId)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var bookingDetail1 = context.BookingDetails.SingleOrDefault(c => (c.BookingReservationId == bookingId && c.RoomId == roomId));
                return bookingDetail1;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<BookingDetail> GetBookingDetailsByBookingID(int bookingId)
        {
            var bookingDetails = new List<BookingDetail>();
            try
            {
                using var db = new FuminiHotelManagementContext();
                bookingDetails = [.. db.BookingDetails.Where(c => c.BookingReservationId == bookingId)];
                return bookingDetails;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<BookingDetail> GetBookingDetailsByRoomID(int roomId)
        {
            var bookingDetails = new List<BookingDetail>();
            try
            {
                using var db = new FuminiHotelManagementContext();
                bookingDetails = [.. db.BookingDetails.Where(c => c.RoomId == roomId)];
                return bookingDetails;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<BookingDetail> GetBookingDetailsByCustomerID(int customerId)
        {
            List<BookingDetail> list = new List<BookingDetail>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                list = context.BookingDetails.Where(b => b.BookingReservation.CustomerId == customerId)
                    .Include(b => b.BookingReservation)
                    .Include(b => b.Room)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
