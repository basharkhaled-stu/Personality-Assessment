using MediatR;

namespace PersonalityAssessment.Application.Features.Options.Commands
{
    public record DeleteOptionCommand(int id) : IRequest<bool>;
}
