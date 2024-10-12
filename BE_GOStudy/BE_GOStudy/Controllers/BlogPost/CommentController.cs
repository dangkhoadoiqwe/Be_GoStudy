using DataAccess.Model;
using GO_Study_Logic.ViewModel;
using GOStudy_Logic.Service;
using Microsoft.AspNetCore.Mvc;

namespace BE_GOStudy.Controllers.BlogPost
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpPost("addComments")]
        public async Task<IActionResult> CreateComment([FromBody] Comment_View_Model commentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = new Comment()
            {
                CommentId = commentViewModel.CommentId,
                Content = commentViewModel.Content,
                PostId = commentViewModel.PostId,
                UserId = commentViewModel.UserId,
                CreatedAt = DateTime.Now
            };
            
            await _commentService.AddCommentAsync(comment);

            return Ok(new { message = "Comment created successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment_View_Model>> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound("Comment not found");
            }
            return Ok(comment);
        }
        
        [HttpPut]
        public async Task<ActionResult> UpdateComment(Comment_View_Model commentViewModel)
        {
            var id = commentViewModel.CommentId;
            if (id != commentViewModel.CommentId)
            {
                return BadRequest("Ids don't match");
            }
            var existingComment = await _commentService.GetCommentByIdAsync(id);
            if (existingComment == null)
            {
                return NotFound("Comment not found");
            }
            await _commentService.UpdateCommentAsync(commentViewModel);
            return NoContent();
        }
        [HttpGet("getAllCommentsByPostId")]
        public async Task<IActionResult> GetCommentsByPostId(int id)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(id);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments);
        }
       
        
    }
}




/*
           // GET ALL COMMENTS FOR A POST
           [HttpGet("post/{postId}")]
           public async Task<IActionResult> GetCommentsByPostId(int postId)
           {
               var comments = await _commentService.GetCommentsByPostIdAsync(postId);
               return Ok(comments);
           }
       }
   }



 */