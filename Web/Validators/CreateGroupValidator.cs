using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using Domain.Exceptions;
using FluentValidation;
using Services;

namespace Web.Validators
{
    public class CreateGroupValidator : AbstractValidator<Group>
    {

        public CreateGroupValidator()
        {
            RuleFor(group => group.administrator).NotEmpty().WithMessage("No has ingresado el nombre del administrador.");
            RuleFor(group => group.name).NotEmpty().WithMessage("No has ingresado el nombre del grupo.");
        }

    }
}