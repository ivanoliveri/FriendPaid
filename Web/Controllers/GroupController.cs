using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class GroupController : Controller
    {

        public ActionResult IndexGroups()
        {
            return View();
        }

        public ActionResult IndexCreateGroup()
        {
            return View();
        }

        public ActionResult Create()
        {
            throw new NotImplementedException();
        }
    }
}
