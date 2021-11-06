using System;
using System.Collections.Generic;
using System.Data;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    public interface IConnectionParamsDao
    {
        DataTable GetDataTable(Criteria criteria);

        ConnectionParams Get(Guid uid);

        void Add(ConnectionParams cp);

        void Save(ConnectionParams cp);

        void Delete(ConnectionParams cp);

        List<ConnectionParams> GetList();
    }
}
