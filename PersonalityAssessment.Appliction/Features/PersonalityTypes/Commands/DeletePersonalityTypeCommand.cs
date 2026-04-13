using MediatR;

namespace PersonalityAssessment.Application.Features.PersonalityTypes.Commands
{
    public record DeletePersonalityTypeCommand(int id) : IRequest<bool>;
}
