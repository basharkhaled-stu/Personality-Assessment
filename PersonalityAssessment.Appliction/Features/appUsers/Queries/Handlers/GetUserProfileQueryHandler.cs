using MediatR;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.Options.DTO;
using PersonalityAssessment.Application.Features.Options.Queries;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.appUsers.Queries.Handlers
{
    public class GetUserProfileQueryHandler
        : IRequestHandler<GetUserProfileQuery, UserProfileDTO>
    {
        private readonly IIdentityUser _identityUser;

        public GetUserProfileQueryHandler(IIdentityUser identityUser)
        {
            _identityUser = identityUser;
        }

        public async Task<UserProfileDTO> Handle(
            GetUserProfileQuery request,
            CancellationToken cancellationToken)
        {
            var profile = await _identityUser.GetProfileAsync(request.UserId);
            if (profile == null)
                throw new NotFoundException("User not found");

            return new UserProfileDTO
            {
                Id              = profile.Id,
                FirstName       = profile.FirstName,
                LastName        = profile.LastName,
                FullName        = profile.FullName,
                UserName        = profile.UserName,
                Email           = profile.Email,
                ProfileImageUrl = profile.ProfileImageUrl,
            };
        }
    }
}
