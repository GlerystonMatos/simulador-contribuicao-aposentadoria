using AutoMapper;
using AutoMapper.QueryableExtensions;
using SimuladorContribuicaoAposentadoria.Domain.Dto;
using SimuladorContribuicaoAposentadoria.Domain.Entities;
using SimuladorContribuicaoAposentadoria.Domain.Exception;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Data;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Services;
using System;
using System.Linq;

namespace SimuladorContribuicaoAposentadoria.Service.Services
{
    public class SimulacaoService : ISimulacaoService
    {
        private readonly IMapper _mapper;
        private readonly ISimulacaoRepository _simulacaoRepository;
        private readonly IResultadoService _resultadoService;
        private readonly IUsuarioService _usuarioService;

        public SimulacaoService(IMapper mapper, ISimulacaoRepository simulacaoRepository, IResultadoService resultadoService, IUsuarioService usuarioService)
        {
            _mapper = mapper;
            _simulacaoRepository = simulacaoRepository;
            _resultadoService = resultadoService;
            _usuarioService = usuarioService;
        }

        private double GetPercentualParaCalculo(double salario, string tipo)
        {
            double valor;
            if (salario <= 998.0)
            {
                valor = 7.5;
            }
            else if (salario >= 999.0 && salario <= 2000.0)
            {
                valor = 8.5;
            }
            else if (salario >= 2001.0 && salario <= 3000.0)
            {
                valor = 9.5;
            }
            else if (salario >= 3001.0 && salario <= 5000.0)
            {
                valor = 10.5;
            }
            else
            {
                double percentual = 13.0;
                double acrescimo = ((salario - 5000) / 1000);

                if (acrescimo >= 1)
                {
                    percentual += (Math.Truncate(acrescimo) * 0.5);
                }

                valor = percentual;
            }

            if (tipo.Equals("R"))
            {
                valor += (valor / 2);
            }

            return valor;
        }

        private double GetValorContribuicao(InformacoesDto informacoesDto, double percentual)
            => ((percentual * informacoesDto.Salario) / 100);

        private int GetIdade(DateTime nascimento)
        {
            DateTime hoje = DateTime.Now;
            int idade = hoje.Year - nascimento.Year;

            if (nascimento > hoje.AddYears(-idade))
            {
                idade--;
            }

            return idade;
        }

        private ResultadoDto SimularContribuicao(InformacoesDto informacoesDto, int idadeMinimaParaAposentar, int tempoMinimoContribuicao, double percentualAplicadoParaCalculo)
        {
            int quantidadeMesesAno = 12;
            int idadeAtual = GetIdade(informacoesDto.Nascimento);
            int tempoFaltaParaAposentar = idadeMinimaParaAposentar - idadeAtual;
            int tempoJaRealizado = idadeAtual - informacoesDto.IdadeComQueComecouContribuicao;
            int tempoTotalRealizado = tempoJaRealizado + tempoFaltaParaAposentar;

            if (tempoTotalRealizado < tempoMinimoContribuicao)
            {
                tempoFaltaParaAposentar += (tempoMinimoContribuicao - tempoTotalRealizado);
            }

            int quantidadeContribuicoesParaPagarIniciandoMesAtual = (tempoFaltaParaAposentar * quantidadeMesesAno);
            DateTime dataUltimaContribuicao = DateTime.Now.AddMonths(quantidadeContribuicoesParaPagarIniciandoMesAtual);
            double valorPagoMes = GetValorContribuicao(informacoesDto, percentualAplicadoParaCalculo);

            SimulacaoDto simulacaoDto = _mapper.Map<SimulacaoDto>(informacoesDto);
            simulacaoDto.IdUsuario = _usuarioService.FindByNome(informacoesDto.Usuario).Id;

            Create(simulacaoDto);
            Resultado resultado = new Resultado(quantidadeContribuicoesParaPagarIniciandoMesAtual, dataUltimaContribuicao, valorPagoMes, percentualAplicadoParaCalculo, simulacaoDto.Id);
            _resultadoService.Create(resultado);
            return _mapper.Map<ResultadoDto>(resultado);
        }

        private ResultadoDto SimularTempoNormalMasculino(InformacoesDto informacoesDto)
        {
            int tempoMinimoContribuicao = 32;
            int idadeMinimaParaAposentar = 67;
            double percentualAplicadoParaCalculo = GetPercentualParaCalculo(informacoesDto.Salario, informacoesDto.Tipo);
            return SimularContribuicao(informacoesDto, idadeMinimaParaAposentar, tempoMinimoContribuicao, percentualAplicadoParaCalculo);
        }

        private ResultadoDto SimularTempoNormalFeminino(InformacoesDto informacoesDto)
        {
            int tempoMinimoContribuicao = 32;
            int idadeMinimaParaAposentar = 63;
            double percentualAplicadoParaCalculo = GetPercentualParaCalculo(informacoesDto.Salario, informacoesDto.Tipo);
            return SimularContribuicao(informacoesDto, idadeMinimaParaAposentar, tempoMinimoContribuicao, percentualAplicadoParaCalculo);
        }

        private ResultadoDto SimularTempoNormal(InformacoesDto informacoesDto)
        {
            switch (informacoesDto.Sexo)
            {
                case "M":
                    return SimularTempoNormalMasculino(informacoesDto);
                case "F":
                    return SimularTempoNormalFeminino(informacoesDto);
                default:
                    throw new SimuladorContribuicaoAposentadoriaException("Sexo invalido. (Masculino = M, Feminino = F)");
            }
        }

        private ResultadoDto SimularTempoReduzido(InformacoesDto informacoesDto)
        {
            if (!informacoesDto.Sexo.Equals("M") && !informacoesDto.Sexo.Equals("F"))
            {
                throw new SimuladorContribuicaoAposentadoriaException("Sexo invalido. (Masculino = M, Feminino = F)");
            }

            int tempoMinimoContribuicao = 25;
            int idadeMinimaParaAposentar = 67;
            double percentualAplicadoParaCalculo = GetPercentualParaCalculo(informacoesDto.Salario, informacoesDto.Tipo);
            return SimularContribuicao(informacoesDto, idadeMinimaParaAposentar, tempoMinimoContribuicao, percentualAplicadoParaCalculo);
        }

        public ResultadoDto Simular(InformacoesDto informacoesDto)
        {
            switch (informacoesDto.Tipo)
            {
                case "N":
                    return SimularTempoNormal(informacoesDto);
                case "R":
                    return SimularTempoReduzido(informacoesDto);
                default:
                    throw new SimuladorContribuicaoAposentadoriaException("Tipo de aposentadoria invalido. (Tempo Normal = N, Tempo Reduzido = R)");
            }
        }

        public void Create(SimulacaoDto simulacaoDto)
            => _simulacaoRepository.Create(_mapper.Map<Simulacao>(simulacaoDto));

        public void Update(SimulacaoDto simulacaoDto)
            => _simulacaoRepository.Update(_mapper.Map<Simulacao>(simulacaoDto));

        public void Remove(SimulacaoDto simulacaoDto)
            => _simulacaoRepository.Remove(_mapper.Map<Simulacao>(simulacaoDto));

        public IQueryable<SimulacaoDto> GetAll()
            => _simulacaoRepository.GetAll().ProjectTo<SimulacaoDto>(_mapper.ConfigurationProvider);

        public SimulacaoDto FindById(Guid id)
            => _simulacaoRepository.FindById(id);
    }
}