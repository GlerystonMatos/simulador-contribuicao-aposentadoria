using SimuladorContribuicaoAposentadoria.Domain.Entities;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Data;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Services;

namespace SimuladorContribuicaoAposentadoria.Service.Services
{
    public class ResultadoService : IResultadoService
    {
        private readonly IResultadoRepository _resultadoRepository;

        public ResultadoService(IResultadoRepository resultadoRepository)
            => _resultadoRepository = resultadoRepository;

        public void Create(Resultado resultado)
            => _resultadoRepository.Create(resultado);
    }
}