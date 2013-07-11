using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Domain;
using FluentValidation.Results;
using Web.Validators;
using friendpaid_web.ViewModels;

namespace Web.Controllers
{
    public class SignUpController : Controller
    {

        public ActionResult SignUp(LoginViewModel loginViewModel)
        {
            var newUser = new User();

            newUser.username = loginViewModel.username;

            newUser.password = loginViewModel.password;

            newUser.name = loginViewModel.name;

            newUser.lastName = loginViewModel.lastName;

            newUser.email = loginViewModel.email;

            SignUpValidator signUpValidator = new SignUpValidator();
            ValidationResult validationResult = signUpValidator.Validate(newUser);

            if (validationResult.IsValid)
            {
                //Agregar a la DB
            }
            else
            {
                loginViewModel = new LoginViewModel();
                loginViewModel.errors = validationResult.Errors;
            }

            return View("~/Views/Login/Index.cshtml", loginViewModel);
        }
    }
}
