using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ReservationController : Controller
    {
        // Page de confirmation de la réservation
        [HttpPost]
        public ActionResult Confirmation(string[] rooms_list)
        {
            // En premier lieu, pour chaque chambre récupérée, je ressors les informations de la chambre
            List<Room> rooms = new List<Room>();
            decimal totalPrice = 0;

            foreach (string item in rooms_list)
            {
                int id = Convert.ToInt16(item);
                Room newRoom = RoomManager.GetRoom(id);
                totalPrice += newRoom.Price;
                rooms.Add(newRoom);
            }

            // Je récupère le reste des informations sur la base du ViewModel ConfirmationVM
            Hotel hotel = HotelManager.GetHotel(Convert.ToInt16(rooms[0].IdHotel));
            DateTime dateStart = Convert.ToDateTime(Session["dateStart"]);
            DateTime dateEnd = Convert.ToDateTime(Session["dateEnd"]);
            int duration = (dateEnd - dateStart).Days;

            ConfirmationVM confirmation = new ConfirmationVM
            {
                DateStart = dateStart,
                DateEnd = dateEnd,
                Duration = duration,
                Name = hotel.Name,
                Location = hotel.Location,
                Rooms = rooms,
                TotalPrice = totalPrice
            };

            // Je récupère l'état de l'increase (hidden input) dans une variable de session
            Session["increase"] = Request["increase"];

            return View(confirmation);
        }


        // Page pour le message de confirmation en ligne de la réservation
        [HttpPost]
        public ActionResult Receipt(string[] rooms_list)
        {
            // Je récupère toutes les informations dont j'ai besoin pour créer une réservation
            DateTime dateReservation = DateTime.Today;
            string clientFirstname = Request["firstname"];
            string clientLastname = Request["lastname"];
            DateTime dateStart = Convert.ToDateTime(Session["dateStart"]);
            DateTime dateEnd = Convert.ToDateTime(Session["dateEnd"]);
            decimal totalPrice = Convert.ToDecimal(Request["totalPrice"]);

            // Je crée la nouvelle réservation en passant les informations ci-dessus en paramètres (et je récupère son identifiant)
            int idReservation = ReservationManager.AddReservation(dateReservation, clientFirstname, clientLastname, dateStart, dateEnd, totalPrice);

            // Pour chaque élément de la liste, je crée une ligne dans ReservationDetails
            foreach (string element in rooms_list)
            {
                Room room = RoomManager.GetRoom(Convert.ToInt16(element));
                int idRoom = Convert.ToInt16(element);
                decimal price = room.Price;
                decimal increase = 0;

                if (Session["increase"].ToString() == "true")
                    increase = price * 20 / 100;

                ReservationDetailsManager.AddReservationDetails(idReservation, idRoom, price, increase);               
            }

            // Je passe ensuite les informations dans un ViewModel, afin de pouvoir afficher la quittance de réservation à l'utilisateur
            ReservationVM reservation = new ReservationVM
            {
                IdReservation = idReservation,
                Gender = Request["gender"],
                ClientFirstname = clientFirstname,
                ClientLastname = clientLastname
            };

            return View(reservation);
        }


        // Page pour le message de confirmation en ligne de l'annulation d'une réservation
        [HttpPost]
        public ActionResult Removal()
        {
            // Je récupère les informations saisies par l'utilisateur (en minuscules, afin d'éviter les problèmes de casse)
            string firstname = Request["firstname"].ToLower();
            string lastname = Request["lastname"].ToLower();
            int identity = Convert.ToInt32(Request["number"]);

            // Je teste ensuite si les informations saisies correspondent à une réservation (comme une sorte de login)
            // Si c'est en ordre, je lance les deux requêtes de suppression
            Boolean login = ReservationManager.LoginReservation(identity, firstname, lastname);

            if (login.Equals(true))
            {
                ReservationManager.RemoveReservation(identity);
            }

            // Je passe ensuite la valeur du booléen login à la vue, afin d'afficher le bon encadré
            // Je passe également les informations de la réservation, afin de les afficher
            ViewData["login"] = login;

            ReservationVM reservation = new ReservationVM
            {
                IdReservation = identity,
                ClientFirstname = Request["firstname"],
                ClientLastname = Request["lastname"]
            };

            return View(reservation);
        }
    }
}
