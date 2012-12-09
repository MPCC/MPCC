using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Web;
using NHibernate.Criterion;
using Rest.Auth;
using Rest.Objects;

namespace Rest.Data
{
    public class FamilyRepository : ServiceAuthorization
    {
        public static List<Family> GetFamilyCollection(int index, int paging, out long count)
        {
            var bufilter = Restrictions.Where<Family>(x => x.BusinessUnitId == CurrentUser.Principal.BusinessUnitID && x.EnterpriseId == CurrentUser.Principal.EnterpriseID);
            return Entity<Family>.FindMany<Family>(bufilter, index, paging, out count);
        }

        public static Family GetFamily(int familyId)
        {
            var f = Entity<Family>.FindOne<Family>(familyId);
            if(f == null) { throw new WebFaultException(HttpStatusCode.NoContent); }
            CheckPermissions(CurrentUser.Principal.MemberID, f.CreatedBy);
            return f;
        }

        public static List<Member> GetFamilyMembers(int familyId, int index, int paging, out long count)
        {
            var bufilter = Restrictions.Where<Member>(x => x.BusinessUnitId == CurrentUser.Principal.BusinessUnitID && x.EnterpriseId == CurrentUser.Principal.EnterpriseID && x.FamilyId == familyId);
            return Entity<Member>.FindMany<Member>(bufilter, index, paging, out count);
        }

        public static Family CreateFamily(Family family)
        {
            var f = new Family()
                {
                    EnterpriseId = CurrentUser.Principal.EnterpriseID,
                    BusinessUnitId = CurrentUser.Principal.BusinessUnitID,
                    CreatedBy = CurrentUser.Principal.MemberID,
                    Image = family.Image ?? String.Empty,
                    Name = family.Name ?? String.Empty,
                    IsActive = true,
                    ModifiedDate = DateTime.Now.ToString(),
                    CreatedDate = DateTime.Now.ToString(),
                };
            Entity<Family>.Save(f);
            AddFamilyMember(f.Id, CurrentUser.Principal.MemberID);
            return f;
        }

        public static Member AddFamilyMember(int familyId, int memberId)
        {
            //TODO: ONLY update if member has a pending request, then grant addition else Unauthorized

            var m = Entity<Member>.FindOne<Member>(memberId);
            var f = GetFamily(familyId); // Make sure the family still exists.
            m.FamilyId = f.Id;
            Entity<Member>.Update(m);

            var n = FamilyRequest(CurrentUser.Principal, memberId, f.Name, f.Id);
            Entity<Notification>.Save(n);

            return m;
        }

        public static void DeleteFamily(int familyId)
        {
            var f = GetFamily(familyId);
            CheckPermissions(CurrentUser.Principal.MemberID, f.CreatedBy);

            long count;
            var members = GetFamilyMembers(familyId, 1, 5, out count);
            if(count > 1) { throw new WebFaultException(HttpStatusCode.Conflict); }

            Entity<Family>.Delete(f);    
        }

        public static Family UpdateFamily(Family family)
        {
            var f = GetFamily(family.Id);
            CheckPermissions(CurrentUser.Principal.MemberID, f.CreatedBy);
            
            f.Image = family.Image ?? String.Empty;
            f.Name = family.Name ?? String.Empty;
            f.ModifiedDate = DateTime.Now.ToString();
            Entity<Family>.Update(f);
            return f;
        }

        /// <summary>
        /// When a system user wants selects a family to join a FamilyRequest notification
        /// should be sent to the createdby user of the family with the from information
        /// being that of the requesting system user
        /// </summary>
        /// <returns></returns>
        public static Notification FamilyRequest(Principal prinicipal, int toMemberId, string familyName, int familyId)
        {
            var message = String.Format(MessageLines.FamilyRequest, prinicipal.Username, familyName);
            var apitext = String.Format("Add {0} to the {1} family", prinicipal.Username, familyName);
            var api = String.Format("{0}/family/{1}/member/{2}", "http://localhost/rest2", familyId, prinicipal.MemberID);
            return NotificationRepository.NotificationRequest(toMemberId, SubjectLines.FamilyRequest,message, api, apitext, "POST");
        }

        /// <summary>
        /// When a system user confirms a family request a family request confirmed notification
        /// should be sent to the family request from system user
        /// </summary>
        /// <returns></returns>
        public static Notification FamilyRequestConfirmed(Principal prinicipal, int toMemberId, string familyName)
        {
            var subject = String.Format(SubjectLines.FamilyRequestConfirmed, familyName);
            var message = String.Format(MessageLines.FamilyRequestConfirmed, familyName);
            return NotificationRepository.NotificationMessage(toMemberId, subject, message);
        }

        private static void CheckPermissions(int x, int y)
        {
            if(x != y) { throw new WebFaultException(HttpStatusCode.Unauthorized); }
        }
    }
}