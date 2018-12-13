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
    public static class RoomDB
    {
        // Requête qui récupère la liste de toutes les chambres d'un hôtel (en fonction de son identifiant en paramètre)
        public static List<Room> GetAllRooms(int idHotel)
        {
            List<Room> results = null;

            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Room WHERE IdHotel = @idHotel";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idHotel", idHotel);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (results == null)
                                results = new List<Room>();

                            Room room = new Room();

                            room.IdRoom = (int)reader["IdRoom"];

                            room.Number = (int)reader["Number"];

                            room.Description = (string)reader["Description"];

                            room.Type = (int)reader["Type"];

                            room.Price = (decimal)reader["Price"];

                            room.HasTV = (Boolean)reader["HasTV"];

                            room.HasHairDryer = (Boolean)reader["HasHairDryer"];

                            room.IdHotel = (int)reader["IdHotel"];

                            results.Add(room);
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


        // Requête qui récupère la liste de toutes les chambres qui correspondent à la recherche simple
        public static List<Room> GetSearchedRoomsSimple(int idHotel, DateTime dateStart, DateTime dateEnd)
        {
            List<Room> results = null;

            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * " +
                                        "FROM Room " +
                                        "WHERE IdHotel = @idHotel " +
                                        "AND IdRoom NOT IN (SELECT R.IdRoom " +
                                                                "FROM Room R, ReservationDetails RD, Reservation RE " +
                                                                "WHERE R.IdRoom = RD.IdRoom " +
                                                                "AND RD.IdReservation = RE.IdReservation " +
                                                                "AND ((@dateStart > RE.DateStart AND @dateStart < RE.DateEnd) " +
                                                                    "OR (@dateEnd > RE.DateStart AND @dateEnd < RE.DateEnd) " +
                                                                    "OR (@dateStart <= RE.DateStart AND @dateEnd >= RE.DateEnd)))";
                
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idHotel", idHotel);
                    command.Parameters.AddWithValue("@dateStart", dateStart);
                    command.Parameters.AddWithValue("@dateEnd", dateEnd);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (results == null)
                                results = new List<Room>();

                            Room room = new Room();

                            room.IdRoom = (int)reader["IdRoom"];

                            room.Number = (int)reader["Number"];

                            room.Description = (string)reader["Description"];

                            room.Type = (int)reader["Type"];

                            room.Price = (decimal)reader["Price"];

                            room.HasTV = (Boolean)reader["HasTV"];

                            room.HasHairDryer = (Boolean)reader["HasHairDryer"];

                            room.IdHotel = (int)reader["IdHotel"];

                            results.Add(room);
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


        // Requête qui récupère la liste de toutes les chambres qui correspondent à la recherche avancée
        public static List<Room> GetSearchedRoomsAdvanced(int idHotel, DateTime dateStart, DateTime dateEnd, Boolean hasTV, Boolean hasHairDryer)
        {
            List<Room> results = null;

            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string hasTVSQL = "";
                    string hasHairDryerSQL = "";

                    if (hasTV.Equals(true))
                    {
                        hasTVSQL = "AND HasTV = 'true' ";
                    }

                    if (hasHairDryer.Equals(true))
                    {
                        hasHairDryerSQL = "AND HasHairDryer = 'true'";
                    }

                    string query = "SELECT * " +
                                        "FROM Room " +
                                        "WHERE IdHotel = @idHotel " +
                                        "AND IdRoom NOT IN (SELECT R.IdRoom " +
                                                                "FROM Room R, ReservationDetails RD, Reservation RE " +
                                                                "WHERE R.IdRoom = RD.IdRoom " +
                                                                "AND RD.IdReservation = RE.IdReservation " +
                                                                "AND ((@dateStart > RE.DateStart AND @dateStart < RE.DateEnd) " +
                                                                    "OR (@dateEnd > RE.DateStart AND @dateEnd < RE.DateEnd) " +
                                                                    "OR (@dateStart <= RE.DateStart AND @dateEnd >= RE.DateEnd))) " + hasTVSQL + hasHairDryerSQL;

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idHotel", idHotel);
                    command.Parameters.AddWithValue("@dateStart", dateStart);
                    command.Parameters.AddWithValue("@dateEnd", dateEnd);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (results == null)
                                results = new List<Room>();

                            Room room = new Room();

                            room.IdRoom = (int)reader["IdRoom"];

                            room.Number = (int)reader["Number"];

                            room.Description = (string)reader["Description"];

                            room.Type = (int)reader["Type"];

                            room.Price = (decimal)reader["Price"];

                            room.HasTV = (Boolean)reader["HasTV"];

                            room.HasHairDryer = (Boolean)reader["HasHairDryer"];

                            room.IdHotel = (int)reader["IdHotel"];

                            results.Add(room);
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


        // Requête qui récupère une chambre en fonction de son identifiant (en paramètre)
        public static Room GetRoom(int idRoom)
        {
            Room room = null;

            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Room WHERE IdRoom = @idRoom";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("idRoom", idRoom);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (room == null)
                                room = new Room();

                            room.IdRoom = (int)reader["IdRoom"];

                            room.Number = (int)reader["Number"];

                            room.Description = (string)reader["Description"];

                            room.Type = (int)reader["Type"];

                            room.Price = (decimal)reader["Price"];

                            room.HasTV = (Boolean)reader["HasTV"];

                            room.HasHairDryer = (Boolean)reader["HasHairDryer"];

                            room.IdHotel = (int)reader["IdHotel"];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return room;
        }


        // Requête qui récupère la liste de toutes les chambres qui sont occupées à une certaine période
        public static List<Room> GetOccupiedRooms(int idHotel, DateTime dateStart, DateTime dateEnd)
        {
            List<Room> results = null;

            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * " +
                                        "FROM Room " +
                                        "WHERE IdHotel = @idHotel " +
                                        "AND IdRoom IN (SELECT R.IdRoom " +
                                                                "FROM Room R, ReservationDetails RD, Reservation RE " +
                                                                "WHERE R.IdRoom = RD.IdRoom " +
                                                                "AND RD.IdReservation = RE.IdReservation " +
                                                                "AND ((@dateStart > RE.DateStart AND @dateStart < RE.DateEnd) " +
                                                                    "OR (@dateEnd > RE.DateStart AND @dateEnd < RE.DateEnd) " +
                                                                    "OR (@dateStart <= RE.DateStart AND @dateEnd >= RE.DateEnd)))";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idHotel", idHotel);
                    command.Parameters.AddWithValue("@dateStart", dateStart);
                    command.Parameters.AddWithValue("@dateEnd", dateEnd);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (results == null)
                                results = new List<Room>();

                            Room room = new Room();

                            room.IdRoom = (int)reader["IdRoom"];

                            room.Number = (int)reader["Number"];

                            room.Description = (string)reader["Description"];

                            room.Type = (int)reader["Type"];

                            room.Price = (decimal)reader["Price"];

                            room.HasTV = (Boolean)reader["HasTV"];

                            room.HasHairDryer = (Boolean)reader["HasHairDryer"];

                            room.IdHotel = (int)reader["IdHotel"];

                            results.Add(room);
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
    }
}
