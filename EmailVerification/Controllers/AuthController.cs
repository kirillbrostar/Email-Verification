using Microsoft.AspNetCore.Mvc;
using EmailVerification.Services;
using EmailVerification.Models;
using System.Threading.Tasks;

namespace EmailVerification.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IEmailQueueService _emailService;

        public AuthController(IEmailQueueService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> SendCode([FromBody] EmailRequest request)
        {
            var success = await _emailService.SendVerificationCodeAsync(request.Email);
            return success ? Ok() : BadRequest("Повторная отправка возможна через 30 секунд");
        }

        [HttpPost("resend-code")]
        public async Task<IActionResult> ResendCode([FromBody] EmailRequest request)
        {
            var success = await _emailService.SendVerificationCodeAsync(request.Email);
            return success ? Ok() : BadRequest("Повторная отправка возможна через 30 секунд");
        }
    }
}