using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.AssessmentTypes.Commands;
using PersonalityAssessment.Application.Features.AssessmentTypes.DTO;
using PersonalityAssessment.Application.Features.AssessmentTypes.Queries;

namespace PersonalityAssessment.Api.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentTypeController : ControllerBase
    {
        public readonly IMediator _mediator;

        public AssessmentTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadAssessmentTypeDTO>> GetById(int id)
        {
            var query = new GetAssessmentTypeByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<AdmainReadAssessmentTypeDTO>> GetByIdAdmain(int id)
        {
            var query = new GetAssessmentTypeByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateAssessmentTypeDTO dto)
        {
            var command = new CreateAssessmentTypeCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadAssessmentTypeDTO>> Update(int id, [FromBody] UpdateAssessmentTypeDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");
            var command = new UpdateAssessmentTypeCommand(id, dto);
            var result = await _mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadAssessmentTypeDTO>>> GetAll([FromQuery] PagingParameters pagingParameters)
        {
            var query = new GetAllAssessmentTypeQuery(pagingParameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteAssessmentTypeCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}
