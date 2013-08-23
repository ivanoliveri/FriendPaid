using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using FluentValidation.Results;
using Services;
using Web.Validators;
using Web.ViewModels;

namespace Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IUserService userService;

        private readonly IGroupService groupService;

        public PurchaseController(IUserService userService, IGroupService groupService)
        {
            this.userService = userService;

            this.groupService = groupService;
        }

        [HttpPost]
        public ActionResult Index(string username)
        {
            return View(Url.RouteUrl(ConfigurationManager.AppSettings["apiURL"] + "Purchase/Index/", new { username = username }));
        }

        [HttpPost]
        public ActionResult Create(PurchaseViewModel viewModel)
        {
            return View("Index", Url.RouteUrl(ConfigurationManager.AppSettings["apiURL"] + "Purchase/Create/", new { viewModel = viewModel }));
        }

    }

}
