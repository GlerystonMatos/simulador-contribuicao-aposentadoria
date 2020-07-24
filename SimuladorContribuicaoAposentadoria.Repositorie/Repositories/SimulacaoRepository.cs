using AutoMapper;
using AutoMapper.QueryableExtensions;
using SimuladorContribuicaoAposentadoria.Data.Comun;
using SimuladorContribuicaoAposentadoria.Data.Context;
using SimuladorContribuicaoAposentadoria.Domain.Dto;
using SimuladorContribuicaoAposentadoria.Domain.Entities;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Data;
using System;
using System.Linq;

namespace SimuladorContribuicaoAposentadoria.Data.Repositories
{
    public class SimulacaoRepository : Repository<Simulacao>, ISimulacaoRepository
    {
        private readonly IMapper _mapper;

        public SimulacaoRepository(SimuladorContribuicaoAposentadoriaContext context, IMapper mapper) : base(context)
            => _mapper = mapper;

        public SimulacaoDto FindById(Guid id)
            => _context.Set<Simulacao>().Where(e => e.Id.Equals(id)).ProjectTo<SimulacaoDto>(_mapper.ConfigurationProvider).FirstOrDefault();
    }
}