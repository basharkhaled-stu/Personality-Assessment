using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Services;

namespace PersonalityAssessment.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminModuleAppService _adminModule;

        public AdminController(IAdminModuleAppService adminModule)
        {
            _adminModule = adminModule;
        }

        [HttpGet("Dashboard")]
        public async Task<IActionResult> Dashboard(CancellationToken cancellationToken)
        {
            var dto = await _adminModule.GetDashboardAsync(cancellationToken);
            return Ok(dto);
        }

        [HttpGet("Users")]
        public async Task<IActionResult> Users([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
        {
            var dto = await _adminModule.GetUsersAsync(page, pageSize, cancellationToken);
            return Ok(dto);
        }

        [HttpGet("Users/{id}")]
        public async Task<IActionResult> GetUser(string id, CancellationToken cancellationToken)
        {
            var dto = await _adminModule.GetUserByIdAsync(id, cancellationToken);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpDelete("Users/{id}")]
        public async Task<IActionResult> DeleteUser(string id, CancellationToken cancellationToken)
        {
            var ok = await _adminModule.DeleteUserAsync(id, cancellationToken);
            return ok ? NoContent() : NotFound();
        }

        [HttpGet("Assessments")]
        public async Task<IActionResult> Assessments(CancellationToken cancellationToken)
        {
            var list = await _adminModule.GetAssessmentsAsync(cancellationToken);
            return Ok(list);
        }

        [HttpGet("Assessments/{id:int}")]
        public async Task<IActionResult> GetAssessment(int id, CancellationToken cancellationToken)
        {
            var dto = await _adminModule.GetAssessmentByIdAsync(id, cancellationToken);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpDelete("Assessments/{id:int}")]
        public async Task<IActionResult> DeleteAssessment(int id, CancellationToken cancellationToken)
        {
            var ok = await _adminModule.DeleteAssessmentAsync(id, cancellationToken);
            return ok ? NoContent() : NotFound();
        }

        [HttpGet("Analytics")]
        public async Task<IActionResult> Analytics([FromQuery] int days = 30, CancellationToken cancellationToken = default)
        {
            var dto = await _adminModule.GetAnalyticsAsync(days, cancellationToken);
            return Ok(dto);
        }

        [HttpGet("SystemHealth")]
        public async Task<IActionResult> SystemHealth(CancellationToken cancellationToken)
        {
            var dto = await _adminModule.GetSystemHealthAsync(cancellationToken);
            return Ok(dto);
        }
    }
}
