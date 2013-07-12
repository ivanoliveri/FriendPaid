using System;
using System.Configuration;
using Domain.NH.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Proxy.DynamicProxy;
using NHibernate.Tool.hbm2ddl;

using Configuration = NHibernate.Cfg.Configuration;
namespace Repository
{

    public class HibernateSessionFactory : IHibernateSessionFactory
    {

        private ISessionFactory sessionFactory;


        private ISession session;

        public static HibernateSessionFactory GetHibernateSessionFactory()
        {
            return null;
        }

        public HibernateSessionFactory()
        {
            var connString = ConfigurationManager.ConnectionStrings["FriendPaid"].ConnectionString;
            this.sessionFactory = Fluently.Configure()
                                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connString))
                                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMapping>())
                                .ExposeConfiguration(BuildSchema)
                                .BuildSessionFactory();
        }

        public ISession GetSession()
        {
            return this.session;
        }

        public void TransactionalInterceptor(Action action)
        {
            using (this.session = this.sessionFactory.OpenSession())
            {
                using (var transaction = this.session.BeginTransaction())
                {
                    action();
                    transaction.Commit();
                }
            }
        }

        public void SessionInterceptor(Action action)
        {
            using (this.session = this.sessionFactory.OpenSession())
            {
                action();
            }
        }

        public void Dispose()
        {
            //hibernateSessionFactory = null;
        }

        private static void BuildSchema(Configuration config)
        {
            var createSchema = ConfigurationManager.AppSettings["CreateDBSchema"];
            var generateSchema = !string.IsNullOrEmpty(createSchema) && bool.Parse(createSchema);

            // This NHibernate tool takes a configuration (with mapping info in) and exports a database schema from it
            var schemaExport = new SchemaExport(config);

            schemaExport.Drop(false, generateSchema);
            schemaExport.Create(false, generateSchema);
        }

        private static void ReferByteCode()
        {
            //Just to make sure the ByteCodeCastle is loaded
            ProxyFactory fake = new ProxyFactory();
        }


    }
}