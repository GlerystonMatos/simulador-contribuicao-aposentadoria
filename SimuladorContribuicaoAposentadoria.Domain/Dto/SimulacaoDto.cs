using System;
using System.ComponentModel;

namespace SimuladorContribuicaoAposentadoria.Domain.Dto
{
    [DisplayName("Simulacao")]
    public class SimulacaoDto
    {
        public SimulacaoDto()
        {
            Id = Guid.NewGuid();
        }

        public SimulacaoDto(double salario, DateTime nascimento, int idadeComQueComecouContribuicao, string sexo, string tipo, Guid idUsuario)
        {
            Id = Guid.NewGuid();
            Salario = salario;
            Nascimento = nascimento;
            IdadeComQueComecouContribuicao = idadeComQueComecouContribuicao;
            Sexo = sexo;
            Tipo = tipo;
            IdUsuario = idUsuario;
        }

        public Guid Id { get; set; }

        public double Salario { get; set; }

        public DateTime Nascimento { get; set; }

        public int IdadeComQueComecouContribuicao { get; set; }

        public string Sexo { get; set; }

        public string Tipo { get; set; }

        public Guid IdUsuario { get; set; }

        public UsuarioDto Usuario { get; set; }

        public ResultadoDto Resultado { get; set; }
    }
}