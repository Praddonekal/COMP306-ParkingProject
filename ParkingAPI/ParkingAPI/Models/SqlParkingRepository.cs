using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comp306Project.Models
{
    public class SqlParkingRepository : IParkingRepository
    {
        private readonly ParkAPIContext context;

        public SqlParkingRepository(ParkAPIContext context)
        {
            this.context = context;
        }

        public ParkingLots Add(ParkingLots parkingLots)
        {
            context.ParkingLots.Add(parkingLots);
            context.SaveChanges();
            return parkingLots;
        }

        public ParkingLots Delete(string id)
        {
            ParkingLots parkingLots = context.ParkingLots.Find(id);
            context.ParkingLots.Remove(parkingLots);
            context.SaveChangesAsync();
            return parkingLots;
        }

        public IEnumerable<ParkingLots> GetParkingLots()
        {
            return context.ParkingLots.ToList();
        }

        public ParkingLots GetParkingLots(string id)
        {
            return context.ParkingLots.Find(id);
        }

        public ParkingLots Edit(string id, ParkingLots parkingLotsChanges)
        {
            var movie = context.ParkingLots.Attach(parkingLotsChanges);
            movie.State = EntityState.Modified;
            context.SaveChanges();
            return parkingLotsChanges;
        }
    }
}
