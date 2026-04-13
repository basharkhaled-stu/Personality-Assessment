using MediatR;

namespace PersonalityAssessment.Application.Features.Options.Commands
{
    public record DeleteappUserCommand(int id) : IRequest<bool>;
}
