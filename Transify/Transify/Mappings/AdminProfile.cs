using AutoMapper;
using Transify.Models.DTOs;
using Transify.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Transify.Mappings
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}