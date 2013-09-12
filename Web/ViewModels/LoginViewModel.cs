﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Results;

namespace Web.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        public override string username { get; set; }

        public override string groupName { get; set; }

        public override IList<string> errors { get; set; }

        public override string message { get; set; }

        public string password { get; set; }

        public string name { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

    }
}