using AutoMapper;

namespace MyBlogsite.Profiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<Entities.News, Models.NewsDto>();
            CreateMap<Entities.News, Models.NewsForCreationDto>();
            CreateMap<Entities.News, Models.NewsForUpdateDto>();
            CreateMap<Models.NewsForCreationDto, Entities.News>();
            CreateMap<Models.NewsForUpdateDto, Entities.News>();
        }
    }
}
