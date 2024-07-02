using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface RoomTypeService
    {
        public List<RoomType> GetRoomTypes();
        public void SaveRoomType(RoomType roomType);
        public void UpdateRoomType(RoomType roomType);
        public void DeleteRoomType(RoomType roomType);
        public RoomType GetRoomTypeById(int roomTypeId);
    }
}
