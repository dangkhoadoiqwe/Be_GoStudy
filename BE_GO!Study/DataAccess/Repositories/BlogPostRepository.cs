using System;
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
    }


    public class BlogPostRepository : IBlogPostRepository
    {
        private GOStudyContext _context;
        public BlogPostRepository(GOStudyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            return await _context.BlogPosts.ToListAsync();
        }
        public async Task<BlogPost?> GetBlogPostByIdAsync(int postId)
        {
            return await _context.BlogPosts.FindAsync(postId);
        }
        public async Task DeleteBlogPostAsync(BlogPost blogPost)
        {
            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBlogPostAsync(BlogPost blogPost)
        {
            _context.BlogPosts.Update(blogPost);
            await _context.SaveChangesAsync();
        }

        public async Task AddBlogPostAsync(BlogPost blogPost)
        {
            blogPost.PostId = new int();

            blogPost.CreatedAt = DateTime.Now;

            _context.BlogPosts.Add(blogPost);

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

    }
}

