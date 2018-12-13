using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public static class ReservationDB
    {
        // Requête qui permet d'ajouter une réservation à la table
        public static int AddReservation(DateTime dateReservation, string clientFirstname, string clientLastname, DateTime dateStart, DateTime dateEnd, decimal totalPrice)
        {
            int result = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Reservation (DateReservation, ClientFirstname, ClientLastname, DateStart, DateEnd, TotalPrice) " +
                                        "VALUES (@dateReservation, @clientFirstname, @clientLastname, @dateStart, @dateEnd, @totalPrice); " +
                                   "SELECT SCOPE_IDENTITY();";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@dateReservation", dateReservation);
                    command.Parameters.AddWithValue("@clientFirstname", clientFirstname);
                    command.Parameters.AddWithValue("@clientLastname", clientLastname);
                    command.Parameters.AddWithValue("@dateStart", dateStart);
                    command.Parameters.AddWithValue("@dateEnd", dateEnd);
                    command.Parameters.AddWithValue("@totalPrice", totalPrice);

                    connection.Open();

                    // Cette requête retourne l'identifiant de la réservation qui vient d'être insérée
                    result = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }


        // Requête qui permet de vérifier l'existence d'une réservation dans la table
        public static Boolean LoginReservation(int idReservation, string firstname, string lastname)
        {
            int result = 0;
            Boolean answer = false;

            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * " +
                                        "FROM Reservation " +
                                        "WHERE IdReservation = @idReservation " +
                                            "AND LOWER(ClientFirstname) = @firstname " +
                                            "AND LOWER(ClientLastname) = @lastname";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idReservation", idReservation);
                    command.Parameters.AddWithValue("@firstname", firstname);
                    command.Parameters.AddWithValue("@lastname", lastname);

                    connection.Open();

                    result = Convert.ToInt32(command.ExecuteScalar());

                    if (result != 0)
                        answer = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            // Elle retourne un booléen qui fonctionne comme une sorte de "login"
            return answer;
        }


        // Requête qui permet de supprimer une réservation de la table (en fonction de son identifiant en paramètre)
        public static void RemoveReservation(int idReservation)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Reservation WHERE IdReservation = @idReservation";

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
