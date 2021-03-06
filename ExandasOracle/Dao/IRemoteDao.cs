using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    public interface IRemoteDao
    {
        bool CheckConnection(bool DBAViews);

        OracleConnection GetOracleConnection();

        List<Table> GetTableList(OracleConnection conn, string schema, bool DBAViews);

        List<TableComment> GetTableCommentList(OracleConnection conn, string schema, bool DBAViews);

        List<TableColumn> GetTableColumnList(OracleConnection conn, string schema, bool DBAViews);
        
        List<IdentityColumn> GetIdentityColumnList(OracleConnection conn, string schema, bool DBAViews);

        List<ColumnComment> GetColumnCommentList(OracleConnection conn, string schema, bool DBAViews);

        List<PrimaryKey> GetPrimaryKeyList(OracleConnection conn, string schema, bool DBAViews);

        List<Unique> GetUniqueList(OracleConnection conn, string schema, bool DBAViews);

        List<ForeignKey> GetForeignKeyList(OracleConnection conn, string schema, bool DBAViews);

        List<Check> GetCheckList(OracleConnection conn, string schema, bool DBAViews);

        List<Constraint> GetConstraintList(OracleConnection conn, string schema, bool DBAViews);

        List<ConstraintColumn> GetConstraintColumnList(OracleConnection conn, string schema, bool DBAViews);

        List<PartitionedTable> GetPartitionedTableList(OracleConnection conn, string schema, bool DBAViews);

        List<TablePartition> GetTablePartitionList(OracleConnection conn, string schema, bool DBAViews);

        List<TableSubpartition> GetTableSubpartitionList(OracleConnection conn, string schema, bool DBAViews);

        List<View> GetViewList(OracleConnection conn, string schema, bool DBAViews);

        List<ViewComment> GetViewCommentList(OracleConnection conn, string schema, bool DBAViews);

        List<ViewColumn> GetViewColumnList(OracleConnection conn, string schema, bool DBAViews);

        List<MaterializedView> GetMaterializedViewList(OracleConnection conn, string schema, bool DBAViews);

        List<MaterializedViewComment> GetMaterializedViewCommentList(OracleConnection conn, string schema, bool DBAViews);

        List<Sequence> GetSequenceList(OracleConnection conn, string schema, bool DBAViews);

        List<TableIndex> GetTableIndexList(OracleConnection conn, string schema, bool DBAViews);

        List<IndexColumn> GetIndexColumnList(OracleConnection conn, string schema, bool DBAViews);
        
        List<IndexExpression> GetIndexExpressionList(OracleConnection conn, string schema, bool DBAViews);

        List<PartitionedIndex> GetPartitionedIndexList(OracleConnection conn, string schema, bool DBAViews);

        List<IndexPartition> GetIndexPartitionList(OracleConnection conn, string schema, bool DBAViews);

        List<IndexSubpartition> GetIndexSubpartitionList(OracleConnection conn, string schema, bool DBAViews);

        List<Source> GetSourceList(OracleConnection conn, string schema, bool DBAViews);

        List<Trigger> GetTriggerList(OracleConnection conn, string schema, bool DBAViews);

        List<Cluster> GetClusterList(OracleConnection conn, string schema, bool DBAViews);

        List<ClusterColumn> GetClusterColumnList(OracleConnection conn, string schema, bool DBAViews);

        List<ClusterColumnMapping> GetClusterColumnMappingList(OracleConnection conn, string schema, bool DBAViews);

        List<ClusterIndex> GetClusterIndexList(OracleConnection conn, string schema, bool DBAViews);
        
        List<OracleType> GetOracleTypeList(OracleConnection conn, string schema, bool DBAViews);

        List<DatabaseLink> GetDatabaseLinkList(OracleConnection conn, string schema, bool DBAViews);

        List<ObjectPrivilege> GetObjectPrivilegeList(OracleConnection conn, string schema, bool DBAViews);
        
        List<Synonym> GetSynonymList(OracleConnection conn, string schema, bool DBAViews);

    }
}
