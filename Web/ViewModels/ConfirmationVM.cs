using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;

namespace Web.ViewModels
{
    public class ConfirmationVM
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int Duration { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Room> Rooms { get; set; }
        public decimal TotalPrice { get; set; }
    }
}