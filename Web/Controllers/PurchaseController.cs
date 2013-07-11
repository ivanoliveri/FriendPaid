using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class PurchaseController : Controller
    {

        public ActionResult Index(string username)
        {
            return View();
        }

        public ActionResult Create(PurchaseViewModel viewModel)
        {
            throw new NotImplementedException();
        }

    }
}
