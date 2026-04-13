using AutoMapper;
using PersonalityAssessment.Application.Features.UsersAssessmentResults.DTO;
using PersonalityAssessment.Core.Entities;
namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Mappings
{
    public class UsersAssessmentResultsProfile : Profile
    {
        public UsersAssessmentResultsProfile()
        {

            CreateMap<CreateUsersAssessmentResultDTO, UsersAssessmentResult>();
            CreateMap<UpdateUsersAssessmentResultDTO, UsersAssessmentResult>();
            CreateMap<UsersAssessmentResult, ReadUsersAssessmentResultDTO>()
                .ForMember(x => x.UsersAssessmentName,
                opt => opt.MapFrom(x => x.usersAssessment.UserId));
            CreateMap<UsersAssessmentResult, AdmainReadUsersAssessmentResultDTO>()
                  .ForMember(x => x.UsersAssessmentName,
                opt => opt.MapFrom(x => x.usersAssessment.UserId));
        }
    }
}
