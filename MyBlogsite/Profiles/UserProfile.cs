using AutoMapper;

namespace MyBlogsite.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<Entities.User, Models.UserDto>();
            CreateMap<Entities.User, Models.UserForCreationDto>();
            CreateMap<Entities.User, Models.UserForUpdateDto>();
            CreateMap<Models.UserForCreationDto, Entities.User>();
            CreateMap<Models.UserForUpdateDto, Entities.User>();
        }
    }
}
