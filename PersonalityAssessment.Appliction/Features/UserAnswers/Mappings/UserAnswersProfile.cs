using AutoMapper;
using PersonalityAssessment.Application.Features.UserAnswers.DTO;
using PersonalityAssessment.Core.Entities;
namespace PersonalityAssessment.Application.Features.UserAnswers.Mappings
{
    public class UserAnswersProfile : Profile
    {
        public UserAnswersProfile()
        {

            CreateMap<CreateUserAnswerDTO, UserAnswer>();
            CreateMap<UserAnswer, ReadUserAnswerDTO>()
               .ForMember(x => x.QuestionName,
               opt => opt.MapFrom(x => x.Question.Text))
               .ForMember(x => x.UsersAssessmentName,
               opt => opt.MapFrom(x => x.UsersAssessment.UserId))
               .ForMember(x => x.OptionName,
               opt => opt.MapFrom(x => x.Option.Text));














            CreateMap<UserAnswer, AdmainReadUserAnswerDTO>()
              .ForMember(x => x.QuestionName,
               opt => opt.MapFrom(x => x.Question.Text))
               .ForMember(x => x.UsersAssessmentName,
               opt => opt.MapFrom(x => x.UsersAssessment.UserId))
               .ForMember(x => x.OptionName,
               opt => opt.MapFrom(x => x.Option.Text));


            CreateMap<UpdateUserAnswerDTO, UserAnswer>();






        }





    }
}
