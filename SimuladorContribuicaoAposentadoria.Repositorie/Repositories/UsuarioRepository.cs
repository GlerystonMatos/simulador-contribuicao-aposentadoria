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
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly IMapper _mapper;

        public UsuarioRepository(SimuladorContribuicaoAposentadoriaContext context, IMapper mapper) : base(context)
            => _mapper = mapper;

        public UsuarioDto FindById(Guid id)
            => _context.Set<Usuario>().Where(e => e.Id.Equals(id)).ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider).FirstOrDefault();

        public UsuarioDto FindByNomeSenha(string nome, string senha)
            => _context.Set<Usuario>().Where(u => u.Nome.Equals(nome) && u.Senha.Equals(senha)).ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider).FirstOrDefault();

        public UsuarioDto FindByNome(string nome)
            => _context.Set<Usuario>().Where(u => u.Nome.Equals(nome)).ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider).FirstOrDefault();
    }
}