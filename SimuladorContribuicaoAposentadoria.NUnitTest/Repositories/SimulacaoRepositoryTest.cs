using AutoMapper;
using NUnit.Framework;
using SimuladorContribuicaoAposentadoria.Data.Repositories;
using SimuladorContribuicaoAposentadoria.Domain.Entities;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Data;
using SimuladorContribuicaoAposentadoriaNUnitTest.Comum;
using System;

namespace SimuladorContribuicaoAposentadoriaNUnitTest.Repositories
{
    public class SimulacaoRepositoryTest
    {
        private IMapper _mapper;
        private Usuario _usuario;
        private IUsuarioRepository _usuarioRepository;
        private ISimulacaoRepository _simulacaoRepository;

        public SimulacaoRepositoryTest()
        {
            _mapper = UtilitariosTest.GetMapper();
            _usuarioRepository = new UsuarioRepository(UtilitariosTest.GetContext(), _mapper);
            _simulacaoRepository = new SimulacaoRepository(UtilitariosTest.GetContext(), _mapper);
            _usuario = new Usuario("Usuario Teste Simulacao", "123");
            _usuarioRepository.Create(_usuario);
        }

        [Test]
        public void CreateTest()
        {
            Simulacao simulacao = new Simulacao(50, DateTime.Parse("1994-08-27"), 18, "M", "N", _usuario.Id);
            _simulacaoRepository.Create(simulacao);

            Assert.IsNotNull(_simulacaoRepository.FindById(simulacao.Id));
        }

        [Test]
        public void UpdateTest()
        {
            Simulacao simulacao = new Simulacao(50, DateTime.Parse("1994-08-27"), 18, "M", "N", _usuario.Id);
            _simulacaoRepository.Create(simulacao);

            simulacao.Salario = 99;
            _simulacaoRepository.Update(simulacao);

            Assert.IsNotNull(_simulacaoRepository.FindById(simulacao.Id).Salario.Equals(99));
        }

        [Test]
        public void RemoveTest()
        {
            Simulacao simulacao = new Simulacao(50, DateTime.Parse("1994-08-27"), 18, "M", "N", _usuario.Id);
            _simulacaoRepository.Create(simulacao);

            _simulacaoRepository.Remove(simulacao);
            Assert.IsNull(_simulacaoRepository.FindById(simulacao.Id));
        }

        [Test]
        public void GetAllTest()
            => Assert.IsNotNull(_simulacaoRepository.GetAll());

        [Test]
        public void FindByIdTest()
        {
            Simulacao simulacao = new Simulacao(50, DateTime.Parse("1994-08-27"), 18, "M", "N", _usuario.Id);
            _simulacaoRepository.Create(simulacao);

            Assert.IsNotNull(_simulacaoRepository.FindById(simulacao.Id));
        }
    }
}