using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class ReservationDetailsManager
    {
        // Appelle la requête qui permet d'ajouter les détails d'une réservation à la table
        public static void AddReservationDetails(int idReservation, int idRoom, decimal roomPrice, decimal increase)
        {
            ReservationDetailsDB.AddReservationDetails(idReservation, idRoom, roomPrice, increase);
        }


        // Appelle la requête qui permet de supprimer les détails d'une réservation (supprime toutes les lignes)
        public static void RemoveReservationDetails(int idReservation)
        {
            ReservationDetailsDB.RemoveReservationDetails(idReservation);
        }
    }
}
