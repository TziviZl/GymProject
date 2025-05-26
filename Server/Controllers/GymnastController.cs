using BL.Api;
using BL.Models;
using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GymnastController : Controller
    {
        private readonly IGymnastBL _igymnastBL;

        public GymnastController(IGymnastBL igymnastBL)
        {
            _igymnastBL = igymnastBL;
        }

        [HttpPost("NewGymnast")]
        public IActionResult NewGymnast([FromBody][Bind("ID", "FirstName", "LastName", "BirthDate", "MedicalInsurance", "Email", "Cell")] M_Gymnast m_gymnast)
        {

            try {
                _igymnastBL.NewGymnast(m_gymnast);
                return Ok("The gymnast was added successfully.");
            }

            catch (Exception ex) {
                return BadRequest(ex.Message);
            }


        }
        [HttpGet]
        public List<M_ViewGymnastBL> Get()
        {
            return _igymnastBL.GetAllGymnast();
        }


        [HttpPut("AddMembershipType")]
        public IActionResult AddMembershipType([FromQuery] string type, [FromQuery] string id)
        {
            if (Enum.TryParse(type, true, out MembershipTypeEnum result))
            {
                _igymnastBL.AddMembershipType(id, result);
                return Ok((int)result);
            }
            else
            {
                return BadRequest("Invalid membership type.");
            }
        }

        [HttpDelete("RemoveGymnastFromClass")]
        public IActionResult RemoveGymnastFromClass([FromQuery] string gymnastId, [FromQuery] int classId)
        {
            try
            {
                _igymnastBL.RemoveGymnastFromClass(gymnastId, classId);
                return Ok("The gymnast was removed successfully.");
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);

            }

        }

        [HttpPut("GetGymnastById")]
        public ActionResult<Gymnast> GetGymnastById([FromQuery] string id)
        {
            try
            {
                _igymnastBL.GetGymnastById(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateGymnast")]
        public IActionResult UpdateGymnast([FromQuery][Bind("ID", "FirstName", "LastName", "BirthDate", "MedicalInsurance")] M_Gymnast m_gymnast)
        {
            try
            {
                _igymnastBL.UpdateGymnanst(m_gymnast);
                return Ok("the gymnast update succsessfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteGymnast")]
        public IActionResult DeleteGymnast(string id)
        {
            try
            {
                _igymnastBL.DeleteGymnast(id);
                return Ok("The gymnast was removed successfully.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddGymnastLesson")]
        public IActionResult AddGymnastLesson(string gymnastId, StudioClass studioClass)
        {
            try
            {
                _igymnastBL.AddGymnastLesson(gymnastId, studioClass);
                return Ok("You have successfully registered for the class!");


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpDelete("RemoveGymnastFromLesson")]
        public IActionResult RemoveGymnastFromLesson(string gymnastId, StudioClass studioClass)
        {
            try
            {
                _igymnastBL.RemoveGymnastFromLesson(gymnastId, studioClass);
                return Ok("The gymnast was removed from the Lesson successfully.!");


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        [HttpGet("GetGymnastLessons")]
        public ActionResult<List<M_ViewStudioClasses>> GetGymnastLessons(string gymnastId, int numOfLesson)
        {
            try
            {
                var lessons = _igymnastBL.GetGymnastLessons(gymnastId, numOfLesson);
                return Ok(lessons);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllGymnastInSpecificClass")]
        public ActionResult<List<M_ViewStudioClasses>> GetAllGymnastInSpecificClass(StudioClass studioClass) 
        {
            try
            {
                var gymnasts = _igymnastBL.GetAllGymnastInSpecificClass(studioClass);
                return Ok(gymnasts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}




