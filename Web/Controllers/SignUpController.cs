using System;
using System.Collections.Generic;
using System.Configuration;
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

        [HttpPost]
        public ActionResult SignUp(LoginViewModel loginViewModel)
        {
            return View("~/Views/Login/Index.cshtml", Url.RouteUrl(ConfigurationManager.AppSettings["apiURL"] + "SignUp/SignUp/", new { loginViewModel = loginViewModel }));
        }
    }
}
