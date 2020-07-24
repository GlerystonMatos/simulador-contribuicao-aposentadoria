using System;

namespace SimuladorContribuicaoAposentadoria.Domain.Entities
{
    public class Resultado : Entity
    {
        public Resultado()
        {
            Id = Guid.NewGuid();
        }

        public Resultado(int quantidadeContribuicoesParaPagarIniciandoMesAtual, DateTime dataUltimaContribuicao, double valorPagoMes, double percentualAplicadoParaCalculo, Guid idSimulacao)
        {
            Id = Guid.NewGuid();
            QuantidadeContribuicoesParaPagarIniciandoMesAtual = quantidadeContribuicoesParaPagarIniciandoMesAtual;
            DataUltimaContribuicao = dataUltimaContribuicao;
            ValorPagoMes = valorPagoMes;
            PercentualAplicadoParaCalculo = percentualAplicadoParaCalculo;
            IdSimulacao = idSimulacao;
        }

        public int QuantidadeContribuicoesParaPagarIniciandoMesAtual { get; set; }

        public DateTime DataUltimaContribuicao { get; set; }

        public double ValorPagoMes { get; set; }

        public double PercentualAplicadoParaCalculo { get; set; }

        public Guid IdSimulacao { get; set; }

        public Simulacao Simulacao { get; set; }
    }
}