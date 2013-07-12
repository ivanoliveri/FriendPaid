namespace Repository
{
    using System;

    using NHibernate;


    public interface IHibernateSessionFactory : IDisposable
    {

        ISession GetSession();


        void TransactionalInterceptor(Action action);


        void SessionInterceptor(Action action);
    }
}