using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class NotificationsController : Controller
    {

        public ActionResult Index(string username)
        {
            var notificationsViewModel = new NotificationsViewModel();
            notificationsViewModel.username = username;
            return View(notificationsViewModel);
        }

    }
}
