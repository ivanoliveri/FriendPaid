using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Facebook;
using Newtonsoft.Json.Linq;
using Repository;
using Repository.Implementations;
using Services;
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
            #if DEBUG
                clientID = "362220377221811";
                clientSecret = "123b37e872674232efe4fb292ca48582";
            #else
                clientID = "427510220681184";
                clientSecret = "cb51bda21a82a578c944524b5438e71b";    
            #endif
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
            #if DEBUG
                clientID = "362220377221811";
                clientSecret = "123b37e872674232efe4fb292ca48582";
            #else
                clientID = "427510220681184";
                clientSecret = "cb51bda21a82a578c944524b5438e71b";    
            #endif

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

   //         try{
   //             MiembroRepositorio.Instance.buscar(me.username);
   //         }catch(NoEncontroMiembroException){
   //             var miembro = new User();
   //             miembro.apellido = me.last_name;
    //            miembro.nombre = me.first_name;
    //            miembro.nombreDeUsuario = me.username;
    //            miembro.email = me.email;
                
     //           JObject unosAmigosDeFacebook = JObject.Parse(me.friends.ToString()); 

      //          foreach (var unAmigoDeFacebook in unosAmigosDeFacebook["data"].Children())
       //         {
         //           string unId = unAmigoDeFacebook["id"].ToString().Replace("\"", "");
           //         string unNombre=unAmigoDeFacebook["name"].ToString().Replace("\"", "");
             //       miembro.addContactoDeFacebook(ContactoDeFacebook.Crear(unId, unNombre));
               // }

//                MiembroRepositorio.Instance.agregar(miembro);
  //          }

            return RedirectToAction("Index", "Notifications", new { username = me.username });
        }

        public ActionResult Index()
        {
            return View(new LoginViewModel());
        }
        
        public ActionResult SignIn(LoginViewModel viewModel)
        {
            userService.GetByUsernameAndPassword(viewModel.username, viewModel.password);
            return RedirectToAction("Index", "Notifications", new { username = viewModel.username });
        }


    }
}
