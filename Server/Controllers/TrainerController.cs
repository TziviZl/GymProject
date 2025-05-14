using BL;
using BL.Api;
using BL.Models;
using BL.Services;
using DAL.Models;
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
            return _itrainerBL.GetAllTrainers();
        }
       
        [HttpPost("NewTrainer")]
        public IActionResult NewTrainer([FromQuery][Bind("ID", "FirstName", "LastName", "BirthDate", "Specialization")] M_Trainer m_Trainer)
        {

            if (m_Trainer == null)
            {
                return BadRequest("Invalid gymnast data.");

            }
            Trainer trainer = m_Trainer.Convert();
            bool isAdded = _itrainerBL.NewTrainer(trainer);

            if (isAdded)
            {
                return Ok("The trainer was added successfully.");
            }
            else
            {
                return StatusCode(500, "Failed to add the trainer.");
            }
        }

        [HttpGet("GetTrainerSudioClasses")]
        public List<ModelStudioClasses> GetTrainerSudioClasses([FromQuery] [Bind] string trainerId)
        {
            return _itrainerBL.GetStudioClasses(trainerId);
        }


    }
}
