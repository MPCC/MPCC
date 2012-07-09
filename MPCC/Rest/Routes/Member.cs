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
            long count;
            var principal = Utility.GetContext(WebOperationContext.Current.IncomingRequest);
            var entities = MemberRepository.GetMemberCollection(principal, index, paging, out count);
            return new GetCollectionResponse<Member>() { Index = index, Paging = paging, Total = count, Entities = entities };
        }

        [WebGet(UriTemplate = "{id}", ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Member> Get(string id)
        {
            var principal = Utility.GetContext(WebOperationContext.Current.IncomingRequest);
            return new GetResponse<Member>() { Entity = MemberRepository.GetMember(principal, Convert.ToInt32(id)) };
        }

        //[WebInvoke(UriTemplate = "", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        //public GetResponse<Member> Create(Member entity)
        //{
        //    Member m = entity;
        //    m.LastVisitDate = Utility.ToDateTime(entity.LastVisitDate.ToString());
        //    m.ModifiedDate = DateTime.Now;

        //    Entity<Member>.Save(m);
        //    return new GetResponse<Member>() { Entity = SampleData.SaveMember(entity) };
        //}
        
        //[WebInvoke(UriTemplate = "{id}", Method = "DELETE", RequestFormat = WebMessageFormat.Json)]
        //public void Delete(string id)
        //{
        //    SampleData.DeleteMember(id);
        //}

        //[WebInvoke(UriTemplate = "{id}", Method = "PUT", RequestFormat = WebMessageFormat.Json)]
        //public string Update(string id, Member entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}