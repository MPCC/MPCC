using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentNHibernate.Mapping;

namespace Rest.Objects
{
    [DataContract]
    public class Family
    {
        [DataMember]
        public virtual int Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string Name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public virtual string Image { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<Member> Members { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual int EnterpriseId { get; set; }

        [DataMember]
        public virtual int BusinessUnitId { get; set; }

        [DataMember]
        public virtual int CreatedBy { get; set; }

        [DataMember]
        public virtual DateTime CreatedDate { get; set; }

        [DataMember]
        public virtual DateTime ModifiedDate { get; set; }
    }

    public class FamilyMap : ClassMap<Family>
    {
        public FamilyMap()
        {
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