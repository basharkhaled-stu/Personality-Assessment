using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Assessmentes.Commands;
using PersonalityAssessment.Application.Features.Assessmentes.DTO;
using PersonalityAssessment.Application.Features.Assessmentes.Queries;
using PersonalityAssessment.Application.Features.Assessments.Queries;


namespace PersonalityAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssessmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadAssessmentDTO>>> GetAll([FromQuery] PagingParameters p)
        {
            var query = new GetAllAssessmentQuery(p);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ReadAssessmentDTO>> GetById(int id)
        {
            var query = new GetAssessmentByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/{id}")]
        public async Task<ActionResult<AdmainReadAssessmentDTO>> GetByIdAdmin(int id)
        {
            var query = new GetAssessmentByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ReadAssessmentDTO>> Create([FromBody] CreateAssessmentDTO dto)
        {
            var command = new CreateAssessmentCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.id }, result);


        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadAssessmentDTO>> Update(int id, [FromBody] UpdateAssessmentDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");

            var command = new UpdateAssessmentCommand(id, dto);
            var Updateresult = await _mediator.Send(command);
            if (!Updateresult)
                return NotFound();

            var result = await _mediator.Send(new GetAssessmentByIdQuery(id));
            return Ok(result);


        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteAssessmentCommand(id);
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();

            return NoContent();


        }
    }
}