using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using Domain.Exceptions;
using FluentValidation;
using Services;

namespace Web.Validators
{
    public class CreateGroupValidator : AbstractValidator<Group>
    {
        
        public CreateGroupValidator(){}

        public CreateGroupValidator(IGroupService groupService)
        {
            RuleFor(group => group.administrator).NotEmpty().WithMessage("No has ingresado el nombre del administrador.");
            RuleFor(group => group.name).NotEmpty().WithMessage("No has ingresado el nombre del grupo.");
            RuleFor(group => group.name).Must((group, groupname) => GroupnameIsFree(groupname,groupService))
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