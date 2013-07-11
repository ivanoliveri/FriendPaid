using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using Web.ViewModels;

namespace Web.ViewModelsBuilders
{
    public class PurchaseViewModelBuilder
    {
        public static PurchaseViewModel Build(Purchase purchase)
        {
            var newViewModel = new PurchaseViewModel();

            newViewModel.description = purchase.description;

            newViewModel.totalAmount = purchase.totalAmount;

            newViewModel.groupName = purchase.group.name;

            return newViewModel;
        }
    }
}