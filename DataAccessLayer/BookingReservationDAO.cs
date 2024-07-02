using BusinessObjects.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccessLayer
{
    public class BookingReservationDAO
    {
        private static BookingReservationDAO instance = null;
        private static readonly object padlock = new object();

        private BookingReservationDAO() { }
        public static BookingReservationDAO Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new BookingReservationDAO();
                    }
                    return instance;
                }
            }
        }

        public List<BookingReservation> GetBookingReservations()
        {
            var bookingReservations = new List<BookingReservation>();
            try
            {
               using var context = new FuminiHotelManagementContext();
                bookingReservations = context.BookingReservations.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return bookingReservations;
        }

        public void SaveBookingReservation(BookingReservation bookingReservation)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.BookingReservations.Add(bookingReservation);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateBookingReservation(BookingReservation bookingReservation)
        {
            
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.Entry<BookingReservation>(bookingReservation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }


        public void DeleteBookingReservation(BookingReservation bookingReservation)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                var booking1 = db.BookingReservations.SingleOrDefault(c => c.BookingReservationId == bookingReservation.BookingReservationId);
                db.BookingReservations.Remove(booking1);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
          
        }


        public BookingReservation GetBookingReservationById(int bookingReservationID)
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingReservations.FirstOrDefault(c=>c.BookingReservationId.Equals(bookingReservationID));
        }


        public List<BookingReservation> GetBookingReservationsByCustomerId(int customerId)
        {
            var bookingReservations = new List<BookingReservation>();
            try
            {
                using var db = new FuminiHotelManagementContext();
                bookingReservations = db.BookingReservations.Where(c=>c.CustomerId == customerId).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return bookingReservations;
        }
        public List<BookingReservation> GetBookingByDateRange(DateTime start, DateTime end)
        {
            var bookings = GetBookingReservations();
            return bookings.Where(b => b.BookingDate >= start && b.BookingDate <= end).ToList();
        }

        public bool IsRoomBooked(int roomId, DateTime startDate, DateTime endDate)
        {
            // Logic để kiểm tra xem phòng đã được đặt chưa
            using (var context = new FuminiHotelManagementContext())
            {
                return context.BookingDetails
                    .Any(bd => bd.RoomId == roomId &&
                               ((startDate >= bd.StartDate && startDate <= bd.EndDate) ||
                                (endDate >= bd.StartDate && endDate <= bd.EndDate) ||
                                (startDate <= bd.StartDate && endDate >= bd.EndDate)));
            }
        }
    }
}
