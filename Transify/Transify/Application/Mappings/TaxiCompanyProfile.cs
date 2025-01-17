using AutoMapper;
using Transify.Domain.Models.Entities;
using Transify.Presentation.ViewModel.TaxiRequest;

namespace Transify.Application.Mappings
{
    public class TaxiCompanyProfile : Profile
    {
        public TaxiCompanyProfile()
        {
            CreateMap<TaxiCompany, TaxiCompanyRequest>()
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo))
                .ReverseMap()
                .ForMember(dest => dest.TaxiCompanyId, opt => opt.Ignore());

            CreateMap<TaxiCompanyRequest, TaxiCompany>()
                .ForMember(dest => dest.TaxiCompanyId, opt => opt.Ignore())
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo));
        }
    }
}