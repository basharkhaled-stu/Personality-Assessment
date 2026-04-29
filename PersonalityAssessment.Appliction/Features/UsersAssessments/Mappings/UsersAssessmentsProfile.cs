using AutoMapper;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Mappings
{
    public class UsersAssessmentsProfile : Profile
    {
        public UsersAssessmentsProfile()
        {
            CreateMap<CreateUsersAssessmentDTO, UsersAssessment>();

            CreateMap<UsersAssessment, ReadUsersAssessmentDTO>()
                .ForMember(x => x.UserAssessmentStatusName,
                    opt => opt.MapFrom(a =>
                        a.UserAssessmentStatus != null ? a.UserAssessmentStatus.Name : ""))
                .ForMember(x => x.UserName,
                    opt => opt.MapFrom(a => a.UserId ?? ""));

            CreateMap<UsersAssessment, AdmainReadUsersAssessmentDTO>();
            CreateMap<UpdateUsersAssessmentDTO, UsersAssessment>();
        }
    }
}
