using System;
using System.Collections.Generic;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Criterion;
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

        public static void Delete(TData Entity)
        {
            var s = CreateSessionFactory();

            using (var session = s.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(Entity);
                    transaction.Commit();
                }
            }
        }
        
        public static T FindOne<T>(object id) where T : class
        {
            var s = Entity<TData>.CreateSessionFactory();
            var entity = new Object();

            using (var session = s.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    entity = session.Get<TData>(id);
                    transaction.Commit();
                }
            }
            return (T)entity;
        }

        public static List<TData> FindMany<T>(ICriterion filter, int index, int paging, out long count) where T : class
        {
            var s = Entity<TData>.CreateSessionFactory();
            var skip = index > 1 ? (index * paging) / 2 : 0;
            var take = Math.Min(paging, 100);
            var list = new List<TData>();

            using (var session = s.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var cnt = session.QueryOver<T>()
                        .Where(filter)
                        .ToRowCountInt64Query()
                        .FutureValue<long>();

                    var entities = session.QueryOver<T>()
                        .Where(filter)
                        .Take(take)
                        .Skip(skip)
                        .List<TData>();

                    foreach (var x in entities)
                    {
                        list.Add(x);
                    }

                    count = cnt.Value;
                    transaction.Commit();
                }
            }
            
            return list;
        }

        private static ISessionFactory CreateSessionFactory()
        {
            try
            {
                return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(c => c
                    .FromConnectionStringWithKey("DefaultConnection"))
                .Cache(c => c
                    .UseQueryCache()).ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Member>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Family>())
                .BuildSessionFactory();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.ToString());
            }
        }
    }
}