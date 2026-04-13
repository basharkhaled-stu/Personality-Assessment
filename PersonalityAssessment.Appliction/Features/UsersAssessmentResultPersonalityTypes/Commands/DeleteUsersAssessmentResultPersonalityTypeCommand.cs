using MediatR;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Commands
{
    public record DeleteUsersAssessmentResultPersonalityTypeCommand(int id) : IRequest<bool>;
}
