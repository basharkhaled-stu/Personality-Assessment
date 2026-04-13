using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Options.Queries;
using PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Commands;
using PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.DTO;
using PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Queries;

namespace PersonalityAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAssessmentResultPersonalityTypeController : ControllerBase
    {
        public readonly IMediator _mediator;

        public UsersAssessmentResultPersonalityTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUsersAssessmentResultPersonalityTypeDTO>> GetById(int id)
        {
            var query = new GetUsersAssessmentResultPersonalityTypeByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<AdmainReadUsersAssessmentResultPersonalityTypeDTO>> GetByIdAdmain(int id)
        {
            var query = new GetUsersAssessmentResultPersonalityTypeByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ReadUsersAssessmentResultPersonalityTypeDTO>> Create(CreateUsersAssessmentResultPersonalityTypeDTO dto)
        {
            var command = new CreateUsersAssessmentResultPersonalityTypeCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadUsersAssessmentResultPersonalityTypeDTO>> Update(int id, [FromBody] UpdateUsersAssessmentResultPersonalityTypeDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");
            var command = new UpdateUsersAssessmentResultPersonalityTypeCommand(id, dto);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            var resultUpdate = await _mediator.Send(new GetUsersAssessmentResultPersonalityTypeByIdQuery(id));
            return Ok(resultUpdate);
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadUsersAssessmentResultPersonalityTypeDTO>>> GetAll([FromQuery] PagingParameters pagingParameters)
        {
            var query = new GetAllOptionQuery(pagingParameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteUsersAssessmentResultPersonalityTypeCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
