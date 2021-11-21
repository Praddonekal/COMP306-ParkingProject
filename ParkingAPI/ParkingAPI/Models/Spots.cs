using System;
using System.Collections.Generic;

namespace Comp306Project.Models
{
    public partial class Spots
    {
        public int SpotId { get; set; }
        public string LotId { get; set; }
        public string SpotAvailable { get; set; }
        public string SpotReserved { get; set; }

        public ParkingLots Lot { get; set; }
    }
}
