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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
            => _usuarioService = usuarioService;

        /// <summary>
        /// Consulta
        /// </summary>
        /// <response code="200">Consulta realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a consulta.</response>
        /// <response code="401">Acesso não autorizado.</response>
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(IQueryable<UsuarioDto>), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Get()
            => Ok(_usuarioService.GetAll());

        /// <summary>
        /// Consulta por id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Consulta realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a consulta.</response>
        /// <response code="404">Não localizado.</response>
        /// <response code="401">Acesso não autorizado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioDto), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Get(Guid id)
        {
            UsuarioDto usuario = _usuarioService.FindById(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Criar
        /// </summary>
        /// <param name="usuario"></param>
        /// <response code="200">Criado com sucesso.</response>
        /// <response code="400">Não foi possível criar.</response>
        /// <response code="401">Acesso não autorizado.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Post(UsuarioDto usuario)
        {
            if (!ModelState.IsValid)
            {
                throw new SimuladorContribuicaoAposentadoriaException("Os dados para criação são inválidos.");
            }

            _usuarioService.Create(usuario);
            return Ok();
        }

        /// <summary>
        /// Atualizar
        /// </summary>
        /// <param name="usuario"></param>
        /// <response code="200">Atualizado com sucesso.</response>
        /// <response code="400">Não foi possível atualizar.</response>
        /// <response code="404">Não localizado.</response>
        /// <response code="401">Acesso não autorizado.</response>
        [HttpPut]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Put(UsuarioDto usuario)
        {
            if (!ModelState.IsValid)
            {
                throw new SimuladorContribuicaoAposentadoriaException("Os dados para atualização são inválidos.");
            }

            if ((usuario.Id == null) || (_usuarioService.FindById(usuario.Id) == null))
            {
                return NotFound();
            }

            _usuarioService.Update(usuario);
            return Ok();
        }

        /// <summary>
        /// Excluir
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Exclusão realizada com sucesso.</response>
        /// <response code="400">Não foi possível realizar a exclusão.</response>
        /// <response code="404">Não localizado.</response>
        /// <response code="401">Acesso não autorizado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UsuarioDto), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Delete(Guid id)
        {
            UsuarioDto usuario = _usuarioService.FindById(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _usuarioService.Remove(usuario);
            return Ok();
        }
    }
}