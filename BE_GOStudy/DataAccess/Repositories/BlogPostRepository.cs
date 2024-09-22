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

        public async Task<BlogPost?> GetBlogPostByIdAsync(int postId)
        {
            return await _context.BlogPosts.FindAsync(postId);
        }

        public async Task AddBlogPostAsync(BlogPost blogPost)
        {
            blogPost.CreatedAt = DateTime.Now;
            blogPost.ViewCount = 0;
            blogPost.shareCount = 0;
            blogPost.likeCount = 0;

            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
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
    }
}
