using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Utils;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly IUserService userService;

        public NotificationsController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index(string username)
        {
            return View(Url.RouteUrl("localhost:8080/api/Notifications/Index/", new { username = username }));
        }

    }
}
