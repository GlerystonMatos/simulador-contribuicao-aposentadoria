using System;
using System.ComponentModel;

namespace SimuladorContribuicaoAposentadoria.Domain.Dto
{
    [DisplayName("Resultado")]
    public class ResultadoDto
    {
        public ResultadoDto()
        {
            Id = Guid.NewGuid();
        }

        public ResultadoDto(int quantidadeContribuicoesParaPagarIniciandoMesAtual, DateTime dataUltimaContribuicao, double valorPagoMes, double percentualAplicadoParaCalculo)
        {
            Id = Guid.NewGuid();
            QuantidadeContribuicoesParaPagarIniciandoMesAtual = quantidadeContribuicoesParaPagarIniciandoMesAtual;
            DataUltimaContribuicao = dataUltimaContribuicao;
            ValorPagoMes = valorPagoMes;
            PercentualAplicadoParaCalculo = percentualAplicadoParaCalculo;
        }

        public Guid Id { get; set; }

        public int QuantidadeContribuicoesParaPagarIniciandoMesAtual { get; set; }

        public DateTime DataUltimaContribuicao { get; set; }

        public double ValorPagoMes { get; set; }

        public double PercentualAplicadoParaCalculo { get; set; }
    }
}