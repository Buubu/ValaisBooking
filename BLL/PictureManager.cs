using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class PictureManager
    {
        // Appelle la requête qui récupère une liste de toutes les images correspondant à un hôtel (identifiant en paramètre)
        public static List<Picture> GetAllPictures(int idHotel)
        {
            return PictureDB.GetAllPictures(idHotel);
        }


        // Appelle la requête qui récupère une liste de toutes les images correspondant à une chambre (identifiant en paramètre)
        public static List<Picture> GetRoomPictures(int idRoom)
        {
            return PictureDB.GetRoomPictures(idRoom);
        }
    }
}
