using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class GroupController : Controller
    {

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
            throw new NotImplementedException();
        }
    }
}
