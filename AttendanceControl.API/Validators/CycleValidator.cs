using AttendanceControl.API.Business.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceControl.API.Validators
{
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
