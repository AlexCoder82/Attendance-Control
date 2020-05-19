using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API.Controllers
{
    /// <summary>
    ///     Cualquera excepción no gestionada localmente es enviada 
    ///     a la ruta /error
    /// </summary>
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Ruta que recupera cualquier excepción no manejada localmente   
        /// </summary>
        /// <returns>
        ///     Retorna un error 500 con un objeto ApiError
        /// </returns>
        [Route("error")]
        public IActionResult Error()
        {

            var excepcionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var exception = excepcionHandler.Error;
            int statusCode = 500;

            ApiError apiError = new ApiError()
            {
                StatusCode = statusCode,
                Error = exception.GetType().ToString(),
                Message = exception.Message,
                Timestamp = DateTime.Now,
                Path = excepcionHandler.Path
            };

            _logger.LogError(apiError.ToString());

            return StatusCode(statusCode);

        }

    }
}