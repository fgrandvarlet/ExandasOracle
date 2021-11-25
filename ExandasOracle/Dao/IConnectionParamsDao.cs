using System;
using System.Collections.Generic;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    public interface IConnectionParamsDao
    {
        DataTable GetDataTable(Criteria criteria);

        ConnectionParams Get(Guid uid);

        void Add(FbTransaction tran, ConnectionParams cp);

        void Add(ConnectionParams cp);

        void Save(ConnectionParams cp);

        void Delete(ConnectionParams cp);

        List<ConnectionParams> GetList();

        int GetDependencyCount(ConnectionParams cp);
    }
}
