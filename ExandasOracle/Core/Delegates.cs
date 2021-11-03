using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;
using Oracle.ManagedDataAccess.Client;

using ExandasOracle.Dao;
using ExandasOracle.Domain;

namespace ExandasOracle.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="schemaType"></param>
    /// <param name="dao"></param>
    /// <param name="conn"></param>
    /// <param name="schema"></param>
    /// <param name="DBAViews"></param>
    public delegate void LoaderDelegate(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="list"></param>
    public delegate void DeltaDelegate(FbConnection conn, List<DeltaReport> list);
}
