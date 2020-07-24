using SimuladorContribuicaoAposentadoria.Domain.Dto;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Commun;

namespace SimuladorContribuicaoAposentadoria.Domain.Interfaces.Services
{
    public interface ISimulacaoService : IService<SimulacaoDto>
    {
        ResultadoDto Simular(InformacoesDto informacoesDto);
    }
}