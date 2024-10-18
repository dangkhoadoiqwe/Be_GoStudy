using GO_Study_Logic.Service;
using Microsoft.AspNetCore.Mvc;

namespace BE_GOStudy.Controllers.Room
{
    [Route("api/[controller]")]
    [ApiController]
    public class YouTubeController : ControllerBase
    {
        private static List<VideoInfo> _videos = new List<VideoInfo>();

        private readonly IClassroomService _service;
        private readonly IUserService _userService;

        public YouTubeController(IClassroomService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }
 
        [HttpPut("UpdateYoutubeUrl/{id}")]
        public async Task<IActionResult> UpdateClassroomLinkUrl(  [FromBody] string linkUrl)
        {
            if (string.IsNullOrEmpty(linkUrl))
            {
                return BadRequest("Link URL cannot be null or empty");
            }

            try
            {
                int classroomId = 6;
                await _service.UpdateClassroomLinkYtbUrlAsync(classroomId, linkUrl);
                return Ok("Link URL updated successfully.");
            }
            catch (Exception ex)
            {
                // Log lỗi (ex) nếu cần
                return StatusCode(500, $"Error updating Link URL: {ex.Message}");
            }
        }
        [HttpGet("saved")]
        public IActionResult GetSavedVideos()
        {
            return Ok(_videos);
        }
        [HttpGet("GetYoutubeUrl")]
        public async Task<IActionResult> GetClassroomLinkUrl()
        {
            try
            {
                int classroomId = 6;
                var classroom = await _service.GetClassroomByIdAsync(classroomId);

                if (classroom == null || string.IsNullOrEmpty(classroom.YoutubeUrl))
                {
                    return NotFound("No YouTube link found for this classroom.");
                }

                return Ok(new { linkUrl = classroom.YoutubeUrl });
            }
            catch (Exception ex)
            {
                // Log lỗi (ex) nếu cần
                return StatusCode(500, $"Error retrieving YouTube link: {ex.Message}");
            }
        }
    }

    public class VideoInfo
    {
        public string VideoId { get; set; }
        public string Title { get; set; }
    }
}
