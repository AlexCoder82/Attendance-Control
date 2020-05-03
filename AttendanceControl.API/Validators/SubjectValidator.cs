using AttendanceControl.API.Business.Models;
using FluentValidation;


namespace AttendanceControl.API.Validators
{
    /// <summary>
    ///     Validación de asignaturas
    /// </summary>
    public class SubjectValidator : AbstractValidator<Subject>
    {

        public SubjectValidator()
        {
            this.SetRuleForName();
        }

        private void SetRuleForName()
        {
            RuleFor(cycle => cycle.Name)
                .NotNull().WithMessage("El nombre de la asignatura debe contener entre 1 y 255 caracteres.")
                .NotEmpty().WithMessage("El nombre de la asignatura debe contener entre 1 y 255 caracteres.")
                .MaximumLength(255).WithMessage("El nombre de la asignatura debe contener entre 1 y 255 caracteres.");
        }
   
    }
}
