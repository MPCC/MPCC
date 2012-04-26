using System;
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
        public GetCollectionResponse<Member> GetCollection(int index, int paging)
        {
            return
                new GetCollectionResponse<Member>()
                    {
                        Index = index,
                        Paging = paging,
                        Total = SampleData.GetMemberList().Count,
                        Entities = SampleData.GetMemberList()
                    };
        }

        [WebInvoke(UriTemplate = "", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        public GetResponse<Member> Create(Member entity)
        {
            return new GetResponse<Member>() { Entity = SampleData.SaveMember(entity) };
        }
        
        [WebGet(UriTemplate = "{id}", ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Member> Get(string id)
        {
            return new GetResponse<Member>() { Entity = SampleData.GetMemberById(id) };
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