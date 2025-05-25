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
        public List<M_ViewTrainerBL> Get()
        {
            return _itrainerBL.GetAllTrainers();
        }
       
        [HttpPost("NewTrainer")]
        public IActionResult NewTrainer([FromQuery][Bind("ID", "FirstName", "LastName", "BirthDate", "Specialization")] M_Trainer m_Trainer)
        {
            try
            {
                _itrainerBL.NewTrainer(m_Trainer);
                return Ok("The trainer was added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

           
        }

        [HttpGet("GetTrainerSudioClasses")]
        public List<M_ViewStudioClasses> GetTrainerSudioClasses([FromQuery] [Bind] string trainerId)
        {
            return _itrainerBL.GetStudioClasses(trainerId);
        }

        [HttpPut("UpdateTrainer")]
        public IActionResult UpdateTrainer([FromBody][Bind] Trainer trainer)
        {
            try
            {
                _itrainerBL.UpdateTrainer(trainer);
                return Ok("The trainer was updated successfully.");
            }
            
              catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetTrainerById")]
        public IActionResult GetTrainerById([FromQuery] string trainerId)
        {
            try
            {
                var trainer = _itrainerBL.GetTrainerById(trainerId);
                return Ok(trainer); 
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
