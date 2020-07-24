using AutoMapper;
using Moq;
using NUnit.Framework;
using SimuladorContribuicaoAposentadoria.Domain.Dto;
using SimuladorContribuicaoAposentadoria.Domain.Entities;
using SimuladorContribuicaoAposentadoria.Domain.Exception;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Data;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Services;
using SimuladorContribuicaoAposentadoria.Service.Services;
using SimuladorContribuicaoAposentadoriaNUnitTest.Comum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimuladorContribuicaoAposentadoriaNUnitTest.Services
{
    public class SimulacaoServiceTest
    {
        private Mock<ISimulacaoRepository> _mockSimulacaoRepository;
        private Mock<IResultadoRepository> _mockResultadoRepository;
        private Mock<IUsuarioRepository> _mockUsuarioRepository;
        private ISimulacaoService _simulacaoService;
        private IResultadoService _resultadoService;
        private IUsuarioService _usuarioService;
        private UsuarioDto _usuarioDto;
        private IMapper _mapper;

        public SimulacaoServiceTest()
        {
            _mapper = UtilitariosTest.GetMapper();
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();
            _usuarioDto = new UsuarioDto("Usuario Teste Resultado", "123");
            _mockResultadoRepository = new Mock<IResultadoRepository>();
            _mockSimulacaoRepository = new Mock<ISimulacaoRepository>();
            _resultadoService = new ResultadoService(_mockResultadoRepository.Object);
            _usuarioService = new UsuarioService(_mapper, _mockUsuarioRepository.Object);
            _simulacaoService = new SimulacaoService(_mapper, _mockSimulacaoRepository.Object, _resultadoService, _usuarioService);            
        }

        [Test]
        [TestCase(50, "1994-08-27", 18, "M", "N", 504, 3.75, 7.50, "Tobias")]
        [TestCase(1200, "1994-08-27", 18, "F", "N", 456, 102.00, 8.50, "Lola")]
        [TestCase(2500, "1994-08-27", 18, "M", "N", 504, 237.50, 9.50, "Tobias")]
        [TestCase(3500, "1994-08-27", 18, "M", "N", 504, 367.50, 10.50, "Tobias")]
        [TestCase(5500, "1994-08-27", 18, "M", "N", 504, 715.00, 13.00, "Tobias")]
        [TestCase(6100, "1994-08-27", 18, "M", "N", 504, 823.50, 13.50, "Tobias")]
        [TestCase(8000, "1994-08-27", 18, "M", "N", 504, 1160.00, 14.50, "Tobias")]
        [TestCase(4728, "1994-08-27", 18, "M", "R", 504, 744.66, 15.75, "Tobias")]
        [TestCase(4728, "1994-08-27", 18, "F", "R", 504, 744.66, 15.75, "Lola")]
        [TestCase(4728, "1979-08-27", 40, "M", "N", 384, 496.44, 10.50, "Tobias")]
        public void SimularTest(double salario, DateTime nascimento, int idadeComQueComecouContribuicao, string sexo, string tipo, int quantidadeContribuicoesParaPagarIniciandoMesAtual,
            double valorPagoMes, double percentualAplicadoParaCalculo, string usuario)
        {
            _mockUsuarioRepository.Setup(r => r.FindByNome(usuario)).Returns(_usuarioDto);
            DateTime dataUltimaContribuicao = DateTime.Now.AddMonths(quantidadeContribuicoesParaPagarIniciandoMesAtual);
            ResultadoDto resultadoDto = _simulacaoService.Simular(new InformacoesDto(salario, nascimento, idadeComQueComecouContribuicao, sexo, tipo, usuario));
            Assert.IsTrue(resultadoDto.QuantidadeContribuicoesParaPagarIniciandoMesAtual.Equals(quantidadeContribuicoesParaPagarIniciandoMesAtual));
            Assert.IsTrue(resultadoDto.PercentualAplicadoParaCalculo.Equals(percentualAplicadoParaCalculo));
            Assert.IsTrue(resultadoDto.DataUltimaContribuicao.Date.Equals(dataUltimaContribuicao.Date));
            Assert.IsTrue(resultadoDto.ValorPagoMes.Equals(valorPagoMes));
        }

        [Test]
        [TestCase(50, "1994-08-27", 18, "M", "G", "Tobias", "Tipo de aposentadoria invalido. (Tempo Normal = N, Tempo Reduzido = R)")]
        [TestCase(50, "1994-08-27", 18, "G", "N", "Tobias", "Sexo invalido. (Masculino = M, Feminino = F)")]
        [TestCase(50, "1994-08-27", 18, "G", "R", "Tobias", "Sexo invalido. (Masculino = M, Feminino = F)")]
        public void SimularTestException(double salario, DateTime nascimento, int idadeComQueComecouContribuicao, string sexo, string tipo, string usuario, string mensagem)
            => Assert.Throws<SimuladorContribuicaoAposentadoriaException>(() => _simulacaoService.Simular(new InformacoesDto(salario, nascimento, idadeComQueComecouContribuicao,
                sexo, tipo, usuario)), mensagem);

        [Test]
        public void CreateTest()
            => Assert.DoesNotThrow(() => _simulacaoService.Create(new SimulacaoDto(50, DateTime.Parse("1994-08-27"), 18, "M", "N", _usuarioDto.Id)));

        [Test]
        public void UpdateTest()
            => Assert.DoesNotThrow(() => _simulacaoService.Update(new SimulacaoDto(50, DateTime.Parse("1994-08-27"), 18, "M", "N", _usuarioDto.Id)));

        [Test]
        public void RemoveTest()
            => Assert.DoesNotThrow(() => _simulacaoService.Remove(new SimulacaoDto(50, DateTime.Parse("1994-08-27"), 18, "M", "N", _usuarioDto.Id)));

        [Test]
        public void GetAllTest()
        {
            Usuario usuario = new Usuario("Usuario Teste Resultado", "123");
            Simulacao simulacao = new Simulacao(50, DateTime.Parse("1994-08-27"), 18, "M", "N", usuario.Id);
            IList<Simulacao> simulacaoList = new List<Simulacao>();
            simulacaoList.Add(simulacao);

            _mockSimulacaoRepository.Setup(r => r.GetAll()).Returns(simulacaoList.AsQueryable());
            Assert.IsNotNull(_simulacaoService.GetAll());
        }

        [Test]
        public void FindByIdTest()
        {
            SimulacaoDto simulacaoDto = new SimulacaoDto(50, DateTime.Parse("1994-08-27"), 18, "M", "N", _usuarioDto.Id);
            _mockSimulacaoRepository.Setup(r => r.FindById(simulacaoDto.Id)).Returns(simulacaoDto);
            Assert.IsNotNull(_simulacaoService.FindById(simulacaoDto.Id));
        }
    }
}