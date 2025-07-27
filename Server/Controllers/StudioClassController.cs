using BL.Api;
using BL.Models;
using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Middleware;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudioClassController:ControllerBase
    {
        private readonly IStudioClassBL _iStudioClass;
        public StudioClassController(IBL iStudioClass)
        {
            _iStudioClass = iStudioClass.StudioClass ;
        }
        [HttpGet("GetAllLessons")]
        public ActionResult<List<M_ViewStudioClasses>> GetAllLessons()
        {
            try
            {
                var lessons = _iStudioClass.GetAllLessons();
                return Ok(lessons);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("isFull")]
        public bool IsFull(int studioClassId)
        {
            return _iStudioClass.IsFull(studioClassId);
        }

        [HttpPost("CancelClass")]
        public IActionResult CancelClass([FromQuery] int classId)
        {
           
                bool result = _iStudioClass.CancelClassAndNotifyGymnasts(classId);
                if (result)
                    return Ok($"Class {classId} was successfully cancelled and gymnasts were notified (console log).");

                return BadRequest("Failed to cancel the class.");
          
        }

        [HttpGet("isCancelled")]
        public IActionResult IsCancelled([FromQuery] int studioClassId)
        {
          
                bool isCancelled = _iStudioClass.IsCancelled(studioClassId);
                return Ok(isCancelled);
           
        }



    }
}
