using System;
using System.Collections.Generic;
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
        [WebGet(UriTemplate = "/?index={index}&paging={paging}", ResponseFormat = WebMessageFormat.Json)]
        public string GetCollection(int index, int paging)
        {
            List<Member> entities = MemberRepository.GetMemberList(index, paging);
            return new GetCollectionResponse<Member>() { Index = index, Paging = paging, Total = entities.Count, Entities = entities }.ToJSON();
        }

        [WebInvoke(UriTemplate = "", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        public string Create(Member entity)
        {
            Member m = entity;
            m.LastVisitDate = Utility.ToDateTime(entity.LastVisitDate.ToString());
            m.ModifiedDate = DateTime.Now;

            Entity<Member>.Save(m);
            return new GetResponse<Member>() { Entity = SampleData.SaveMember(m) }.ToJSON();
        }
        
        [WebGet(UriTemplate = "{id}", ResponseFormat = WebMessageFormat.Json)]
        public string Get(string id)
        {
            return new GetResponse<Member>() { Entity = SampleData.GetMemberById(id) }.ToJSON();
        }
        
        [WebInvoke(UriTemplate = "{id}", Method = "DELETE", RequestFormat = WebMessageFormat.Json)]
        public void Delete(string id)
        {
            SampleData.DeleteMember(id);
        }

        [WebInvoke(UriTemplate = "{id}", Method = "PUT", RequestFormat = WebMessageFormat.Json)]
        public string Update(string id, Member entity)
        {
            throw new NotImplementedException();
        }
    }
}