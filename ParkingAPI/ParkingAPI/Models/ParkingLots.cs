using System;
using System.Collections.Generic;

namespace Comp306Project.Models
{
    public partial class ParkingLots
    {
        public ParkingLots()
        {
            Spots = new HashSet<Spots>();
        }

        public string LotId { get; set; }
        public string LotName { get; set; }
        public int LotStreetNumber { get; set; }
        public string LotStreetName { get; set; }
        public string LotCity { get; set; }
        public decimal LotHourlyRate { get; set; }
        public decimal LotDailyRate { get; set; }
        public decimal LotWeeklyRate { get; set; }
        public decimal LotMonthlyRate { get; set; }
        public decimal LotYearlyRate { get; set; }

        public ICollection<Spots> Spots { get; set; }
    }
}
