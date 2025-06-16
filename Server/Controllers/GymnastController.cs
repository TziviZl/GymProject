using BL.Api;
using BL.Models;
using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
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

        [HttpGet("GetGymnastById")]
        public ActionResult<Gymnast> GetGymnastById([FromQuery] string id)
        {
            try
            {
                return Ok(_igymnastBL.GetGymnastById(id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateGymnast")]
        public IActionResult UpdateGymnast([FromBody][Bind("ID", "FirstName", "LastName", "BirthDate", "MedicalInsurance","Level")] M_Gymnast m_gymnast)
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
        public IActionResult AddGymnastLesson([FromQuery] string gymnastId, [FromQuery] int studioClassId)
        {
            try
            {
                _igymnastBL.AddGymnastLesson(gymnastId, studioClassId);
                return Ok("You have successfully registered for the class!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("== ERROR on AddGymnastLesson ==");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                Console.WriteLine(ex.InnerException?.InnerException?.Message);
                return BadRequest(ex.InnerException?.InnerException?.Message ?? ex.Message);
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
        public ActionResult<List<M_ViewStudioClasses>> GetGymnastLessons(string gymnastId)
        {
            try
            {
                var lessons = _igymnastBL.GetGymnastLessons(gymnastId);
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
        [HttpGet("GetAllGymnastInSpecificLevel")]
        public ActionResult<List<M_ViewGymnast>> GetAllGymnastInSpecificLevel(char level)
        {
            try
            {
                var gymnasts = _igymnastBL.GetAllGymnastInSpecificLevel(level);
                return Ok(gymnasts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllGymnastByAge")]
        public ActionResult<List<M_ViewGymnast>> GetAllGymnastByAge(int minAge, int maxAge)

        {
            try
            {
                var gymnasts = _igymnastBL.GetAllGymnastByAge(minAge,maxAge);
                return Ok(gymnasts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllGymnastByMembershipType")]
        public ActionResult<List<M_ViewGymnast>> GetAllGymnastByMembershipType(MembershipTypeEnum membershipType)

        {
            try
            {
                var gymnasts = _igymnastBL.GetAllGymnastByMembershipType(membershipType);
                return Ok(gymnasts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllGymnastJoinedAfter")]
        public ActionResult<List<M_ViewGymnast>> GetAllGymnastJoinedAfter([FromQuery] DateOnly joinDate)
        {
            var result = _igymnastBL.GetAllGymnastJoinedAfter(joinDate);
            return Ok(result);
        }




    }
}




