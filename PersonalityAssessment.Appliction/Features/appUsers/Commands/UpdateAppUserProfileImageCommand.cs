using MediatR;

namespace PersonalityAssessment.Application.Features.Options.Commands
{
    /// <summary>
    /// Caller must dispose <see cref="ImageStream"/> after the pipeline completes.
    /// </summary>
    public record UpdateAppUserProfileImageCommand(string UserId, Stream ImageStream, string OriginalFileName, long FileSize)
        : IRequest<bool>;
}
