using System.Collections.Generic;
using System.Web;
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

        public static Member UpdateMember(Principal principal, Member member)
        {
            if(principal.MemberID != member.MemberId)
            {
                throw new HttpException(401, "Unauthorized");
            }
            Entity<Member>.Save(member);
            return member;
        }
    }
}