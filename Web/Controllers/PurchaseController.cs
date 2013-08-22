using System;
using System.Collections.Generic;
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

        public ActionResult Index(string username)
        {
            return View(Url.RouteUrl("localhost:8080/api/Purchase/Index/", new { username = username }));
        }

        public ActionResult Create(PurchaseViewModel viewModel)
        {
            return View("Index", Url.RouteUrl("localhost:8080/api/Purchase/Create/", new { viewModel = viewModel }));
        }

    }

}
