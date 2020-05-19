using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using AttendanceControl.API.Application.Contracts.IAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using AttendanceControl.API.Business.Enums;

namespace AttendanceControl.API.Application.Auth
{
    /// <summary>
    ///     Objeto que genera un token de sesion
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        public readonly IConfiguration _configuration;

        public AuthService(ILogger<AuthService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        /// <summary>
        ///     Genera un token tanto para administrador 
        ///     como para un profesor
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

            //Tiempo de sesión para cada role
            int minutes = 0;
            if (role == Role.TEACHER)
            {
                minutes =_configuration.GetValue<int>("Jwt:TeacherSessionExpirationTime");
            }
            if((role == Role.ADMIN))
            {
                minutes = _configuration.GetValue<int>("Jwt:AdminSessionExpirationTime");
            }      
            DateTime date = DateTime.UtcNow;
            TimeSpan validTime = TimeSpan.FromMinutes(minutes);
            var expire = date.Add(validTime);

            //Reclamaciones para cada petición
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, sub ),
                new Claim(JwtRegisteredClaimNames.Iat,new DateTimeOffset(date).ToUniversalTime()
                        .ToUnixTimeMilliseconds().ToString(),ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Role, role)
            };

            //Generación del token a partir de la clave secreta
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Secret"));
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: date,
                expires: expire,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            _logger.LogInformation("Token de sesión generado");

            return token;

        }
    }
}
