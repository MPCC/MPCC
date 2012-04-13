using System;
using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cache;

namespace Rest.Data
{
    public static class Entity<TData>
    {
        public static void Save(TData Entity)
        {
            var s = CreateSessionFactory();

            using (var session = s.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(Entity);
                    transaction.Commit();
                }
            }
        }

        public static void Update(TData Entity)
        {
            var s = CreateSessionFactory();

            using (var session = s.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(Entity);
                    transaction.Commit();
                }
            }
        }

        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(c => c.FromAppSetting("connectionString"))
                    .DefaultSchema("dbo"))
                .Cache(c => c.UseQueryCache().ProviderClass<HashtableCacheProvider>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Objects.Member>())
                .BuildSessionFactory();
        }

        //private void ConfigureAssemblies()
        //{
        //    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        //    {
        //        foreach (object attribute in assembly.GetCustomAttributes(true))
        //        {
        //            //if (attribute is HibernatePersistenceAssembly)
        //                //configuration.AddAssembly(assembly);
        //        }
        //    }
        //}
    }
}