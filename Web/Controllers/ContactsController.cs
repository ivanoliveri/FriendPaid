using System;
using System.Collections.Generic;
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

        public ActionResult SendContactRequest(string username,string usernameToInvite)
        {
            var viewModel = Url.RouteUrl("localhost:8080/api/Contacts/SendContactRequest/", new { username = username, usernameToInvite = usernameToInvite });

            return View("../Search/IndexSearchUsers", viewModel);
        }

        public ActionResult DeleteContactRequest(string username, string usernameToDeleteRequest)
        {
            var viewModel = Url.RouteUrl("localhost:8080/api/Contacts/DeleteContactRequest/", new { username = username, usernameToDeleteRequest = usernameToDeleteRequest });

            return View("../Search/IndexSearchUsers", viewModel);
        }

        public ActionResult RejectContactRequest(string usernameReceiver, string usernameSender)
        {
            var viewModel = Url.RouteUrl("localhost:8080/api/Contacts/RejectContactRequest/", new { usernameReceiver = usernameReceiver, usernameSender = usernameSender });

            return View("../Notifications/Index", viewModel);
        }

        public ActionResult AceptContactRequest(string usernameReceiver, string usernameSender)
        {
            var username = Url.RouteUrl("localhost:8080/api/Contacts/AceptContactRequest/", new { usernameReceiver = usernameReceiver, usernameSender = usernameSender });

            return RedirectToAction("Index", "Notifications", new { username = username });
        }

        public ActionResult DeleteContact(string username, string usernameToDelete)
        {
            var newUsername = Url.RouteUrl("localhost:8080/api/Contacts/DeleteContact/", new { username = username, usernameToDelete = usernameToDelete });

            return RedirectToAction("Index", new { username = newUsername });
        }

    }
}
