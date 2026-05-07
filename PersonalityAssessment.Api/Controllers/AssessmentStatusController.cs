using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.AssessmentStatuses.Commands;
using PersonalityAssessment.Application.Features.AssessmentStatuses.DTO;
using PersonalityAssessment.Application.Features.AssessmentStatuses.Queries;
namespace PersonalityAssessment.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentStatusController : ControllerBase
    {
        public readonly IMediator _mediator;

        public AssessmentStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadAssessmentStatusDTO>> GetById(int id)
        {
            var query = new GetAssessmentStatusByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<ReadAssessmentStatusDTO>> Create(CreateAssessmentStatusDTO dto)
        {
            var command = new CreateAssessmentStatusCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadAssessmentStatusDTO>> Update(int id, [FromBody] UpdateAssessmentStatusDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");
            var command = new UpdateAssessmentStatusCommand(id, dto);
            var result = await _mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadAssessmentStatusDTO>>> GetAll([FromQuery] PagingParameters pagingParameters)
        {
            var query = new GetAllAssessmentStatusQuery(pagingParameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteAssessmentStatusCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<AdmainReadAssessmentStatusDTO>> GetByIdAdmain(int id)
        {
            var query = new GetAssessmentStatusesByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
