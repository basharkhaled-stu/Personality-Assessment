using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.UserAnswers.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.UserAnswers.Queries.Handlers
{
    public class GetAllUserAnswerQueryHandler :
    IRequestHandler<GetAllUserAnswerQuery, PagedResult<ReadUserAnswerDTO>>

    {
        private readonly IRepository<UserAnswer> _repository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GetAllUserAnswerQueryHandler
            (IRepository<UserAnswer> repository,
            IIdentityService identityService,
        IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<PagedResult<ReadUserAnswerDTO>> Handle(GetAllUserAnswerQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().AsNoTracking();

            // 🔎 Filtering optional

            // استدعاء Generic Pagination Helper
            var result = await query.ToPagedResultAsync<UserAnswer, ReadUserAnswerDTO>(
                _mapper,
                request.p.PageNumber,
                request.p.PageSize,
                cancellationToken
            );

            foreach (var item in result.Items)
            {
                item.UsersAssessmentName = await _identityService.GetFullNameAsync(item.UsersAssessmentName);
            }


            return result;
        }
    }
}
