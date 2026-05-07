using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.QuestionTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.QuestionTypes.Queries.Handlers
{
    public class GetAllQuestionTypeQueryHandler :
    IRequestHandler<GetAllQuestionTypeQuery, PagedResult<ReadQuestionTypeDTO>>

    {
        private readonly IRepository<QuestionType> _repository;
        private readonly IMapper _mapper;

        public GetAllQuestionTypeQueryHandler
            (IRepository<QuestionType> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadQuestionTypeDTO>> Handle
            (GetAllQuestionTypeQuery request,
            CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional
            if (!string.IsNullOrWhiteSpace(request.p.Search))
            {
                query = query.Where(x => x.Name.Contains(request.p.Search));
            }

            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<QuestionType, ReadQuestionTypeDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );

            return result;
        }
    }
}
