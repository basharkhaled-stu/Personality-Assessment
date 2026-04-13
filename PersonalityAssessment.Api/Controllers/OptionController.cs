using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Options.Commands;
using PersonalityAssessment.Application.Features.Options.DTO;
using PersonalityAssessment.Application.Features.Options.Queries;

namespace PersonalityAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        public readonly IMediator _mediator;

        public OptionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadOptionDTO>> GetById(int id)
        {
            var query = new GetOptionByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<AdmainReadOptionDTO>> GetByIdAdmain(int id)
        {
            var query = new GetOptionByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ReadOptionDTO>> Create(CreateOptionDTO dto)
        {
            var command = new CreateOptionCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadOptionDTO>> Update(int id, [FromBody] UpdateOptionDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");
            var command = new UpdateOptionCommand(id, dto);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            var resultUpdate = await _mediator.Send(new GetOptionByIdQuery(id));
            return Ok(resultUpdate);
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadOptionDTO>>> GetAll([FromQuery] PagingParameters pagingParameters)
        {
            var query = new GetAllOptionQuery(pagingParameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
       

        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteOptionCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}
