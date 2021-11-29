using System.Collections.Generic;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    public interface IReferenceDao
    {
        bool NeedInitialization();

        void InitializeReferences();

        List<EntityReference> GetEntityReferenceList();

        List<PropertyReference> GetPropertyReferenceListByEntity(EntityReference er);

    }
}
