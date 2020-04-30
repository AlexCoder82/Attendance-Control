using AttendanceControl.API.Business.Models;
using FluentValidation;

namespace AttendanceControl.API.Validators
{
    /// <summary>
    ///     Valida los datos de un profesor
    /// </summary>
    public class TeacherValidator : AbstractValidator<Teacher>
    {
        public TeacherValidator()
        {
            Include(new PersonDataValidator());
        }

    }
}
