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
        public GymnastController(IBL igymnastBL)
        {
            _igymnastBL = igymnastBL.Gymnasts;
        }

        [HttpPost("NewGymnast")]
        public IActionResult NewGymnast([FromQuery][Bind("ID", "FirstName", "LastName", "BirthDate", "MedicalInsurance")] M_Gymnast m_gymnast)
        {
            if (m_gymnast == null)
            {
                return BadRequest("Invalid gymnast data.");

            }
            Gymnast gymnast = m_gymnast.Convert();
            bool isAdded = _igymnastBL.NewGymnast(gymnast);

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
        public List<ModelGymnastBL> Get()
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
        [HttpDelete("DeleteGymnast")]
        public IActionResult DeleteGymnast(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id.Length != 9 || !id.All(char.IsDigit))
                return BadRequest("No ID provided.");

            int result = _igymnastBL.DeleteGymnast(id);

            switch (result)
            {
                case 1:
                    return Ok("The gymnast was removed successfully.");
                case 0:
                    return NotFound("No gymnast found with the provided ID.");
                case -1:
                    return StatusCode(500, "An error occurred while trying to remove the gymnast.");
                default:
                    return StatusCode(500, "Unexpected error.");
            }
        }


    }
}
