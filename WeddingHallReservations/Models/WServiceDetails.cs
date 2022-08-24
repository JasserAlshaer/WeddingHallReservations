using System.Collections.Generic;

namespace WeddingHallReservations.Models
{
    public class WServiceDetails
    {
        public Catregory Catregory { get; set; }
        public Service Service { get; set; }
        public List<ProductInfo> ProdouctInfo { get; set; }

    }
}
