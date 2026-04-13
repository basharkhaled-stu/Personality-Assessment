using AutoMapper;
using PersonalityAssessment.Application.Features.Weaknesses.DTO;
using PersonalityAssessment.Core.Entities;
namespace PersonalityAssessment.Application.Features.Weaknesses.Mappings
{
    public class WeaknessesProfile : Profile
    {
        public WeaknessesProfile()
        {

            CreateMap<CreateWeaknessDTO, Weakness>();
            CreateMap<UpdateWeaknessDTO, Weakness>();
            CreateMap<Weakness, ReadWeakneesDTO>()
                .ForMember(x => x.PersonalityTypeName,
                opt => opt.MapFrom(x => x.PersonalityType.Name));



            CreateMap<Weakness, AdmainReadWeaknessDTO>()
                 .ForMember(x => x.PersonalityTypeName,
                opt => opt.MapFrom(x => x.PersonalityType.Name));
        }
    }
}
