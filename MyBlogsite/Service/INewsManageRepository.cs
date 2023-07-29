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
        Task<int> GetNumberOfCommentsForNewsAsync(int newsId);
        Task<IEnumerable<Comment>> GetCommentsForNewsAsync(int newsId);
        Task<Comment> GetSpecificCommentForNews(int newsId, int commentId);
        Task<IEnumerable<User>> GetUserProfileAsync(int userId);
        Task<User> CreateUserAsync(User user);
        public void DeleteCommentsForNewsAsync(Comment comment);
        Task<User> Login(string userName, string password);
        Task<bool> SaveChangesAsync();
    }
}
