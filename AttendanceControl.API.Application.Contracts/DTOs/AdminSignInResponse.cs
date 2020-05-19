
namespace AttendanceControl.API.Application.Contracts.DTOs
{
    /// <summary>
    ///     Objeto que contiene los datos que se retornan al cliente cuando 
    ///     un administrador abre una sesión
    /// </summary>
    public class AdminSignInResponse
    {
        public int Id { get; set; }
        public string Token { get; set; }

    }
}
