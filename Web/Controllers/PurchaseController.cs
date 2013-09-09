using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using FluentValidation.Results;
using Services;
using Web.Validators;
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

            var currentUser = userService.GetByUsername(username);

            var groups = currentUser.groups;

            newViewModel.groups = "[";

            var count = 0;

            foreach (var group in groups)
            {
                count++;
                if (!count.Equals(groups.Count))
                {
                    newViewModel.groups = newViewModel.groups + "\"" + group.name + "\",";
                }else{
                    newViewModel.groups = newViewModel.groups + "\"" + group.name + "\"";
                }
            }

            newViewModel.groups += "]";

            newViewModel.username = username;

            return View(newViewModel);
        }

        public ActionResult Create(PurchaseViewModel viewModel)
        {
            var registerPurchaseValidator = new RegisterPurchaseValidator(userService);

            var validationResult = registerPurchaseValidator.Validate(viewModel);

            if (validationResult.IsValid){

                var currentGroup = groupService.GetByName(viewModel.groupName);

                var currentBuyer = userService.GetByUsername(viewModel.username);

                var currentDebtors = currentGroup.members;

                var index = 0;

                for (index = 0; index < currentDebtors.Count; index++)                
                    if (currentDebtors[index].equals(currentBuyer)) 
                        break;
                

                currentDebtors.RemoveAt(index);

                var newPurchase = new Purchase()
                {
                    buyer = currentBuyer,
                    debtors = currentDebtors,
                    description = viewModel.description,
                    group = currentGroup,
                    totalAmount = viewModel.totalAmount
                };
                try{
                    userService.RegisterPurchase(currentBuyer, newPurchase);
                    viewModel.message = "Se ha registrado satisfactoriamente la compra.";
                }catch{
                    viewModel.errors = new List<string>() { "El grupo ingresado no contiene miembros."};
                }
            }else{
                var stringErrors = new List<string>();
                validationResult.Errors.ToList().ForEach(oneError => stringErrors.Add(oneError.ErrorMessage));
                viewModel.errors = stringErrors;
            }

            ModelState.Clear();

            return View("Index", viewModel);
        }

    }

}
