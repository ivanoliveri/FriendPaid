using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Exceptions;
using FluentValidation.Results;
using Services;
using Web.Validators;
using Web.ViewModels;
using Web.ViewModelsBuilders;

namespace Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService groupService;

        private readonly IUserService userService;
        
        public GroupController(IGroupService groupService, IUserService userService)
        {
            this.groupService = groupService;
            this.userService = userService;
        }

        public ActionResult IndexGroups(string username)
        {
            var newViewModel = new GroupsViewModel();

            newViewModel.username = username;

            var currentUser = userService.GetByUsername(username);

            var groupsNamesJoined = new List<string>();

            currentUser.groups.ToList().ForEach(group => groupsNamesJoined.Add(group.name));

            newViewModel.groups = groupService.GetAll().Where(group => group.administrator.username.Equals(username) || groupsNamesJoined.Contains(group.name) ).ToList();
            
            return View(newViewModel);
        }

        public ActionResult IndexCreateGroup(string username)
        {
            var newViewModel = new CreateGroupViewModel();
            newViewModel.username = username;
            return View(newViewModel);
        }

        public ActionResult Create(CreateGroupViewModel viewModel)
        {
            var newViewModel = Url.RouteUrl("localhost:8080/api/Group/Create/", new { viewModel = viewModel });

            return View("IndexCreateGroup", newViewModel);
        }

        public ActionResult Join(string username,string groupname)
        {
            var viewModel = Url.RouteUrl("localhost:8080/api/Group/Join/", new { username = username, groupname = groupname });
            
            return View("IndexGroups",viewModel);
        }
        public ActionResult Leave(string username,string groupname)
        {
            var viewModel = Url.RouteUrl("localhost:8080/api/Group/Leave/", new {username=username, groupname=groupname} );

            return View("IndexGroups", viewModel);
        }

    }
}
