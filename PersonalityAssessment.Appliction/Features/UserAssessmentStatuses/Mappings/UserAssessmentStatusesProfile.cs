using AutoMapper;
using PersonalityAssessment.Application.Features.UserAssessmentStatuses.DTO;
using PersonalityAssessment.Core.Entities;
namespace PersonalityAssessment.Application.Features.UserAssessmentStatuses.Mappings
{
    public class UserAssessmentStatusesProfile : Profile
    {
        public UserAssessmentStatusesProfile()
        {

            CreateMap<CreateUserAssessmentStatusDTO, UserAssessmentStatus>();
            CreateMap<UserAssessmentStatus, ReadUserAssessmentStatusDTO>();
            CreateMap<UserAssessmentStatus, AdmainUserAssessmentStatusDTO>();
            CreateMap<UpdateUserAssessmentStatusDTO, UserAssessmentStatus>();






        }





    }
}
