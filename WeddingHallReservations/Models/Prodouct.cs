using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WeddingHallReservations.Models
{
    public partial class Prodouct
    {
        public Prodouct()
        {
            Media = new HashSet<Media>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ServiceId { get; set; }
        public int? Quantity { get; set; }
        public double? PricePerOne { get; set; }

        public virtual Service Service { get; set; }
        public virtual ICollection<Media> Media { get; set; }
    }
}
