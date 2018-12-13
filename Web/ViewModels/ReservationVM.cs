using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class ReservationVM
    {
        public int IdReservation { get; set; }
        public string Gender { get; set; }
        public string ClientFirstname { get; set; }
        public string ClientLastname { get; set; }
    }
}