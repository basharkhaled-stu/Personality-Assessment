using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UsersAssessmentResults.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Queries.Handlers
{
    public class GetByIDUsersAssessmentResultQueryHandler :
        IRequestHandler<GetUsersAssessmentResultByIdQuery, ReadUsersAssessmentResultDTO>
    {
        private readonly IRepository<UsersAssessmentResult> _repository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GetByIDUsersAssessmentResultQueryHandler(
            IRepository<UsersAssessmentResult> repository,
            IIdentityService identityService,
            IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;

            _mapper = mapper;
        }
        public async Task<ReadUsersAssessmentResultDTO> Handle(GetUsersAssessmentResultByIdQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.GetAll()
                 .Where(a => a.Id == request.id)
                 .ProjectTo<ReadUsersAssessmentResultDTO>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(cancellationToken);
            if (dto == null)
            {
                throw new NotFoundException($"UsersAssessmentResultDTO with ID {request.id} not found.");
            }


            dto.UsersAssessmentName = await _identityService.GetFullNameAsync(dto.UsersAssessmentName);
            return dto;
        }
    }
}
