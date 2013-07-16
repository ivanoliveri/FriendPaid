using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using Domain.Requests;
using FluentValidation.Results;
using Domain.Notifications;

namespace Web.ViewModels
{
    public class NotificationsViewModel : BaseViewModel
    {
        public override string username { get; set; }

        public override string groupName { get; set; }

        public override IList<ValidationFailure> errors { get; set; }

        public IEnumerable<Notification> unreadNotifications { get; set; }

        public IEnumerable<ContactRequest> pendingContactRequest { get; set; }

        public IEnumerable<Payment> unpaidPayments { get; set; }

        public IEnumerable<Payment> paidPayments { get; set; }

        public NotificationsViewModel()
        {
            unreadNotifications = new List<Notification>();
            pendingContactRequest= new List<ContactRequest>();
            unpaidPayments = new List<Payment>();
            paidPayments = new List<Payment>();
        }

    }
}