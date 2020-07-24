using Moq;
using NUnit.Framework;
using SimuladorContribuicaoAposentadoria.Domain.Entities;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Data;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Services;
using SimuladorContribuicaoAposentadoria.Service.Services;
using System;

namespace SimuladorContribuicaoAposentadoriaNUnitTest.Services
{
    public class ResultadoServiceTest
    {
        private Mock<IResultadoRepository> _mockResultadoRepository;
        private IResultadoService _resultadoService;
        private Simulacao _simulacao;

        public ResultadoServiceTest()
        {
            _mockResultadoRepository = new Mock<IResultadoRepository>();
            _resultadoService = new ResultadoService(_mockResultadoRepository.Object);
            Usuario usuario = new Usuario("Usuario Teste Resultado", "123");
            _simulacao = new Simulacao(50, DateTime.Parse("1994-08-27"), 18, "M", "N", usuario.Id);
        }

        [Test]
        public void CreateTest()
            => Assert.DoesNotThrow(() => _resultadoService.Create(new Resultado(504, DateTime.Parse("2062-07-13"), 3.75, 7.50, _simulacao.Id)));
    }
}