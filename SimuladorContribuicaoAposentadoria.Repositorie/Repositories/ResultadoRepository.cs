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
    public class ResultadoRepository : Repository<Resultado>, IResultadoRepository
    {
        private readonly IMapper _mapper;

        public ResultadoRepository(SimuladorContribuicaoAposentadoriaContext context, IMapper mapper) : base(context)
            => _mapper = mapper;

        public ResultadoDto FindById(Guid id)
            => _context.Set<Resultado>().Where(e => e.Id.Equals(id)).ProjectTo<ResultadoDto>(_mapper.ConfigurationProvider).FirstOrDefault();
    }
}