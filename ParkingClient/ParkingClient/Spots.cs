using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingClient
{
    class Spots
    {
        public int SpotId { get; set; }
        public string LotId { get; set; }
        public string SpotAvailable { get; set; }
        public string SpotReserved { get; set; }

        public Parking Lot { get; set; }

        public override string ToString()
        {
            return $"\t{SpotId}\n\t{LotId}\n\t{SpotAvailable}\n\t{SpotReserved}\n";
        }
    }
}
