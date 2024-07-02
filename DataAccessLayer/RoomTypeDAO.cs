using BusinessObjects.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoomTypeDAO
    {
        private static RoomTypeDAO instance = null;
        private static readonly object padlock = new object();

        private RoomTypeDAO() { }
       
        public static RoomTypeDAO Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RoomTypeDAO();
                    }
                    return instance;
                }
            }

        }
        public List<RoomType> GetRoomTypes()
        {
            var roomTypes = new List<RoomType>();
            
            try
            {
                using var db = new FuminiHotelManagementContext();
                roomTypes = db.RoomTypes.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            

            return roomTypes;
        }

        public void SaveRoomType(RoomType roomType)
        {
            
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.RoomTypes.Add(roomType);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
          
        }

        public void UpdateRoomType(RoomType roomType)
        {
           
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.Entry(roomType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
          
        }

        public void DeleteRoomType(RoomType roomType)
        {
           
            try
            {
                using var db = new FuminiHotelManagementContext();
                //var room = db.RoomTypes.SingleOrDefault(c=>c.RoomTypeId==roomType.RoomTypeId);
                db.RoomTypes.Remove(roomType);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public RoomType GetRoomTypeById(int roomTypeId)
        {
            

            try
            {
                using var db = new FuminiHotelManagementContext();
                return db.RoomTypes.FirstOrDefault(c => c.RoomTypeId == roomTypeId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }
    }
}
