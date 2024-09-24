using System;
using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GO_Study_Logic.Service
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPost_View_Model>> GetAllBlogPostsAsync();
        Task<BlogPost_View_Model> GetBlogPostByIdAsync(int postId);
        Task AddBlogPostAsync(BlogPost_Create_Model1 blogPostCreateModel);
        Task UpdateBlogPostAsync(BlogPost_Create_Model blogPostCreateModel);
        Task<bool> DeleteBlogPostAsync(int postId);
        Task<List<BlogPost>> GetTrendingBlogPosts();
        Task<List<BlogPost>> GetUserBlogPosts(int userId);
        Task<List<BlogPost>> GetFavoriteBlogPosts(int userId);
        Task<bool> UpdateFavoriteBlogPost(int blogPostId, bool favorite);
    }

    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _repository;
        private readonly IMapper _mapper;

        public BlogPostService(IBlogPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogPost_View_Model>> GetAllBlogPostsAsync()
        {
            var blogPosts = await _repository.GetAllBlogPostsAsync();
            return _mapper.Map<IEnumerable<BlogPost_View_Model>>(blogPosts);
        }

        public async Task AddBlogPostAsync(BlogPost_Create_Model1 blogPostCreateModel)
        {
            var blogPost = _mapper.Map<BlogPost>(blogPostCreateModel);

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

            await _repository.AddBlogPostAsync(blogPost);
        }


        public async Task<BlogPost_View_Model> GetBlogPostByIdAsync(int postId)
        {
            var blogPost = await _repository.GetBlogPostByIdAsync(postId);
            if (blogPost == null) return null;

            return _mapper.Map<BlogPost_View_Model>(blogPost);
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
        }

        public async Task<bool> DeleteBlogPostAsync(int postId)
        {
            var blogPost = await _repository.GetBlogPostByIdAsync(postId);
            if (blogPost == null) return false;

            await _repository.DeleteBlogPostAsync(blogPost);
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

        public async Task<List<BlogPost>> GetUserBlogPosts(int userId)
        {
            return await _repository.GetUserBlogPosts(userId);
        }
    }
}
