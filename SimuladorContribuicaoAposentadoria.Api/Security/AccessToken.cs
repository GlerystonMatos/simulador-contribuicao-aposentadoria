using Microsoft.IdentityModel.Tokens;
using SimuladorContribuicaoAposentadoria.Api.Configuracoes;
using SimuladorContribuicaoAposentadoria.Domain.Dto;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimuladorContribuicaoAposentadoria.Api.Security
{
    public static class AccessToken
    {
        public static string GenerateToken(UsuarioDto usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenConfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Nome.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(TokenConfig.ExpireTimeInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}