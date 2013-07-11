using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ContactsController : Controller
    {

        public ActionResult Index(string username)
        {
            var newViewModel = new ContactsViewModel();
            newViewModel.username = username;
            return View(newViewModel);
        }

    }
}
