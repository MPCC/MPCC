using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Web;
using System.Web;
using NHibernate.Criterion;
using Rest.Objects;
using System.Web.Security;
using Rest.Auth;

namespace Rest.Data
{
    public class MemberRepository : ServiceAuthorization
    {
        public static List<Member> GetMemberCollection(int index, int paging, out long count)
        {
            var bufilter = Restrictions.Where<Member>(x => x.BusinessUnitId == CurrentUser.Principal.BusinessUnitID && x.EnterpriseId == CurrentUser.Principal.EnterpriseID);
            return Entity<Member>.FindMany<Member>(bufilter, index, paging, out count);
        }

        //public static List<Member> GetMemberCollection(int index, int paging, out long count)
        //{
        //    var bufilter = Restrictions.Where<Member>(x => x.BusinessUnitId == principal.BusinessUnitID && x.EnterpriseId == principal.EnterpriseID);
        //    return Entity<Member>.FindMany<Member>(bufilter, index, paging, out count);
        //}

        public static Member GetMember(int id)
        {
            var member = Entity<Member>.FindOne<Member>(id);
            if (member.MemberId != CurrentUser.Principal.MemberID)
            {
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }
            return member;
        }

        public static Member GetMember()
        {
            return GetMember(CurrentUser.Principal.MemberID);
        }

        public static Member UpdateMember(Member member)
        {
            if (CurrentUser.Principal.MemberID != member.MemberId)
            {
                throw new HttpException(400, "You do not have permissions to update this member.");
            }

            var m = GetMember(CurrentUser.Principal.MemberID);
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
            m.ModifiedDate = DateTime.Now.ToString();
            m.Image = String.IsNullOrEmpty(member.Image) ? String.Empty : member.Image;
            //m.StartDate
            //m.LastVisitDate

            Entity<Member>.Update(m);
            return m;
        }
    }
}