using MediatR;

namespace PersonalityAssessment.Application.Features.appUsers.Commands
{
    public record UpdateAppUserProfileImageCommand(string UserId, Stream ImageStream, string OriginalFileName, long FileSize)
        : IRequest<bool>;
}
