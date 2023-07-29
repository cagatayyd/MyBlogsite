using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogsite.Entities;
using MyBlogsite.Models;
using MyBlogsite.Service;
using System.Text.Json;

namespace MyBlogsite.Controllers
{
    [Route("News")]
    [Controller]
    public class NewsController : Controller
    {
        #region Fields
        private readonly INewsManageRepository _newsManageRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public NewsController(INewsManageRepository newsManageRepository, IMapper mapper)
        {
            _newsManageRepository = newsManageRepository ?? throw new ArgumentNullException(nameof(newsManageRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            var news = await _newsManageRepository.GetNewsAsync();
            _mapper.Map<IEnumerable<NewsDto>>(news);
            return View();
        }
        [HttpGet("{newsId}")]
        public async Task<IActionResult> GetNews(int newsId)
        {
            var news = await _newsManageRepository.GetNewsAsync(newsId);
            _mapper.Map<NewsDto>(news);
            return View(news);
        }
        [HttpGet]
        public async Task<IActionResult> SearchNews(string? searchQuery, string? title)
        {
            var newsEntities = await _newsManageRepository
                .GetNewsAsync(title, searchQuery);

            var newsMap = _mapper.Map<IEnumerable<NewsDto>>(newsEntities);

            return View(newsMap);
        }
        [HttpPost]
        public async Task<IActionResult> AddNews(NewsForCreationDto news) 
        {
            var finalNews = _mapper.Map<Entities.News>(news);

            await _newsManageRepository.AddNewsAsync(finalNews);

            await _newsManageRepository.SaveChangesAsync();

            var createdNews =
                _mapper.Map<Models.NewsDto>(finalNews);

            var createdAtRoute = CreatedAtRoute("GetNews",
                 new
                 {
                     newsId = createdNews.Id
                 },
                 createdNews);
            return View(createdAtRoute);
        }
        [HttpDelete]
        public async Task DeleteNews(int newsId)
        {
            //var newsEntity = await _newsManageRepository
            //    .GetNewsAsync(newsId);

            _newsManageRepository.DeleteNewsAsync(newsId);
            await _newsManageRepository.SaveChangesAsync();
        }
        #endregion
    }
}
