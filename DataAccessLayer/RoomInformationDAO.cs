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
    public class RoomInformationDAO
    {
        private static RoomInformationDAO instance = null;
        private static readonly object padlock = new object();

        private RoomInformationDAO() { }
        
        public static RoomInformationDAO Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RoomInformationDAO();
                    }
                    return instance;
                }
            }

        }

        public List<RoomInformation> GetRoomInformations()
        {
            var roomInformations = new List<RoomInformation>();
           try
            {
                using var db = new FuminiHotelManagementContext();
                roomInformations = db.RoomInformations.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           

            return roomInformations;
        }

        public void SaveRoomInformation(RoomInformation roomInformation)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.RoomInformations.Add(roomInformation);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        public void UpdateRoomInformation(RoomInformation roomInformation)
        {
           
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.Entry(roomInformation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        public void DeleteRoomInformation(RoomInformation roomInformation)
        {
            
            try
            {
                using var db = new FuminiHotelManagementContext();
                var room1 = db.RoomInformations.FirstOrDefault(c=> c.RoomId == roomInformation.RoomId);
                db.RoomInformations.Remove(room1);
                db.SaveChanges() ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        public RoomInformation GetRoomInformationById(int roomId)
        {
           try
            {
                using var db = new FuminiHotelManagementContext();
                var room = db.RoomInformations.FirstOrDefault(c => c.RoomId == roomId);
                return room;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<RoomInformation> GetRoomInformationsByNumber(string roomNumber)
        {
            var roomInformations = new List<RoomInformation>();
            
            try
            {
                using var db = new FuminiHotelManagementContext();
                roomInformations = db.RoomInformations.Where(c => c.RoomNumber.Equals(roomNumber)).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return roomInformations;
        }
    }
}
