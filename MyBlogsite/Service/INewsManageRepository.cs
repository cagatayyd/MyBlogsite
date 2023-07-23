using MyBlogsite.Entities;

namespace MyBlogsite.Service
{
    public interface INewsManageRepository
    {
        Task<IEnumerable<News>> GetNewsAsync();
        Task<News> GetNewsAsync(int newsId);
        Task<IEnumerable<News>> GetNewsAsync(string? searchQuery, string? title);
        Task AddNewsAsync(News news);
        Task DeleteNewsAsync(int newsId);
        Task AddCommentForNewsAsync(int newsId, Comment comment); // ? 
        // Task<IEnumerable<Comment>> GetNumberOfCommentsForNewsAsync(int newsId);
        Task<IEnumerable<Comment>> GetCommentsForNewsAsync(int newsId);
        Task<IEnumerable<User>> GetUserProfileAsync(int userId);
        public void DeleteCommentsForNewsAsync(Comment comment);
        Task<bool> SaveChangesAsync();
    }
}
