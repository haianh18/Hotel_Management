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
    public class RoomInformationRepository : RoomInformationService
    {
        public void DeleteRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.Instance.DeleteRoomInformation(roomInformation);

        public List<RoomInformation> FindByRoomNumber(string roomNumber) => RoomInformationDAO.Instance.GetRoomInformationsByNumber(roomNumber);


        public RoomInformation GetRoomInformationById(int roomId) => RoomInformationDAO.Instance.GetRoomInformationById(roomId);

        public List<RoomInformation> GetRoomInformations() => RoomInformationDAO.Instance.GetRoomInformations();

        public void SaveRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.Instance.SaveRoomInformation(roomInformation);

        public void UpdateRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.Instance.UpdateRoomInformation(roomInformation);

    }
}
