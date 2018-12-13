using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class RoomManager
    {
        // Appelle la requête qui récupère la liste de toutes les chambres d'un hôtel (en fonction de son identifiant en paramètre)
        public static List<Room> GetAllRooms(int idHotel)
        {
            return RoomDB.GetAllRooms(idHotel);
        }


        // Appelle la requête qui récupère la liste de toutes les chambres qui correspondent à la recherche simple
        public static List<Room> GetSearchedRoomsSimple(int idHotel, DateTime dateStart, DateTime dateEnd)
        {
            return RoomDB.GetSearchedRoomsSimple(idHotel, dateStart, dateEnd);
        }


        // Appelle la requête qui récupère la liste de toutes les chambres qui correspondent à la recherche avancée
        public static List<Room> GetSearchedRoomsAdvanced(int idHotel, DateTime dateStart, DateTime dateEnd, Boolean hasTV, Boolean hasHairDryer)
        {
            return RoomDB.GetSearchedRoomsAdvanced(idHotel, dateStart, dateEnd, hasTV, hasHairDryer);
        }


        // Appelle la requête qui récupère une chambre en fonction de son identifiant (en paramètre)
        public static Room GetRoom(int idRoom)
        {
            return RoomDB.GetRoom(idRoom);
        }


        // Appelle la requête qui récupère la liste de toutes les chambres qui sont occupées à une certaine période
        public static List<Room> GetOccupiedRooms(int idHotel, DateTime dateStart, DateTime dateEnd)
        {
            return RoomDB.GetOccupiedRooms(idHotel, dateStart, dateEnd);
        }
    }
}
