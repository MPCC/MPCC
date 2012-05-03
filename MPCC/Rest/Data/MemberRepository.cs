using System.Collections.Generic;
using Rest.Objects;

namespace Rest.Data
{
    public class MemberRepository
    {
        public static List<Member> GetMemberList(int index, int paging)
        {
            var list = new List<Member>();

            var s = Entity<Member>.CreateSessionFactory();

            using (var session = s.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var entities = session.CreateCriteria(typeof(Member))
                        .List<Member>();

                    foreach (var x in entities)
                    {
                        list.Add(x);
                    }
                    transaction.Commit();
                }
            }

            return list;
        }
    }
}