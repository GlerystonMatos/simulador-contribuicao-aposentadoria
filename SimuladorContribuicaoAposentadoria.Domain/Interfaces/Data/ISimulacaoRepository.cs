using SimuladorContribuicaoAposentadoria.Domain.Dto;
using SimuladorContribuicaoAposentadoria.Domain.Entities;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Commun;
using System;

namespace SimuladorContribuicaoAposentadoria.Domain.Interfaces.Data
{
    public interface ISimulacaoRepository : IRepository<Simulacao>
    {
        SimulacaoDto FindById(Guid id);
    }
}