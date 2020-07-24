using SimuladorContribuicaoAposentadoria.Domain.Dto;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Commun;

namespace SimuladorContribuicaoAposentadoria.Domain.Interfaces.Services
{
    public interface IUsuarioService : IService<UsuarioDto>
    {
        UsuarioDto FindByNomeSenha(string nome, string senha);

        UsuarioDto FindByNome(string nome);
    }
}