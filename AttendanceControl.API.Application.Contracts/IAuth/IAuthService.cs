﻿
namespace AttendanceControl.API.Application.Contracts.IAuth
{
    /// <summary>
    ///     Contratos del servicio de autenticación
    /// </summary>
    public interface IAuthService
    {
        public bool ValidateToken();
        public string GenerateToken(string sub, string role);
    }
}
