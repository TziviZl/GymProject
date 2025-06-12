using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private static Dictionary<string, string> VerificationCodes = new();

    [HttpPost("SendCode")]
    public IActionResult SendCode([FromBody] string phone)
    {
        var random = new Random();
        var code = random.Next(1000, 9999).ToString();

        VerificationCodes[phone] = code;

        Console.WriteLine($"📞 Phone call to {phone}: Your verification code is {code}");

        return Ok();
    }

    [HttpPost("VerifyCode")]
    public IActionResult VerifyCode([FromBody] CodeVerificationRequest request)
    {
        if (VerificationCodes.TryGetValue(request.Phone, out var realCode))
        {
            if (realCode == request.Code)
            {
                return Ok(true);
            }
        }

        return BadRequest("קוד שגוי");
    }

    public class CodeVerificationRequest
    {
        public string Phone { get; set; }
        public string Code { get; set; }
    }
}
