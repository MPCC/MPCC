﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using Rest.Data;
using Rest.Objects;

namespace Rest.Routes
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]

    public class NotificationService
    {
        [WebGet(UriTemplate = "v1/?index={index}&paging={paging}", ResponseFormat = WebMessageFormat.Json)]
        public GetCollectionResponse<Notification> GetCollection(int index, int paging)
        {
            long count;
            var entities = NotificationRepository.GetNotificationCollection(index, paging, out count);
            return new GetCollectionResponse<Notification>() { Index = index, Paging = paging, Total = count, Entities = entities };
        }

        [WebGet(UriTemplate = "v1/sent/?index={index}&paging={paging}", ResponseFormat = WebMessageFormat.Json)]
        public GetCollectionResponse<Notification> GetSentCollection(int index, int paging)
        {
            long count;
            var entities = NotificationRepository.GetSentNotificationCollection(index, paging, out count);
            return new GetCollectionResponse<Notification>() { Index = index, Paging = paging, Total = count, Entities = entities };
        }

        [WebInvoke(UriTemplate = "v1/", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Notification> Create(Notification entity)
        {
            return new GetResponse<Notification>() { Entity = NotificationRepository.CreateNotification(entity) };
        }

        [WebInvoke(UriTemplate = "v1/{id}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Notification> Update(string id, Notification entity)
        {
            entity.ID = Convert.ToInt32(id);
            return new GetResponse<Notification>() { Entity = NotificationRepository.UpdateNotification(entity) };
        }

        [WebInvoke(UriTemplate = "v1/{id}/cancel", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void Cancel(string id)
        {
            NotificationRepository.CancelNotification(Convert.ToInt32(id));
        }
    }
}