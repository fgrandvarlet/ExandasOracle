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

        private void LoadTableColumns(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadTableColumnList(tran, schemaType, dao.GetTableColumnList(conn, schema, DBAViews));
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

        private void LoadViews(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadViewList(tran, schemaType, dao.GetViewList(conn, schema, DBAViews));
        }

        private void LoadSequences(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadSequenceList(tran, schemaType, dao.GetSequenceList(conn, schema, DBAViews));
        }

        private void LoadTableIndexes(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadTableIndexList(tran, schemaType, dao.GetTableIndexList(conn, schema, DBAViews));
        }

        private void LoadIndexPartitions(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadIndexPartitionList(tran, schemaType, dao.GetIndexPartitionList(conn, schema, DBAViews));
        }

        private void LoadSources(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadSourceList(tran, schemaType, dao.GetSourceList(conn, schema, DBAViews));
            this._localDao.LoadSourceSynthesis(tran, schemaType);
        }

        private void LoadClusters(FbTransaction tran, SchemaType schemaType, IRemoteDao dao, OracleConnection conn, string schema, bool DBAViews)
        {
            this._localDao.LoadClusterList(tran, schemaType, dao.GetClusterList(conn, schema, DBAViews));
        }

    }
}
