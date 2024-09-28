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
        Task<List<BlogPost>> GetUserBlogPosts(int userId, int pageNumber, int pageSize);
        Task<List<BlogPost>> GetFavoriteBlogPosts(int userId);
        Task AddBlogImgAsync(BlogImg blogImg);
        Task AddCommentAsync(Comment comment);
        Task<IEnumerable<BlogPost>> GetPaginatedBlogPostsAsync(int pageNumber, int pageSize);
        Task<bool> HasUserLikedPostAsync(int userId, int blogId);

        Task UpdateBlogImagesAsync(int blogPostId, List<string> newImages);
        Task AddUserLikeAsync(UserLike userLike);

        Task<int> CountUserCommentsTodayAsync(int userId, int blogPostId);

        Task<int> CountTotalBlogs();

        Task<int> CountUserLikedPostsAsync(int userId, int blogPostId);

        Task<BlogPost?> GetBlogPostsByIdAsync(int postId);

        Task UpdateBlogPostsAsync(BlogPost blogPost);
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
            return await _context.BlogPosts
                                 .Where(bp => !bp.IsDraft) 
                                 .Include(bp => bp.BlogImgs) 
                                 .ToListAsync();
        }

        public async Task UpdateBlogPostsAsync(BlogPost blogPost)
        {
            // Kiểm tra nếu bài viết có tồn tại trong cơ sở dữ liệu
            var existingPost = await _context.BlogPosts.FindAsync(blogPost.PostId);
            if (existingPost != null)
            {
                // Cập nhật các thuộc tính của bài viết
                existingPost.Title = blogPost.Title;
                existingPost.Content = blogPost.Content;
                existingPost.IsDraft = blogPost.IsDraft;
                existingPost.CreatedAt = blogPost.CreatedAt; // Cập nhật thời gian sửa đổi

                // Cập nhật các hình ảnh (nếu có)
                if (blogPost.BlogImgs != null && blogPost.BlogImgs.Any())
                {
                    existingPost.BlogImgs = blogPost.BlogImgs;
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BlogPost>> GetPaginatedBlogPostsAsync(int pageNumber, int pageSize)
        {
            return await _context.BlogPosts
                 .Where(bp => !bp.IsDraft)
                .Include(bp => bp.BlogImgs)      
                .Include(bp => bp.User)          
                .OrderByDescending(bp => bp.CreatedAt)  
                .Skip((pageNumber - 1) * pageSize)      
                .Take(pageSize)                         
                .ToListAsync();
        }

        public async Task AddUserLikeAsync(UserLike userLike)
        {
            _context.UserLikes.Add(userLike);
            await _context.SaveChangesAsync();
        }
        public async Task<BlogPost?> GetBlogPostByIdAsync(int postId)
        {
            return await _context.BlogPosts
                .Include(bp => bp.BlogImgs)                          // Include hình ảnh của blog
                .Include(bp => bp.User)                              // Include thông tin user của blog
                .Include(bp => bp.Comments)                          // Include các bình luận
                    .ThenInclude(c => c.User)                        // Include thông tin user của từng bình luận
                .Where(bp => bp.PostId == postId)                    // Lọc theo postId
                .FirstOrDefaultAsync();
        }
        public async Task UpdateBlogImagesAsync(int blogPostId, List<string> newImages)
        {
            // Lấy tất cả hình ảnh hiện tại của bài viết từ bảng BlogImg
            var existingImages = await _context.BlogImgs
                .Where(bi => bi.BlogId == blogPostId)
                .ToListAsync();

            // Xóa các hình ảnh cũ không còn trong danh sách hình ảnh mới
            var imagesToRemove = existingImages
                .Where(img => !newImages.Contains(img.Img))  // Nếu hình ảnh hiện tại không có trong danh sách mới, xóa nó
                .ToList();

            if (imagesToRemove.Any())
            {
                _context.BlogImgs.RemoveRange(imagesToRemove);  // Xóa hình ảnh cũ khỏi bảng BlogImg
            }

            // Thêm các hình ảnh mới vào bảng BlogImg nếu chưa có trong danh sách hiện tại
            var imagesToAdd = newImages
                .Where(imgUrl => !existingImages.Any(bi => bi.Img == imgUrl))  // Nếu hình ảnh mới không có trong database, thêm nó
                .Select(imgUrl => new BlogImg
                {
                    BlogId = blogPostId,  // Liên kết với bài viết hiện tại
                    Img = imgUrl
                })
                .ToList();

            if (imagesToAdd.Any())
            {
                await _context.BlogImgs.AddRangeAsync(imagesToAdd);  // Thêm các hình ảnh mới vào bảng BlogImg
            }

            // Lưu thay đổi vào database
            await _context.SaveChangesAsync();
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
        public async Task<BlogPost?> GetBlogPostsByIdAsync(int postId)
        {
            return await _context.BlogPosts
                .Include(bp => bp.BlogImgs)                          // Bao gồm hình ảnh của blog
                .Include(bp => bp.User)                              // Bao gồm thông tin người dùng của blog
                .Include(bp => bp.Comments)                          // Bao gồm các bình luận
                    .ThenInclude(c => c.User)                        // Bao gồm thông tin người dùng của từng bình luận
                .Where(bp => bp.PostId == postId && !bp.IsDraft)    // Lọc theo postId và đảm bảo không phải là bản nháp
                .FirstOrDefaultAsync();
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

        public async Task<List<BlogPost>> GetUserBlogPosts(int userId, int pageNumber, int pageSize)
        {
           // int pageSize = 10; // 10 items per page

            return await _context.BlogPosts
                 .Where(bp => !bp.IsDraft)
                 .Include(bp => bp.BlogImgs)
                .Where(p => p.UserId == userId)
                .Include(p => p.User)  // Lấy tất cả thông tin của user liên quan
                .OrderByDescending(p => p.CreatedAt)  // Sắp xếp theo CreatedAt giảm dần
                .Skip((pageNumber - 1) * pageSize)  // Bỏ qua các bài viết của các trang trước
                .Take(pageSize)  // Lấy 10 bài viết của trang hiện tại
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

        public async Task<int> CountTotalBlogs()
        {
            return await _context.BlogPosts.CountAsync();
        }
        
        public async Task<int> CountUserLikedPostsAsync(int userId, int blogPostId)
        {
            return await _context.BlogPosts.Where(c => c.UserId == userId).CountAsync();
        }
    }
}
