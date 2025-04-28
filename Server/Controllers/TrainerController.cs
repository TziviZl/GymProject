using BL;
using BL.Api;
using BL.Models;
using BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerBL _itrainerBL;//=new TrainerBL();
        public TrainerController(IBL itrainerBL)
        {
            _itrainerBL = itrainerBL.Trainers ;
        }
        [HttpGet("GetNumOfGymnasts")]
        public int GetNumOfGymnasts([FromQuery] string trainerId, [FromQuery] DateTime courseDate)
        {
            return _itrainerBL.GetNumOfGymnasts(trainerId, courseDate); 
        }
        [HttpGet]
        public List<ModelTrainerBL> Get()
        {
            return _itrainerBL.GetList();
        }
    }
}
