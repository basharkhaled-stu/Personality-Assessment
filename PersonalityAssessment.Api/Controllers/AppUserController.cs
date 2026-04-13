using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Features.Options.Commands;
using PersonalityAssessment.Application.Features.Options.DTO;
using PersonalityAssessment.Application.Features.Options.Queries;
using System.Security.Claims;

namespace PersonalityAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        public IMediator _Mediator { get; set; }

        public AppUserController(IMediator mediator)
        {
            _Mediator = mediator;
        }



        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterappUserDTO dto)
        {
            var commamd = new CreateappUserCommand(dto);
            var result = await _Mediator.Send(commamd);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginappUserDTO dto)
        {
            var query = new GetappUserByUserNameQuery(dto);
            var result = await _Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("LoginByEmail")]
        public async Task<IActionResult> LoginByEmail(LoginByEmailappUserDTO dto)
        {
            var query = new GetappUserByEmailQuery(dto);
            var result = await _Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordappUserDTO dto)
        {
            var result = await _Mediator.Send(new ForgetPasswordappUserCommand(dto));
            return Ok(result);
        }

        [HttpPost("GoogleLogin")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginappUserDTO dto)
        {
            var token = await _Mediator.Send(new GoogleLoginappUserCommand(dto));
            if (string.IsNullOrEmpty(token))
                return Unauthorized();
            return Ok(token);
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _Mediator.Send(new LogoutappUserCommand());
            return Ok(result);
        }

        [Authorize]
        [HttpPut("Username")]
        public async Task<IActionResult> UpdateUsername([FromBody] UpdateUsernameappUserDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var ok = await _Mediator.Send(new UpdateAppUserUsernameCommand(userId, dto));
            return ok ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpPut("FirstName")]
        public async Task<IActionResult> UpdateFirstName([FromBody] UpdateFirstNameappUserDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var ok = await _Mediator.Send(new UpdateAppUserFirstNameCommand(userId, dto));
            return ok ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpPut("LastName")]
        public async Task<IActionResult> UpdateLastName([FromBody] UpdateLastNameappUserDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var ok = await _Mediator.Send(new UpdateAppUserLastNameCommand(userId, dto));
            return ok ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpPut("Email")]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailappUserDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var ok = await _Mediator.Send(new UpdateAppUserEmailCommand(userId, dto));
            return ok ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpPut("Password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordappUserDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var ok = await _Mediator.Send(new UpdateAppUserPasswordCommand(userId, dto));
            return ok ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpPut("Profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileappUserDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var ok = await _Mediator.Send(new UpdateAppUserProfileCommand(userId, dto));
            return ok ? Ok() : BadRequest();
        }

        [Authorize]
        [HttpPut("ProfileImage")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfileImage(IFormFile file)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (file == null || file.Length == 0)
                return BadRequest();

            await using var stream = file.OpenReadStream();
            var ok = await _Mediator.Send(new UpdateAppUserProfileImageCommand(
                userId,
                stream,
                file.FileName,
                file.Length));

            return ok ? Ok() : BadRequest();
        }




    }
}
