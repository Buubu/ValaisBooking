using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using DTO;


namespace BLL
{
    public class ReservationDetailsManager
    {
        static string localhost = "http://localhost:1787/";


        // Appelle la requête qui permet d'ajouter les détails d'une réservation à la table
        // POST: api/ReservationDetails
        public static void AddReservationDetails(int idReservation, int idRoom, decimal roomPrice, decimal increase)
        {
            ReservationDetails RD = new ReservationDetails();
            RD.IdReservation = idReservation;
            RD.IdRoom = idRoom;
            RD.RoomPrice = roomPrice;
            RD.Increase = increase;

            string url = localhost + "api/ReservationDetails";
            using (HttpClient http = new HttpClient())
            {
                string detail = JsonConvert.SerializeObject(RD);
                StringContent frame = new StringContent(detail, Encoding.UTF8, "Application/json");
                Task<HttpResponseMessage> response = http.PostAsync(url, frame);
                response.Wait();
            }
        }


        // Appelle la requête qui permet de supprimer les détails d'une réservation (supprime toutes les lignes)
        // DELETE: api/ReservationDetails/{idReservation}
        public static void RemoveReservationDetails(int idReservation)
        {
            string url = localhost + "api/ReservationDetails/" + idReservation;

            using (HttpClient http = new HttpClient())
            {
                Task<HttpResponseMessage> response = http.DeleteAsync(url);
                response.Wait();
            }
        }
    }
}
