using Microsoft.AspNetCore.Mvc;
using PusherServer;

namespace BE_GOStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly Pusher _pusher;

        public ChatController(IConfiguration config)
        {
            var options = new PusherOptions
            {
                Cluster = config["Pusher:Cluster"],
                Encrypted = true
            };

            _pusher = new Pusher(
                config["Pusher:AppId"],
                config["Pusher:Key"],
                config["Pusher:Secret"],
                options);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
        {
            var result = await _pusher.TriggerAsync("chat-channel", "new-message", new
            {
                user = message.User,
                message = message.Text
            });

            return Ok(result);
        }
    }

    public class ChatMessage
    {
        public string User { get; set; }
        public string Text { get; set; }
    }
}
