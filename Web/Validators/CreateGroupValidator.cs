using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using Domain.Exceptions;
using FluentValidation;
using Services;
using Web.ViewModels;

namespace Web.Validators
{
    public class CreateGroupValidator : AbstractValidator<CreateGroupViewModel>
    {
        
        public CreateGroupValidator(){}

        public CreateGroupValidator(IGroupService groupService)
        {
            RuleFor(group => group.groupName).NotEmpty().WithMessage("No has ingresado el nombre del grupo.");
            RuleFor(group => group.groupName).Must((group, groupname) => GroupnameIsFree(groupname,groupService))
                                             .WithMessage("El nombre de grupo que ingresaste no esta disponible.");
        }

        public bool GroupnameIsFree(string groupname,IGroupService groupService)
        {
            try{
                groupService.GetByName(groupname);
            }catch(GroupNotFoundException){
                return true;
            }
            return false;
        }

    }
}