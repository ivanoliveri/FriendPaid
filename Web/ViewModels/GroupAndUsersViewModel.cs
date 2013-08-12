using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using FluentValidation.Results;

namespace Web.ViewModels
{
    public class GroupsAndUsersViewModel:BaseViewModel
    {
        public override string username { get; set; }

        public override string groupName { get; set; }

        public override IList<ValidationFailure> errors { get; set; }

        public override string message { get; set; }

        public List<string> names { get; set; }

        public GroupsAndUsersViewModel()
        {
            names = new List<string>();
        }
    }
}