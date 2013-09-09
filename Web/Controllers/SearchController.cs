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
            var viewModel = new GroupsViewModel();
            viewModel.username = username;
            if(textToSearch.Trim().Equals("")){
                viewModel.errors = new List<string>() {"No se ha ingresado un criterio de búsqueda."};
            }else{
                try{
                    viewModel.groups = groupService.GetGroupsWhichNamesBeginWith(textToSearch).ToList();
                }catch (GroupNotFoundException){
                    viewModel.errors = new List<string>() {"No se han encontrado grupos."};
                }
            }
            return View("IndexSearchGroups", viewModel);
        }
        public ActionResult SearchByUsername(string username,string textToSearch)
        {
            var viewModel = new UsersViewModel();
            viewModel.username = username;
            if(textToSearch.Trim().Equals("")){
                viewModel.errors = new List<string>() {"No se ha ingresado un criterio de búsqueda."};
            }else{
                try{
                    viewModel.users = userService.GetUsersWhoseNamesBeginWith(textToSearch).ToList();
                }catch (UserNotFoundException){
                    viewModel.errors = new List<string>() {"No se han encontrado usuarios."};
                }
            }
           
            return View("IndexSearchUsers", viewModel);
        }
        public ActionResult SearchByGroupnameAndUsername(BaseViewModel baseViewModel)
        {
            var viewModel = new GroupsAndUsersViewModel();
            viewModel.username = baseViewModel.username;
            try{
                var usersList = userService.GetUsersWhoseNamesBeginWith(baseViewModel.groupName).ToList();
                foreach (var user in usersList)
                {
                    viewModel.names.Add(user.username);
                }
            }catch (GroupNotFoundException){
                viewModel.errors = new List<string>() {"No se han encontrado grupos."};
            }

            try{
                var groupsList = groupService.GetGroupsWhichNamesBeginWith(baseViewModel.groupName).ToList();
                foreach (var group in groupsList)
                {
                    viewModel.names.Add(group.name);
                }
            }catch (GroupNotFoundException){
                viewModel.errors = new List<string>() {"No se han encontrado grupos."};
            }

            return View("IndexSearchGroupsAndUsers", viewModel);
        }
    }
}
