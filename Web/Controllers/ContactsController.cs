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
            var user = userService.GetByUsername(username);
            var userToSendContactRequest = userService.GetByUsername(usernameToInvite);
            userService.SendContactRequest(user, userToSendContactRequest);
            var viewModel = new UsersViewModel();
            viewModel.username = username;
            viewModel.message = "Se ha enviado la solicitud de contacto satisfactoriamente.";
            viewModel.users.Add(userToSendContactRequest);
            return View("../Search/IndexSearchUsers", viewModel);
        }

        public ActionResult DeleteContactRequest(string username, string usernameToDeleteRequest)
        {
            var user = userService.GetByUsername(username);
            var userToDeleteRequest = userService.GetByUsername(usernameToDeleteRequest);
            var request = userToDeleteRequest.getContactPendingRequestFrom(user);
            userService.DeleteContactRequest(request);
            var viewModel = new UsersViewModel();
            viewModel.username = username;
            viewModel.message = "Se ha eliminado la solicitud de contacto satisfactoriamente.";
            viewModel.users.Add(userToDeleteRequest);
            return View("../Search/IndexSearchUsers", viewModel);
        }

        public ActionResult RejectContactRequest(string usernameReceiver, string usernameSender)
        {
            var user = userService.GetByUsername(usernameReceiver);
            var userToDeleteRequest = userService.GetByUsername(usernameSender);
            var request = user.getContactPendingRequestFrom(userToDeleteRequest);
            userService.RejectContactRequest(request);
            var viewModel = new NotificationsViewModel();
            viewModel.username = usernameReceiver;
            viewModel.message = "Se ha rechazado la solicitud de contacto satisfactoriamente.";
            return View("../Notifications/Index", viewModel);
        }

        public ActionResult AceptContactRequest(string usernameReceiver, string usernameSender)
        {
            var receiver = userService.GetByUsername(usernameReceiver);
            var sender = userService.GetByUsername(usernameSender);
            userService.AceptContactRequest(receiver.getContactPendingRequestFrom(sender));
            return RedirectToAction("Index", "Notifications", new { username = usernameReceiver });
        }

        public ActionResult DeleteContact(string username, string usernameToDelete)
        {
            var user = userService.GetByUsername(username);
            var userToDelete = userService.GetByUsername(usernameToDelete);
            userService.DeleteContact(user, userToDelete);
            return RedirectToAction("Index", new { username = username });
        }

    }
}
