using AutoMapper;

namespace MyBlogsite.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Entities.Comment, Models.CommentDto>();
            CreateMap<Entities.Comment, Models.CommentForCreationDto>();
            CreateMap<Entities.Comment, Models.CommentForUpdateDto>();
            CreateMap<Models.CommentForCreationDto, Entities.Comment>();
            CreateMap<Models.CommentForUpdateDto, Entities.Comment>();
        }
    }
}
