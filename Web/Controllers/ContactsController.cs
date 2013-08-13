using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Facebook;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ContactsController : Controller
    {

        public ActionResult Index(string username)
        {
            var newViewModel = new ContactsViewModel();
            newViewModel.username = username;
            var allFacebookContacts = (List<FacebookContact>) Session["facebookContacts"];
            if(allFacebookContacts!=null)
                newViewModel.facebookContacts = allFacebookContacts.OrderBy(elem => Guid.NewGuid()).Take(4).ToList();
            return View(newViewModel);
        }

    }
}
