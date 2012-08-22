using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using FluentNHibernate.Mapping;
using Rest.Data;

namespace Rest.Objects
{
    [DataContract]
    public class Notification : BaseObject
    {
        private static string _time;

        [DataMember]
        public virtual int ID { get; set; }

        [DataMember]
        public virtual int EnterpriseID { get; set; }

        [DataMember]
        public virtual int BusinessUnitID { get; set; }

        [DataMember]
        public virtual int ToMemberID { get; set; }

        [DataMember]
        public virtual int FromMemberID { get; set; }

        [DataMember]
        public virtual string FromUsername { get; set; }

        [DataMember]
        public virtual string Category { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string Subject { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string Message { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string ActionText { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string ActionApiMethod { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string ActionApi { get; set; }

        [DataMember]
        public virtual bool ActionOccurred { get; set; }

        [DataMember]
        public virtual bool HasRead { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual string CreatedDate
        {
            get { return formatToISO86(Convert.ToDateTime(_time)); }
            set { _time = value; }
        }

        [DataMember]
        public virtual string ModifiedDate
        {
            get { return formatToISO86(Convert.ToDateTime(_time)); }
            set { _time = value; }
        }
    }

    public class NotificationMap : ClassMap<Notification>
    {
        public NotificationMap()
        {
            Schema("dbo");
            Table("Notification");
            Id(x => x.ID).Column("NotificationId");
            Map(x => x.EnterpriseID);
            Map(x => x.BusinessUnitID);
            Map(x => x.ToMemberID);
            Map(x => x.FromMemberID);
            Map(x => x.FromUsername);
            Map(x => x.Category);
            Map(x => x.Subject);
            Map(x => x.Message);
            Map(x => x.ActionText);
            Map(x => x.ActionApiMethod);
            Map(x => x.ActionApi);
            Map(x => x.ActionOccurred);
            Map(x => x.HasRead);
            Map(x => x.IsActive);
            Map(x => x.CreatedDate).ReadOnly();
            Map(x => x.ModifiedDate);
        }
    }
    
}