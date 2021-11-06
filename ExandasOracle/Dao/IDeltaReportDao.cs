using System;
using System.Collections.Generic;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    public interface IDeltaReportDao
    {
        FbConnection GetFirebirdConnection();

        DataTable GetDataTable(Criteria criteria);

        void LoadDeltaReportList(FbConnection conn, Guid comparisonSetUid, List<DeltaReport> list);
    }
}
