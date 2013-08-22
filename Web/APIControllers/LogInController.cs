using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Exceptions;
using FluentValidation.Results;
using Services;
using Web.Encryption;
using Web.ViewModels;

namespace Web.APIControllers
{
    public class LogInController : ApiController
    {
        private readonly IUserService userService;

        public LogInController(IUserService userService)
        {
            this.userService = userService;
        }

        public string SignIn(LoginViewModel viewModel)
        {
            try{
                string hashPass = userService.GetHashPasswordFromUser(viewModel.username); //trae de la db la pass original(si no la encuentra es porque no existe user, tira excepcion)

                if (!PasswordHash.ValidatePassword(viewModel.password, hashPass)) throw new UserNotFoundException();


            }catch (UserNotFoundException){
                ModelState.Clear();

                return null;
            }

            return viewModel.username;
        }
    }
}
