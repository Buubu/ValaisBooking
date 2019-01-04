using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Reservation
    {
        public int IdReservation { get; set; }
        public DateTime DateReservation { get; set; }
        public string ClientFirstname { get; set; }
        public string ClientLastname { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal TotalPrice { get; set; }


        public override string ToString()
        {
            return "IdReservation: " + IdReservation +
                   "DateReservation: " + DateReservation +
                   "ClientFirstname: " + ClientFirstname +
                   "ClientLastname: " + ClientLastname +
                   "DateStart: " + DateStart +
                   "DateEnd: " + DateEnd +
                   "TotalPrice: " + TotalPrice;
        }
    }
}
