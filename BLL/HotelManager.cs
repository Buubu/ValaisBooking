using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class HotelManager
    {
        // Appelle la requête qui récupère la liste de tous les hôtels
        public static List<Hotel> GetAllHotels()
        {
            return HotelDB.GetAllHotels();
        }


        // Appelle la requête qui récupère la liste des hôtels correspondant à la recherche simple
        public static List<Hotel> GetAllHotelsSimple(string location)
        {
            return HotelDB.GetAllHotelsSimple(location);
        }


        // Appelle la requête qui récupère la liste des hôtels correspondant à la recherche avancée
        public static List<Hotel> GetAllHotelsAdvanced(string location, int category, Boolean hasWifi, Boolean hasParking)
        {
            return HotelDB.GetAllHotelsAdvanced(location, category, hasWifi, hasParking);
        }


        // Appelle la requête qui récupère un hôtel en fonction d'un identifiant en paramètre
        public static Hotel GetHotel(int idHotel)
        {
            return HotelDB.GetHotel(idHotel);
        }
    }
}
