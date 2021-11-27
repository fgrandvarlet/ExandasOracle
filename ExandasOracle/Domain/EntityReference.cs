using System.Collections.Generic;

namespace ExandasOracle.Domain
{
    public class EntityReference
    {
        public string Entity { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EntityReference reference &&
                   Entity == reference.Entity;
        }

        public override int GetHashCode()
        {
            return 1875520522 + EqualityComparer<string>.Default.GetHashCode(Entity);
        }

    }
}
