using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimuladorContribuicaoAposentadoria.Api.Security;
using SimuladorContribuicaoAposentadoria.Domain.Dto;
using SimuladorContribuicaoAposentadoria.Domain.Exception;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Services;

namespace SimuladorContribuicaoAposentadoria.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
            => _usuarioService = usuarioService;

        /// <summary>
        /// Autenticar usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <response code="200">Usuário autenticado.</response>
        /// <response code="400">Usuário não autenticado.</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("Authenticate")]
        [ProducesResponseType(typeof(TokenDto), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Authenticate([FromBody] UsuarioDto usuario)
        {
            if (_usuarioService.FindByNomeSenha(usuario.Nome.ToLower(), usuario.Senha) == null)
            {
                throw new SimuladorContribuicaoAposentadoriaException("Usuário não autenticado.");
            }

            var accessToken = AccessToken.GenerateToken(usuario);
            return Ok(new TokenDto(usuario.Nome, accessToken));
        }

        /// <summary>
        /// Verificar o usuário autenticado.
        /// </summary>
        /// <response code="200">Usuário autenticado.</response>
        /// <response code="400">Usuário não autenticado.</response>
        /// <response code="401">Acesso não autorizado.</response>
        [HttpGet]
        [Authorize]
        [Route("Authenticated")]
        [ProducesResponseType(typeof(ExceptionMessage), 200)]
        [ProducesResponseType(typeof(ExceptionMessage), 400)]
        public IActionResult Authenticated()
            => Ok(new ExceptionMessage(string.Format("Usuário autenticado - {0}", User.Identity.Name)));
    }
}
