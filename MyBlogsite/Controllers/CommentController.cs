/*
 * using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlogsite.Entities;
using MyBlogsite.Models;
using MyBlogsite.Service;
using System.Diagnostics;

namespace MyBlogsite.Controllers
{
    [Route("News/{newsId}/Comment/{commentId}")]
    [Controller]
    public class CommentController : Controller
    {
        private readonly INewsManageRepository _newsManageRepository;
        private readonly IMapper _mapper;

        public CommentController(INewsManageRepository newsManageRepository, IMapper mapper)
        {
            _newsManageRepository = newsManageRepository ?? throw new ArgumentNullException(nameof(newsManageRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet("{newsId}")]
        public async Task<IActionResult> GetCommentsForNewsAsync(int newsId)
        {
            var commentsForNews = await _newsManageRepository
                .GetCommentsForNewsAsync(newsId);
            var commentMap = _mapper.Map<IEnumerable<CommentDto>>(commentsForNews);

            return View(commentMap);
        }
        [HttpGet]
        public async Task<IActionResult> AddCommentForNews(int newsId, Comment comment)
        {
            await _newsManageRepository.AddCommentForNewsAsync(newsId, comment);

            var comments = await _newsManageRepository.GetCommentsForNewsAsync(newsId);

            var commentMap = _mapper.Map<IEnumerable<CommentForCreationDto>>(comments);

            return View(commentMap);
        }

     }

}*/