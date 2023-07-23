using Microsoft.EntityFrameworkCore;
using MyBlogsite.DbContexts;
using MyBlogsite.Entities;

namespace MyBlogsite.Service
{
    public class NewsManageRepository : INewsManageRepository
    { 
        private readonly BlogContext _context;
        
        public NewsManageRepository(BlogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
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
        public Task AddNewsAsync( //haber ekle 
            News news)
        {
            if (news != null)
            {
                _context.News.Add(news);
            }

            return Task.CompletedTask;
        }

        public async Task DeleteNewsAsync(int newsId)
        {
            var news = await _context.News.Include(n => n.Comments).FirstOrDefaultAsync(n => n.Id == newsId);

            if (news != null)
            {
                // Habere ait yorumları silin.
                _context.Comments.RemoveRange(news.Comments);

                _context.News.Remove(news);
                await _context.SaveChangesAsync();
            }
        }

        //Yorum
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
        public void DeleteCommentsForNewsAsync(Comment comment) //yorum sil 
        {
            _context.Comments.Remove(comment);
        }
        //user
        public async Task<IEnumerable<User>> GetUserProfileAsync( // profillere tıklama girme
           int userId)
        {
            return await _context.Users
                           .Where(p => p.Id == userId).ToListAsync();
        }
        
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
        
    }
}
