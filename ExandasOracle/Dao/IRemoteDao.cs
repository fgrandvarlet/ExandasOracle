using System;
using System.Collections.Generic;
using System.Text;
using Oracle.ManagedDataAccess.Client;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    public interface IRemoteDao
    {
        bool CheckConnection(bool DBAViews);

        OracleConnection GetOracleConnection();

        List<Table> GetTableList(OracleConnection conn, string schema, bool DBAViews);

        List<TableColumn> GetTableColumnList(OracleConnection conn, string schema, bool DBAViews);

        List<PrimaryKey> GetPrimaryKeyList(OracleConnection conn, string schema, bool DBAViews);

        List<Unique> GetUniqueList(OracleConnection conn, string schema, bool DBAViews);

        List<ForeignKey> GetForeignKeyList(OracleConnection conn, string schema, bool DBAViews);

        List<Check> GetCheckList(OracleConnection conn, string schema, bool DBAViews);

        List<Constraint> GetConstraintList(OracleConnection conn, string schema, bool DBAViews);

        List<ConstraintColumn> GetConstraintColumnList(OracleConnection conn, string schema, bool DBAViews);

        List<View> GetViewList(OracleConnection conn, string schema, bool DBAViews);

        List<Sequence> GetSequenceList(OracleConnection conn, string schema, bool DBAViews);

        List<TableIndex> GetTableIndexList(OracleConnection conn, string schema, bool DBAViews);

        List<IndexPartition> GetIndexPartitionList(OracleConnection conn, string schema, bool DBAViews);

        List<Source> GetSourceList(OracleConnection conn, string schema, bool DBAViews);
    }
}
