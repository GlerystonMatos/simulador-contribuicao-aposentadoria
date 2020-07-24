using SimuladorContribuicaoAposentadoria.Domain.Dto;
using SimuladorContribuicaoAposentadoria.Domain.Entities;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Commun;
using System;

namespace SimuladorContribuicaoAposentadoria.Domain.Interfaces.Data
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        UsuarioDto FindById(Guid id);

        UsuarioDto FindByNomeSenha(string nome, string senha);

        UsuarioDto FindByNome(string nome);
    }
}