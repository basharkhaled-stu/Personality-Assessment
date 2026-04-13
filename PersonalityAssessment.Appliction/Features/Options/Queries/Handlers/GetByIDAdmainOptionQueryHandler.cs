using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Features.Options.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
namespace PersonalityAssessment.Application.Features.Options.Queries.Handlers
{
    public class GetByIDAdmainOptionQueryHandler
         : IRequestHandler<GetOptionByIdAdmainQuery, AdmainReadOptionDTO>
    {
        private readonly IRepository<Option> _repository;

        private readonly IMapper _mapper;

        public GetByIDAdmainOptionQueryHandler
            (IRepository<Option> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AdmainReadOptionDTO> Handle
            (GetOptionByIdAdmainQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAll()
                .Where(a => a.Id == request.id)
                .ProjectTo<AdmainReadOptionDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new Exception("Not Found");
            }
            return _mapper.Map<AdmainReadOptionDTO>(entity);
        }
    }



}
