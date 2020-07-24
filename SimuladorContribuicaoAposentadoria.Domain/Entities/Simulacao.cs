using System;

namespace SimuladorContribuicaoAposentadoria.Domain.Entities
{
    public class Simulacao : Entity
    {
        public Simulacao(double salario, DateTime nascimento, int idadeComQueComecouContribuicao, string sexo, string tipo, Guid idUsuario)
        {
            Id = Guid.NewGuid();
            Salario = salario;
            Nascimento = nascimento;
            IdadeComQueComecouContribuicao = idadeComQueComecouContribuicao;
            Sexo = sexo;
            Tipo = tipo;
            IdUsuario = idUsuario;
        }

        public double Salario { get; set; }

        public DateTime Nascimento { get; set; }

        public int IdadeComQueComecouContribuicao { get; set; }

        public string Sexo { get; set; }

        public string Tipo { get; set; }

        public Guid IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        public Resultado Resultado { get; set; }
    }
}