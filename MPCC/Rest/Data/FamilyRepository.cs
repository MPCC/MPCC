﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            var family = Entity<Family>.FindOne<Family>(familyId);
            if (family.BusinessUnitId != principal.BusinessUnitID)
            {
                return new Family();
            }
            else
            {
                return family;
            }
        }

        public static List<Member> GetFamilyMembers(Principal principal, int familyId, int index, int paging, out long count)
        {
            var bufilter = Restrictions.Where<Member>(x => x.BusinessUnitId == principal.BusinessUnitID && x.EnterpriseId == principal.EnterpriseID && x.FamilyId == familyId);
            return Entity<Member>.FindMany<Member>(bufilter, index, paging, out count);
        }

        public static Family CreateFamily(Principal principal, Family family)
        {
            var f = new Family();
            f.EnterpriseId = principal.EnterpriseID;
            f.BusinessUnitId = principal.BusinessUnitID;
            f.CreatedBy = principal.MemberID;
            f.Image = family.Image ?? String.Empty;
            f.Name = family.Name ?? String.Empty;
            Entity<Family>.Save(f);
            return f;
        }

        public static void DeleteFamily(Principal principal, int familyId)
        {
            var f = GetFamily(principal, familyId) ?? new Family();
            
            if(f.CreatedBy == principal.MemberID)
            {
                long count;
                var members = GetFamilyMembers(principal, familyId, 1, 5, out count);
                if(count > 0)
                {
                    throw new HttpException(400, "You must remove all of the members from the family before deleting.");
                }
                else
                {
                    Entity<Family>.Delete(f);    
                }
            }
            else
            {
                throw new HttpException(400, "You do not have permissions to delete this family.");
            }
        }

        public static Family UpdateFamily(Principal principal, Family family)
        {
            if (principal.MemberID != family.CreatedBy)
            {
                throw new HttpException(400, "You do not have permissions to update this family.");
            }

            var f = GetFamily(principal, family.CreatedBy);
            f.Image = family.Image ?? String.Empty;
            f.Name = family.Name ?? String.Empty;
            f.ModifiedDate = DateTime.Now;
            Entity<Family>.Update(f);
            return f;
        }
    }
}