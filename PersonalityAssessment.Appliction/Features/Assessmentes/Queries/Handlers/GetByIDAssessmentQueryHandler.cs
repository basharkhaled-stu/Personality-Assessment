using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.Assessmentes.DTO;
using PersonalityAssessment.Application.Features.Assessments.Queries;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.Assessmentes.Queries.Handlers
{
    public class GetByIDAssessmentQueryHandler :
        IRequestHandler<GetAssessmentByIdQuery, ReadAssessmentDTO>
    {
        private readonly IRepository<Assessment> _repository;

        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;
        private string CacheKey = string.Empty;
        public GetByIDAssessmentQueryHandler(
            IRepository<Assessment> repository,
            ICacheService cacheService,
            IMapper mapper)
        {
            _repository = repository;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<ReadAssessmentDTO> Handle
            (GetAssessmentByIdQuery request,
            CancellationToken cancellationToken)
        {

            CacheKey = $"Assessmentes:ByID:{request.id}";

            var cachData = await _cacheService.GetAsync<ReadAssessmentDTO>(CacheKey);

            if (cachData != null)
            {
                return cachData;
            }



            var dto = await _repository.GetAll()
                     .Where(a => a.Id == request.id)
                     .ProjectTo<ReadAssessmentDTO>(_mapper.ConfigurationProvider)
                     .FirstOrDefaultAsync(cancellationToken);

            if (dto == null)
                throw new NotFoundException($"Assessment with ID {request.id} not found.");

            await _cacheService.SetAsync(CacheKey, dto, TimeSpan.FromMinutes(30));
            return dto;
        }
    }
}
