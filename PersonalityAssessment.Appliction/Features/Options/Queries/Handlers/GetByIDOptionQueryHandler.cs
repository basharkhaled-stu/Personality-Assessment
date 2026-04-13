using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.Options.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.Options.Queries.Handlers
{
    public class GetByIDOptionQueryHandler :
        IRequestHandler<GetOptionByIdQuery, ReadOptionDTO>
    {
        private readonly IRepository<Option> _repository;
        private readonly ICacheService _cacheService;
        private string CacheKey;
        private readonly IMapper _mapper;

        public GetByIDOptionQueryHandler(
            IRepository<Option> repository,
            ICacheService cacheService,
            IMapper mapper)
        {
            _repository = repository;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<ReadOptionDTO> Handle
            (GetOptionByIdQuery request,
            CancellationToken cancellationToken)
        {

            CacheKey = $"Option:GetByID:{request.id}";
            var cachData = await _cacheService.GetAsync<ReadOptionDTO>(CacheKey);

            if (cachData != null)
            {
                return cachData;
            }

            var dto = await _repository.GetAll()
                 .Where(a => a.Id == request.id)
                 .ProjectTo<ReadOptionDTO>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(cancellationToken);

            if (dto == null)
                throw new NotFoundException($"Option with ID {request.id} not found.");

            await _cacheService.SetAsync(CacheKey, dto, TimeSpan.FromMinutes(60));

            return dto;
        }
    }
}
