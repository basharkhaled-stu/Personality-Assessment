using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Weaknesses.Commands;
using PersonalityAssessment.Application.Features.Weaknesses.DTO;
using PersonalityAssessment.Application.Features.Weaknesses.Queries;

namespace PersonalityAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeakneesController : ControllerBase
    {
        public readonly IMediator _mediator;

        public WeakneesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadWeakneesDTO>> GetById(int id)
        {
            var query = new GetWeakneesByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<AdmainReadWeaknessDTO>> GetByIdAdmain(int id)
        {
            var query = new GetWeakneesByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ReadWeakneesDTO>> Create(CreateWeaknessDTO dto)
        {
            var command = new CreateWeaknessCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadWeakneesDTO>> Update(int id, [FromBody] UpdateWeaknessDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");
            var command = new UpdateWeaknessCommand(id, dto);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            var resultUpdate = await _mediator.Send(new GetWeakneesByIdQuery(id));
            return Ok(resultUpdate);
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadWeakneesDTO>>> GetAll([FromQuery] PagingParameters pagingParameters)
        {
            var query = new GetAllWeakneesQuery(pagingParameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteWeaknessCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}
