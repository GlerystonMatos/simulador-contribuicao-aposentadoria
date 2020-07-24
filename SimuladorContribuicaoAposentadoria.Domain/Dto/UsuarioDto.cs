using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimuladorContribuicaoAposentadoria.Domain.Dto
{
    [DisplayName("Usuario")]
    public class UsuarioDto
    {
        public UsuarioDto()
        {
            Id = Guid.NewGuid();
        }

        public UsuarioDto(string nome, string senha)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Senha = senha;
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo 'Nome' obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo 'Senha' obrigatório.")]
        public string Senha { get; set; }
    }
}