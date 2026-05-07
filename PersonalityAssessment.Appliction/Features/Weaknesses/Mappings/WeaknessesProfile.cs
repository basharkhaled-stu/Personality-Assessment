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

            // BUG FIX: null-safe navigation
            CreateMap<Weakness, ReadWeakneesDTO>()
                .ForMember(d => d.PersonalityTypeName,
                    o => o.MapFrom(s => s.PersonalityType != null ? s.PersonalityType.Name : ""));

            CreateMap<Weakness, AdmainReadWeaknessDTO>()
                .ForMember(d => d.PersonalityTypeName,
                    o => o.MapFrom(s => s.PersonalityType != null ? s.PersonalityType.Name : ""));
        }
    }
}
