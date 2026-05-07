using MediatR;
using PersonalityAssessment.Application.Features.PersonalityTypes.DTO;

namespace PersonalityAssessment.Application.Features.PersonalityTypes.Commands
{
    public record UpdatePersonalityTypeCommand(int id, UpdatePersonalityTypeDTO dto)
       : IRequest<bool>;
}
