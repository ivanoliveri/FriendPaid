using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Domain.Exceptions;
using FluentValidation.Results;
using Services;
using Web.ViewModels;

namespace Web.APIControllers
{
    public class SearchController : ApiController
    {
        private readonly IGroupService groupService;

        private readonly IUserService userService;

        public SearchController(IGroupService groupService, IUserService userService)
        {
            this.groupService = groupService;
            this.userService = userService;
        }

        [System.Web.Http.HttpGet]
        public GroupsViewModel SearchByGroupname(string username, string textToSearch)
        {
            var viewModel = new GroupsViewModel();
            viewModel.username = username;
            if (textToSearch.Trim().Equals(""))
            {
                viewModel.errors = new List<ValidationFailure>() { new ValidationFailure("", "No se ha ingresado un criterio de búsqueda.") };
            }
            else
            {
                try
                {
                    viewModel.groups = groupService.GetGroupsWhichNamesBeginWith(textToSearch).ToList();
                }
                catch (GroupNotFoundException)
                {
                    viewModel.errors = new List<ValidationFailure>() { new ValidationFailure("", "No se han encontrado grupos.") };
                }
            }
            return viewModel;
        }

        [System.Web.Http.HttpGet]
        public UsersViewModel SearchByUsername(string username, string textToSearch)
        {
            var viewModel = new UsersViewModel();
            viewModel.username = username;
            if (textToSearch.Trim().Equals(""))
            {
                viewModel.errors = new List<ValidationFailure>() { new ValidationFailure("", "No se ha ingresado un criterio de búsqueda.") };
            }
            else
            {
                try
                {
                    viewModel.users = userService.GetUsersWhoseNamesBeginWith(textToSearch).ToList();
                }
                catch (UserNotFoundException)
                {
                    viewModel.errors = new List<ValidationFailure>() { new ValidationFailure("", "No se han encontrado usuarios.") };
                }
            }

            return viewModel;
        }

        [System.Web.Http.HttpGet]
        public GroupsAndUsersViewModel SearchByGroupnameAndUsername(BaseViewModel baseViewModel)
        {
            var viewModel = new GroupsAndUsersViewModel();
            viewModel.username = baseViewModel.username;
            try
            {
                var usersList = userService.GetUsersWhoseNamesBeginWith(baseViewModel.groupName).ToList();
                foreach (var user in usersList)
                {
                    viewModel.names.Add(user.username);
                }
            }
            catch (GroupNotFoundException)
            {
                viewModel.errors = new List<ValidationFailure>() { new ValidationFailure("", "No se han encontrado grupos.") };
            }

            try
            {
                var groupsList = groupService.GetGroupsWhichNamesBeginWith(baseViewModel.groupName).ToList();
                foreach (var group in groupsList)
                {
                    viewModel.names.Add(group.name);
                }
            }
            catch (GroupNotFoundException)
            {
                viewModel.errors = new List<ValidationFailure>() { new ValidationFailure("", "No se han encontrado grupos.") };
            }

            return viewModel;
        }
    }
}
