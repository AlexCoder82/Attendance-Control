using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Application.Contracts.IAuth
{
    public interface IAuthService
    {
        public bool ValidateToken();
        public string GenerateToken(string sub, string role);
    }
}
