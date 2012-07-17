using System;
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
                throw new HttpException(400, "You do not have permissions to update this member.");
            }

            var m = GetMember(principal, member.MemberId);
            m.Street = String.IsNullOrEmpty(member.Street) ? m.Street : member.Street;
            m.Zip = member.Zip ?? m.Zip;
            m.Apt = String.IsNullOrEmpty(member.Apt) ? m.Apt : member.Apt; 
            m.City = String.IsNullOrEmpty(member.City) ? m.City : member.City; 
            m.DateOfBirth = member.DateOfBirth;
            m.Email = String.IsNullOrEmpty(member.Email) ? m.Email : member.Email;
            m.FamilyId = member.FamilyId ?? 0;
            m.FirstName = String.IsNullOrEmpty(member.FirstName) ? String.Empty : member.FirstName;
            m.MiddleName = String.IsNullOrEmpty(member.MiddleName) ? String.Empty : member.MiddleName;
            m.LastName = String.IsNullOrEmpty(member.LastName) ? String.Empty : member.LastName;
            m.ModifiedDate = DateTime.Now;
            m.Image = String.IsNullOrEmpty(member.Image) ? String.Empty : member.Image;
            //m.StartDate
            //m.LastVisitDate

            Entity<Member>.Update(m);
            return m;
        }
    }
}