using AttendanceControl.API.Business.Models;
using FluentValidation;

namespace AttendanceControl.API.Validators
{
    /// <summary>
    ///     Validación de los datos de un alumno
    /// </summary>
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            Include(new PersonDataValidator());
        }
    }
}
