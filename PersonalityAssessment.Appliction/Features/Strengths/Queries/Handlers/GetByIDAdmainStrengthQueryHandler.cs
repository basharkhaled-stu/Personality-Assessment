using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Application.Features.Strengths.DTO;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Repository;

namespace PersonalityAssessment.Application.Features.Strengths.Queries.Handlers
{
    public class GetByIDAdmainStrengthQueryHandler
         : IRequestHandler<GetStrengthByIdAdmainQuery, AdmainReadStrengthDTO>
    {
        private readonly IRepository<Strength> _repository;

        private readonly IMapper _mapper;

        public GetByIDAdmainStrengthQueryHandler
            (IRepository<Strength> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AdmainReadStrengthDTO> Handle(GetStrengthByIdAdmainQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.GetAll()
                .Where(a => a.Id == request.id)
                .ProjectTo<AdmainReadStrengthDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);




            if (dto == null)
            {
                throw new Exception("Not Found");
            }
            return _mapper.Map<AdmainReadStrengthDTO>(dto);
        }
    }



}
