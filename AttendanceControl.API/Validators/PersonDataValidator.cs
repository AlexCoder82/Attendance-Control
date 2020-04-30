using AttendanceControl.API.Business.Models;
using FluentValidation;

namespace AttendanceControl.API.Validators
{
    /// <summary>
    ///     Valida los datos de profesores y alumnos
    /// </summary>
    public  class PersonDataValidator : AbstractValidator<Person> 
    {
        public PersonDataValidator()
        {
            this.SetRuleForDni();
            this.SetRuleForFirstName();
            this.SetRuleForLastName1();
            this.SetRuleForLastName2();
        }

        private void SetRuleForLastName2()
        {
            RuleFor(p => p.LastName2)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .MaximumLength(255).WithMessage("El segundo apellido no puede contener más de 255 caracteres.")
               .Matches("^[a-zA-Z ]*$").WithMessage("El segundo apellido no es correcto");
        }

        private void SetRuleForLastName1()
        {
            RuleFor(p =>p.LastName1)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("El primer apellido debe contener entre 1 y 255 caracteres.")
                .NotEmpty().WithMessage("El primer apellido debe contener entre 1 y 255 caracteres.")
                .MaximumLength(255).WithMessage("El primer apellido debe contener entre 1 y 255 caracteres.")
                .Matches("^[a-zA-Z ]*$").WithMessage("El primer apellido no es correcto");
        }

        private void SetRuleForFirstName()
        {
            RuleFor(p => p.FirstName)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotNull().WithMessage("El nombre debe contener entre 1 y 255 caracteres.")
               .NotEmpty().WithMessage("El nombre debe contener entre 1 y 255 caracteres.")
               .MaximumLength(255).WithMessage("El nombre debe contener entre 1 y 255 caracteres.")
               .Matches("^[a-zA-Z ]*$").WithMessage("El nombre no es correcto");
        }

        private void SetRuleForDni()
        {
            RuleFor(p => p.Dni)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotNull().WithMessage("El dni debe contener 9 caracteres.")
               .NotEmpty().WithMessage("El dni debe contener 9 caracteres.")
               .MaximumLength(9).WithMessage("El dni debe contener 9 caracteres.")
               .MinimumLength(9).WithMessage("El dni debe contener 9 caracteres.")
               .Matches("^[a-zA-Z0-9]*$").WithMessage("El dni no es correcto");
        }
    }
}
