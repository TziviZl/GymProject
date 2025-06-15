using BL.Api;
using BL.Models;
using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

    }
}
