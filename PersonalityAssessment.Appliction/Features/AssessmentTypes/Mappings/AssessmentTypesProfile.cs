using AutoMapper;
using PersonalityAssessment.Application.Features.AssessmentTypes.DTO;
using PersonalityAssessment.Core.Entities;
namespace PersonalityAssessment.Application.Features.AssessmentTypes.Mappings
{
    public class AssessmentTypesProfile : Profile
    {
        public AssessmentTypesProfile()
        {

            CreateMap<CreateAssessmentTypeDTO, AssessmentType>();
            CreateMap<UpdateAssessmentTypeDTO, AssessmentType>();
            CreateMap<AssessmentType, ReadAssessmentTypeDTO>();
            CreateMap<AssessmentType, AdmainReadAssessmentTypeDTO>();
        }
    }
}
