using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Web;
using Auth;
using NHibernate.Criterion;
using Rest.Objects;

namespace Rest.Data
{
    public class NotificationRepository
    {
        public static List<Notification> GetNotificationCollection(Principal principal, int index, int paging, out long count)
        {
            var bufilter = Restrictions.Where<Notification>(x => x.BusinessUnitID == principal.BusinessUnitID && x.EnterpriseID == principal.EnterpriseID && x.ToMemberID == principal.MemberID && x.IsActive);
            return Entity<Notification>.FindMany<Notification>(bufilter, index, paging, out count);
        }

        public static List<Notification> GetSentNotificationCollection(Principal principal, int index, int paging, out long count)
        {
            var bufilter = Restrictions.Where<Notification>(x => x.BusinessUnitID == principal.BusinessUnitID && x.EnterpriseID == principal.EnterpriseID && x.FromMemberID == principal.MemberID && x.IsActive);
            return Entity<Notification>.FindMany<Notification>(bufilter, index, paging, out count);
        }

        public static Notification CreateNotification(Principal principal, Notification notification)
        {
            var n = new Notification()
            {
                EnterpriseID = notification.EnterpriseID,
                BusinessUnitID = notification.BusinessUnitID,
                ToMemberID = notification.ToMemberID,
                FromMemberID = principal.MemberID,
                FromUsername = notification.FromUsername,
                Subject = notification.Subject ?? String.Empty,
                Message = notification.Message ?? String.Empty,
                Category = notification.Category ?? "Message",
                IsActive = true,
                HasRead = false,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
            };
            Entity<Notification>.Save(n);
            return n;
        }

        public static Notification GetNotification(Principal principal, int id)
        {
            return Entity<Notification>.FindOne<Notification>(id);
        }

        public static Notification UpdateNotification(Principal principal, Notification notification)
        {
            var n = GetNotification(principal, notification.ID);
            CheckPermissions(principal.MemberID, n.ToMemberID);
            n.ModifiedDate = DateTime.Now;
            n.IsActive = notification.IsActive;
            n.HasRead = notification.HasRead;
            Entity<Notification>.Update(n);
            return n;
        }

        public static void CancelNotification(Principal principal, int notificationID)
        {
            var n = GetNotification(principal, notificationID);
            CheckPermissions(principal.MemberID, n.FromMemberID);
            n.ModifiedDate = DateTime.Now;
            n.IsActive = false;
            Entity<Notification>.Update(n);
        }

        private static void CheckPermissions(int x, int y)
        {
            if (x != y) { throw new WebFaultException(HttpStatusCode.Unauthorized); }
        }
    }
}