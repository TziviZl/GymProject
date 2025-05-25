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
        public IActionResult NewGymnast([FromBody][Bind("ID", "FirstName", "LastName", "BirthDate", "MedicalInsurance","Email","Cell")] M_Gymnast m_gymnast)
        {
            if (m_gymnast == null)
            {
                return BadRequest("Invalid gymnast data.");

            }
            bool isAdded = _igymnastBL.NewGymnast(m_gymnast);

            if (isAdded)
            {
                return Ok("The gymnast was added successfully.");
            }
            else
            {
                return StatusCode(500, "Failed to add the gymnast.");
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

            if (!_igymnastBL.RemoveGymnastFromClass(gymnastId, classId))
                return BadRequest("The trainee is not registered for the class.");

            _igymnastBL.RemoveGymnastFromClass(gymnastId, classId);
            return Ok("The trainee was successfully removed from the lesson.");
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
            if (m_gymnast == null)
            {
                return BadRequest("Invalid gymnast data.");

            }
            try
            {
                _igymnastBL.UpdateGymnanst(m_gymnast);
                return Ok();
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
    }


}




