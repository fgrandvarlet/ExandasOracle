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

        void LoadViewList(FbTransaction tran, SchemaType schemaType, List<View> list);

        void LoadSequenceList(FbTransaction tran, SchemaType schemaType, List<Sequence> list);

        void LoadTableIndexList(FbTransaction tran, SchemaType schemaType, List<TableIndex> list);

        void LoadIndexPartitionList(FbTransaction tran, SchemaType schemaType, List<IndexPartition> list);

        void LoadSourceList(FbTransaction tran, SchemaType schemaType, List<Source> list);

        void LoadSourceSynthesis(FbTransaction tran, SchemaType schemaType);

        void LoadClusterList(FbTransaction tran, SchemaType schemaType, List<Cluster> list);
    }

}
