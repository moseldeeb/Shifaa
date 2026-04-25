using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Shifaa.DTOs.Response;
using Shifaa.JwtFeatures;
using Shifaa.Services;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shifaa.Areas.Identity.Controllers
{
    [Route("[Area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register/member")]
        public async Task<IActionResult> RegisterMember(MemberRegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _authService.RegisterMemberAsync(request);
            return StatusCode(result.StatusCode, new { success = result.Success, message = result.Message });
        }

        [HttpPost("register/caregiver")]
        public async Task<IActionResult> RegisterCaregiver(CaregiverRegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _authService.RegisterCaregiverAsync(request);
            return StatusCode(result.StatusCode, new { success = result.Success, message = result.Message });
        }

        [HttpPost("register/doctor")]
        public async Task<IActionResult> RegisterDoctor(DoctorRegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _authService.RegisterDoctorAsync(request);
            return StatusCode(result.StatusCode, new { success = result.Success, message = result.Message });
        }

        [HttpPost("register/medical-center")]
        public async Task<IActionResult> RegisterMedicalCenter(MedicalCenterRegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _authService.RegisterMedicalCenterAsync(request);
            return StatusCode(result.StatusCode, new { success = result.Success, message = result.Message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _authService.LoginAsync(request);
            return StatusCode(result.StatusCode, new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgetPasswordRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _authService.ForgotPasswordAsync(request.UserNameOrEmail);
            return StatusCode(result.StatusCode, new { success = result.Success, message = result.Message });
        }

        [HttpPost("validate-otp")]
        public async Task<IActionResult> ValidateOtp(ValidateOTPRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _authService.ValidateOtpAsync(request);
            return StatusCode(result.StatusCode, new { success = result.Success, message = result.Message });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _authService.ResetPasswordAsync(request);
            return StatusCode(result.StatusCode, new { success = result.Success, message = result.Message });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(TokenApiRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _authService.RefreshTokenAsync(request);
            return StatusCode(result.StatusCode, new { success = result.Success, message = result.Message, data = result.Data });
        }
    }
}
