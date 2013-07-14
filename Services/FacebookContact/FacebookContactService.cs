using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Domain.Facebook;
using Repository.Interfaces;

namespace Services
{
    public class FacebookContactService : IFacebookContactService
    {
         private readonly IFacebookContactRepository facebookContactRepository;

         public FacebookContactService(IFacebookContactRepository facebookContactRepository)
        {
            this.facebookContactRepository = facebookContactRepository;
        }

         public IList<FacebookContact> GetAll()
        {
            IList<FacebookContact> result = null;

            this.facebookContactRepository.GetSessionFactory().SessionInterceptor(() =>
            {
                result = this.facebookContactRepository.GetAll();
            });

            return result;
        }

         public FacebookContact Get(int id)
        {
            FacebookContact result = null;

            this.facebookContactRepository.GetSessionFactory().SessionInterceptor(() =>
            {
                result = this.facebookContactRepository.Get(id);
            });

            return result;
        }

         public void Create(FacebookContact facebookContact)
        {
            this.facebookContactRepository.GetSessionFactory().TransactionalInterceptor(() =>
            {
                this.facebookContactRepository.Add(facebookContact);
            });
        }

      //  void Update(int id, Realty realty, string addres, string details);

      //  void Delete(int id);
    }
}
