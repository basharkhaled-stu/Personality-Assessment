using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.Questions.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
namespace PersonalityAssessment.Application.Features.Questions.Queries.Handlers
{
    public class GetByIDAdmainQuestionQueryHandler
         : IRequestHandler<GetQuestionByIdAdmainQuery, AdmainReadQuestionDTO>
    {
        private readonly IRepository< Question> _repository;

        private readonly IMapper _mapper;

        public GetByIDAdmainQuestionQueryHandler
            (IRepository< Question> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AdmainReadQuestionDTO> Handle
            (GetQuestionByIdAdmainQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAll()
                .Where(a => a.Id == request.id)
                .ProjectTo<AdmainReadQuestionDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException("Not Found Question");
            }
            return _mapper.Map<AdmainReadQuestionDTO>(entity);
        }
    }



}
