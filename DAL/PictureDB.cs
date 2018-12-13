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
    public static class PictureDB
    {
        // Requête qui récupère une liste de toutes les images correspondant à un hôtel (identifiant en paramètre)
        public static List<Picture> GetAllPictures(int idHotel)
        {
            List<Picture> results = null;

            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Picture P, Room R, Hotel H WHERE P.IdRoom = R.IdRoom AND R.IdHotel = @idHotel";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idHotel", idHotel);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (results == null)
                                results = new List<Picture>();

                            Picture picture = new Picture();

                            picture.IdPicture = (int)reader["IdPicture"];

                            picture.Url = (string)reader["Url"];

                            picture.IdRoom = (int)reader["IdRoom"];

                            results.Add(picture);
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


        // Requête qui récupère une liste de toutes les images correspondant à une chambre (identifiant en paramètre)
        public static List<Picture> GetRoomPictures(int idRoom)
        {
            List<Picture> results = null;

            string connectionString = ConfigurationManager.ConnectionStrings["ValaisBookingDB"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Picture WHERE IdRoom = @idRoom";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idRoom", idRoom);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (results == null)
                                results = new List<Picture>();

                            Picture picture = new Picture();

                            picture.IdPicture = (int)reader["IdPicture"];

                            picture.Url = (string)reader["Url"];

                            picture.IdRoom = (int)reader["IdRoom"];

                            results.Add(picture);
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
