using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Web;
using Auth;
using NHibernate.Criterion;
using Rest.Objects;

namespace Rest.Data
{
    public class FamilyRepository
    {
        public static List<Family> GetFamilyCollection(Principal principal, int index, int paging, out long count)
        {
            var bufilter = Restrictions.Where<Family>(x => x.BusinessUnitId == principal.BusinessUnitID && x.EnterpriseId == principal.EnterpriseID);
            return Entity<Family>.FindMany<Family>(bufilter, index, paging, out count);
        }

        public static Family GetFamily(Principal principal, int familyId)
        {
            var f = Entity<Family>.FindOne<Family>(familyId);
            if(f == null) { throw new WebFaultException(HttpStatusCode.NoContent); }
            CheckPermissions(principal.MemberID, f.CreatedBy);
            return f;
        }

        public static List<Member> GetFamilyMembers(Principal principal, int familyId, int index, int paging, out long count)
        {
            var bufilter = Restrictions.Where<Member>(x => x.BusinessUnitId == principal.BusinessUnitID && x.EnterpriseId == principal.EnterpriseID && x.FamilyId == familyId);
            return Entity<Member>.FindMany<Member>(bufilter, index, paging, out count);
        }

        public static Family CreateFamily(Principal principal, Family family)
        {
            var f = new Family()
                {
                    EnterpriseId = principal.EnterpriseID,
                    BusinessUnitId = principal.BusinessUnitID,
                    CreatedBy = principal.MemberID,
                    Image = family.Image ?? String.Empty,
                    Name = family.Name ?? String.Empty,
                    IsActive = true,
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                };
            Entity<Family>.Save(f);
            AddFamilyMember(principal, f.Id, principal.MemberID);
            return f;
        }

        public static Member AddFamilyMember(Principal principal, int familyId, int memberId)
        {
            //TODO: ONLY update if member has a pending request, then grant addition else Unauthorized

            var m = Entity<Member>.FindOne<Member>(memberId);
            var f = GetFamily(principal, familyId); // Make sure the family still exists.
            m.FamilyId = f.Id;
            Entity<Member>.Update(m);
            return m;
        }

        public static void DeleteFamily(Principal principal, int familyId)
        {
            var f = GetFamily(principal, familyId);
            CheckPermissions(principal.MemberID, f.CreatedBy);

            long count;
            var members = GetFamilyMembers(principal, familyId, 1, 5, out count);
            if(count > 1) { throw new WebFaultException(HttpStatusCode.Conflict); }

            Entity<Family>.Delete(f);    
        }

        public static Family UpdateFamily(Principal principal, Family family)
        {
            var f = GetFamily(principal, family.Id);
            CheckPermissions(principal.MemberID, f.CreatedBy);
            
            f.Image = family.Image ?? String.Empty;
            f.Name = family.Name ?? String.Empty;
            f.ModifiedDate = DateTime.Now;
            Entity<Family>.Update(f);
            return f;
        }

        private static void CheckPermissions(int x, int y)
        {
            if(x != y) { throw new WebFaultException(HttpStatusCode.Unauthorized); }
        }
    }
}