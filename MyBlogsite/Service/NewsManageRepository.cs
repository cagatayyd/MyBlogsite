using Microsoft.EntityFrameworkCore;
using MyBlogsite.DbContexts;
using MyBlogsite.Entities;

namespace MyBlogsite.Service
{
    public class NewsManageRepository : INewsManageRepository
    {
        #region Props
        private readonly BlogContext _context;
        #endregion
        #region Ctor
        public NewsManageRepository(BlogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region News
        public async Task<IEnumerable<News>> GetNewsAsync() //tüm haber
        {
            return await _context.News.OrderBy(c => c.Title).ToListAsync();
        }
        public async Task<News?> GetNewsAsync(int newsId) //idye göre haber
        {
            return await _context.News.Where(c => c.Id == newsId).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<News>> GetNewsAsync( //haber ara
            string? searchQuery, string? title)
        {
            var collection = _context.News as IQueryable<News>;

            if (!string.IsNullOrWhiteSpace(title))
            {
                title = title.Trim();
                collection = collection.Where(c => c.Title == title);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Title.Contains(searchQuery)
                    || (a.Content != null && a.Content.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var collectionToReturn = await collection.OrderBy(c => c.Title)
                .ToListAsync();

            return (collectionToReturn);
        }
        public async Task AddNewsAsync(News news)
        {
            if (news != null)
            {
                _context.News.Add(news);
                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteNewsAsync(int newsId)
        {
            var news = await _context.News.Include(n => n.Comments).FirstOrDefaultAsync(n => n.Id == newsId);

            if (news != null)
            {
                _context.Comments.RemoveRange(news.Comments);

                _context.News.Remove(news);

                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Comment
        public async Task AddCommentForNewsAsync(int newsId, Comment comment) //yorum ekle 
        {
            var news = await GetNewsAsync(newsId);
            if (news != null)
            {
                news.Comments.Add(comment);
            }
        }
        public async Task<IEnumerable<Comment>> GetCommentsForNewsAsync( // yorumları getir
           int newsId)
        {
            return await _context.Comments
                           .Where(p => p.Id == newsId).ToListAsync();
        }
        public async Task<Comment?> GetSpecificCommentForNews(int newsId, int commentId)
        {
            return await _context.Comments
               .Where(p => p.NewsId == newsId && p.Id == commentId)
               .FirstOrDefaultAsync();
        }
        public void DeleteCommentsForNewsAsync(Comment comment) //yorum sil 
        {
            _context.Comments.Remove(comment);
        }
        public async Task<int> GetNumberOfCommentsForNewsAsync(int newsId)
        {
            var commentsForNews = await _context.Comments
                .Where(c => c.NewsId == newsId)
                .ToListAsync();

            return commentsForNews.Count;
        }
        #endregion

        #region User
        public async Task<IEnumerable<User>> GetUserProfileAsync( // profillere tıklama girme
           int userId)
        {
            return await _context.Users
                           .Where(p => p.Id == userId).ToListAsync();
        }
        public async Task<User> CreateUserAsync(User user)
        {
            if (user != null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }

            return null;
        }
        public async Task<User> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userName && u.Password == password);

            return user;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
        #endregion


    }
}
