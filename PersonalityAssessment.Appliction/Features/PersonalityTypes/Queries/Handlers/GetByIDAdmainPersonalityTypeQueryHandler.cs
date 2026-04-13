using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Features.PersonalityTypes.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;
namespace PersonalityAssessment.Application.Features.PersonalityTypes.Queries.Handlers
{
    public class GetByIDAdmainPersonalityTypeQueryHandler
         : IRequestHandler<GetPersonalityTypeByIdAdmainQuery, AdmainPersonalityTypeDTO>
    {
        private readonly IRepository<PersonalityType> _repository;

        private readonly IMapper _mapper;

        public GetByIDAdmainPersonalityTypeQueryHandler
            (IRepository<PersonalityType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AdmainPersonalityTypeDTO> Handle
            (GetPersonalityTypeByIdAdmainQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAll()
                .Where(a => a.Id == request.id)
                .ProjectTo<AdmainPersonalityTypeDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new Exception("Not Found");
            }
            return _mapper.Map<AdmainPersonalityTypeDTO>(entity);
        }
    }



}
