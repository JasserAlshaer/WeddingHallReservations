using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WeddingHallReservations.Models
{
    public partial class Media
    {
        public int MediaId { get; set; }
        public string ImagePath { get; set; }
        public bool? IsMain { get; set; }
        public int? ProductId { get; set; }

        public virtual Prodouct Product { get; set; }
    }
}
