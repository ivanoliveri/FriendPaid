using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IUserService userService;

        private readonly IGroupService groupService;

        public PurchaseController(IUserService userService, IGroupService groupService)
        {
            this.userService = userService;

            this.groupService = groupService;
        }

        public ActionResult Index(string username)
        {
            var newViewModel = new PurchaseViewModel();
            newViewModel.username = username;
            return View(newViewModel);
        }

        public ActionResult Create(PurchaseViewModel viewModel)
        {
            var currentBuyer = userService.GetByUsername(viewModel.username);

            var currentGroup = groupService.GetByName(viewModel.groupName);

            var currentDebtors = currentGroup.members;
            currentDebtors.Add(currentGroup.administrator);

            var index = 0;
            var found = false;

            foreach (var currentDebtor in currentDebtors)
            {
                if (found == false)
                {
                    if (currentDebtor.username.Equals(currentBuyer.username))
                    {
                        found = true;
                    }else{
                        index++;
                    }
                }
            }
             
            currentDebtors.RemoveAt(index);

            var newPurchase = new Purchase()
            {
                buyer = currentBuyer,
                debtors = currentDebtors,
                description = viewModel.description,
                group = currentGroup,
                totalAmount = viewModel.totalAmount
            };

            userService.RegisterPurchase(currentBuyer, newPurchase);

            return View("Index", new PurchaseViewModel() { username = viewModel.username, message = "Se ha registrado satisfactoriamente la compra."});
        }

    }

}
