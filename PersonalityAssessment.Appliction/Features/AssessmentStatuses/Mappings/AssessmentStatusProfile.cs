using AutoMapper;
using PersonalityAssessment.Application.Features.AssessmentStatuses.DTO;
using PersonalityAssessment.Core.Entities;
namespace PersonalityAssessment.Application.Features.AssessmentStatuses.Mappings
{
    public class AssessmentStatusProfile : Profile
    {
        public AssessmentStatusProfile()
        {

            CreateMap<CreateAssessmentStatusDTO, AssessmentStatus>();
            CreateMap<UpdateAssessmentStatusDTO, AssessmentStatus>();
            CreateMap<AssessmentStatus, ReadAssessmentStatusDTO>();
            CreateMap<AssessmentStatus, AdmainReadAssessmentStatusDTO>();
        }
    }
}
