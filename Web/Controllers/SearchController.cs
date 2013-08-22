using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Exceptions;
using FluentValidation.Results;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IGroupService groupService;

        private readonly IUserService userService;

        public SearchController(IGroupService groupService, IUserService userService)
        {
            this.groupService = groupService;
            this.userService = userService;
        }

        public ActionResult SearchByGroupname(string username,string textToSearch)
        {
            return View("IndexSearchGroups", Url.RouteUrl("localhost:8080/api/Search/SearchByGroupname/", new { username = username, textToSearch = textToSearch }));
        }

        public ActionResult SearchByUsername(string username,string textToSearch)
        {
           return View("IndexSearchUsers", Url.RouteUrl("localhost:8080/api/Search/SearchByUsername/", new { username = username, textToSearch = textToSearch }));
        }
        
        public ActionResult SearchByGroupnameAndUsername(BaseViewModel baseViewModel)
        {
            return View("IndexSearchGroupsAndUsers", Url.RouteUrl("localhost:8080/api/Search/SearchByGroupnameAndUsername/", new { baseViewModel = baseViewModel }));
        }
    }
}
