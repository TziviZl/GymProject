using BL.Api;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerBL _itrainerBL;
        public TrainerController(ITrainerBL itrainerBL)
        {
            _itrainerBL = itrainerBL;
        }
        [HttpGet("GetNumOfGymnasts")]
        public int GetNumOfGymnasts([FromQuery] string trainerId, [FromQuery] DateTime courseDate)
        {
            return _itrainerBL.GetNumOfGymnasts(trainerId, courseDate); 
        }
    }
}
