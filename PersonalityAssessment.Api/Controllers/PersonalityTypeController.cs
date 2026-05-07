using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.PersonalityTypes.Commands;
using PersonalityAssessment.Application.Features.PersonalityTypes.DTO;
using PersonalityAssessment.Application.Features.PersonalityTypes.Queries;

namespace PersonalityAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalityTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonalityTypeController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadPersonalityTypeDTO>> GetById(int id)
        {
            var query = new GetPersonalityTypeByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ReadPersonalityTypeDTO>> Create([FromBody] CreatePersonalityTypeDTO dto)
        {
            var command = new CreatePersonalityTypeCommand(dto);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/{id}")]
        public async Task<IActionResult> GetByIdAdmain(int id)
        {
            var query = new GetPersonalityTypeByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadPersonalityTypeDTO>> Update(int id, [FromBody] UpdatePersonalityTypeDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");
            var command = new UpdatePersonalityTypeCommand(id, dto);
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();
            var resultupdate = await _mediator.Send(new GetPersonalityTypeByIdQuery(id));
            return Ok(resultupdate);

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeletePersonalityTypeCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadPersonalityTypeDTO>>> GetAll([FromQuery] PagingParameters pagingParameters)
        {
            var query = new GetAllPersonalityTypeQuery(pagingParameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
