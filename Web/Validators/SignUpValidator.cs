using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using FluentValidation;

namespace Web.Validators
{
    public class SignUpValidator: AbstractValidator<User>
    {
        public SignUpValidator()
        {
            RuleFor(user => user.username).NotNull().WithMessage("Por favor, introduzca su nombre de usuario.");
            RuleFor(user => user.password).NotNull().WithMessage("Por favor, introduzca su password.");
            RuleFor(user => user.name).NotNull().WithMessage("Por favor, introduzca su nombre.");
            RuleFor(user => user.lastName).NotNull().WithMessage("Por favor, introduzca su apellido.");
            RuleFor(user => user.email).NotNull().WithMessage("Por favor, introduzca su e-mail.");
     
        }
    }
}