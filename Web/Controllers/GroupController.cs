using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;
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
            var newGroup = CreateGroupViewModelBuilder.Convert(viewModel);
            newGroup.administrator = userService.GetByUsername(viewModel.username);
            groupService.Create(newGroup);
            return View("IndexCreateGroup",new CreateGroupViewModel());
        }
    }
}
