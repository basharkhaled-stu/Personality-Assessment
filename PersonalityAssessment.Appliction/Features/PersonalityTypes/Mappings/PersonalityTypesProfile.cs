using AutoMapper;
using PersonalityAssessment.Application.Features.PersonalityTypes.DTO;
using PersonalityAssessment.Core.Entities;
namespace PersonalityAssessment.Application.Features.PersonalityTypes.Mappings
{
    public class PersonalityTypesProfile : Profile
    {
        public PersonalityTypesProfile()
        {

            CreateMap<CreatePersonalityTypeDTO, PersonalityType>();
            CreateMap<PersonalityType, ReadPersonalityTypeDTO>();
            CreateMap<PersonalityType, AdmainPersonalityTypeDTO>();
            CreateMap<UpdatePersonalityTypeDTO, PersonalityType>();






        }





    }
}
