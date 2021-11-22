using AutoMapper;

namespace Comp306Project.Models
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ParkingLots, ParkingDTO>();
        }
    }
}