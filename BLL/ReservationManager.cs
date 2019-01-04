﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Newtonsoft.Json;
using System.Net.Http;
using DTO;


namespace BLL
{
    public class ReservationManager
    {
        // À MODIFIER EN FONCTION DE SON PROPRE LOCALHOST
        static string localhost = "http://localhost:50021/";


        // Appelle la requête qui permet d'ajouter une réservation à la table
        // POST: api/Reservations
        public static int AddReservation(DateTime dateReservation, string clientFirstname, string clientLastname, DateTime dateStart, DateTime dateEnd, decimal totalPrice)
        {
            Reservation res = new Reservation();
            res.DateReservation = dateReservation;
            res.ClientFirstname = clientFirstname;
            res.ClientLastname = clientLastname;
            res.DateStart = dateStart;
            res.DateEnd = dateEnd;
            res.TotalPrice = totalPrice;

            string url = localhost + "api/Reservations";
            using (HttpClient http = new HttpClient())
            {
                string reservation = JsonConvert.SerializeObject(res);
                StringContent frame = new StringContent(reservation, Encoding.UTF8, "Application/json");
                Task<HttpResponseMessage> response = http.PostAsync(url, frame);
                return response.Id;
            }
        }


        // Appelle la requête qui permet de vérifier l'existence d'une réservation dans la table
        // GET: api/Reservation/LoginValidation/{idReservation}/{firstname}/{lastname}
        public static Boolean LoginReservation(int idReservation, string firstname, string lastname)
        {
            string url = localhost + "api/Reservation/LoginValidation/" + idReservation + "/" + firstname + "/" + lastname;

            using (HttpClient http = new HttpClient())
            {
                Task<String> response = http.GetStringAsync(url);
                return JsonConvert.DeserializeObject<Boolean>(response.Result);
            }
        }


        // Appelle la requête qui permet de supprimer une réservation de la table (en fonction de son identifiant en paramètre)
        // DELETE: api/Reservations/{idReservation}
        public static void RemoveReservation(int idReservation)
        {
            string url = localhost + "api/Reservations/" + idReservation;

            using (HttpClient http = new HttpClient())
            {
                Task<HttpResponseMessage> response = http.DeleteAsync(url);
            }
        }
    }
}
