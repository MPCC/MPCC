using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Web;
using NHibernate.Criterion;
using Rest.Auth;
using Rest.Objects;

namespace Rest.Data
{
    public class NotificationRepository : ServiceAuthorization
    {
        public static List<Notification> GetNotificationCollection(int index, int paging, out long count)
        {
            var bufilter = Restrictions.Where<Notification>(x => x.BusinessUnitID == CurrentUser.Principal.BusinessUnitID && x.EnterpriseID == CurrentUser.Principal.EnterpriseID && x.ToMemberID == CurrentUser.Principal.MemberID && x.IsActive);
            return Entity<Notification>.FindMany<Notification>(bufilter, index, paging, out count);
        }

        public static List<Notification> GetSentNotificationCollection(int index, int paging, out long count)
        {
            var bufilter = Restrictions.Where<Notification>(x => x.BusinessUnitID == CurrentUser.Principal.BusinessUnitID && x.EnterpriseID == CurrentUser.Principal.EnterpriseID && x.FromMemberID == CurrentUser.Principal.MemberID && x.IsActive);
            return Entity<Notification>.FindMany<Notification>(bufilter, index, paging, out count);
        }

        public static Notification CreateNotification(Notification notification)
        {
            var n = new Notification()
            {
                EnterpriseID = notification.EnterpriseID,
                BusinessUnitID = notification.BusinessUnitID,
                ToMemberID = notification.ToMemberID,
                FromMemberID = CurrentUser.Principal.MemberID,
                FromUsername = notification.FromUsername,
                Subject = notification.Subject ?? String.Empty,
                Message = notification.Message ?? String.Empty,
                ActionApiMethod = notification.ActionApiMethod,
                ActionApi = notification.ActionApi,
                ActionText = notification.ActionText,
                ActionOccurred = false,
                Category = GetNotificationCategory(notification.Category).ToString(),
                IsActive = true,
                HasRead = false,
                ModifiedDate = DateTime.Now.ToString(),
                CreatedDate = DateTime.Now.ToString(),
            };
            Entity<Notification>.Save(n);
            return n;
        }

        public static Notification GetNotification(int id)
        {
            return Entity<Notification>.FindOne<Notification>(id);
        }

        public static Notification UpdateNotification(Notification notification)
        {
            var n = GetNotification(notification.ID);
            CheckPermissions(CurrentUser.Principal.MemberID, n.ToMemberID);
            n.ModifiedDate = DateTime.Now.ToString();
            n.IsActive = notification.IsActive;
            n.HasRead = notification.HasRead;
            n.ActionOccurred = notification.ActionOccurred;
            Entity<Notification>.Update(n);
            return n;
        }

        public static void CancelNotification(int notificationID)
        {
            var n = GetNotification(notificationID);
            CheckPermissions(CurrentUser.Principal.MemberID, n.FromMemberID);
            n.ModifiedDate = DateTime.Now.ToString();
            n.IsActive = false;
            Entity<Notification>.Update(n);
        }

        private static void CheckPermissions(int x, int y)
        {
            if (x != y) { throw new WebFaultException(HttpStatusCode.Unauthorized); }
        }

        public static Enums.NotificationCategory GetNotificationCategory(string notification)
        {
            Enums.NotificationCategory category;
            if(!Enum.TryParse(notification.Trim().ToUpper(), out category))
            {
                category = Enums.NotificationCategory.MESSAGE;
            }
            return category;
        }

        /// <summary>
        /// Used to build a standard Notification Request by a system user
        /// </summary>
        /// <param name="principal">Authenicated User building the request notification</param>
        /// <param name="toMemberId">Whom the notification should be sent too</param>
        /// <param name="subject">Subject of the message</param>
        /// <param name="message"></param>
        /// <param name="actionApi"></param>
        /// <param name="actionApiText"></param>
        /// <returns></returns>
        
        public static Notification NotificationRequest(int toMemberId, string subject, string message)
        {
            return NotificationRequest(toMemberId, subject, message, String.Empty, String.Empty, String.Empty);
        }

        public static Notification NotificationRequest(int toMemberId, string subject, string message, string actionApi, string actionApiText, string actionApiMethod)
        {
            return new Notification()
                       {
                           EnterpriseID = CurrentUser.Principal.EnterpriseID,
                           BusinessUnitID = CurrentUser.Principal.BusinessUnitID,
                           ToMemberID = toMemberId,
                           FromMemberID = CurrentUser.Principal.MemberID,
                           FromUsername = CurrentUser.Principal.Username,
                           Subject = subject,
                           Message = message,
                           ActionApiMethod = actionApiMethod,
                           ActionApi = actionApi,
                           ActionText = actionApiText,
                           ActionOccurred = false,
                           Category = Enums.NotificationCategory.REQUEST.ToString(),
                           IsActive = true,
                           HasRead = false,
                           ModifiedDate = DateTime.Now.ToString(),
                           CreatedDate = DateTime.Now.ToString(),   
                       };
        }

        public static Notification NotificationMessage(int toMemberId, string subject, string message)
        {
            return NotificationMessage(toMemberId, subject, message, String.Empty, String.Empty, String.Empty);
        }

        public static Notification NotificationMessage(int toMemberId, string subject, string message, string actionApi, string actionApiText, string actionApiMethod)
        {
            return new Notification()
            {
                EnterpriseID = CurrentUser.Principal.EnterpriseID,
                BusinessUnitID = CurrentUser.Principal.BusinessUnitID,
                ToMemberID = toMemberId,
                FromMemberID = CurrentUser.Principal.MemberID,
                FromUsername = CurrentUser.Principal.Username,
                Subject = subject,
                Message = message,
                ActionApiMethod = actionApiMethod,
                ActionApi = actionApi,
                ActionText = actionApiText,
                ActionOccurred = false,
                Category = Enums.NotificationCategory.MESSAGE.ToString(),
                IsActive = true,
                HasRead = false,
                ModifiedDate = DateTime.Now.ToString(),
                CreatedDate = DateTime.Now.ToString(),
            };
        }
    }
}