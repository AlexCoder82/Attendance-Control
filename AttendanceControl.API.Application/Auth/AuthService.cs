using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using AttendanceControl.API.Application.Contracts.IAuth;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API.Application.Auth
{
    /// <summary>
    ///     Objeto que genera un token de sesion
    /// </summary>
    public class AuthService : IAuthService
    {
        private ILogger<AuthService> _logger;

        public AuthService(ILogger<AuthService> logger)
        {
            _logger = logger;
        }

        public bool ValidateToken()
        {
            return true;
        }

        /// <summary>
        ///     Genera un token tanto para administrador como para
        ///     un profesor
        /// </summary>
        /// <param name="sub">
        ///     Cadena a partir de la cual se va crear el token
        /// </param>
        /// <param name="role">
        ///     Role de la sesión
        /// </param>
        /// <returns></returns>
        public string GenerateToken(string sub, string role)
        {

            //Tiempo de sesión
            DateTime date = DateTime.UtcNow;
            TimeSpan validTime = TimeSpan.FromMinutes(30);
            var expire = date.Add(validTime);

            //Reclamaciones
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, sub ),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(date).ToUniversalTime()
                        .ToUnixTimeMilliseconds().ToString(),ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Role, role)
            };


            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding
                            .ASCII.GetBytes("AAfjoègfjèjf`jeof`jeòfjpo`jfo51561f456a4f")),
                            SecurityAlgorithms.HmacSha256Signature);

            var jwt = new JwtSecurityToken(
                issuer: "viva la pepa",
                audience: "",
                claims: claims,
                notBefore: date,
                expires: expire,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            _logger.LogInformation("Token de administrador generado");

            return token;

        }
    }
}
