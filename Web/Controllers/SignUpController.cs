using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Domain;
using FluentValidation.Results;
using Repository;
using Repository.Implementations;
using Web.Validators;
using Web.ViewModels;

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
                var sessionFactory = new HibernateSessionFactory();
                var repository = new UserRepository(sessionFactory);
                repository.Add(newUser);
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
