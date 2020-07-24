using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimuladorContribuicaoAposentadoria.Domain.Dto
{
    [DisplayName("Informacoes")]
    public class InformacoesDto
    {
        public InformacoesDto()
        {
        }

        public InformacoesDto(double salario, DateTime nascimento, int idadeComQueComecouContribuicao, string sexo, string tipo, string usuario)
        {
            Salario = salario;
            Nascimento = nascimento;
            IdadeComQueComecouContribuicao = idadeComQueComecouContribuicao;
            Sexo = sexo;
            Tipo = tipo;
            Usuario = usuario;
        }

        [Required(ErrorMessage = "Campo 'Salario' obrigatório.")]
        public double Salario { get; set; }

        [Required(ErrorMessage = "Campo 'Nascimento' obrigatório.")]
        public DateTime Nascimento { get; set; }

        [Required(ErrorMessage = "Campo 'IdadeComQueComecouContribuicao' obrigatório.")]
        public int IdadeComQueComecouContribuicao { get; set; }

        [Required(ErrorMessage = "Campo 'Sexo' obrigatório.")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Campo 'Tipo' obrigatório.")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Campo 'Usuario' obrigatório.")]
        public string Usuario { get; set; }
    }
}