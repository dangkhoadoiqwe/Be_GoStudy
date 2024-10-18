using DataAccess.Model;
using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using GOStudy_Logic.Service.Responses;
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
            _blogPostService = blogPostService ?? throw new ArgumentNullException(nameof(blogPostService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("like/{blogId}")]
        public async Task<IActionResult> LikePost(int blogId, int userId)
        {
            var result = await _blogPostService.UpdateLikeCountAsync(userId, blogId);
            var blogPost = await _blogPostService.GetBlogPostByIdAsync(blogId);
            if (blogPost == null)
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Blog post not found.",
                    IsSuccess = false
                });
            }

            if (!result)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "You have already liked this blog.",
                    IsSuccess = false
                });
            }

            return Ok(new BaseResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Blog post liked successfully.",
                IsSuccess = true
            });
        }

        [HttpPost("postvip")]
        public async Task<IActionResult> PostVip([FromBody] BlogPost_Create_Model2 blogPostCreateModel, int userId)
        {
            try
            {
                await _blogPostService.AddBlogPostVIPAsync(blogPostCreateModel, userId);
                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Blog post created successfully!",
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                // Log the error (consider logging to a file or external service)
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "An error occurred while creating the blog post.",
                   // Details = ex.Message,
                    IsSuccess = false
                });
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
            return Ok(new BaseResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Blog post created successfully.",
                IsSuccess = true
            });
        }
        [HttpGet("UserPosts")]
        public async Task<IActionResult> GetUserBlogPosts(int userId,[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var blogPosts = await _blogPostService.GetUserBlogPosts(userId, pageNumber, pageSize);

            if (blogPosts == null)
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No blog posts found for the user.",
                    IsSuccess = false
                });
            }

            return Ok(new BaseResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "User blog posts retrieved successfully.",
                Data = blogPosts,
                IsSuccess = true
            });
        }
        [HttpGet("trending")]
        public async Task<IActionResult> GetAllBlogPosts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var paginatedResult = await _blogPostService.GetPaginatedBlogPostsAsync(pageNumber, pageSize);

                if (paginatedResult == null || !paginatedResult.Data.Any())
                {
                    return NotFound(new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "No blog posts found.",
                        IsSuccess = false
                    });
                }

                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Get All Blogs Success",
                    Data = paginatedResult,
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }

        [HttpGet("Detail")]
        public async Task<ActionResult<BlogPostViewdetailModel>> GetBlogPostById(int id)
        {
            var blogPost = await _blogPostService.GetBlogPostByIdAsync(id);
            if (blogPost == null)
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Blog post not found.",
                    IsSuccess = false
                });
            }

            return Ok(new BaseResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Blog post retrieved successfully.",
                Data = blogPost,
                IsSuccess = true
            });
        }

        [HttpPut("UpdateBlog")]
        public async Task<ActionResult> UpdateBlogPost([FromBody] BlogPost_Upadte_Model blogPostCreateModel)
        {
            if (blogPostCreateModel == null || blogPostCreateModel.PostId == 0)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Invalid blog post data.",
                    IsSuccess = false
                });
            }

            var existingPost = await _blogPostService.GetBlogPostByIdAsync(blogPostCreateModel.PostId);
            if (existingPost == null)
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Blog post not found.",
                    IsSuccess = false
                });
            }

            await _blogPostService.UpdateBlogPostTitleAndImagesAsync(blogPostCreateModel);
            return Ok(new BaseResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Blog post updated successfully with title and image.",
                IsSuccess = true
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBlogPost(int id)
        {
            var blogPost = await _blogPostService.GetBlogPostsByIdAsync(id);
            if (blogPost == null)
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Blog post not found.",
                    IsSuccess = false
                });
            }

            // Cập nhật thuộc tính IsDraft thành true thay vì xóa
            blogPost.IsDraft = true;
            await _blogPostService.UpdateBlogPostAsync(blogPost);

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
                return BadRequest(new BaseResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "You have reached the comment limit for today (3 comments per post).",
                    IsSuccess = false
                });
            }

            return Ok(new BaseResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Comment added successfully.",
                IsSuccess = true
            });
        }

         
    }
}
