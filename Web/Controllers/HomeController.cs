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
    public class HomeController : Controller
    {
        // Page d'accueil de l'application
        public ActionResult Index()
        {
            // Le controller récupère la liste de tous les hôtels proposés par Valais Booking
            var hotels = HotelManager.GetAllHotels();
            List<ListHotelsVM> list = new List<ListHotelsVM>();

            // Pour chaque hôtel, le controller récupère toutes les photos qui correspondent aux chambres de l'hôtel
            foreach (var item in hotels)
            {
                // Une fonction "DISTINCT" est utilisée pour récupérer seulement les photos différentes
                // Elle est utilisée uniquement car la BDD contient souvent les mêmes URL
                List<string> urls = PictureManager.GetAllPictures(item.IdHotel);

                ListHotelsVM hotel = new ListHotelsVM
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
                    PicturesUrls = urls
                };

                list.Add(hotel);
            }

            return View(list);
        }


        // Page d'annulation d'une réservation
        public ActionResult Cancellation()
        {
            return View();
        }
    }
}
