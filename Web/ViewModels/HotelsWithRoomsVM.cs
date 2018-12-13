using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;

namespace Web.ViewModels
{
    public class HotelsWithRoomsVM
    {
        public int IdHotel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Category { get; set; }
        public Boolean HasWifi { get; set; }
        public Boolean HasParking { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public List<string> PicturesUrls { get; set; }
        public List<Room> Rooms { get; set; }
        public List<AvailableRoomVM> AvailableRooms { get; set; }
        public List<Room> OccupiedRooms { get; set; }
        public int Occupation { get; set; }        
        public int AveragePrice { get; set; }
    }
}