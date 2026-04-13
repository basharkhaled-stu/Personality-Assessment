using AutoMapper;
using PersonalityAssessment.Application.Features.Options.DTO;
using PersonalityAssessment.Core.Entities;
namespace PersonalityAssessment.Application.Features.Options.Mappings
{
    public class OptionsProfile : Profile
    {
        public OptionsProfile()
        {

            CreateMap<CreateOptionDTO, Option>();
            CreateMap<Option, ReadOptionDTO>()
                .ForMember(x => x.QuestionName,
                 opt => opt.MapFrom(y => y.Question.Text));

            CreateMap<Option, AdmainReadOptionDTO>()
                  .ForMember(x => x.QuestionName,
                 opt => opt.MapFrom(y => y.Question.Text));
            CreateMap<UpdateOptionDTO, Option>();






        }





    }
}
