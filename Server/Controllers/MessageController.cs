using BL.Api;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageBL _messageBL;

        public MessageController(IMessageBL messageBL)
        {
            _messageBL = messageBL;
        }

        [HttpGet]
        public ActionResult<List<Message>> GetMessages()
        {
            return Ok(_messageBL.GetAllMessages());
        }

        [HttpPost]
        public IActionResult PostMessage([FromBody] Message message)
        {
            _messageBL.AddMessage(message);
            return Ok();
        }
    }
}
