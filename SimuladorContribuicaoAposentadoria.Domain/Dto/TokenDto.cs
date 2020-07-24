using System.ComponentModel;

namespace SimuladorContribuicaoAposentadoria.Domain.Dto
{
    [DisplayName("Token")]
    public class TokenDto
    {
        public TokenDto(string usuario, string token)
        {
            Usuario = usuario;
            Token = token;
        }

        public string Usuario { get; set; }

        public string Token { get; set; }
    }
}