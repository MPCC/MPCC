﻿using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Rest.Data;
using Rest.Objects;

namespace Rest.Routes
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    
    public class MemberService
    {
        [WebGet(UriTemplate = "v1/?index={index}&paging={paging}", ResponseFormat = WebMessageFormat.Json)]
        public GetCollectionResponse<Member> GetCollection(int index, int paging)
        {
            if (WebOperationContext.Current != null)
            {
                long count;
                var principal = Utility.GetContext(WebOperationContext.Current.IncomingRequest);
                var entities = MemberRepository.GetMemberCollection(principal, index, paging, out count);
                return new GetCollectionResponse<Member>()
                           {Index = index, Paging = paging, Total = count, Entities = entities};
            }
            throw new WebFaultException(HttpStatusCode.BadRequest);
        }

        [WebGet(UriTemplate = "v1/{id}", ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Member> Get(string id)
        {
            if (WebOperationContext.Current != null)
            {
                var principal = Utility.GetContext(WebOperationContext.Current.IncomingRequest);
                if(id.ToLower().Trim() == "me")
                {
                    return new GetResponse<Member>() { Entity = MemberRepository.GetMember(principal, principal.MemberID) };
                }
                else
                {
                    return new GetResponse<Member>() { Entity = MemberRepository.GetMember(principal, Convert.ToInt32(id)) };    
                }
            }
            throw new WebFaultException(HttpStatusCode.BadRequest);
        }

        //[WebInvoke(UriTemplate = "v1/{id}", Method = "DELETE", RequestFormat = WebMessageFormat.Json)]
        //public void Delete(string id)
        //{
        //    SampleData.DeleteMember(id);
        //}

        [WebInvoke(UriTemplate = "v1/{id}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Member> Update(string id, Member entity)
        {
            if (WebOperationContext.Current != null)
            {
                var principal = Utility.GetContext(WebOperationContext.Current.IncomingRequest);
                return new GetResponse<Member>() { Entity = MemberRepository.UpdateMember(principal, entity) };
            }
            throw new WebFaultException(HttpStatusCode.BadRequest);
        }

        [WebInvoke(UriTemplate = "v1/{id}/changepassword", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Member> ChangePassword(string id, Member entity)
        {
            if(WebOperationContext.Current != null)
            {
                // TODO: WRITE CHANGE PASSWORD LOGIC
                var principal = Utility.GetContext(WebOperationContext.Current.IncomingRequest);
                return new GetResponse<Member>() { Entity = MemberRepository.UpdateMember(principal, entity) };
            }
            throw new WebFaultException(HttpStatusCode.BadRequest);
        }
    }
}