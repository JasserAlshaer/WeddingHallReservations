using System.Collections.Generic;

namespace WeddingHallReservations.Models
{
    public class ReserviationHistory
    {
        public Service Service { get; set; }
        public User User { get; set; }  
        public Reservaition Reservaition { get; set; }
    }
}
