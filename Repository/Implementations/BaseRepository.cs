using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Implementations
{

    public abstract class BaseRepository<T> where T : class
    {

        private IHibernateSessionFactory hibernateSessionFactory;

        protected BaseRepository(IHibernateSessionFactory hibernateSessionFactory)
        {
            this.hibernateSessionFactory = hibernateSessionFactory;
        }


        public T Get(int id)
        {
            return (T)this.GetSessionFactory().GetSession().Get(typeof(T), id);
        }

        public IList<T> GetAll()
        {
            return this.GetSessionFactory().GetSession().CreateCriteria(typeof(T)).List<T>();
        }

        public void Add(T o)
        {
            this.GetSessionFactory().GetSession().Save(o);
        }

        public void Delete(T o)
        {
            this.GetSessionFactory().GetSession().Delete(o);
        }

        public IHibernateSessionFactory GetSessionFactory()
        {
            return this.hibernateSessionFactory;
        }

    }
}

