using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Domain;
using Domain.Exceptions;
using Domain.Facebook;
using Facebook;
using FluentValidation.Results;
using Newtonsoft.Json.Linq;
using Repository;
using Repository.Implementations;
using Services;
using Web.Encryption;
using Web.ViewModels;

namespace Web.Controllers
{
    public class LogInController : Controller
    {        
        private readonly IUserService userService;

        public LogInController(IUserService userService)
        {
            this.userService = userService;
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult Facebook()
        {
            string clientID;
            string clientSecret;

            clientID = ConfigurationManager.AppSettings["clientID"];

            clientSecret = ConfigurationManager.AppSettings["clientSecret"];

            var fb = new FacebookClient();

            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = clientID,
                client_secret = clientSecret,
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email" 
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            string clientID;
            string clientSecret;

            clientID = ConfigurationManager.AppSettings["clientID"];

            clientSecret = ConfigurationManager.AppSettings["clientSecret"];

            var fb = new FacebookClient();

            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = clientID,
                client_secret = clientSecret,
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            // Store the access token in the session
            Session["AccessToken"] = accessToken;

            // update the facebook client with the access token so 
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

            // Get the user's information
            dynamic me = fb.Get("me?fields=first_name,last_name,id,email,username,friends");

            // Set the auth cookie
            FormsAuthentication.SetAuthCookie(me.email, false);

            try{

                userService.GetByUsername(me.username);

            }catch(UserNotFoundException){

                var newUser = new User();
                newUser.lastName = me.last_name;
                newUser.name = me.first_name;
                newUser.username = me.username;
                newUser.email = me.email;
                
                userService.Create(newUser);
            }

            JObject facebookContacts = JObject.Parse(me.friends.ToString());

            var allFacebookContacts = new List<FacebookContact>();
            foreach (var facebookContact in facebookContacts["data"].Children())
            {
                Int64 facebookContactId = Int64.Parse(facebookContact["id"].ToString().Replace("\"", ""));

                string facebookContacName = facebookContact["name"].ToString().Replace("\"", "");

                var newFacebookContact = new FacebookContact()
                {
                    name = facebookContacName,
                    facebookId = facebookContactId
                };

                allFacebookContacts.Add(newFacebookContact);

            }

            Session["facebookContacts"] = allFacebookContacts;

            return RedirectToAction("Index", "Notifications", new { username = me.username });
        }

        public ActionResult Index()
        {
            return View(new LoginViewModel());
        }
        
        public ActionResult SignIn(LoginViewModel viewModel)
        {
            try
            {
                string hashPass = userService.GetHashPasswordFromUser(viewModel.username); //trae de la db la pass original(si no la encuentra es porque no existe user, tira excepcion)

                if(! PasswordHash.ValidatePassword(viewModel.password, hashPass)) throw new UserNotFoundException();

                
            }catch(UserNotFoundException)
            {
                viewModel.errors = new List<ValidationFailure>() { new ValidationFailure(null, "Combinación incorrecta de usuario/contraseña") };
                
                ModelState.Clear();

                return View("Index", new LoginViewModel());
            }

            Session["facebookContacts"] = null;

            return RedirectToAction("Index", "Notifications", new { username = viewModel.username });
        }


    }
}
