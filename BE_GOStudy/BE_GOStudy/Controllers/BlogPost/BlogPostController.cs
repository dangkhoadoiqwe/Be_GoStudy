using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DataAccess.Model;
using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BE_GOStudy.Controllers.BlogPost
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private IBlogPostService _blogPostService;
         
        private IUserService _userService;
        public BlogPostController(IBlogPostService blogPostService,   IUserService userService)
        {
            _blogPostService = blogPostService;
            
            _userService = userService;
        }

        [HttpGet]
         
        public async Task<ActionResult<IEnumerable<BlogPost_View_Model>>> GetAllBlogPosts(int userid)
        {
             
            var claims = HttpContext.User.Claims;
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

             
            var user = await _userService.GetById(userid);  

             
            if (user.Role == 2 || user.Role == 3)  
            {
                 
                var blogPosts = await _blogPostService.GetAllBlogPostsAsync();
                if (blogPosts == null)
                {
                    return NotFound();
                }
                return Ok(blogPosts);
            }
            else
            {
                return StatusCode(403, "Bạn không có quyền vào");
            }
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
        public async Task<ActionResult> UpdateBlogPost(int id, BlogPost_View_Model blogPostViewModel)
        {
            if (id != blogPostViewModel.PostId)
            {
                return BadRequest();
            }

            var existingPost = await _blogPostService.GetBlogPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound();
            }

            await _blogPostService.UpdateBlogPostAsync(blogPostViewModel);
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
        public async Task<IActionResult> CreateBlogPost([FromBody] BlogPost_View_Model blogPostViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _blogPostService.AddBlogPostAsync(blogPostViewModel);

            return Ok(new { message = "Blog post created successfully." });
        }
    }
}

