using BL.Api;
using BL.Models;
using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                bool result = _iStudioClass.CancelClassAndNotifyGymnasts(classId);
                if (result)
                    return Ok($"Class {classId} was successfully cancelled and gymnasts were notified (console log).");

                return BadRequest("Failed to cancel the class.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("isCancelled")]
        public IActionResult IsCancelled([FromQuery] int studioClassId)
        {
            try
            {
                bool isCancelled = _iStudioClass.IsCancelled(studioClassId);
                return Ok(isCancelled);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }



    }
}
