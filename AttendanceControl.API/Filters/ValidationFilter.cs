using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AttendanceControl.API.Filters
{
    /// <summary>
    ///     Filtro de validación de datos.
    ///     Retorna un error 409 y un mensaje de error al cliente  
    ///     sobre el primer campo no validado encontrado. 
    /// </summary>
    public class ValidationFilter : IAsyncActionFilter
    {       
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {         
            if (!context.ModelState.IsValid)
            {
                var error = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => x.Value.Errors).ToList()
                    .First().Select(e => e.ErrorMessage).First();

                context.Result = new BadRequestObjectResult(error);

                return;
            }
           
            await next();
        }

    }
}
