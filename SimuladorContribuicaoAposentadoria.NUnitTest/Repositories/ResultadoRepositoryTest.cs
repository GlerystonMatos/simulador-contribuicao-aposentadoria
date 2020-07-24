using AutoMapper;
using NUnit.Framework;
using SimuladorContribuicaoAposentadoria.Data.Repositories;
using SimuladorContribuicaoAposentadoria.Domain.Entities;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Data;
using SimuladorContribuicaoAposentadoriaNUnitTest.Comum;
using System;

namespace SimuladorContribuicaoAposentadoriaNUnitTest.Repositories
{
    public class ResultadoRepositoryTest
    {
        private IUsuarioRepository _usuarioRepository;
        private ISimulacaoRepository _simulacaoRepository;
        private IResultadoRepository _resultadoRepository;
        private Simulacao _simulacao;
        private Usuario _usuario;
        private IMapper _mapper;

        public ResultadoRepositoryTest()
        {
            _mapper = UtilitariosTest.GetMapper();
            _usuarioRepository = new UsuarioRepository(UtilitariosTest.GetContext(), _mapper);
            _simulacaoRepository = new SimulacaoRepository(UtilitariosTest.GetContext(), _mapper);
            _resultadoRepository = new ResultadoRepository(UtilitariosTest.GetContext(), _mapper);
            _usuario = new Usuario("Usuario Teste Resultado", "123");
            _usuarioRepository.Create(_usuario);
            _simulacao = new Simulacao(50, DateTime.Parse("1994-08-27"), 18, "M", "N", _usuario.Id);
            _simulacaoRepository.Create(_simulacao);
        }

        [Test]
        public void CreateTest()
        {
            Resultado resultado = new Resultado(10, DateTime.Now, 50, 7.5, _simulacao.Id);
            _resultadoRepository.Create(resultado);

            Assert.IsNotNull(_resultadoRepository.FindById(resultado.Id));
        }

        [Test]
        public void UpdateTest()
        {
            Resultado resultado = new Resultado(10, DateTime.Now, 50, 7.5, _simulacao.Id);
            _resultadoRepository.Create(resultado);

            resultado.QuantidadeContribuicoesParaPagarIniciandoMesAtual = 99;
            _resultadoRepository.Update(resultado);

            Assert.IsNotNull(_resultadoRepository.FindById(resultado.Id).QuantidadeContribuicoesParaPagarIniciandoMesAtual.Equals(99));
        }

        [Test]
        public void RemoveTest()
        {
            Resultado resultado = new Resultado(10, DateTime.Now, 50, 7.5, _simulacao.Id);
            _resultadoRepository.Create(resultado);

            _resultadoRepository.Remove(resultado);
            Assert.IsNull(_resultadoRepository.FindById(resultado.Id));
        }

        [Test]
        public void GetAllTest()
            => Assert.IsNotNull(_resultadoRepository.GetAll());

        [Test]
        public void FindByIdTest()
        {
            Resultado resultado = new Resultado(10, DateTime.Now, 50, 7.5, _simulacao.Id);
            _resultadoRepository.Create(resultado);

            Assert.IsNotNull(_resultadoRepository.FindById(resultado.Id));
        }
    }
}