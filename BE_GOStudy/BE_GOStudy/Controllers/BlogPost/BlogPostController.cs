using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Model;
using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_GOStudy.Controllers.BlogPost
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IUserService _userService;

        public BlogPostController(IBlogPostService blogPostService, IUserService userService)
        {
            _blogPostService = blogPostService;
            _userService = userService;
        }

        [HttpGet("Userid")]
        public async Task<ActionResult<IEnumerable<BlogPost_View_Model>>> GetAllBlogPosts(int userid)
        {
            var user = await _userService.GetById(userid);

            if (user == null || user.Role != 1)
            {
                return StatusCode(403, "Bạn không có quyền vào");
            }

            var blogPosts = await _blogPostService.GetAllBlogPostsAsync();
            if (blogPosts == null)
            {
                return NotFound();
            }

            return Ok(blogPosts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost_View_Model>> GetBlogPostById(int id)
        {
            var blogPost = await _blogPostService.GetBlogPostByIdAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return Ok(blogPost);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBlogPost(int id, [FromForm] BlogPost_Create_Model blogPostCreateModel, IFormFile imageFile)
        {
            if (id != blogPostCreateModel.PostId)
            {
                return BadRequest("ID mismatch.");
            }

            var existingPost = await _blogPostService.GetBlogPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound();
            }

            await _blogPostService.UpdateBlogPostAsync(blogPostCreateModel);

            return NoContent();
        }
     
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBlogPost(int id)
        {
            var blogPost = await _blogPostService.GetBlogPostByIdAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            await _blogPostService.DeleteBlogPostAsync(id);
            return NoContent();
        }

        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingBlogPosts()
        {
            var trendingPosts = await _blogPostService.GetTrendingBlogPosts();
            return Ok(trendingPosts);
        }

        [HttpGet("yourblog/{userId}")]
        public async Task<IActionResult> GetUserBlogPosts(int userId)
        {
            var userPosts = await _blogPostService.GetUserBlogPosts(userId);
            return Ok(userPosts);
        }

        [HttpPost(Name = "CreateBlogPost")]
        public async Task<IActionResult> CreateBlogPost([FromForm] BlogPost_Create_Model1 blogPostCreateModel, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _blogPostService.AddBlogPostAsync(blogPostCreateModel, imageFile);

            return Ok(new { message = "Blog post created successfully." });
        }
    }
}
