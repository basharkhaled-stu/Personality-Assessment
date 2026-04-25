using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.UsersAssessments.Commands;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;
using PersonalityAssessment.Application.Features.UsersAssessments.Queries;

namespace PersonalityAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAssessmentController : ControllerBase
    {
        public readonly IMediator _mediator;

        public UsersAssessmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUsersAssessmentDTO>> GetById(int id)
        {
            var query = new GetUsersAssessmentByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReadUsersAssessmentDTO>> Create(CreateUsersAssessmentDTO dto)
        {
            var userId = User.FindFirst(
            System.Security.Claims.ClaimTypes.NameIdentifier
            )?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var command = new CreateUsersAssessmentCommand(dto, userId);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<AdmainReadUsersAssessmentDTO>> GetByIdAdmain(int id)
        {
            var query = new GetUsersAssessmentByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteUsersAssessmentCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadUsersAssessmentDTO>> Update(int id, [FromBody] UpdateUsersAssessmentDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");
            var userId = "E97C7805-59BD-4BA5-A467-09D03ACDB619";
            var command = new UpdateUsersAssessmentCommand(id, dto, userId);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            var resultUpdate = await _mediator.Send(new GetUsersAssessmentByIdQuery(id));
            return Ok(resultUpdate);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadUsersAssessmentDTO>>> GetAll([FromQuery] PagingParameters pagingParameters)
        {
            var query = new GetAllUsersAssessmentQuery(pagingParameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Submit")]
        public async Task<ActionResult<AssessmentResultDTO>> Submit([FromBody] SubmitAssessmentDTO dto)
        {
            var command = new SubmitAssessmentCommand(dto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("Result/{usersAssessmentId}")]
        public async Task<ActionResult<AssessmentResultDTO>> GetResult(int usersAssessmentId)
        {
            var query = new GetAssessmentResultQuery(usersAssessmentId);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

    }
}
