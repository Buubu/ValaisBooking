using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using Newtonsoft.Json;
using System.Net.Http;

namespace BLL
{
    public class RoomManager
    {
        // À MODIFIER EN FONCTION DE SON PROPRE LOCALHOST
        static string localhost = "http://localhost:50021/";


        // Appelle la requête qui récupère la liste de toutes les chambres d'un hôtel (en fonction de son identifiant en paramètre)
        // GET: api/Hotel/{IdHotel}/Rooms
        public static List<Room> GetAllRooms(int idHotel)
        {
            string url = localhost + "api/Hotel/" + idHotel + "/Rooms";

            using (HttpClient http = new HttpClient())
            {
                Task<String> response = http.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }
        }


        // Appelle la requête qui récupère la liste de toutes les chambres qui correspondent à la recherche simple
        // GET: api/Hotel/{idHotel}/Availability/{dateStart}/{dateEnd}
        public static List<Room> GetSearchedRoomsSimple(int idHotel, DateTime dateStart, DateTime dateEnd)
        {
            string url = localhost + "api/Hotel/" + idHotel + "/Availability/" + dateStart + "/" + dateEnd;

            using (HttpClient http = new HttpClient())
            {
                Task<String> response = http.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }
        }


        // Appelle la requête qui récupère la liste de toutes les chambres qui correspondent à la recherche avancée
        // Utilise GetSearchedRoomsSimple et filtre le résultat
        public static List<Room> GetSearchedRoomsAdvanced(int idHotel, DateTime dateStart, DateTime dateEnd, Boolean hasTV, Boolean hasHairDryer)
        {
            List<Room> reference = GetSearchedRoomsSimple(idHotel, dateStart, dateEnd);
            List<Room> rooms = new List<Room>();

            // Filtre de la liste en fonction de la recherche avancée
            foreach (var room in reference)
            {
                Boolean added = false;

                // Si le critère de la télévision a été mentionné: garde les chambres qui en possèdent une
                if (hasTV.Equals(true))
                {
                    if (room.HasTV.Equals(hasTV))
                    {
                        added = true;
                    }
                }

                // Si le critère du foehn a été mentionné: garde les chembres qui en possèdent un
                if (hasHairDryer.Equals(true))
                {
                    if (room.HasHairDryer.Equals(hasHairDryer))
                    {
                        added = true;
                    }
                    else
                    {
                        added = false;
                    }
                }

                // Si la chambre répond à tous les critères, elle est ajoutée à la liste retournée
                if (added.Equals(true))
                {
                    rooms.Add(room);
                }
            }

            return rooms;
        }


        // Appelle la requête qui récupère une chambre en fonction de son identifiant (en paramètre)
        // GET: api/Rooms/{idRoom}
        public static Room GetRoom(int idRoom)
        {
            string url = localhost + "api/Rooms/" + idRoom;

            using (HttpClient http = new HttpClient())
            {
                Task<String> response = http.GetStringAsync(url);
                return JsonConvert.DeserializeObject<Room>(response.Result);
            }
        }


        // Appelle la requête qui récupère la liste de toutes les chambres qui sont occupées à une certaine période
        // GET: api/Hotel/{idHotel}/occupied/{dateStart}/{dateEnd}
        public static List<Room> GetOccupiedRooms(int idHotel, DateTime dateStart, DateTime dateEnd)
        {
            string url = localhost + "api/Hotel/" + idHotel + "/occupied/" + dateStart + "/" + dateEnd;

            using (HttpClient http = new HttpClient())
            {
                Task<String> response = http.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }
        }
    }
}
