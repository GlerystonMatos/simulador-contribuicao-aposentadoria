using AutoMapper;
using AutoMapper.QueryableExtensions;
using SimuladorContribuicaoAposentadoria.Domain.Dto;
using SimuladorContribuicaoAposentadoria.Domain.Entities;
using SimuladorContribuicaoAposentadoria.Domain.Exception;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Data;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Services;
using System;
using System.Linq;

namespace SimuladorContribuicaoAposentadoria.Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        public void Create(UsuarioDto usuarioDto)
        {
            if (FindByNome(usuarioDto.Nome) != null)
            {
                throw new SimuladorContribuicaoAposentadoriaException("Usuário já cadastrado");
            }

            _usuarioRepository.Create(_mapper.Map<Usuario>(usuarioDto));
        }

        public void Update(UsuarioDto usuarioDto)
            => _usuarioRepository.Update(_mapper.Map<Usuario>(usuarioDto));

        public void Remove(UsuarioDto usuarioDto)
            => _usuarioRepository.Remove(_mapper.Map<Usuario>(usuarioDto));

        public IQueryable<UsuarioDto> GetAll()
            => _usuarioRepository.GetAll().ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider);

        public UsuarioDto FindById(Guid id)
            => _usuarioRepository.FindById(id);

        public UsuarioDto FindByNomeSenha(string nome, string senha)
            => _usuarioRepository.FindByNomeSenha(nome, senha);

        public UsuarioDto FindByNome(string nome)
            => _usuarioRepository.FindByNome(nome);
    }
}