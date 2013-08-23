using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain;
using Services;
using Web.Encryption;
using Web.Validators;
using Web.ViewModels;

namespace Web.APIControllers
{
    public class SignUpController : ApiController
    {
        private readonly IUserService userService;

        public SignUpController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public LoginViewModel SignUp(LoginViewModel loginViewModel)
        {
            var newUser = new User();

            newUser.username = loginViewModel.username;

            newUser.password = loginViewModel.password;

            newUser.name = loginViewModel.name;

            newUser.lastName = loginViewModel.lastName;

            newUser.email = loginViewModel.email;

            var signUpValidator = new SignUpValidator(userService);

            var validationResult = signUpValidator.Validate(loginViewModel);

            loginViewModel = new LoginViewModel();

            if (validationResult.IsValid)
            {
                newUser.password = PasswordHash.CreateHash(newUser.password);
                userService.Create(newUser);
                loginViewModel.message = "Se ha creado satisfactoriamente el usuario.";
            }
            else
            {
                loginViewModel = new LoginViewModel();
                loginViewModel.errors = validationResult.Errors;
            }

            ModelState.Clear();

            return loginViewModel;
        }
    }
}
