using AutoMapper;

namespace ExploreCalifornia.BookingService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Contracts.Booking, Domain.Booking>();
        }
    }
}