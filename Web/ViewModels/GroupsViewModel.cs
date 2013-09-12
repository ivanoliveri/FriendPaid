using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using FluentValidation.Results;

namespace Web.ViewModels
{
    public class GroupsViewModel:BaseViewModel
    {
        public override string username { get; set; }

        public override string groupName { get; set; }

        public override IList<string> errors { get; set; }

        public override string message { get; set; }

        public List<Group> groups { get; set; }

        public GroupsViewModel()
        {
            groups = new List<Group>();
        }
    }
}