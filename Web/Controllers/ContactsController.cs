using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Facebook;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IUserService userService;

        public ContactsController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public ActionResult Index(string username)
        {
            var newViewModel = new ContactsViewModel();
            var user = userService.GetByUsername(username);
            foreach (var contact in user.contacts)
            {
                newViewModel.contacts.Add(contact.username);
            }
            newViewModel.username = username;
            var allFacebookContacts = (List<FacebookContact>) Session["facebookContacts"];
            if(allFacebookContacts!=null)
                newViewModel.facebookContacts = allFacebookContacts.OrderBy(elem => Guid.NewGuid()).Take(4).ToList();
            return View(newViewModel);
        }

        [HttpPost]
        public ActionResult SendContactRequest(string username,string usernameToInvite)
        {
            var viewModel = Url.RouteUrl(ConfigurationManager.AppSettings["apiURL"] + "Contacts/SendContactRequest/", new { username = username, usernameToInvite = usernameToInvite });

            return View("../Search/IndexSearchUsers", viewModel);
        }

        [HttpPost]
        public ActionResult DeleteContactRequest(string username, string usernameToDeleteRequest)
        {
            var viewModel = Url.RouteUrl(ConfigurationManager.AppSettings["apiURL"] + "Contacts/DeleteContactRequest/", new { username = username, usernameToDeleteRequest = usernameToDeleteRequest });

            return View("../Search/IndexSearchUsers", viewModel);
        }

        [HttpPost]
        public ActionResult RejectContactRequest(string usernameReceiver, string usernameSender)
        {
            var viewModel = Url.RouteUrl(ConfigurationManager.AppSettings["apiURL"] +"Contacts/RejectContactRequest/", new { usernameReceiver = usernameReceiver, usernameSender = usernameSender });

            return View("../Notifications/Index", viewModel);
        }

        [HttpPost]
        public ActionResult AceptContactRequest(string usernameReceiver, string usernameSender)
        {
            var username = Url.RouteUrl(ConfigurationManager.AppSettings["apiURL"] +"Contacts/AceptContactRequest/", new { usernameReceiver = usernameReceiver, usernameSender = usernameSender });

            return RedirectToAction("Index", "Notifications", new { username = username });
        }

        [HttpPost]
        public ActionResult DeleteContact(string username, string usernameToDelete)
        {
            var newUsername = Url.RouteUrl(ConfigurationManager.AppSettings["apiURL"] +"Contacts/DeleteContact/", new { username = username, usernameToDelete = usernameToDelete });

            return RedirectToAction("Index", new { username = newUsername });
        }

    }
}
