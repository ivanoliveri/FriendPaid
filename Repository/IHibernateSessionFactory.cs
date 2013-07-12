using System;
using NHibernate;

namespace Repository
{

    public interface IHibernateSessionFactory : IDisposable
    {

        ISession GetSession();

        void TransactionalInterceptor(Action action);

        void SessionInterceptor(Action action);

    }
}