using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Facebook;
using FluentValidation.Results;

namespace Web.ViewModels
{
    public class ContactsViewModel : BaseViewModel
    {
        public override string username { get; set; }

        public override IList<ValidationFailure> errors { get; set; }

        public List<FacebookContact> facebookContacts { get; set; }

        public List<string> contacts { get; set; }

        public ContactsViewModel()
        {
            contacts = new List<string>();
            facebookContacts = new List<FacebookContact>();
        }
    }
}