using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeltaReportDao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        FbConnection GetFirebirdConnection();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        DataTable GetDataTable(Criteria criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="comparisonSetUid"></param>
        /// <param name="list"></param>
        void LoadDeltaReportList(FbConnection conn, Guid comparisonSetUid, List<DeltaReport> list);
    }
}
