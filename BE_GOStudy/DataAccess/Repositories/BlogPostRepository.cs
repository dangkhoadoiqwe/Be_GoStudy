using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
        Task<BlogPost?> GetBlogPostByIdAsync(int postId);
        Task AddBlogPostAsync(BlogPost blogPost);
        Task UpdateBlogPostAsync(BlogPost blogPost);
        Task DeleteBlogPostAsync(BlogPost blogPost);
        Task<List<BlogPost>> GetTrendingBlogPosts();
        Task<List<BlogPost>> GetUserBlogPosts(int userId);
        Task<List<BlogPost>> GetFavoriteBlogPosts(int userId);
        Task AddBlogImgAsync(BlogImg blogImg);
        Task AddCommentAsync(Comment comment);
        Task<IEnumerable<BlogPost>> GetPaginatedBlogPostsAsync(int pageNumber, int pageSize);
        Task<bool> HasUserLikedPostAsync(int userId, int blogId);

        Task AddUserLikeAsync(UserLike userLike);

        Task<int> CountUserCommentsTodayAsync(int userId, int blogPostId);
    }

    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly GOStudyContext _context;

        public BlogPostRepository(GOStudyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            return await _context.BlogPosts.Include(u => u.User).ToListAsync();
        }
        public async Task<IEnumerable<BlogPost>> GetPaginatedBlogPostsAsync(int pageNumber, int pageSize)
        {
            return await _context.BlogPosts
                .OrderByDescending(bp => bp.CreatedAt)  // Optional: order by creation date
                .Skip((pageNumber - 1) * pageSize)      // Skip posts for previous pages
                .Take(pageSize)                         // Take the posts for the current page
                .ToListAsync();
        }
        public async Task AddUserLikeAsync(UserLike userLike)
        {
            _context.UserLikes.Add(userLike);
            await _context.SaveChangesAsync();
        }
        public async Task<BlogPost?> GetBlogPostByIdAsync(int postId)
        {
            return await _context.BlogPosts.FindAsync(postId);
        }
        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .Include(c => c.User) // Include user information if needed
                .ToListAsync();
        }
        public async Task AddBlogImgAsync(BlogImg blogImg)
        {
            _context.BlogImgs.Add(blogImg);
            await _context.SaveChangesAsync();
        }
        public async Task AddBlogPostAsync(BlogPost blogPost)
        {
       

            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> HasUserLikedPostAsync(int userId, int blogId)
        {
            // Check if a like by the user for this blog post already exists
            return await _context.UserLikes
                .AnyAsync(ul => ul.UserId == userId && ul.BlogId == blogId);
        }
        public async Task UpdateBlogPostAsync(BlogPost blogPost)
        {
            _context.BlogPosts.Update(blogPost);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBlogPostAsync(BlogPost blogPost)
        {
            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BlogPost>> GetTrendingBlogPosts()
        {
            return await _context.BlogPosts
                .Where(p => p.IsTrending && p.CreatedAt >= DateTime.Now.AddDays(-7))
                .OrderByDescending(p => p.ViewCount + p.likeCount + p.Comments.Count)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetUserBlogPosts(int userId)
        {
            return await _context.BlogPosts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetFavoriteBlogPosts(int userId)
        {
            return await _context.BlogPosts
                .Where(p => p.IsFavorite && p.UserId == userId)
                .ToListAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountUserCommentsTodayAsync(int userId, int blogPostId)
        {
            // Get the current date
            var today = DateTime.UtcNow.Date;

            // Count how many times the user has commented on this blog post today
            return await _context.Comments
                .Where(c => c.UserId == userId &&
                            c.PostId == blogPostId &&
                            c.CreatedAt.Date == today)
                .CountAsync();
        }
    }
}
