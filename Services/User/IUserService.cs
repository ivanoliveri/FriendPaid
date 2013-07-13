﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Services
{
    public interface IUserService
    {
        IList<User> GetAll();

        User Get(int id);

        void Create(User user);

        //  void Update(int id, Realty realty, string addres, string details);

        //  void Delete(int id);

    }
}
