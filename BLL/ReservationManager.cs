using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class ReservationManager
    {
        // Appelle la requête qui permet d'ajouter une réservation à la table
        public static int AddReservation(DateTime dateReservation, string clientFirstname, string clientLastname, DateTime dateStart, DateTime dateEnd, decimal totalPrice)
        {
            return ReservationDB.AddReservation(dateReservation, clientFirstname, clientLastname, dateStart, dateEnd, totalPrice);
        }


        // Appelle la requête qui permet de vérifier l'existence d'une réservation dans la table
        public static Boolean LoginReservation(int idReservation, string firstname, string lastname)
        {
            return ReservationDB.LoginReservation(idReservation, firstname, lastname);
        }


        // Appelle la requête qui permet de supprimer une réservation de la table (en fonction de son identifiant en paramètre)
        public static void RemoveReservation(int idReservation)
        {
            ReservationDB.RemoveReservation(idReservation);
        }
    }
}
