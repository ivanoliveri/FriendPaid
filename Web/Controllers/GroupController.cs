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
            return View();
        }

        public ActionResult IndexCreateGroup(string username)
        {
            return View();
        }

        public ActionResult Create(CreateGroupViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
