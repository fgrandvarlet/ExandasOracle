using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    public interface ILocalDao
    {
        FbConnection GetFirebirdConnection();

        void LoadTableList(FbTransaction tran, SchemaType schemaType, List<Table> list);

        void LoadTableColumnList(FbTransaction tran, SchemaType schemaType, List<TableColumn> list);
        
        void LoadColumnCommentList(FbTransaction tran, SchemaType schemaType, List<ColumnComment> list);

        void LoadPrimaryKeyList(FbTransaction tran, SchemaType schemaType, List<PrimaryKey> list);

        void LoadUniqueList(FbTransaction tran, SchemaType schemaType, List<Unique> list);

        void LoadForeignKeyList(FbTransaction tran, SchemaType schemaType, List<ForeignKey> list);

        void LoadCheckList(FbTransaction tran, SchemaType schemaType, List<Check> list);

        void LoadConstraintList(FbTransaction tran, SchemaType schemaType, List<Constraint> list);

        void LoadConstraintColumnList(FbTransaction tran, SchemaType schemaType, List<ConstraintColumn> list);

        void LoadPartitionedTableList(FbTransaction tran, SchemaType schemaType, List<PartitionedTable> list);

        void LoadTablePartitionList(FbTransaction tran, SchemaType schemaType, List<TablePartition> list);

        void LoadTableSubpartitionList(FbTransaction tran, SchemaType schemaType, List<TableSubpartition> list);

        void LoadViewList(FbTransaction tran, SchemaType schemaType, List<View> list);

        void LoadViewColumnList(FbTransaction tran, SchemaType schemaType, List<ViewColumn> list);

        void LoadMaterializedViewList(FbTransaction tran, SchemaType schemaType, List<MaterializedView> list);

        void LoadSequenceList(FbTransaction tran, SchemaType schemaType, List<Sequence> list);

        void LoadTableIndexList(FbTransaction tran, SchemaType schemaType, List<TableIndex> list);

        void LoadIndexColumnList(FbTransaction tran, SchemaType schemaType, List<IndexColumn> list);

        void LoadPartitionedIndexList(FbTransaction tran, SchemaType schemaType, List<PartitionedIndex> list);

        void LoadIndexPartitionList(FbTransaction tran, SchemaType schemaType, List<IndexPartition> list);

        void LoadIndexSubpartitionList(FbTransaction tran, SchemaType schemaType, List<IndexSubpartition> list);

        void LoadSourceList(FbTransaction tran, SchemaType schemaType, List<Source> list);

        void LoadSourceSynthesis(FbTransaction tran, SchemaType schemaType);

        void LoadTriggerList(FbTransaction tran, SchemaType schemaType, List<Trigger> list);

        void LoadClusterList(FbTransaction tran, SchemaType schemaType, List<Cluster> list);

        void LoadClusterColumnList(FbTransaction tran, SchemaType schemaType, List<ClusterColumn> list);

        void LoadClusterColumnMappingList(FbTransaction tran, SchemaType schemaType, List<ClusterColumnMapping> list);

        void LoadClusterIndexList(FbTransaction tran, SchemaType schemaType, List<ClusterIndex> list);
        
        void LoadOracleTypeList(FbTransaction tran, SchemaType schemaType, List<OracleType> list);

        void LoadObjectPrivilegeList(FbTransaction tran, SchemaType schemaType, List<ObjectPrivilege> list);

        void PurgeMetaDataTables();

        void PurgeDeltaReport();
    }

}
