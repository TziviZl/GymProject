using BL.Api;
using BL.Models;
using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Controllers;

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

            
                _igymnastBL.NewGymnast(m_gymnast);
                return Ok("The gymnast was added successfully.");
           

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
           
                _igymnastBL.RemoveGymnastFromClass(gymnastId, classId);
                return Ok("The gymnast was removed successfully.");
           

        }

        [HttpGet("GetGymnastById")]
        public ActionResult<Gymnast> GetGymnastById([FromQuery] string id)
        {
                return Ok(_igymnastBL.GetGymnastById(id));

        }
        [HttpPut("UpdateGymnast")]
        public IActionResult UpdateGymnast([FromBody][Bind("Id", "FirstName", "LastName", "BirthDate", "MedicalInsurance", "Email", "Cell", "Level")] M_Gymnast m_gymnast)
        {
           
                _igymnastBL.UpdateGymnanst(m_gymnast);
                return Ok("the gymnast update succsessfully");
            

        }

        [HttpDelete("DeleteGymnast")]
        public IActionResult DeleteGymnast(string id)
        {
                _igymnastBL.DeleteGymnast(id);
                return Ok("The gymnast was removed successfully.");

        }
        [HttpPost("AddGymnastLesson")]
        public IActionResult AddGymnastLesson([FromQuery] string gymnastId, [FromQuery] int studioClassId)
        {
            
                _igymnastBL.AddGymnastLesson(gymnastId, studioClassId);
                return Ok("You have successfully registered for the class!");
           

        }


        [HttpDelete("RemoveGymnastFromLesson")]
        public IActionResult RemoveGymnastFromLesson(string gymnastId, StudioClass studioClass)
        {
                _igymnastBL.RemoveGymnastFromLesson(gymnastId, studioClass);
                return Ok("The gymnast was removed from the Lesson successfully.!");



        }

        [HttpGet("GetGymnastLessons")]
        public ActionResult<List<M_ViewStudioClasses>> GetGymnastLessons(string gymnastId)
        {
                var lessons = _igymnastBL.GetGymnastLessons(gymnastId);
                return Ok(lessons);
          
        }
        [HttpGet("GetAllGymnastInSpecificClass")]
        public ActionResult<List<M_ViewStudioClasses>> GetAllGymnastInSpecificClass(int studioClassId)
        {
                var gymnasts = _igymnastBL.GetAllGymnastInSpecificClass(studioClassId);
                return Ok(gymnasts);
          
        }
        [HttpGet("GetAllGymnastInSpecificLevel")]
        public ActionResult<List<M_ViewGymnast>> GetAllGymnastInSpecificLevel(char level)
        {
                var gymnasts = _igymnastBL.GetAllGymnastInSpecificLevel(level);
                return Ok(gymnasts);
           
        }
        [HttpGet("GetAllGymnastByAge")]
        public ActionResult<List<M_ViewGymnast>> GetAllGymnastByAge(int minAge, int maxAge)

        {
                var gymnasts = _igymnastBL.GetAllGymnastByAge(minAge,maxAge);
                return Ok(gymnasts);
           
        }
        [HttpGet("GetAllGymnastByMembershipType")]
        public ActionResult<List<M_ViewGymnast>> GetAllGymnastByMembershipType(MembershipTypeEnum membershipType)

        {
           
                var gymnasts = _igymnastBL.GetAllGymnastByMembershipType(membershipType);
                return Ok(gymnasts);
          
        }
        [HttpGet("GetAllGymnastJoinedAfter")]
        public ActionResult<List<M_ViewGymnast>> GetAllGymnastJoinedAfter([FromQuery] DateOnly joinDate)
        {
            var result = _igymnastBL.GetAllGymnastJoinedAfter(joinDate);
            return Ok(result);
        }




    }
}




