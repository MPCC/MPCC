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

    public class FamilyService
    {
        [WebGet(UriTemplate = "v1/?index={index}&paging={paging}", ResponseFormat = WebMessageFormat.Json)]
        public GetCollectionResponse<Family> GetCollection(int index, int paging)
        {
            long count;
            var entities = FamilyRepository.GetFamilyCollection(index, paging, out count);
            return new GetCollectionResponse<Family>() { Index = index, Paging = paging, Total = count, Entities = entities };
        }

        [WebGet(UriTemplate = "v1/{id}/members?index={index}&paging={paging}", ResponseFormat = WebMessageFormat.Json)]
        public GetCollectionResponse<Member> GetFamilyMemberCollection(string id, int index, int paging)
        {
            long count;
            var entities = FamilyRepository.GetFamilyMembers(Convert.ToInt32(id), index, paging, out count);
            return new GetCollectionResponse<Member>() { Index = index, Paging = paging, Total = count, Entities = entities };
        }

        [WebInvoke(UriTemplate = "v1/{id}/member/{memberId}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Member> AddFamilyMember(string id, string memberId)
        {
            return new GetResponse<Member>() { Entity = FamilyRepository.AddFamilyMember(Convert.ToInt32(id), Convert.ToInt32(memberId)) };
        }

        [WebGet(UriTemplate = "v1/{id}", ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Family> Get(string id)
        {
            return new GetResponse<Family>() { Entity = FamilyRepository.GetFamily(Convert.ToInt32(id)) };
        }

        [WebInvoke(UriTemplate = "v1/{id}", Method = "DELETE", RequestFormat = WebMessageFormat.Json)]
        public void Delete(string id)
        {
            FamilyRepository.DeleteFamily(Convert.ToInt32(id));
        }

        [WebInvoke(UriTemplate = "v1/", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Family> Create(Family entity)
        {
            return new GetResponse<Family>() { Entity = FamilyRepository.CreateFamily(entity) };
        }

        [WebInvoke(UriTemplate = "v1/{id}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Family> Update(string id, Family entity)
        {
            entity.Id = Convert.ToInt32(id);
            return new GetResponse<Family>() { Entity = FamilyRepository.UpdateFamily(entity) };
        }
    }
}