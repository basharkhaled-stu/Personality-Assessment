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

            CreateMap<Assessment, ReadAssessmentDTO>()
                .ForMember(des => des.AssessmentStatusName,
                opt => opt.MapFrom(src => src.AssessmentStatus.Name)

                )
                .ForMember(des => des.AssessmentTypeName,
                 opt => opt.MapFrom(src => src.AssessmentType.Name)
                );

            CreateMap<UpdateAssessmentDTO, Assessment>();


            CreateMap<Assessment, AdmainReadAssessmentDTO>()
                .ForMember(des => des.AssessmentStatusName,
                opt => opt.MapFrom(src => src.AssessmentStatus.Name)

                )
                .ForMember(des => des.AssessmentTypeName,
                 opt => opt.MapFrom(src => src.AssessmentType.Name)
                );





        }





    }
}
