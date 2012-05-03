using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Rest.Objects;

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
                    try
                    {
                        session.Save(Entity);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.InnerException.ToString());
                    }
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
            try
            {
                return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(c => c
                    .FromAppSetting("connectionString"))
                .Cache(c => c
                    .UseQueryCache()).ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Member>())
                .BuildSessionFactory();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.ToString());
            }
        }
    }
}