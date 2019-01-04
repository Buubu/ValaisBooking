using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ReservationDetails
    {
        public int IdReservationDetails { get; set; }
        public int IdReservation { get; set; }
        public int IdRoom { get; set; }
        public decimal RoomPrice { get; set; }
        public decimal Increase { get; set; }


        public override string ToString()
        {
            return "IdReservationDetails: " + IdReservationDetails +
                   "IdReservation: " + IdReservation +
                   "IdRoom: " + IdRoom +
                   "RoomPrice: " + RoomPrice +
                   "Increase: " + Increase;
        }
    }
}
