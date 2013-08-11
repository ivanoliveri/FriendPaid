using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using FluentValidation;
using Services;
using Web.ViewModels;

namespace Web.Validators
{
    public class RegisterPurchaseValidator : AbstractValidator<PurchaseViewModel>
    {
        public RegisterPurchaseValidator(){}

        public RegisterPurchaseValidator(IUserService userService)
        {
            RuleFor(purchase => purchase.groupName).NotNull().WithMessage("No has ingresado el nombre del grupo.");
            RuleFor(purchase => purchase.groupName).Must((purchase, groupname) => AlreadyJoinedGroup(groupname, purchase.username, userService))
                                                   .WithMessage("No estas unido al grupo ingresado.");
            RuleFor(purchase => purchase.description).NotNull().WithMessage("No has ingresado la descripcion.");
            RuleFor(purchase => purchase.totalAmount).NotEqual(0).WithMessage("No has ingresado el monto."); 
        }
        private bool AlreadyJoinedGroup(string groupname,string username,IUserService userService)
        {
            var user = userService.GetByUsername(username);
            var groups = user.groups;
            return groups.Any(oneGroup=>oneGroup.name.Equals(groupname));
        }
    }
}