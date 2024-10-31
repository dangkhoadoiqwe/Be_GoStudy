using System;
using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using GO_Study_Logic.Service;

namespace GO_Study_Logic.Service
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPost_View_Model_All>> GetAllBlogPostsAsync();
        Task<BlogPostViewdetailModel> GetBlogPostByIdAsync(int postId);
        Task AddBlogPostAsync(BlogPost_Create_Model1 blogPostCreateModel, int userid);
        Task UpdateBlogPostAsync(BlogPost_Create_Model blogPostCreateModel);
        Task<bool> DeleteBlogPostAsync(int postId);
        Task<List<BlogPost>> GetTrendingBlogPosts();
        Task<PaginatedResult<BlogPost_View_Model_All>> GetUserBlogPosts(int userId, int pageNumber, int pageSize);
        Task<List<BlogPost>> GetFavoriteBlogPosts(int userId);
        Task<bool> UpdateFavoriteBlogPost(int blogPostId, bool favorite);
        Task AddBlogPostVIPAsync(BlogPost_Create_Model2 blogPostCreateModel, int userId);
        Task<bool> UpdateLikeCountAsync(int userId, int blogId);
        Task<bool> AddCommentAsync(Comment comment);
        Task<bool> CanCreateBlogPostAsync(int userId);
        Task<bool> CanAddCommentAsync(int userId, int postId);
        Task<bool> UpdateBlogPostAsync(BlogPost blogPost);

        Task<BlogPost?> GetBlogPostsByIdAsync(int postId);
        Task UpdateBlogPostTitleAndImagesAsync(BlogPost_Upadte_Model blogPostCreateModel);
        Task<PaginatedResult<BlogPost_View_Model_All>> GetPaginatedBlogPostsAsync(int pageNumber, int pageSize);
    }
}

    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _repository;
        private readonly IMapper _mapper;
          private readonly IUserRepository _userRepository;
         public BlogPostService(IBlogPostRepository repository, IMapper mapper, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
        _userRepository = userRepository;
        }

        public async Task<IEnumerable<BlogPost_View_Model_All>> GetAllBlogPostsAsync()
        {
            var blogPosts = await _repository.GetAllBlogPostsAsync();
            
            return _mapper.Map<IEnumerable<BlogPost_View_Model_All>>(blogPosts);
        }

    public async Task<PaginatedResult<BlogPost_View_Model_All>> GetUserBlogPosts(int userId, int pageNumber, int pageSize)
    {
        var blogPosts =  await _repository.GetUserBlogPosts(userId, pageNumber, pageSize);
        var blogPostsViewModels = _mapper.Map<IEnumerable<BlogPost_View_Model_All>>(blogPosts);
        var totalBlogPostsCount = await _repository.CountUserLikedPostsAsync(userId, pageNumber);
        return new PaginatedResult<BlogPost_View_Model_All>
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalCount = totalBlogPostsCount,
            Data = blogPostsViewModels
        };
    }
    public async Task<bool> CanCreateBlogPostAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return false;

        DateTime today = DateTime.UtcNow.Date;

        // Role-based limits
        int maxPosts = user.Role switch
        {
            1 => 3, // Role 1: overall limit of 3 posts
            3 => 3, // Role 3: daily limit of 3 posts
            4 => int.MaxValue, // Role 4: no limit
            _ => 0 // No posts allowed for other roles
        };

        // Count posts based on role-specific limits
        int currentPostCount = user.Role switch
        {
            1 => await _repository.CountUserPostsAsync(userId), // Overall count for Role 1
            3 => await _repository.CountUserPostsTodayAsync(userId, today), // Daily count for Role 3
            _ => 0
        };

        return currentPostCount < maxPosts;
    }
    public async Task<bool> CanAddCommentAsync(int userId, int postId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return false;

        DateTime today = DateTime.UtcNow.Date;

        // Role-based limits
        int maxComments = user.Role switch
        {
            1 => 3, // Role 1: overall limit of 3 comments
            3 => 10, // Role 3: daily limit of 3 comments
            4 => int.MaxValue, // Role 4: no limit
            _ => 0 // No comments allowed for other roles
        };

        // Count comments based on role-specific limits
        int currentCommentCount = user.Role switch
        {
            1 => await _repository.CountUserCommentsAsync(userId), // Overall count for Role 1
            3 => await _repository.CountUserCommentsTodayAsync(userId, today), // Daily count for Role 3
            _ => 0
        };

        return currentCommentCount < maxComments;
    }
    public async Task<bool> UpdateBlogPostAsync(BlogPost blogPost)
    {
        var existingPost = await _repository.GetBlogPostByIdAsync(blogPost.PostId);
        if (existingPost == null)
        {
            return false;
        }

      
        existingPost.Title = blogPost.Title;
        existingPost.Content = blogPost.Content;
        existingPost.IsDraft = blogPost.IsDraft;
        existingPost.CreatedAt = blogPost.CreatedAt; 

      
        if (blogPost.BlogImgs != null && blogPost.BlogImgs.Any())
        {
            existingPost.BlogImgs = blogPost.BlogImgs;
        }

        
        await _repository.UpdateBlogPostAsync(existingPost);
        return true;
    }

    public async Task<PaginatedResult<BlogPost_View_Model_All>> GetPaginatedBlogPostsAsync(int pageNumber, int pageSize)
    {
        var blogPosts = await _repository.GetPaginatedBlogPostsAsync(pageNumber, pageSize);
       
        var blogPostsViewModels = _mapper.Map<IEnumerable<BlogPost_View_Model_All>>(blogPosts);
        var totalBlogPostsCount = await _repository.CountTotalBlogs();
        return new PaginatedResult<BlogPost_View_Model_All>
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalCount = totalBlogPostsCount,
            Data = blogPostsViewModels
        };
    }


    public async Task<bool> AddCommentAsync(Comment comment)
    {
        // Count how many times the user has commented today
        var commentCountToday = await _repository.CountUserCommentsTodayAsync(comment.UserId, comment.PostId);

        // Limit the number of comments to 3 per day
       
        // Add the comment
        await _repository.AddCommentAsync(comment);
        return true;
    }

    public async Task AddBlogPostAsync(BlogPost_Create_Model1 blogPostCreateModel, int userid)
        {
            var blogPost = _mapper.Map<BlogPost>(blogPostCreateModel);
            blogPost.UserId = userid;
            blogPost.CreatedAt = DateTime.Now;
            blogPost.ViewCount = 0;
            blogPost.shareCount = 0;
            blogPost.likeCount = 0;
            blogPost.IsDraft = false;
            blogPost.IsFavorite = false;
            blogPost.IsTrending = false;
            blogPost.Tags = "123";
            blogPost.CreatedAt = DateTime.Now;
            blogPost.ViewCount = 0;
            blogPost.shareCount = 0;
            blogPost.likeCount = 0;
            blogPost.Category = "123";
            await _repository.AddBlogPostAsync(blogPost);
        }


        public async Task<BlogPostViewdetailModel> GetBlogPostByIdAsync(int postId)
        {
            var blogPost = await _repository.GetBlogPostByIdAsync(postId);
            if (blogPost == null) return null;

            return _mapper.Map<BlogPostViewdetailModel>(blogPost);
        }

        public async Task UpdateBlogPostAsync(BlogPost_Create_Model blogPostCreateModel)
        {
            var updatedBlogPost = _mapper.Map<BlogPost>(blogPostCreateModel);

            var existingBlogPost = await _repository.GetBlogPostByIdAsync(blogPostCreateModel.PostId);
            if (existingBlogPost != null)
            {
                existingBlogPost.Title = updatedBlogPost.Title;
                existingBlogPost.Content = updatedBlogPost.Content;
                existingBlogPost.CreatedAt = updatedBlogPost.CreatedAt;
                existingBlogPost.image = updatedBlogPost.image;
                existingBlogPost.IsFavorite = updatedBlogPost.IsFavorite;
                existingBlogPost.IsTrending = updatedBlogPost.IsTrending;
                await _repository.UpdateBlogPostAsync(existingBlogPost);
            }
    }//BlogPost_Upadte_Model
    public async Task UpdateBlogPostTitleAndImagesAsync(BlogPost_Upadte_Model blogPostCreateModel)
    {
        // Lấy blog post hiện tại từ repository
        var existingBlogPost = await _repository.GetBlogPostByIdAsync(blogPostCreateModel.PostId);

        if (existingBlogPost != null)
        {
            // Cập nhật các thuộc tính của bài viết
            existingBlogPost.Title = blogPostCreateModel.Title;
            existingBlogPost.Content = blogPostCreateModel.Content;
            existingBlogPost.CreatedAt = DateTime.Now;  // Nếu bạn có trường UpdatedAt
            existingBlogPost.UserId = blogPostCreateModel.userId;
            existingBlogPost.Category = "updated_category";  // Bạn có thể thêm logic cập nhật category nếu cần
            existingBlogPost.Tags = "updated_tags";          // Bạn có thể thêm logic cập nhật tags nếu cần

            // Cập nhật tiêu đề hình ảnh chính nếu có
            existingBlogPost.image = blogPostCreateModel.Images != null && blogPostCreateModel.Images.Count > 0
                ? blogPostCreateModel.Images[0] : null;

            // Cập nhật bài viết trong database
            await _repository.UpdateBlogPostAsync(existingBlogPost);

            // Xử lý các hình ảnh liên quan
            await _repository.UpdateBlogImagesAsync(blogPostCreateModel.PostId, blogPostCreateModel.Images);
        }
    }

    public async Task AddBlogPostVIPAsync(BlogPost_Create_Model2 blogPostCreateModel, int userId)
    {
        // Ánh xạ BlogPost_Create_Model1 thành BlogPost
        var blogPost = _mapper.Map<BlogPost>(blogPostCreateModel);

        // Thiết lập các thuộc tính bổ sung
        blogPost.UserId = userId;
        blogPost.CreatedAt = DateTime.Now;
        blogPost.ViewCount = 0;
        blogPost.shareCount = 0;
        blogPost.likeCount = 0;
        blogPost.IsDraft = false;
        blogPost.IsFavorite = false;
        blogPost.IsTrending = false;
        blogPost.Tags = "default_tags";
        blogPost.Category = "default_category";

        // Kiểm tra ảnh đại diện
        if (blogPostCreateModel.Images != null && blogPostCreateModel.Images.Count > 0)
        {
            // Nếu ảnh đầu tiên là "string", gán giá trị ảnh là chuỗi rỗng
            blogPost.image = blogPostCreateModel.Images[0] == "string" ? "" : blogPostCreateModel.Images[0];
        }
        else
        {
            // Nếu không có ảnh, đặt giá trị ảnh là null
            blogPost.image = null;
        }

        // Thêm bài viết blog vào cơ sở dữ liệu
        await _repository.AddBlogPostAsync(blogPost);

        // Xử lý nhiều ảnh bằng cách thêm từng ảnh vào bảng BlogImg
        if (blogPostCreateModel.Images != null && blogPostCreateModel.Images.Count > 0)
        {
            foreach (var imageUrl in blogPostCreateModel.Images)
            {
                var blogImg = new BlogImg
                {
                    BlogId = blogPost.PostId,  // Liên kết với bài viết blog vừa tạo
                    Img = imageUrl
                };

                // Thêm từng ảnh vào bảng BlogImg
                await _repository.AddBlogImgAsync(blogImg);
            }
        }
    }


    //public async Task AddBlogPostVIPAsync(BlogPost_Create_Model2 blogPostCreateModel, int userId)
    //    {
    //        // Map the BlogPost_Create_Model1 to BlogPost
    //        var blogPost = _mapper.Map<BlogPost>(blogPostCreateModel);

    //        // Set additional properties
    //        blogPost.UserId = userId;
    //        blogPost.CreatedAt = DateTime.Now;
    //        blogPost.ViewCount = 0;
    //        blogPost.shareCount = 0;
    //        blogPost.likeCount = 0;
    //        blogPost.IsDraft = false;
    //        blogPost.IsFavorite = false;
    //        blogPost.IsTrending = false;
    //        blogPost.Tags = "default_tags";
    //        blogPost.Category = "default_category";
    //    blogPost.image = (blogPostCreateModel.Images != null && blogPostCreateModel.Images.Count > 0 && blogPostCreateModel.Images[0] != "string")
    //? blogPostCreateModel.Images[0]
    //: null;
    //    // Add the blog post to the database
    //    await _repository.AddBlogPostAsync(blogPost);

    //        // Handle multiple images by adding each one to the BlogImg table
    //        if (blogPostCreateModel.Images != null && blogPostCreateModel.Images.Count > 0)
    //        {
    //            foreach (var imageUrl in blogPostCreateModel.Images)
    //            {
    //                var blogImg = new BlogImg
    //                {
    //                    BlogId = blogPost.PostId,  // Link to the created blog post
    //                    Img = imageUrl
    //                };

    //                // Add each image to the BlogImg table
    //                await _repository.AddBlogImgAsync(blogImg);
    //            }
    //        }
    //    }

    public async Task<bool> DeleteBlogPostAsync(int postId)
    {
        var blogPost = await _repository.GetBlogPostByIdAsync(postId);
        if (blogPost == null) return false;

        // Cập nhật thuộc tính IsDraft thành true
        blogPost.IsDraft = true;
        await _repository.UpdateBlogPostAsync(blogPost);
        return true;
    }


    public async Task<List<BlogPost>> GetTrendingBlogPosts()
        {
            return await _repository.GetTrendingBlogPosts();
        }

        public async Task<List<BlogPost>> GetFavoriteBlogPosts(int userId)
        {
            return await _repository.GetFavoriteBlogPosts(userId);
        }

        public async Task<bool> UpdateFavoriteBlogPost(int blogPostId, bool favorite)
        {
            var blogPost = await _repository.GetBlogPostByIdAsync(blogPostId);
            if (blogPost == null) return false;

            blogPost.IsFavorite = favorite;
            await _repository.UpdateBlogPostAsync(blogPost);
            return true;
        }

        

    public async Task<bool> UpdateLikeCountAsync(int userId, int blogId)
    {
        // Check if the user has already liked the post
        var hasLiked = await _repository.HasUserLikedPostAsync(userId, blogId);

        if (hasLiked)
        {
            return false; // User has already liked this post
        }

        // Fetch the blog post by its ID
        var blogPost = await _repository.GetBlogPostByIdAsync(blogId);

        if (blogPost == null)
        {
            return false; // Blog post does not exist
        }

        // Increment the like count
        blogPost.likeCount += 1;

        // Update the blog post
        await _repository.UpdateBlogPostAsync(blogPost);

        // Add the like entry to track that the user liked the post
        var userLike = new UserLike
        {
            UserId = userId,
            BlogId = blogId,
           
        };
        await _repository.AddUserLikeAsync(userLike); // Assuming you have a method to add likes

        return true; // Like was successfully added
    }

    public async Task<BlogPost?> GetBlogPostsByIdAsync(int postId)
    {
        return await _repository.GetBlogPostsByIdAsync(postId);
    }
}

