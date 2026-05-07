using AutoMapper;
using PersonalityAssessment.Application.Features.Assessmentes.DTO;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Application.Features.Assessmentes.Mappings
{
    public class AssessmentsProfile : Profile
    {
        public AssessmentsProfile()
        {
            CreateMap<CreateAssessmentDTO, Assessment>();
            CreateMap<UpdateAssessmentDTO, Assessment>();

            // BUG FIX: null-safe navigation on AssessmentStatus and AssessmentType
            CreateMap<Assessment, ReadAssessmentDTO>()
                .ForMember(d => d.AssessmentStatusName,
                    o => o.MapFrom(s => s.AssessmentStatus != null ? s.AssessmentStatus.Name : ""))
                .ForMember(d => d.AssessmentTypeName,
                    o => o.MapFrom(s => s.AssessmentType != null ? s.AssessmentType.Name : ""));

            CreateMap<Assessment, AdmainReadAssessmentDTO>()
                .ForMember(d => d.AssessmentStatusName,
                    o => o.MapFrom(s => s.AssessmentStatus != null ? s.AssessmentStatus.Name : ""))
                .ForMember(d => d.AssessmentTypeName,
                    o => o.MapFrom(s => s.AssessmentType != null ? s.AssessmentType.Name : ""));
        }
    }
}
