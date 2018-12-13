using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Hotel
    {
        public int IdHotel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Category { get; set; }
        public Boolean HasWifi { get; set; }
        public Boolean HasParking { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }


        public override string ToString()
        {
            return "IdHotel: " + IdHotel +
                   "Name: " + Name +
                   "Description: " + Description +
                   "Location: " + Location +
                   "Category: " + Category +
                   "HasWifi: " + HasWifi +
                   "HasParking: " + HasParking +
                   "Phone: " + Phone +
                   "Email: " + Email +
                   "Website: " + Website;
        }
    }
}
