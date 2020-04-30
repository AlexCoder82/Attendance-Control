using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Application.Contracts.DTOs
{
    public class AdminSignInResponse
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
