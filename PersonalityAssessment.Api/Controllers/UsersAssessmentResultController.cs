using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.QuestionTypes.DTO;
using PersonalityAssessment.Application.Features.UsersAssessmentResults.Commands;
using PersonalityAssessment.Application.Features.UsersAssessmentResults.DTO;
using PersonalityAssessment.Application.Features.UsersAssessmentResults.Queries;

namespace PersonalityAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAssessmentResultController : ControllerBase
    {
        public readonly IMediator _mediator;

        public UsersAssessmentResultController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUsersAssessmentResultDTO>> GetById(int id)
        {
            var query = new GetUsersAssessmentResultByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<AdmainReadUsersAssessmentResultDTO>> GetByIdAdmain(int id)
        {
            var query = new GetUsersAssessmentResultByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ReadUsersAssessmentResultDTO>> Create(CreateUsersAssessmentResultDTO dto)
        {
            var command = new CreateUsersAssessmentResultCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadUsersAssessmentResultDTO>> Update(int id, [FromBody] UpdateUsersAssessmentResultDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");
            var command = new UpdateUsersAssessmentResultCommand(id, dto);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            var UpdateResult = await _mediator.Send(new GetUsersAssessmentResultByIdQuery(id));
            return Ok(UpdateResult);
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadQuestionTypeDTO>>> GetAll([FromQuery] PagingParameters pagingParameters)
        {
            var query = new GetAllUsersAssessmentResultQuery(pagingParameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteUsersAssessmentResultCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
