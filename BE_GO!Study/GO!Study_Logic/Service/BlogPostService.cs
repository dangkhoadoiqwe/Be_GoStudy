using System;
using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using GO_Study_Logic.ViewModel.User;

namespace GO_Study_Logic.Service
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPost_View_Model>> GetAllBlogPostsAsync();
        Task<BlogPost_View_Model> GetBlogPostByIdAsync(int postId);
        Task AddBlogPostAsync(BlogPost_View_Model blogPostViewModel);
        Task UpdateBlogPostAsync(BlogPost_View_Model blogPostViewModel);
        Task<bool> DeleteBlogPostAsync(int postId);
        Task<List<BlogPost>> GetTrendingBlogPosts();
        Task<List<BlogPost>> GetUserBlogPosts(int userId);
    }
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _repository;
        private readonly IMapper _mapper;

        public BlogPostService(
            IBlogPostRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<BlogPost_View_Model>> GetAllBlogPostsAsync()
        {
            var blogPost = await _repository.GetAllBlogPostsAsync();
            return _mapper.Map<IEnumerable<BlogPost_View_Model>>(blogPost);
        }

        public async Task AddBlogPostAsync(BlogPost_View_Model blogPostViewModel)
        {
            var blogPostEntity = _mapper.Map<BlogPost>(blogPostViewModel);
            await _repository.AddBlogPostAsync(blogPostEntity);
        }

        public async Task<bool> DeleteBlogPostAsync(int postId)
        {
            var blogPost = await _repository.GetBlogPostByIdAsync(postId);
            if (blogPost == null)
            {
                return false;
            }
            await _repository.DeleteBlogPostAsync(blogPost);
            return true;
        }

        public async Task<BlogPost_View_Model> GetBlogPostByIdAsync(int postId)
        {
            var blogPost = await _repository.GetBlogPostByIdAsync(postId);
            if (blogPost == null)
                return null;
            var blogPostViewModel = _mapper.Map<BlogPost_View_Model>(blogPost);

            return blogPostViewModel;
        }

        public async Task UpdateBlogPostAsync(BlogPost_View_Model blogPostViewModel)
        {
            var updatedBlogPost = _mapper.Map<BlogPost>(blogPostViewModel);

            var existingBlogPost = await _repository.GetBlogPostByIdAsync(blogPostViewModel.PostId);
            if (existingBlogPost != null)
            {
                existingBlogPost.Title = updatedBlogPost.Title;
                existingBlogPost.Content = updatedBlogPost.Content;
                existingBlogPost.CreatedAt = updatedBlogPost.CreatedAt;
                existingBlogPost.Category = updatedBlogPost.Category;
                existingBlogPost.Tags = updatedBlogPost.Tags;
                existingBlogPost.ViewCount = updatedBlogPost.ViewCount;
                existingBlogPost.IsDraft = updatedBlogPost.IsDraft;
                existingBlogPost.shareCount = updatedBlogPost.shareCount;
                existingBlogPost.likeCount = updatedBlogPost.likeCount;
                existingBlogPost.image = updatedBlogPost.image;
                existingBlogPost.IsFavorite = updatedBlogPost.IsFavorite;
                existingBlogPost.IsTrending = updatedBlogPost.IsTrending;
                await _repository.UpdateBlogPostAsync(existingBlogPost);
            }
        }

        public async Task<List<BlogPost>> GetTrendingBlogPosts()
        {
            return await _repository.GetTrendingBlogPosts();
        }

        public async Task<List<BlogPost>> GetUserBlogPosts(int userId)
        {
            return await _repository.GetUserBlogPosts(userId);
        }
    }
}

