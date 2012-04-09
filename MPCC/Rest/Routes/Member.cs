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
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Json)]
        public string GetCollection()
        {
            return new GetCollectionResponse<Member>() { Total = SampleData.GetMemberList().Count, Entities = SampleData.GetMemberList() }.ToJSON();
        }

        [WebInvoke(UriTemplate = "", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        public string Create(Member entity)
        {
            return new GetResponse<Member>() { Entity = SampleData.SaveMember(entity) }.ToJSON();
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