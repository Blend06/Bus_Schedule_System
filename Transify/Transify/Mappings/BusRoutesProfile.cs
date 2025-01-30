using AutoMapper;
using Transify.Models.Entities;
using Transify.ViewModel.BusSchedul;

namespace Transify.Mappings
{
    public class BusRoutesProfile : Profile
    {
        public BusRoutesProfile()
        {
            CreateMap<AddRouteViewModel, BusRoutes>()
                .ForMember(dest => dest.RouteId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<BusRoutes, AddRouteViewModel>();
        }
    }
}