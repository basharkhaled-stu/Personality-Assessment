using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Strengths.Commands;
using PersonalityAssessment.Application.Features.Strengths.DTO;
using PersonalityAssessment.Application.Features.Strengths.Queries;

namespace PersonalityAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StrengthController : ControllerBase
    {
        public readonly IMediator _mediator;

        public StrengthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadStrengthDTO>> GetById(int id)
        {
            var query = new GetStrengthByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<AdmainReadStrengthDTO>> GetByIdAdmain(int id)
        {
            var query = new GetStrengthByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ReadStrengthDTO>> Create(CreateStrengthDTO dto)
        {
            var command = new CreateStrengthCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadStrengthDTO>> Update(int id, [FromBody] UpdateStrengthDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");
            var command = new UpdateStrengthCommand(id, dto);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            var resultUpdate = await _mediator.Send(new GetStrengthByIdQuery(id));
            return Ok(resultUpdate);
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadStrengthDTO>>> GetAll([FromQuery] PagingParameters pagingParameters)
        {
            var query = new GetAllStrengthQuery(pagingParameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteStrengthCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}
