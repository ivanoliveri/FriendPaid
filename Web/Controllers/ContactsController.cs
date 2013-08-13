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

        public ActionResult Invite(string username, string usernameToInvite)
        {
            var user = userService.GetByUsername(username);
            var userToInvite = userService.GetByUsername(usernameToInvite);
            userService.Invite(user, userToInvite);
            return RedirectToAction("Index",new{username=username});
        }

        public ActionResult Delete(string username, string usernameToDelete)
        {
            var user = userService.GetByUsername(username);
            var userToDelete = userService.GetByUsername(usernameToDelete);
            userService.Delete(user, userToDelete);
            return RedirectToAction("Index", new { username = username });
        }

    }
}
