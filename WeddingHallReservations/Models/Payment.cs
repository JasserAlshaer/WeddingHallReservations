using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WeddingHallReservations.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public string Visa { get; set; }
        public string Cvv2 { get; set; }
        public DateTime? ExpireDate { get; set; }
        public double? Balance { get; set; }
    }
}
