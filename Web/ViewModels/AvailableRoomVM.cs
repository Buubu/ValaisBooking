using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class AvailableRoomVM
    {
        public int IdRoom { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public decimal Price { get; set; }
        public Boolean HasTV { get; set; }
        public Boolean HasHairDryer { get; set; }
        public List<string> PicturesUrls { get; set; }
    }
}
