using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.Exceptions;
using PersonalityAssessment.Application.Features.UserAnswers.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.UserAnswers.Queries.Handlers
{
    public class GetByIDUserAnswerQueryHandler :
        IRequestHandler<GetUserAnswerByIdQuery, ReadUserAnswerDTO>
    {
        private readonly IRepository<UserAnswer> _repository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GetByIDUserAnswerQueryHandler(
            IRepository<UserAnswer> repository,
             IIdentityService identityService,
        IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<ReadUserAnswerDTO> Handle
            (GetUserAnswerByIdQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await _repository.GetAll()
                 .Where(a => a.Id == request.id)
                 .ProjectTo<ReadUserAnswerDTO>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(cancellationToken);

            if (dto == null)
                throw new NotFoundException($"Question with ID {request.id} not found.");


            var userName = await _identityService.GetFullNameAsync(dto.UsersAssessmentName);
            dto.UsersAssessmentName = userName;
            return dto;
        }
    }
}
