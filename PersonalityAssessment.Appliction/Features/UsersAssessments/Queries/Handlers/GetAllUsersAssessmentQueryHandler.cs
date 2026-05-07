using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Queries.Handlers
{
    public class GetAllUsersAssessmentQueryHandler :
    IRequestHandler<GetAllUsersAssessmentQuery, PagedResult<ReadUsersAssessmentDTO>>
    {
        private readonly IRepository<UsersAssessment> _repository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GetAllUsersAssessmentQueryHandler(
            IRepository<UsersAssessment> repository,
            IIdentityService identityService,
            IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<PagedResult<ReadUsersAssessmentDTO>> Handle(
            GetAllUsersAssessmentQuery request,
            CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.p.Search))
                query = query.Where(x => x.UserId.Contains(request.p.Search));

            // BUG FIX: get raw entities first to access UserId
            var rawQuery = query.Select(x => new { x.Id, x.UserId, x.UserAssessmentStatusId, x.StartedAt, x.CompletedAt });
            var total = await rawQuery.CountAsync(cancellationToken);
            var items = await rawQuery
                .Skip((request.p.PageNumber - 1) * request.p.PageSize)
                .Take(request.p.PageSize)
                .ToListAsync(cancellationToken);

            var dtos = new List<ReadUsersAssessmentDTO>();
            foreach (var item in items)
            {
                var fullName = "";
                try { fullName = await _identityService.GetFullNameAsync(item.UserId) ?? item.UserId; }
                catch { fullName = item.UserId; }

                dtos.Add(new ReadUsersAssessmentDTO
                {
                    Id = item.Id,
                    UserName = fullName,
                    StartedAt = item.StartedAt,
                    CompletedAt = item.CompletedAt,
                });
            }

            return new PagedResult<ReadUsersAssessmentDTO>
            {
                Items = dtos,
                TotalCount = total,
                PageNumber = request.p.PageNumber,
                PageSize = request.p.PageSize,
            };
        }
    }
}
