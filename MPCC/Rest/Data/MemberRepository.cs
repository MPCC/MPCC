using System.Collections.Generic;
using Auth;
using NHibernate.Criterion;
using Rest.Objects;

namespace Rest.Data
{
    public class MemberRepository
    {
        public static List<Member> GetMemberCollection(Principal principal, int index, int paging, out long count)
        {
            var bufilter = Restrictions.Where<Member>(x => x.BusinessUnitId == principal.BusinessUnitID && x.EnterpriseId == principal.EnterpriseID);
            return Entity<Member>.FindMany<Member>(bufilter, index, paging, out count);
        }

        public static Member GetMember(Principal principal, int id)
        {
            var member = Entity<Member>.FindOne<Member>(id);
            if(member.BusinessUnitId != principal.BusinessUnitID)
            {
                return new Member();
            }
            else
            {
                return member;
            }
        }
    }
}