using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.Options.Commands
{
    public record class CreateOptionCommand(CreateOptionDTO DTO)
        : IRequest<ReadOptionDTO>;

}
