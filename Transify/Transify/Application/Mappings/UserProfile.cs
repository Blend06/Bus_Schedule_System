using AutoMapper;
using Transify.Domain.Models.Entities;
using Transify.Presentation.ViewModel.Authenticate;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Transify.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Map User entity to RegisterViewModel and EditUserViewModel
            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<User, EditUserViewModel>().ReverseMap();
        }
    }
}