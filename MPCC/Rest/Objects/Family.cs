using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentNHibernate.Mapping;

namespace Rest.Objects
{
    [DataContract]
    public class Family : BaseObject
    {
        private static string _time;

        [DataMember]
        public virtual int Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string Name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string Image { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual List<Member> Members { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual int EnterpriseId { get; set; }

        [DataMember]
        public virtual int BusinessUnitId { get; set; }

        [DataMember]
        public virtual int CreatedBy { get; set; }

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

    public class FamilyMap : ClassMap<Family>
    {
        public FamilyMap()
        {
            Schema("dbo");
            Table("Family");
            Id(x => x.Id).Column("FamilyId");
            Map(x => x.Name);
            Map(x => x.Image);
            Map(x => x.EnterpriseId);
            Map(x => x.BusinessUnitId);
            Map(x => x.CreatedBy);
            Map(x => x.IsActive);
            Map(x => x.CreatedDate).ReadOnly();
            Map(x => x.ModifiedDate);
        }
    }
}