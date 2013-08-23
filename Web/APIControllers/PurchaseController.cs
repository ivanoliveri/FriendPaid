using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain;
using FluentValidation.Results;
using Services;
using Web.Validators;
using Web.ViewModels;

namespace Web.APIControllers
{
    public class PurchaseController : ApiController
    {
        private readonly IUserService userService;

        private readonly IGroupService groupService;

        public PurchaseController(IUserService userService, IGroupService groupService)
        {
            this.userService = userService;

            this.groupService = groupService;
        }

        [HttpGet]
        public PurchaseViewModel Index(string username)
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
                }
                else
                {
                    newViewModel.groups = newViewModel.groups + "\"" + group.name + "\"";
                }
            }

            newViewModel.groups += "]";

            newViewModel.username = username;

            return newViewModel;
        }

        [HttpGet]
        public PurchaseViewModel Create(PurchaseViewModel viewModel)
        {
            var registerPurchaseValidator = new RegisterPurchaseValidator(userService);

            var validationResult = registerPurchaseValidator.Validate(viewModel);

            if (validationResult.IsValid)
            {

                var currentGroup = groupService.GetByName(viewModel.groupName);

                var currentBuyer = userService.GetByUsername(viewModel.username);

                var currentDebtors = currentGroup.members;

                var index = 0;
                var found = false;

                foreach (var currentDebtor in currentDebtors)
                {
                    if (found == false)
                    {
                        if (currentDebtor.username.Equals(currentBuyer.username))
                        {
                            found = true;
                        }
                        else
                        {
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
                try
                {
                    userService.RegisterPurchase(currentBuyer, newPurchase);
                    viewModel.message = "Se ha registrado satisfactoriamente la compra.";
                }
                catch
                {
                    viewModel.errors = new List<ValidationFailure>() { new ValidationFailure("", "El grupo ingresado no contiene miembros.") };
                }
            }
            else
            {
                viewModel.errors = validationResult.Errors;
            }

            ModelState.Clear();

            return viewModel;
        }
    }
}
