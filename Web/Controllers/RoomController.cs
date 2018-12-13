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
    public class RoomController : Controller
    {
        // Page d'affichage des résultats de la recherche
        [HttpPost]
        public ActionResult Research()
        {
            // En premier lieu, je récupère les informations envoyées par le formulaire (sauf le nombre de personnes)
            string location = Request["location"].ToString();
            DateTime dateStart = Convert.ToDateTime(Request["checkIn"]);
            DateTime dateEnd = Convert.ToDateTime(Request["checkOut"]);            
            int category = Convert.ToInt16(Request["category"]);
        
            // Pour les checkboxes, j'effectue un test de ce qu'elles retournent pour leur affecter la bonne valeur
            Boolean hasWifi, hasParking, hasTV, hasHairDryer;

            if (Convert.ToString(Request["wifi"]) == "wifi")
                hasWifi = true;
            else
                hasWifi = false;

            if (Convert.ToString(Request["parking"]) == "parking")
                hasParking = true;
            else
                hasParking = false;

            if (Convert.ToString(Request["tv"]) == "tv")
                hasTV = true;
            else
                hasTV = false;

            if (Convert.ToString(Request["hairDryer"]) == "hairDryer")
                hasHairDryer = true;
            else
                hasHairDryer = false;


            // D'abord, je récupère la liste des hôtels correspondant à la recherche (test pour savoir si elle est avancée)
            List<Hotel> hotels = new List<Hotel>();

            if (category.Equals(0) && hasWifi.Equals(false) && hasParking.Equals(false))
                hotels = HotelManager.GetAllHotelsSimple(location);
            else
                hotels = HotelManager.GetAllHotelsAdvanced(location, category, hasWifi, hasParking);


            // Je récupère, pour chaque hôtel, la liste de toutes les photos qui lui correspondent
            // Puis, pour chaque hôtel, je vais rechercher la liste de toutes ses chambres
            // Je vais également rechercher la liste des chambres correspondant à la recherche (test pour savoir si elle est avancée)
            // Tout cela est effectué sur la base du ViewModel "HotelsWithRoomsVM"
            List<HotelsWithRoomsVM> list = new List<HotelsWithRoomsVM>();

            if (hotels != null)
            {
                foreach (var item in hotels)
                {
                    // Ici, je récupère toutes les photos liées à un hôtel
                    // La fonction DISTINCT est utilisée puisque beaucoup d'URL dans la BDD sont identiques
                    IEnumerable<Picture> pics = PictureManager.GetAllPictures(item.IdHotel);
                    List<string> urls = pics.Select(pic => pic.Url).Distinct().ToList();


                    // Je récupère toutes les chambres qui sont liées à l'hôtel
                    List<Room> rooms = RoomManager.GetAllRooms(item.IdHotel);
                    decimal totalPrice = 0;
                    int numberRooms = rooms.Count();
     
                    foreach (var element in rooms)
                    {
                        totalPrice += element.Price;
                    }

                    // Je calcule un prix moyen pour l'hôtel, en fonction de toutes les chambres
                    int avgPrice = (int)(Math.Round((totalPrice / numberRooms), 0));
                   

                    // Je récupère une liste de chambres occupées et une liste de chambres disponibles
                    List<Room> occupiedRooms = RoomManager.GetOccupiedRooms(item.IdHotel, dateStart, dateEnd);
                    List<Room> availableRooms = new List<Room>();

                    // Les chambres disponibles sont recherchées en fonction de la recherche simple ou avancée
                    if (hasTV.Equals(false) && hasHairDryer.Equals(false))
                        availableRooms = RoomManager.GetSearchedRoomsSimple(item.IdHotel, dateStart, dateEnd);
                    else
                        availableRooms = RoomManager.GetSearchedRoomsAdvanced(item.IdHotel, dateStart, dateEnd, hasTV, hasHairDryer);


                    // Pour chaque chambre disponible, je vais rechercher ses photos
                    List<AvailableRoomVM> roomsList = new List<AvailableRoomVM>();

                    if (availableRooms != null)
                    {
                        foreach (var room in availableRooms)
                        {
                            IEnumerable<Picture> roomPics = PictureManager.GetRoomPictures(room.IdRoom);
                            List<string> roomUrls = roomPics.Select(pic => pic.Url).Distinct().ToList();

                            AvailableRoomVM availableRoom = new AvailableRoomVM
                            {
                                IdRoom = room.IdRoom,
                                Number = room.Number,
                                Description = room.Description,
                                Type = room.Type,
                                Price = room.Price,
                                HasTV = room.HasTV,
                                HasHairDryer = room.HasHairDryer,
                                PicturesUrls = roomUrls
                            };

                            roomsList.Add(availableRoom);
                        }
                    }


                    // Je calcule ensuite le taux d'occupation de l'hôtel selon la période sélectionnée par l'utilisateur
                    var calcul = 0.0;
                    if (occupiedRooms != null)
                        calcul = (double)((occupiedRooms.Count() * 100) / rooms.Count());                       
                    int rate = (int)(Math.Round(calcul, 0));


                    // Je crée ensuite, selon le ViewModel HotelsWithRoomsVM, chaque hôtel et je l'ajoute à la liste
                    HotelsWithRoomsVM hotelWithRooms = new HotelsWithRoomsVM
                    {
                        IdHotel = item.IdHotel,
                        Name = item.Name,
                        Description = item.Description,
                        Location = item.Location,
                        Category = item.Category,
                        HasWifi = item.HasWifi,
                        HasParking = item.HasParking,
                        Phone = item.Phone,
                        Email = item.Email,
                        Website = item.Website,
                        PicturesUrls = urls,
                        Rooms = rooms,
                        AvailableRooms = roomsList,
                        OccupiedRooms = occupiedRooms,
                        Occupation = rate,
                        AveragePrice = avgPrice
                    };

                    list.Add(hotelWithRooms);
                }
            }


            // Le dernier paramètre à tester est le nombre de personnes sélectionné
            // Si la capacité totale des chambres disponibles est inférieure au choix, la liste ne sera pas affichée
            int total = 0;

            foreach (var element in list)
            {
                if (element.AvailableRooms != null)
                {
                    foreach (var item in element.AvailableRooms)
                    {
                        total = total + item.Type;
                    }
                }
            }
            
            string personsString = Convert.ToString(Request["numberPersons"]);
            List<HotelsWithRoomsVM> emptyList = new List<HotelsWithRoomsVM>();

            if (personsString.Equals("10+"))
            {
                if (total < 11)
                    list = emptyList;
            }
            else
            {
                int persons = Convert.ToInt16(Request["numberPersons"]);
                if (total < persons)
                    list = emptyList;
            }


            // Passage des informations de dates dans des variables "session" pour pouvoir les passer à la confirmation de réservation
            Session["dateStart"] = dateStart;
            Session["dateEnd"] = dateEnd;
            Session["duration"] = (dateEnd - dateStart).Days;


            // Je retourne la liste, désormais complète, à la vue
            return View(list);
        }
    }
}
