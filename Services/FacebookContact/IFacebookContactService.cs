using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Domain.Facebook;

namespace Services
{
    public interface IFacebookContactService
    {
        IList<FacebookContact> GetAll();

        FacebookContact Get(int id);

        void Create(FacebookContact user);

        //  void Update(int id, Realty realty, string addres, string details);

        //  void Delete(int id);

    }
}
