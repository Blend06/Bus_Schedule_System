using AutoMapper;
using Transify.Models.Entities;
using Transify.ViewModel.Authenticate;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Transify.Mappings
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