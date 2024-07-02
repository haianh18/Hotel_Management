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
    public class RoomTypeRepository : RoomTypeService
    {
        public void DeleteRoomType(RoomType roomType) => RoomTypeDAO.Instance.DeleteRoomType(roomType);

        public RoomType GetRoomTypeById(int roomTypeId) => RoomTypeDAO.Instance.GetRoomTypeById(roomTypeId);

        public List<RoomType> GetRoomTypes() => RoomTypeDAO.Instance.GetRoomTypes();

        public void SaveRoomType(RoomType roomType) => RoomTypeDAO.Instance.SaveRoomType(roomType);

        public void UpdateRoomType(RoomType roomType) => RoomTypeDAO.Instance.UpdateRoomType(roomType);
    }
}
