using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingClient
{
    class Parking
    {
        public Parking()
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

        public override string ToString()
        {
            return $"Lot ID: {LotId}\nLot Name: {LotName}\nStreet Number {LotStreetNumber}\nStreet Name: {LotStreetName}\nCity: {LotCity}\nHourly Rate: {LotHourlyRate}\nDaily Rate:{LotDailyRate}\nWeekly Rate: {LotWeeklyRate}\nMonthly Rate: {LotMonthlyRate}\n";
        }
    }


}
