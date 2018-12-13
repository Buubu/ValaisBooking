using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public static class HotelDB
    {
        // Requête qui récupère la liste de tous les hôtels
        public static List<Hotel> GetAllHotels()
        {
            List<Hotel> results = null;

            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Hotel";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (results == null)
                                results = new List<Hotel>();

                            Hotel hotel = new Hotel();

                            hotel.IdHotel = (int)reader["idHotel"];

                            hotel.Name = (string)reader["Name"];

                            hotel.Description = (string)reader["Description"];

                            hotel.Location = (string)reader["Location"];

                            hotel.Category = (int)reader["Category"];

                            hotel.HasWifi = (Boolean)reader["HasWifi"];

                            hotel.HasParking = (Boolean)reader["HasParking"];

                            if (reader["Phone"] != null)
                                hotel.Phone = (string)reader["Phone"];

                            if (reader["Email"] != null)
                                hotel.Email = (string)reader["Email"];

                            if (reader["Website"] != null)
                                hotel.Website = (string)reader["Website"];

                            results.Add(hotel);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }


        // Requête qui récupère la liste des hôtels correspondant à la recherche simple
        public static List<Hotel> GetAllHotelsSimple(string location)
        {
            List<Hotel> results = null;

            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Hotel WHERE Location = @location";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@location", location);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (results == null)
                                results = new List<Hotel>();

                            Hotel hotel = new Hotel();

                            hotel.IdHotel = (int)reader["idHotel"];

                            hotel.Name = (string)reader["Name"];

                            hotel.Description = (string)reader["Description"];

                            hotel.Location = (string)reader["Location"];

                            hotel.Category = (int)reader["Category"];

                            hotel.HasWifi = (Boolean)reader["HasWifi"];

                            hotel.HasParking = (Boolean)reader["HasParking"];

                            if (reader["Phone"] != null)
                                hotel.Phone = (string)reader["Phone"];

                            if (reader["Email"] != null)
                                hotel.Email = (string)reader["Email"];

                            if (reader["Website"] != null)
                                hotel.Website = (string)reader["Website"];

                            results.Add(hotel);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }


        // Requête qui récupère la liste des hôtels correspondant à la recherche avancée
        public static List<Hotel> GetAllHotelsAdvanced(string location, int category, Boolean hasWifi, Boolean hasParking)
        {
            List<Hotel> results = null;

            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string categorySQL = "";
                    string hasWifiSQL = "";
                    string hasParkingSQL = "";

                    if (category != 0)
                    {
                        categorySQL = "AND Category >= @category ";
                    }

                    if (hasWifi.Equals(true))
                    {
                        hasWifiSQL = "AND HasWifi = 'true' ";
                    }

                    if (hasParking.Equals(true))
                    {
                        hasParkingSQL = "AND HasParking = 'true'";
                    }

                    string query = "SELECT * FROM Hotel WHERE Location = @location " + categorySQL + hasWifiSQL + hasParkingSQL;
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@location", location);
                    command.Parameters.AddWithValue("@category", category);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (results == null)
                                results = new List<Hotel>();

                            Hotel hotel = new Hotel();

                            hotel.IdHotel = (int)reader["idHotel"];

                            hotel.Name = (string)reader["Name"];

                            hotel.Description = (string)reader["Description"];

                            hotel.Location = (string)reader["Location"];

                            hotel.Category = (int)reader["Category"];

                            hotel.HasWifi = (Boolean)reader["HasWifi"];

                            hotel.HasParking = (Boolean)reader["HasParking"];

                            if (reader["Phone"] != null)
                                hotel.Phone = (string)reader["Phone"];

                            if (reader["Email"] != null)
                                hotel.Email = (string)reader["Email"];

                            if (reader["Website"] != null)
                                hotel.Website = (string)reader["Website"];

                            results.Add(hotel);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }


        // Requête qui récupère un hôtel en fonction d'un identifiant en paramètre
        public static Hotel GetHotel(int idHotel)
        {
            Hotel hotel = null;

            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Hotel WHERE IdHotel = @idHotel";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idHotel", idHotel);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (hotel == null)
                                hotel = new Hotel();

                            hotel.IdHotel = (int)reader["idHotel"];

                            hotel.Name = (string)reader["Name"];

                            hotel.Description = (string)reader["Description"];

                            hotel.Location = (string)reader["Location"];

                            hotel.Category = (int)reader["Category"];

                            hotel.HasWifi = (Boolean)reader["HasWifi"];

                            hotel.HasParking = (Boolean)reader["HasParking"];

                            if (reader["Phone"] != null)
                                hotel.Phone = (string)reader["Phone"];

                            if (reader["Email"] != null)
                                hotel.Email = (string)reader["Email"];

                            if (reader["Website"] != null)
                                hotel.Website = (string)reader["Website"];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return hotel;
        }
    }
}
        
         