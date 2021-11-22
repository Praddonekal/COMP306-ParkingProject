using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comp306Project.Models
{
    public interface IParkingRepository
    {
        ParkingLots GetParkingLots(string id);
        IEnumerable<ParkingLots> GetParkingLots();
        ParkingLots Add(ParkingLots parkingLots);
        ParkingLots Edit(string id, ParkingLots parkingLots);
        ParkingLots Delete(string id);
    }
}