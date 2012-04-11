using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentNHibernate.Mapping;
using Rest.Data;

namespace Rest.Objects
{

    //TODO: https://github.com/jagregory/fluent-nhibernate/wiki/Getting-started
    
    #region Entities
    
    [DataContract]
    public class Member
    {
        [DataMember]
        public virtual int Id { get; set; }

        [DataMember]
        public virtual Name Name { get; set; }

        [DataMember]
        public virtual string DateOfBirth { get; set; }

        [DataMember]
        public virtual Address Address { get; set; }

        [DataMember]
        public virtual List<Channel> Channels { get; set; }

        [DataMember]
        public virtual Membership Membership { get; set; }
    }

    [DataContract]
    public class Name
    {
        [DataMember]
        public virtual string First { get; set; }

        [DataMember]
        public virtual string Middle { get; set; }

        [DataMember]
        public virtual string Last { get; set; }
    }

    [DataContract]
    public class Address
    {
        [DataMember]
        public virtual string Street { get; set; }

        [DataMember]
        public virtual string Apt { get; set; }

        [DataMember]
        public virtual string City { get; set; }

        [DataMember]
        public virtual string State { get; set; }

        [DataMember]
        public virtual int Zip { get; set; }
    }

    [DataContract]
    public class Channel
    {
        [DataMember]
        public virtual int Id { get; set; }

        [DataMember]
        public virtual MessageChannel Type { get; set; }

        [DataMember]
        public virtual string AccountId { get; set; }

        [DataMember]
        public virtual bool IsPreferred { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual bool IsOptIn { get; set; }
    }

    [DataContract]
    public class MessageChannel
    {
        [DataMember]
        public virtual Enums.MessageChannels Id { get; set; }

        [DataMember]
        public virtual string Name { get; set; }
    }

    [DataContract]
    public class Membership
    {
        [DataMember]
        public virtual string StartDate { get; set; }

        [DataMember]
        public virtual string LastVisitDate { get; set; }

        [DataMember]
        public virtual string EndDate { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }
    }

    #endregion

    #region Mappings
    public class MemberMap : ClassMap<Member>
    {
        public MemberMap()
        {
            Id(x => x.Id);
            Map(x => x.Name.First).Column("FirstName");
            Map(x => x.Name.Middle).Column("MiddleName");
            Map(x => x.Name.Last).Column("LastName");
            Map(x => x.Address.Street).Column("StreetAddress");
            Map(x => x.Address.Apt);
            Map(x => x.Address.City);
            Map(x => x.Address.State);
            Map(x => x.Address.Zip);
            Map(x => x.DateOfBirth);
            Map(x => x.Membership.StartDate).Column("StartDate");
            Map(x => x.Membership.LastVisitDate).Column("LastVisitDate");
            Map(x => x.Membership.EndDate).Column("EndDate");
            Map(x => x.Membership.IsActive).Column("IsActive");
            HasMany(x => x.Channels)
                .Inverse()
                .Cascade.All();
        }
    }

    public class ChannelMap : ClassMap<Channel>
    {
        public ChannelMap()
        {
            Id(x => x.Id);
            Map(x => x.Type.Id).Column("ChannelTypeId");
            Map(x => x.Type.Name).Column("ChannelTypeName");
            Map(x => x.AccountId);
            Map(x => x.IsActive);
            Map(x => x.IsPreferred);
            Map(x => x.IsOptIn);
        }
    } 
    #endregion
}