using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Results;

namespace Web.ViewModels
{
    public abstract class BaseViewModel
    {

        public abstract string username { get; set; }

        public abstract string groupName { get; set; }

        public abstract IList<ValidationFailure> errors { get; set; }

    }
}