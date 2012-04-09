using System.Collections.Generic;

namespace Rest.Objects
{
    public class Route { }

    public class GetCollectionResponse<TData>
    {
        public virtual int Total { get; set; }
        public virtual List<TData> Entities { get; set; }
    }

    public class GetResponse<TData>
    {
        public virtual TData Entity { get; set; }
    }
}