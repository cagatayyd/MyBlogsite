
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlogsite.Entities;
using MyBlogsite.Models;
using MyBlogsite.Service;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace MyBlogsite.Controllers
{
    [Controller]
    public class CommentController : Controller
    {
        #region Fields
        private readonly INewsManageRepository _newsManageRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public CommentController(INewsManageRepository newsManageRepository, IMapper mapper)
        {
            _newsManageRepository = newsManageRepository ?? throw new ArgumentNullException(nameof(newsManageRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion

        #region Methods
        [HttpGet("{newsId}/Comment")]
        public async Task<IActionResult> GetCommentsForNewsAsync(int newsId)
        {
            var commentsForNews = await _newsManageRepository.GetCommentsForNewsAsync(newsId);
            var commentMap = _mapper.Map<IEnumerable<CommentDto>>(commentsForNews);
            return View(commentMap);
        }

        [HttpPost("{newsId}/Comment")]
        public async Task<IActionResult> CreateComment(int newsId, CommentForCreationDto comment)
        {
            var finalComment = _mapper.Map<Entities.Comment>(comment);
            await _newsManageRepository.AddCommentForNewsAsync(newsId, finalComment);
            await _newsManageRepository.SaveChangesAsync();
            var createdComment = _mapper.Map<Models.CommentDto>(finalComment);
            var createdAtRoute = CreatedAtRoute("GetPointOfInterest", new { newsId = newsId, commentId = createdComment.Id }, createdComment);
            return View("CommentDetail", createdAtRoute);
        }

        [HttpDelete("{newsId}/Comment/{commentId}")]
        public async Task<IActionResult> DeleteCommentForNews(int newsId, int commentId)
        {
            var commentEntity = await _newsManageRepository.GetSpecificCommentForNews(commentId, newsId);
            if (commentEntity == null)
            {
                return NotFound();
            }

            _newsManageRepository.DeleteCommentsForNewsAsync(commentEntity);
            await _newsManageRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{newsId}")]
        public async Task<IActionResult> GetNumberOfCommentsForNewsAsync(int newsId)
        {
            int numberOfComments = await _newsManageRepository.GetNumberOfCommentsForNewsAsync(newsId);
            ViewBag.NumberOfComments = numberOfComments;
            return NoContent();
        }

        #endregion

    }

}