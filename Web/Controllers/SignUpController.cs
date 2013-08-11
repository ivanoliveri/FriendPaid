using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Domain;
using FluentValidation.Results;
using Services;
using Web.Encryption;
using Web.Validators;
using Web.ViewModels;

namespace Web.Controllers
{
    public class SignUpController : Controller
    {

        private readonly IUserService userService;

        public SignUpController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult SignUp(LoginViewModel loginViewModel)
        {
            var newUser = new User();

            newUser.username = loginViewModel.username;

            newUser.password = loginViewModel.password;

            newUser.name = loginViewModel.name;

            newUser.lastName = loginViewModel.lastName;

            newUser.email = loginViewModel.email;

            var signUpValidator = new SignUpValidator(userService);
            
            var validationResult = signUpValidator.Validate(loginViewModel);

            loginViewModel= new LoginViewModel();

            if (validationResult.IsValid)
            {
                newUser.password = PasswordHash.CreateHash(newUser.password);
                userService.Create(newUser);
                loginViewModel.message = "Se ha creado satisfactoriamente el usuario.";
            }else{
                loginViewModel = new LoginViewModel();
                loginViewModel.errors = validationResult.Errors;
            }

            ModelState.Clear();

            return View("~/Views/Login/Index.cshtml", loginViewModel);
        }
    }
}
