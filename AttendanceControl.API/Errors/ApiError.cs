using System;

namespace AttendanceControl.API.Errors
{
    /// <summary>
    ///     Objecto instanciado si se lanza 
    ///     una excepción no prevista, contiene información
    ///     sobre la excepción.
    /// </summary>
    public class ApiError
    {
        public DateTime Timestamp { get; set; }
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public override string ToString()
        {
            return String.Format("\n\nERROR RETORNADO:\t {0}\t{1}\t{2}\t{3}\n\n",
                Timestamp, StatusCode, Error, Message);
        }
    }
}
