using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Questions.Commands;
using PersonalityAssessment.Application.Features.Questions.DTO;
using PersonalityAssessment.Application.Features.Questions.Queries;

namespace PersonalityAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        public readonly IMediator _mediator;

        public QuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadQuestionDTO>> GetById(int id)
        {
            var query = new GetQuestionByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<AdmainReadQuestionDTO>> GetByIdAdmain(int id)
        {
            var query = new GetQuestionByIdAdmainQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ReadQuestionDTO>> Create(CreateQuestionDTO dto)
        {
            var command = new CreateQuestionCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadQuestionDTO>> Update(int id, [FromBody] UpdateQuestionDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID does not match body ID.");
            var command = new UpdateQuestionCommand(id, dto);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            var resultUpdate = await _mediator.Send(new GetQuestionByIdQuery(id));
            return Ok(resultUpdate);
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadQuestionDTO>>> GetAll([FromQuery] PagingParameters pagingParameters)
        {
            var query = new GetAllQuestionQuery(pagingParameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteQuestionCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}
