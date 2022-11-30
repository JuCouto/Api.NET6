using ApiDotNet6.Domain.Authorization;
using ApiDotNet6.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Infra.Data.Authentication
{
    public class TokenGenerator : ITokenGenerator
    {
        // Claim - informações que serão retornadas dentro do token.
        public dynamic Generator(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email)
,
            new Claim("Id", user.Id.ToString())
            };

            var expires = DateTime.Now.AddDays(1);

            // Compondo a Chave do token.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("projetoDotNet6"));
            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                expires: expires,
                claims: claims
            );

            // Gerar e retornar o token.
            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
            return new
            {
                acess_token = token,
                expirations = expires
            };
        }
    }
}
