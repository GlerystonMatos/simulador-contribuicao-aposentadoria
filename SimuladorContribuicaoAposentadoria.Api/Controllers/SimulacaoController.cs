using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimuladorContribuicaoAposentadoria.Domain.Dto;
using SimuladorContribuicaoAposentadoria.Domain.Exception;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Services;
using System;
using System.Linq;

namespace SimuladorContribuicaoAposentadoria.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SimulacaoController : ControllerBase
    {
        private readonly ISimulacaoService _simulacaoService;

        public SimulacaoController(ISimulacaoService simulacaoService)
            => _simulacaoService = simulacaoService;

        /// <summary>
        /// Realizar simulação.
        /// </summary>
        /// <param name="informacoes"></param>
        /// <response code="200">Simulação realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a simulação.</response>
        [HttpPost("Simular")]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Simular(InformacoesDto informacoes)
        {
            if (!ModelState.IsValid)
            {
                throw new SimuladorContribuicaoAposentadoriaException("Os dados para realizar a simulação são inválidos.");
            }

            return Ok(_simulacaoService.Simular(informacoes));
        }

        /// <summary>
        /// Consulta de simulações.
        /// </summary>
        /// <response code="200">Consulta realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a consulta.</response>
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(IQueryable<SimulacaoDto>), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Get()
            => Ok(_simulacaoService.GetAll());

        /// <summary>
        /// Excluir simulação.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Exclusão realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a exclusão.</response>
        /// <response code="404">Simulação não localizada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SimulacaoDto), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Delete(Guid id)
        {
            SimulacaoDto simulacao = _simulacaoService.FindById(id);
            if (simulacao == null)
            {
                return NotFound();
            }

            _simulacaoService.Remove(simulacao);
            return Ok();
        }
    }
}