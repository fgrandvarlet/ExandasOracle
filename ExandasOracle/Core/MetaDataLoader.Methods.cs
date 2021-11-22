using FirebirdSql.Data.FirebirdClient;
using Oracle.ManagedDataAccess.Client;

using ExandasOracle.Dao;

namespace ExandasOracle.Core
{
    public partial class MetaDataLoader
    {
        private void LoadTables(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadTableList(tran, schemaType, dao.GetTableList(conn, schema, DBAViews));
        }

        private void LoadTableComments(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadTableCommentList(tran, schemaType, dao.GetTableCommentList(conn, schema, DBAViews));
        }

        private void LoadTableColumns(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadTableColumnList(tran, schemaType, dao.GetTableColumnList(conn, schema, DBAViews));
        }

        private void LoadIdentityColumns(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadIdentityColumnList(tran, schemaType, dao.GetIdentityColumnList(conn, schema, DBAViews));
        }

        private void LoadColumnComments(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadColumnCommentList(tran, schemaType, dao.GetColumnCommentList(conn, schema, DBAViews));
        }

        private void LoadPrimaryKeys(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadPrimaryKeyList(tran, schemaType, dao.GetPrimaryKeyList(conn, schema, DBAViews));
        }

        private void LoadUniques(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadUniqueList(tran, schemaType, dao.GetUniqueList(conn, schema, DBAViews));
        }

        private void LoadForeignKeys(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadForeignKeyList(tran, schemaType, dao.GetForeignKeyList(conn, schema, DBAViews));
        }

        private void LoadChecks(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadCheckList(tran, schemaType, dao.GetCheckList(conn, schema, DBAViews));
        }

        private void LoadConstraints(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadConstraintList(tran, schemaType, dao.GetConstraintList(conn, schema, DBAViews));
        }

        private void LoadConstraintColumns(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadConstraintColumnList(tran, schemaType, dao.GetConstraintColumnList(conn, schema, DBAViews));
        }

        private void LoadPartitionedTables(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadPartitionedTableList(tran, schemaType, dao.GetPartitionedTableList(conn, schema, DBAViews));
        }

        private void LoadTablePartitions(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadTablePartitionList(tran, schemaType, dao.GetTablePartitionList(conn, schema, DBAViews));
        }

        private void LoadTableSubpartitions(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadTableSubpartitionList(tran, schemaType, dao.GetTableSubpartitionList(conn, schema, DBAViews));
        }

        private void LoadViews(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadViewList(tran, schemaType, dao.GetViewList(conn, schema, DBAViews));
        }

        private void LoadViewComments(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadViewCommentList(tran, schemaType, dao.GetViewCommentList(conn, schema, DBAViews));
        }

        private void LoadViewColumns(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadViewColumnList(tran, schemaType, dao.GetViewColumnList(conn, schema, DBAViews));
        }

        private void LoadMaterializedViews(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadMaterializedViewList(tran, schemaType, dao.GetMaterializedViewList(conn, schema, DBAViews));
        }

        private void LoadMaterializedViewComments(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadMaterializedViewCommentList(tran, schemaType, dao.GetMaterializedViewCommentList(conn, schema, DBAViews));
        }

        private void LoadSequences(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadSequenceList(tran, schemaType, dao.GetSequenceList(conn, schema, DBAViews));
        }

        private void LoadTableIndexes(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadTableIndexList(tran, schemaType, dao.GetTableIndexList(conn, schema, DBAViews));
        }

        private void LoadIndexColumns(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadIndexColumnList(tran, schemaType, dao.GetIndexColumnList(conn, schema, DBAViews));
        }

        private void LoadIndexExpressions(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadIndexExpressionList(tran, schemaType, dao.GetIndexExpressionList(conn, schema, DBAViews));
        }

        private void LoadPartitionedIndexes(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadPartitionedIndexList(tran, schemaType, dao.GetPartitionedIndexList(conn, schema, DBAViews));
        }

        private void LoadIndexPartitions(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadIndexPartitionList(tran, schemaType, dao.GetIndexPartitionList(conn, schema, DBAViews));
        }

        private void LoadIndexSubpartitions(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadIndexSubpartitionList(tran, schemaType, dao.GetIndexSubpartitionList(conn, schema, DBAViews));
        }

        private void LoadSources(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadSourceList(tran, schemaType, dao.GetSourceList(conn, schema, DBAViews));
            this._localDao.LoadSourceSynthesis(tran, schemaType);
        }

        private void LoadTriggers(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadTriggerList(tran, schemaType, dao.GetTriggerList(conn, schema, DBAViews));
        }

        private void LoadClusters(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadClusterList(tran, schemaType, dao.GetClusterList(conn, schema, DBAViews));
        }

        private void LoadClusterColumns(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadClusterColumnList(tran, schemaType, dao.GetClusterColumnList(conn, schema, DBAViews));
        }

        private void LoadClusterColumnMappings(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadClusterColumnMappingList(tran, schemaType, dao.GetClusterColumnMappingList(conn, schema, DBAViews));
        }

        private void LoadClusterIndexes(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadClusterIndexList(tran, schemaType, dao.GetClusterIndexList(conn, schema, DBAViews));
        }

        private void LoadOracleTypes(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadOracleTypeList(tran, schemaType, dao.GetOracleTypeList(conn, schema, DBAViews));
        }

        private void LoadDatabaseLinks(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadDatabaseLinkList(tran, schemaType, dao.GetDatabaseLinkList(conn, schema, DBAViews));
        }

        private void LoadObjectPrivileges(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadObjectPrivilegeList(tran, schemaType, dao.GetObjectPrivilegeList(conn, schema, DBAViews));
        }

        private void LoadSynonyms(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadSynonymList(tran, schemaType, dao.GetSynonymList(conn, schema, DBAViews));
        }

    }
}
