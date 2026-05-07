using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.UserAssessmentStatuses.Commands;
using PersonalityAssessment.Application.Features.UserAssessmentStatuses.DTO;
using PersonalityAssessment.Application.Features.UserAssessmentStatuses.Queries;

namespace PersonalityAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAssessmentStatusController : ControllerBase
    {
        public readonly IMediator _mediator;

        public UserAssessmentStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUserAssessmentStatusDTO>> GetById(int id)
        {
            var query = new GetUserAssessmentStatusByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<AdmainUserAssessmentStatusDTO>> GetByIdAdmain(int id)
        {
            var query = new GetUserAssessmentStatusByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ReadUserAssessmentStatusDTO>> Create(CreateUserAssessmentStatusDTO dto)
        {
            var command = new CreateUserAssessmentStatusCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadUserAssessmentStatusDTO>> Update(int id, [FromBody] UpdateUserAssessmentStatusDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");
            var command = new UpdateUserAssessmentStatusCommand(id, dto);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            var UpdateResult = await _mediator.Send(new GetUserAssessmentStatusByIdQuery(id));
            return Ok(UpdateResult);
            ;
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadUserAssessmentStatusDTO>>> GetAll([FromQuery] PagingParameters pagingParameters)
        {
            var query = new GetAllUserAssessmentStatusQuery(pagingParameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteUserAssessmentStatusCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}
