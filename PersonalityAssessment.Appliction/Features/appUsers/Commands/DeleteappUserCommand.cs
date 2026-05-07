using MediatR;

namespace PersonalityAssessment.Application.Features.appUsers.Commands
{
    public record DeleteappUserCommand(int id) : IRequest<bool>;
}
