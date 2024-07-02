using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface RoomInformationService
    {
        List<RoomInformation> GetRoomInformations();
        void SaveRoomInformation(RoomInformation roomInformation);
        void UpdateRoomInformation(RoomInformation roomInformation);
        void DeleteRoomInformation(RoomInformation roomInformation);
        RoomInformation GetRoomInformationById(int roomId);
        List<RoomInformation> FindByRoomNumber(string roomNumber);
    }
}
