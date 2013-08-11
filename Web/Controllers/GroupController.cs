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
            var createGroupValidator = new CreateGroupValidator(groupService);

            var validationResult = createGroupValidator.Validate(viewModel);

            var user = userService.GetByUsername(viewModel.username);

            if (validationResult.IsValid){
                userService.CreateGroup(user,viewModel.groupName);
                viewModel.message = "Se ha creado satisfactoriamente el grupo.";
            }else{

                viewModel.errors = validationResult.Errors;

            }
            return View("IndexCreateGroup", viewModel);
        }

        public ActionResult SearchByName(string groupName, string username)
        {
            var viewModel = new GroupsViewModel();
            viewModel.username = username;
            try{
                viewModel.groups = groupService.GetGroupsWhichNamesBeginWith(groupName).ToList();
            }catch(GroupNotFoundException){
                viewModel.errors = new List<ValidationFailure>() {new ValidationFailure("", "No se han encontrado grupos.")};
            }
            return View("IndexSearchGroups", viewModel);
        }

        public ActionResult Join(string username,string groupname)
        {
            var currentUser = userService.GetByUsername(username);

            var currentGroup = groupService.GetByName(groupname);

            userService.JoinGroup(currentUser, currentGroup);

            return View("IndexGroups", new GroupsViewModel(){username = username});
        }
    }
}
