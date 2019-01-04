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
    public class PictureManager
    {
        // À MODIFIER EN FONCTION DE SON PROPRE LOCALHOST
        static string localhost = "http://localhost:50021/";


        // Appelle la requête qui récupère une liste de toutes les images correspondant à un hôtel (identifiant en paramètre)
        // GET: api/Hotel/{id}/Pictures
        public static List<Picture> GetAllPictures(int idHotel)
        {
            string url = localhost + "api/Hotel/" + idHotel + "/Pictures";

            using (HttpClient http = new HttpClient())
            {
                Task<String> response = http.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Picture>>(response.Result);
            }
        }


        // Appelle la requête qui récupère une liste de toutes les images correspondant à une chambre (identifiant en paramètre)
        // GET: api/Room/{id}/Pictures
        public static List<Picture> GetRoomPictures(int idRoom)
        {
            string url = localhost + "api/Room/" + idRoom + "/Pictures";

            using (HttpClient http = new HttpClient())
            {
                Task<String> response = http.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Picture>>(response.Result);
            }
        }
    }
}
