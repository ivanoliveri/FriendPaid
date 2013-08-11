using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using Domain.Exceptions;
using FluentValidation;
using Services;
using Web.ViewModels;

namespace Web.Validators
{
    public class SignUpValidator: AbstractValidator<LoginViewModel>
    {
        public SignUpValidator(IUserService userService)
        {
            RuleFor(user => user.username).NotNull().WithMessage("Por favor, introduzca su nombre de usuario.");
            RuleFor(user => user.username).Must((user, username) => UsernameIsFree(username, userService))
                                          .WithMessage("El nombre de usuario ingresado no esta disponible.");
            RuleFor(user => user.password).NotNull().WithMessage("Por favor, introduzca su password.");
            RuleFor(user => user.name).NotNull().WithMessage("Por favor, introduzca su nombre.");
            RuleFor(user => user.lastName).NotNull().WithMessage("Por favor, introduzca su apellido.");
            RuleFor(user => user.email).NotNull().WithMessage("Por favor, introduzca su e-mail.");
        }

        private bool UsernameIsFree(string username, IUserService userService)
        {
            try{
                userService.GetByUsername(username);
            }catch(UserNotFoundException){
                return true;
            }
            return false;
        }
    }
}