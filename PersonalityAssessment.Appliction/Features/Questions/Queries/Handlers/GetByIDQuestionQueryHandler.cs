using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.Questions.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.Questions.Queries.Handlers
{
    public class GetByIDQuestionQueryHandler :
        IRequestHandler<GetQuestionByIdQuery, ReadQuestionDTO>
    {
        private readonly IRepository<Question> _repository;
        private readonly ICacheService _cacheService;
        private string CacheKey;
        private readonly IMapper _mapper;

        public GetByIDQuestionQueryHandler(
            IRepository<Question> repository,
            ICacheService cacheService,
            IMapper mapper)
        {
            _repository = repository;
            _cacheService = cacheService;

            _mapper = mapper;
        }
        public async Task<ReadQuestionDTO> Handle
            (GetQuestionByIdQuery request,
            CancellationToken cancellationToken)
        {
            CacheKey = $"Question:GetByID:{request.id}";

            var cachData = await _cacheService.GetAsync<ReadQuestionDTO>(CacheKey);
            if (cachData != null)
            {
                return cachData;
            }

            var dto = await _repository.GetAll()
                 .Where(a => a.Id == request.id)
                 .ProjectTo<ReadQuestionDTO>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(cancellationToken);

            if (dto == null)
                throw new NotFoundException($"Question with ID {request.id} not found.");

            await _cacheService.SetAsync(CacheKey, dto, TimeSpan.FromMinutes(60));
            return dto;
        }
    }
}
