using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Net.Http;
using Newtonsoft.Json;


namespace BLL
{
    public class HotelManager
    {
        // À MODIFIER EN FONCTION DE SON PROPRE LOCALHOST
        static string localhost = "http://localhost:50021/";


        // Appelle la requête qui récupère la liste de tous les hôtels
        // GET: api/Hotels
        public static List<Hotel> GetAllHotels()
        {
            string url = localhost + "api/Hotels";

            using (HttpClient http = new HttpClient())
            {
                Task<String> response = http.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Hotel>>(response.Result);
            }
        }


        // Appelle la requête qui récupère la liste des hôtels correspondant à la recherche simple
        // GET: api/Hotels/locality/{location}
        public static List<Hotel> GetAllHotelsSimple(string location)
        {
            string url = localhost + "api/Hotels/locality/" + location;

            using (HttpClient http = new HttpClient())
            {
                Task<String> response = http.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Hotel>>(response.Result);
            }
        }


        // Appelle la requête qui récupère la liste des hôtels correspondant à la recherche avancée
        // Utilise GetAllHotelsSimple et filtre le résultat
        public static List<Hotel> GetAllHotelsAdvanced(string location, int category, Boolean hasWifi, Boolean hasParking)
        {
            List<Hotel> reference = GetAllHotelsSimple(location);
            List<Hotel> hotels = new List<Hotel>();

            // Filtre de la liste en fonction de la recherche avancée
            foreach (var hotel in reference)
            {
                Boolean added = false;

                // Si le critère de la catégorie a été mentionné: garde les hôtels qui sont à cette valeur ou plus
                if (category != 0)
                {
                    if (hotel.Category >= category)
                    {
                        added = true;
                    }
                }

                // Si le critère du Wifi a été mentionné: garde les hôtels qui le possède
                if (hasWifi.Equals(true))
                {
                    if (hotel.HasWifi.Equals(hasWifi))
                    {
                        added = true;
                    }
                    else
                    {
                        added = false;
                    }
                }

                // Si le critère du parking a été mentionné: garde les hôtels qui en possède
                if (hasParking.Equals(true))
                {
                    if (hotel.HasParking.Equals(hasParking))
                    {
                        added = true;
                    }
                    else
                    {
                        added = false;
                    }
                }

                // Si l'hôtel répond à tous les critères, il est ajouté à la liste retournée
                if (added.Equals(true))
                {
                    hotels.Add(hotel);
                }
            }

            return hotels;
        }


        // Appelle la requête qui récupère un hôtel en fonction d'un identifiant en paramètre
        // GET: api/Hotel/{id}
        public static Hotel GetHotel(int idHotel)
        {
            string url = localhost + "api/Hotel/" + idHotel;

            using (HttpClient http = new HttpClient())
            {
                Task<String> response = http.GetStringAsync(url);
                return JsonConvert.DeserializeObject<Hotel>(response.Result);
            }
        }
    }
}
