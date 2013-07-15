using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using Web.ViewModels;

namespace Web.ViewModelsBuilders
{
    public class CreateGroupViewModelBuilder
    {
        public static Group Convert(CreateGroupViewModel viewModel)
        {
            var newGroup = new Group();

            newGroup.name = viewModel.groupName;

            return newGroup;
        }
    }
}