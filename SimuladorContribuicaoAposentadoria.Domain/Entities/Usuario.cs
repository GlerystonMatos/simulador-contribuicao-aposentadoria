using System;
using System.Collections.Generic;

namespace SimuladorContribuicaoAposentadoria.Domain.Entities
{
    public class Usuario : Entity
    {
        public Usuario(string nome, string senha)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Senha = senha;
        }

        public string Nome { get; set; }

        public string Senha { get; set; }

        public ICollection<Simulacao> Simulacoes { get; set; }
    }
}