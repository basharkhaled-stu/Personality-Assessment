using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.OptionPersonalityScores.Commands;
using PersonalityAssessment.Application.Features.OptionPersonalityScores.DTO;
using PersonalityAssessment.Application.Features.OptionPersonalityScores.Queries;

namespace PersonalityAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionPersonalityScoreController : ControllerBase
    {

        public readonly IMediator _mediator;

        public OptionPersonalityScoreController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadOptionPersonalityScoreDTO>> GetById(int id)
        {
            var query = new GetOptionPersonalityScoreByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<AdmainReadOptionPersonalityScoreDTO>> GetByIdAdmain(int id)
        {
            var query = new GetOptionPersonalityScoreByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ReadOptionPersonalityScoreDTO>> Create(CreateOptionPersonalityScoreDTO dto)
        {
            var command = new CreateOptionPersonalityScoreCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadOptionPersonalityScoreDTO>> Update(int id, [FromBody] UpdateOptionPersonalityScoreDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");
            var command = new UpdateOptionPersonalityScoreCommand(id, dto);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            var resultUpdate = await _mediator.Send(new GetOptionPersonalityScoreByIdQuery(id));
            return Ok(resultUpdate);
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadOptionPersonalityScoreDTO>>> GetAll([FromQuery] PagingParameters pagingParameters)
        {
            var query = new GetAllOptionPersonalityScoreQuery(pagingParameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteOptionPersonalityScoreCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}
