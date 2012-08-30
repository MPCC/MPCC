using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentNHibernate.Mapping;
using Rest.Data;

namespace Rest.Objects
{

    //TODO: https://github.com/jagregory/fluent-nhibernate/wiki/Getting-started
    
    #region Entities
    
    [DataContract]
    public class Member : BaseObject
    {
        private static string _time;

        [DataMember]
        public virtual int MemberId { get; set; }

        [DataMember]
        public virtual int EnterpriseId { get; set; }

        [DataMember]
        public virtual int BusinessUnitId { get; set; }

        [DataMember]
        public virtual Guid ProviderUserKey { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string UserName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string FirstName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string MiddleName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string LastName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string DateOfBirth
        {
            get { return formatToISO86(Convert.ToDateTime(_time)); }
            set { _time = value; }
        }

        [DataMember(EmitDefaultValue = false)]
        public virtual string Image { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string Email { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string Street { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string Apt { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string City { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string State { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual int? Zip { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string StartDate
        {
            get { return formatToISO86(Convert.ToDateTime(_time)); }
            set { _time = value; }
        }

        [DataMember(EmitDefaultValue = false)]
        public virtual string LastVisitDate
        {
            get { return formatToISO86(Convert.ToDateTime(_time)); }
            set { _time = value; }
        }

        [DataMember(EmitDefaultValue = false)]
        public virtual string EndDate
        {
            get { return formatToISO86(Convert.ToDateTime(_time)); }
            set { _time = value; }
        }

        [DataMember(EmitDefaultValue = false)]
        public virtual bool IsActive { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual int? FamilyId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual List<Channel> Channels { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string CreatedDate
        {
            get { return formatToISO86(Convert.ToDateTime(_time)); }
            set { _time = value; }
        }

        [DataMember(EmitDefaultValue = false)]
        public virtual string ModifiedDate
        {
            get { return formatToISO86(Convert.ToDateTime(_time)); }
            set { _time = value; }
        }
    }

    [DataContract]
    public class Channel
    {
        [DataMember]
        public virtual int Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual MessageChannel Type { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string AccountId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual bool IsPreferred { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual bool IsActive { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual bool IsOptIn { get; set; }
    }

    [DataContract]
    public class MessageChannel
    {
        [DataMember]
        public virtual Enums.MessageChannels Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string Name { get; set; }
    }

    #endregion

    #region Mappings
    public class MemberMap : ClassMap<Member>
    {
        public MemberMap()
        {
            Schema("dbo");
            Table("Member");
            Id(x => x.MemberId).Column("MemberId");
            Map(x => x.EnterpriseId);
            Map(x => x.BusinessUnitId);
            Map(x => x.ProviderUserKey);
            Map(x => x.UserName).ReadOnly();
            Map(x => x.FirstName);
            Map(x => x.MiddleName);
            Map(x => x.LastName);
            Map(x => x.Street);
            Map(x => x.Image);
            Map(x => x.Email);
            Map(x => x.Apt);
            Map(x => x.City);
            Map(x => x.State);
            Map(x => x.Zip);
            Map(x => x.DateOfBirth);
            Map(x => x.StartDate);
            Map(x => x.LastVisitDate);
            Map(x => x.EndDate);
            Map(x => x.IsActive);
            Map(x => x.FamilyId);
            Map(x => x.CreatedDate).ReadOnly();
            Map(x => x.ModifiedDate);
            //HasMany(x => x.Channels)
            //    .Inverse()
            //    .Cascade.All();
        }
    }

    //public class ChannelMap : ClassMap<Channel>
    //{
    //    public ChannelMap()
    //    {
    //        Id(x => x.Id);
    //        Map(x => x.Type.Id).Column("ChannelTypeId");
    //        Map(x => x.Type.Name).Column("ChannelTypeName");
    //        Map(x => x.AccountId);
    //        Map(x => x.IsActive);
    //        Map(x => x.IsPreferred);
    //        Map(x => x.IsOptIn);
    //    }
    //} 
    #endregion
}