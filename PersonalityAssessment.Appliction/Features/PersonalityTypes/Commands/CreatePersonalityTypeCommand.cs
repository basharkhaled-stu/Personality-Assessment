using MediatR;
using PersonalityAssessment.Application.Features.PersonalityTypes.DTO;

namespace PersonalityAssessment.Application.Features.PersonalityTypes.Commands
{
    public record class CreatePersonalityTypeCommand(CreatePersonalityTypeDTO DTO)
        : IRequest<ReadPersonalityTypeDTO>;

}
