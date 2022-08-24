using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WeddingHallReservations.Models
{
    public partial class Reservaition
    {
        public int ResrvitionId { get; set; }
        public DateTime? ReservaitionDateFrom { get; set; }
        public DateTime? ReservaitionDateTo { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public double? TotalPrice { get; set; }
        public string Notes { get; set; }
        public int? ServiceId { get; set; }

        public virtual Service Service { get; set; }
    }
}
