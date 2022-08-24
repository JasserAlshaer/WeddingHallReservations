using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WeddingHallReservations.Models
{
    public partial class Service
    {
        public Service()
        {
            Prodouct = new HashSet<Prodouct>();
            Reservaition = new HashSet<Reservaition>();
        }

        public int ServiceId { get; set; }
        public int? CategoryId { get; set; }
        public string ServiceProvided { get; set; }
        public string ProfileImage { get; set; }
        public double? StartingPrice { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Prodouct> Prodouct { get; set; }
        public virtual ICollection<Reservaition> Reservaition { get; set; }
    }
}
