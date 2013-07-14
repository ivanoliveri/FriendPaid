using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Domain.Exceptions;
using Domain.Facebook;
using NHibernate.Criterion;
using Repository.Interfaces;

namespace Repository.Implementations
{
    public class FacebookContactRepository : BaseRepository<FacebookContact>,IFacebookContactRepository
    {

        public FacebookContactRepository(IHibernateSessionFactory hibernateSessionFactory)
            : base(hibernateSessionFactory)
        {
        }

    }
}
