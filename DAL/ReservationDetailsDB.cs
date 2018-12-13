using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public static class ReservationDetailsDB
    {
        // Requête qui permet d'ajouter les détails d'une réservation à la table
        public static void AddReservationDetails(int idReservation, int idRoom, decimal roomPrice, decimal increase)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ReservationDetails (IdReservation, IdRoom, RoomPrice, Increase) " +
                                        "VALUES (@idReservation, @idRoom, @roomPrice, @increase)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idReservation", idReservation);
                    command.Parameters.AddWithValue("@idRoom", idRoom);
                    command.Parameters.AddWithValue("@roomPrice", roomPrice);
                    command.Parameters.AddWithValue("@increase", increase);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        // Requête qui permet de supprimer les détails d'une réservation (supprime toutes les lignes)
        public static void RemoveReservationDetails(int idReservation)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM ReservationDetails WHERE IdReservation = @idReservation";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idReservation", idReservation);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
