using AttendanceControl.API.Business.Models;
using FluentValidation;

namespace AttendanceControl.API.Validators
{
    /// <summary>
    ///     Valida los datos de un ciclo formativo
    /// </summary>
    public class CycleValidator : AbstractValidator<Cycle>
    {
        public CycleValidator()
        {
            this.SetRuleForName();
        }

        private void SetRuleForName()
        {
            RuleFor(cycle => cycle.Name)
                .NotNull().WithMessage("El nombre del ciclo debe contener entre 1 y 255 caracteres.")
                .NotEmpty().WithMessage("El nombre del ciclo debe contener entre 1 y 255 caracteres.")
                .MaximumLength(255).WithMessage("El nombre del ciclo debe contener entre 1 y 255 caracteres.");
        }
    }
}
