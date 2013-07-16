using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            newViewModel.groups = groupService.GetAll().Where( group =>group.administrator.username.Equals(username)).ToList();
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

            CreateGroupValidator createGroupValidator = new CreateGroupValidator();

            ValidationResult validationResult = createGroupValidator.Validate(newGroup);

            if (validationResult.IsValid)
            {
                groupService.Create(newGroup);

            }else{

                viewModel.errors = validationResult.Errors;

            }
            return View("IndexCreateGroup", viewModel);
        }

        public ActionResult SearchByName(string groupName, string username)
        {
            var viewModel = new GroupsViewModel();
            viewModel.username = username;
            viewModel.groups = groupService.GetGroupsWhichNamesBeginWith(groupName).ToList();
            return View("IndexSearchGroups", viewModel);
        }
    }
}
