using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.QuestionTypes.Commands;
using PersonalityAssessment.Application.Features.QuestionTypes.DTO;
using PersonalityAssessment.Application.Features.QuestionTypes.Queries;

namespace PersonalityAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionTypeController : ControllerBase
    {
        public readonly IMediator _mediator;

        public QuestionTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadQuestionTypeDTO>> GetById(int id)
        {
            var query = new GetQuestionTypeByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpGet("Admain/{id}")]
        public async Task<ActionResult<AdmainReadQuestionTypeDTO>> GetByIdAdmain(int id)
        {
            var query = new GetQuestionTypeByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ReadQuestionTypeDTO>> Create(CreateQuestionTypeDTO dto)
        {
            var command = new CreateQuestionTypeCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadQuestionTypeDTO>> Update(int id, [FromBody] UpdateQuestionTypeDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");
            var command = new UpdateQuestionTypeCommand(id, dto);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            var UpdateResult = await _mediator.Send(new GetQuestionTypeByIdQuery(id));
            return Ok(UpdateResult);
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadQuestionTypeDTO>>> GetAll([FromQuery] PagingParameters pagingParameters)
        {
            var query = new GetAllQuestionTypeQuery(pagingParameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteQuestionTypeCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
