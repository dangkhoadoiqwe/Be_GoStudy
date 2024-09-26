using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        [HttpPost("like/{blogId}")]
        public async Task<IActionResult> LikePost(int blogId, int userId)
        {
            // Attempt to update the like count for the blog post
            var result = await _blogPostService.UpdateLikeCountAsync(userId, blogId);

            if (result == null)
            {
                return NotFound("Blog post not found.");
            }
            else if (!result)
            {
                return BadRequest("You have already liked this blog.");
            }
            else
            {
                return Ok("Blog post liked successfully.");
            }
        }


         
        [HttpPost("postvip")]
        public async Task<IActionResult> PostVip([FromBody] BlogPost_Create_Model2 blogPostCreateModel,int userId)
        {
     
            try
            {
                // Call the service method to add the blog post
                await _blogPostService.AddBlogPostVIPAsync(blogPostCreateModel, userId);
                return Ok(new { Message = "Blog post created successfully!" });
            }
            catch (Exception ex)
            {
                // Log the error and return a bad request response
                return BadRequest(new { Message = "An error occurred while creating the blog post.", Details = ex.Message });
            }
        }
        [HttpPost(Name = "CreateBlogPost")]
        public async Task<IActionResult> CreateBlogPost([FromBody] BlogPost_Create_Model1 blogPostCreateModel, [FromQuery] int userid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _blogPostService.AddBlogPostAsync(blogPostCreateModel, userid);

            return Ok(new { message = "Blog post created successfully." });
        }
        [HttpGet("trending")]
        public async Task<IActionResult> GetAllBlogPosts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var blogPosts = await _blogPostService.GetPaginatedBlogPostsAsync(pageNumber, pageSize);
            if (blogPosts == null || !blogPosts.Any())
            {
                return NotFound("No blog posts found.");
            }

            return Ok(blogPosts);
        }
        [HttpGet("Detail")]
        public async Task<ActionResult<BlogPostViewdetailModel>> GetBlogPostById(int id)
        {
            var blogPost = await _blogPostService.GetBlogPostByIdAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return Ok(blogPost);
        }

        [HttpPut("UpdateBlog")]
        public async Task<ActionResult> UpdateBlogPost([FromBody] BlogPost_Upadte_Model blogPostCreateModel)
        {
            // Kiểm tra nếu PostId không hợp lệ
            if (blogPostCreateModel == null || blogPostCreateModel.PostId == 0)
            {
                return BadRequest("Invalid blog post data.");
            }

            // Tìm bài viết hiện tại
            var existingPost = await _blogPostService.GetBlogPostByIdAsync(blogPostCreateModel.PostId);

            if (existingPost == null)
            {
                // Trả về thông báo nếu bài viết không tồn tại
                return NotFound(new { message = "Blog post not found." });
            }

            // Chỉ cập nhật title và image
            await _blogPostService.UpdateBlogPostTitleAndImagesAsync(blogPostCreateModel);

            // Trả về thông báo thành công
            return Ok(new { message = "Blog post updated successfully with title and image." });
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

        [HttpPost("comments")]
        public async Task<IActionResult> AddComment([FromBody] CommentModel commentViewModel)
        {
           

            var comment = new Comment
            {
                PostId = commentViewModel.PostId,
                UserId = commentViewModel.UserId,
                Content = commentViewModel.Content,
                CreatedAt = commentViewModel.CreatedAt ?? DateTime.UtcNow
            };


            var result = await _blogPostService.AddCommentAsync(comment);

            if (!result)
            {
                return BadRequest("You have reached the comment limit for today (3 comments per post).");
            }

            return Ok("Comment added successfully.");
        }


        [HttpGet("UserPosts")]
        public async Task<IActionResult> GetUserBlogPosts(int userId, int pageNumber = 1)
        {
            // Lấy danh sách bài viết của user kèm phân trang
            var blogPosts = await _blogPostService.GetUserBlogPosts(userId, pageNumber);

            if (blogPosts == null || !blogPosts.Any())
            {
                return NotFound(new { message = "No blog posts found for the user." });
            }

            return Ok(blogPosts);
        }



    }
}
