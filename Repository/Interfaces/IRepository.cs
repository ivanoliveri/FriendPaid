using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);

        void Add(T t);

        void Update(T t);

        void Delete(T t);

        IList<T> GetAll();

        IHibernateSessionFactory GetSessionFactory();
    }
}
