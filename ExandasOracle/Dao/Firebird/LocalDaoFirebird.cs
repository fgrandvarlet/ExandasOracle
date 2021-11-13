using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao.Firebird
{
    public class LocalDaoFirebird : AbstractDaoFirebird, ILocalDao
    {
        public LocalDaoFirebird(string connectionString) : base(connectionString)
        {
        }

        protected override FbCommand CreateCommand(Criteria criteria)
        {
            throw new NotImplementedException();
        }

        private static string GetPrefix(SchemaType schemaType)
        {
            string result;
            switch (schemaType)
            {
                case SchemaType.Source:
                    result = "src";
                    break;
                case SchemaType.Target:
                    result = "tgt";
                    break;
                default:
                    throw new ArgumentException(nameof(schemaType));
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadTableList(FbTransaction tran, SchemaType schemaType, List<Table> list)
        {
            var tableName = string.Format("{0}_tables", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@table_name, @tablespace_name, @cluster_name, @iot_name, @status, @logging, @degree, @partitioned," +
                " @iot_type, @tab_temporary, @nested, @duration, @cluster_owner, @compression, @compress_for, @dropped, @read_only, @clustering," +
                " @has_identity, @container_data, @default_collation, @tab_external)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ta in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("table_name", ta.TableName);
                cmd.Parameters.AddWithValue("tablespace_name", ta.TablespaceName);
                cmd.Parameters.AddWithValue("cluster_name", ta.ClusterName);
                cmd.Parameters.AddWithValue("iot_name", ta.IOTName);
                cmd.Parameters.AddWithValue("status", ta.Status);
                cmd.Parameters.AddWithValue("logging", ta.Logging);
                cmd.Parameters.AddWithValue("degree", ta.Degree);
                cmd.Parameters.AddWithValue("partitioned", ta.Partitioned);
                cmd.Parameters.AddWithValue("iot_type", ta.IOTType);
                cmd.Parameters.AddWithValue("tab_temporary", ta.Temporary);
                cmd.Parameters.AddWithValue("nested", ta.Nested);
                cmd.Parameters.AddWithValue("duration", ta.Duration);
                cmd.Parameters.AddWithValue("cluster_owner", ta.ClusterOwner);
                cmd.Parameters.AddWithValue("compression", ta.Compression);
                cmd.Parameters.AddWithValue("compress_for", ta.CompressFor);
                cmd.Parameters.AddWithValue("dropped", ta.Dropped);
                cmd.Parameters.AddWithValue("read_only", ta.ReadOnly);
                cmd.Parameters.AddWithValue("clustering", ta.Clustering);
                cmd.Parameters.AddWithValue("has_identity", ta.HasIdentity);
                cmd.Parameters.AddWithValue("container_data", ta.ContainerData);
                cmd.Parameters.AddWithValue("default_collation", ta.DefaultCollation);
                cmd.Parameters.AddWithValue("tab_external", ta.External);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadTableColumnList(FbTransaction tran, SchemaType schemaType, List<TableColumn> list)
        {
            var tableName = string.Format("{0}_tab_cols", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@table_name, @column_name, @data_type, @data_type_mod, @data_type_owner, @data_length, @data_precision," +
                " @data_scale, @nullable, @column_id, @default_length, @data_default, @col_char_length, @char_used, @hidden_column, @virtual_column," +
                " @default_on_null, @identity_column, @collation)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var tc in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("table_name", tc.TableName);
                cmd.Parameters.AddWithValue("column_name", tc.ColumnName);
                cmd.Parameters.AddWithValue("data_type", tc.DataType);
                cmd.Parameters.AddWithValue("data_type_mod", tc.DataTypeMod);
                cmd.Parameters.AddWithValue("data_type_owner", tc.DataTypeOwner);
                cmd.Parameters.AddWithValue("data_length", tc.DataLength);
                cmd.Parameters.AddWithValue("data_precision", tc.DataPrecision);
                cmd.Parameters.AddWithValue("data_scale", tc.DataScale);
                cmd.Parameters.AddWithValue("nullable", tc.Nullable);
                cmd.Parameters.AddWithValue("column_id", tc.ColumnId);
                cmd.Parameters.AddWithValue("default_length", tc.DefaultLength);
                cmd.Parameters.AddWithValue("data_default", tc.DataDefault);
                cmd.Parameters.AddWithValue("col_char_length", tc.CharLength);
                cmd.Parameters.AddWithValue("char_used", tc.CharUsed);
                cmd.Parameters.AddWithValue("hidden_column", tc.HiddenColumn);
                cmd.Parameters.AddWithValue("virtual_column", tc.VirtualColumn);
                cmd.Parameters.AddWithValue("default_on_null", tc.DefaultOnNull);
                cmd.Parameters.AddWithValue("identity_column", tc.IdentityColumn);
                cmd.Parameters.AddWithValue("collation", tc.Collation);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadColumnCommentList(FbTransaction tran, SchemaType schemaType, List<ColumnComment> list)
        {
            var tableName = string.Format("{0}_col_comments", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@table_name, @column_name, @comments)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var cc in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("table_name", cc.TableName);
                cmd.Parameters.AddWithValue("column_name", cc.ColumnName);
                cmd.Parameters.AddWithValue("comments", cc.Comments);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadPrimaryKeyList(FbTransaction tran, SchemaType schemaType, List<PrimaryKey> list)
        {
            var tableName = string.Format("{0}_primary_keys", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@constraint_name, @table_name, @status, @deferrable, @deferred, @validated," +
                " @rely, @index_owner, @index_name, @invalid, @view_related)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var pk in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("constraint_name", pk.ConstraintName);
                cmd.Parameters.AddWithValue("table_name", pk.TableName);
                cmd.Parameters.AddWithValue("status", pk.Status);
                cmd.Parameters.AddWithValue("deferrable", pk.Deferrable);
                cmd.Parameters.AddWithValue("deferred", pk.Deferred);
                cmd.Parameters.AddWithValue("validated", pk.Validated);
                cmd.Parameters.AddWithValue("rely", pk.Rely);
                cmd.Parameters.AddWithValue("index_owner", pk.IndexOwner);
                cmd.Parameters.AddWithValue("index_name", pk.IndexName);
                cmd.Parameters.AddWithValue("invalid", pk.Invalid);
                cmd.Parameters.AddWithValue("view_related", pk.ViewRelated);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadUniqueList(FbTransaction tran, SchemaType schemaType, List<Unique> list)
        {
            var tableName = string.Format("{0}_uniques", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@constraint_name, @table_name, @status, @deferrable, @deferred, @validated," +
                " @rely, @index_owner, @index_name, @invalid, @view_related)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var un in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("constraint_name", un.ConstraintName);
                cmd.Parameters.AddWithValue("table_name", un.TableName);
                cmd.Parameters.AddWithValue("status", un.Status);
                cmd.Parameters.AddWithValue("deferrable", un.Deferrable);
                cmd.Parameters.AddWithValue("deferred", un.Deferred);
                cmd.Parameters.AddWithValue("validated", un.Validated);
                cmd.Parameters.AddWithValue("rely", un.Rely);
                cmd.Parameters.AddWithValue("index_owner", un.IndexOwner);
                cmd.Parameters.AddWithValue("index_name", un.IndexName);
                cmd.Parameters.AddWithValue("invalid", un.Invalid);
                cmd.Parameters.AddWithValue("view_related", un.ViewRelated);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadForeignKeyList(FbTransaction tran, SchemaType schemaType, List<ForeignKey> list)
        {
            var tableName = string.Format("{0}_foreign_keys", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@constraint_name, @table_name, @r_owner, @r_constraint_name, @delete_rule," +
                " @status, @deferrable, @deferred, @validated, @invalid, @view_related)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var fk in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("constraint_name", fk.ConstraintName);
                cmd.Parameters.AddWithValue("table_name", fk.TableName);
                cmd.Parameters.AddWithValue("r_owner", fk.ROwner);
                cmd.Parameters.AddWithValue("r_constraint_name", fk.RConstraintName);
                cmd.Parameters.AddWithValue("delete_rule", fk.DeleteRule);
                cmd.Parameters.AddWithValue("status", fk.Status);
                cmd.Parameters.AddWithValue("deferrable", fk.Deferrable);
                cmd.Parameters.AddWithValue("deferred", fk.Deferred);
                cmd.Parameters.AddWithValue("validated", fk.Validated);
                cmd.Parameters.AddWithValue("invalid", fk.Invalid);
                cmd.Parameters.AddWithValue("view_related", fk.ViewRelated);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadCheckList(FbTransaction tran, SchemaType schemaType, List<Check> list)
        {
            var tableName = string.Format("{0}_checks", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@constraint_name, @table_name, @search_condition, @search_condition_vc," +
                " @status, @deferrable, @deferred, @validated, @invalid, @view_related)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ch in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("constraint_name", ch.ConstraintName);
                cmd.Parameters.AddWithValue("table_name", ch.TableName);
                cmd.Parameters.AddWithValue("search_condition", ch.SearchCondition);
                cmd.Parameters.AddWithValue("search_condition_vc", ch.SearchConditionVC);
                cmd.Parameters.AddWithValue("status", ch.Status);
                cmd.Parameters.AddWithValue("deferrable", ch.Deferrable);
                cmd.Parameters.AddWithValue("deferred", ch.Deferred);
                cmd.Parameters.AddWithValue("validated", ch.Validated);
                cmd.Parameters.AddWithValue("invalid", ch.Invalid);
                cmd.Parameters.AddWithValue("view_related", ch.ViewRelated);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadConstraintList(FbTransaction tran, SchemaType schemaType, List<Constraint> list)
        {
            var tableName = string.Format("{0}_constraints", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@constraint_name, @constraint_type, @table_name, @search_condition, @search_condition_vc," +
                " @status, @deferrable, @deferred, @validated, @invalid, @view_related)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var co in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("constraint_name", co.ConstraintName);
                cmd.Parameters.AddWithValue("constraint_type", co.ConstraintType);
                cmd.Parameters.AddWithValue("table_name", co.TableName);
                cmd.Parameters.AddWithValue("search_condition", co.SearchCondition);
                cmd.Parameters.AddWithValue("search_condition_vc", co.SearchConditionVC);
                cmd.Parameters.AddWithValue("status", co.Status);
                cmd.Parameters.AddWithValue("deferrable", co.Deferrable);
                cmd.Parameters.AddWithValue("deferred", co.Deferred);
                cmd.Parameters.AddWithValue("validated", co.Validated);
                cmd.Parameters.AddWithValue("invalid", co.Invalid);
                cmd.Parameters.AddWithValue("view_related", co.ViewRelated);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadConstraintColumnList(FbTransaction tran, SchemaType schemaType, List<ConstraintColumn> list)
        {
            var tableName = string.Format("{0}_cons_columns", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@constraint_name, @table_name, @column_name, @col_position)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var cc in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("constraint_name", cc.ConstraintName);
                cmd.Parameters.AddWithValue("table_name", cc.TableName);
                cmd.Parameters.AddWithValue("column_name", cc.ColumnName);
                cmd.Parameters.AddWithValue("col_position", cc.Position);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadPartitionedTableList(FbTransaction tran, SchemaType schemaType, List<PartitionedTable> list)
        {
            var tableName = string.Format("{0}_part_tables", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@table_name, @partitioning_type, @subpartitioning_type, @partition_count, @def_subpartition_count," +
                " @partitioning_key_count, @subpartitioning_key_count, @status, @def_tablespace_name, @def_logging, @def_compression, @def_compress_for," +
                " @ref_ptn_constraint_name, @interval, @autolist, @interval_subpartition, @autolist_subpartition, @is_nested, @def_indexing, @def_read_only)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var pt in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("table_name", pt.TableName);
                cmd.Parameters.AddWithValue("partitioning_type", pt.PartitioningType);
                cmd.Parameters.AddWithValue("subpartitioning_type", pt.SubpartitioningType);
                cmd.Parameters.AddWithValue("partition_count", pt.PartitionCount);
                cmd.Parameters.AddWithValue("def_subpartition_count", pt.DefSubpartitionCount);
                cmd.Parameters.AddWithValue("partitioning_key_count", pt.PartitioningKeyCount);
                cmd.Parameters.AddWithValue("subpartitioning_key_count", pt.SubpartitioningKeyCount);
                cmd.Parameters.AddWithValue("status", pt.Status);
                cmd.Parameters.AddWithValue("def_tablespace_name", pt.DefTablespaceName);
                cmd.Parameters.AddWithValue("def_logging", pt.DefLogging);
                cmd.Parameters.AddWithValue("def_compression", pt.DefCompression);
                cmd.Parameters.AddWithValue("def_compress_for", pt.DefCompressFor);
                cmd.Parameters.AddWithValue("ref_ptn_constraint_name", pt.RefPtnConstraintName);
                cmd.Parameters.AddWithValue("interval", pt.Interval);
                cmd.Parameters.AddWithValue("autolist", pt.Autolist);
                cmd.Parameters.AddWithValue("interval_subpartition", pt.IntervalSubpartition);
                cmd.Parameters.AddWithValue("autolist_subpartition", pt.AutolistSubpartition);
                cmd.Parameters.AddWithValue("is_nested", pt.IsNested);
                cmd.Parameters.AddWithValue("def_indexing", pt.DefIndexing);
                cmd.Parameters.AddWithValue("def_read_only", pt.DefReadOnly);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadTablePartitionList(FbTransaction tran, SchemaType schemaType, List<TablePartition> list)
        {
            var tableName = string.Format("{0}_tab_partitions", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@table_name, @composite, @partition_name, @subpartition_count, @high_value, @high_value_length," +
                " @partition_position, @tablespace_name, @logging, @compression, @compress_for, @is_nested, @parent_table_partition, @interval, @indexing, @read_only)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var tp in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("table_name", tp.TableName);
                cmd.Parameters.AddWithValue("composite", tp.Composite);
                cmd.Parameters.AddWithValue("partition_name", tp.PartitionName);
                cmd.Parameters.AddWithValue("subpartition_count", tp.SubpartitionCount);
                cmd.Parameters.AddWithValue("high_value", tp.HighValue);
                cmd.Parameters.AddWithValue("high_value_length", tp.HighValueLength);
                cmd.Parameters.AddWithValue("partition_position", tp.PartitionPosition);
                cmd.Parameters.AddWithValue("tablespace_name", tp.TablespaceName);
                cmd.Parameters.AddWithValue("logging", tp.Logging);
                cmd.Parameters.AddWithValue("compression", tp.Compression);
                cmd.Parameters.AddWithValue("compress_for", tp.CompressFor);
                cmd.Parameters.AddWithValue("is_nested", tp.IsNested);
                cmd.Parameters.AddWithValue("parent_table_partition", tp.ParentTablePartition);
                cmd.Parameters.AddWithValue("interval", tp.Interval);
                cmd.Parameters.AddWithValue("indexing", tp.Indexing);
                cmd.Parameters.AddWithValue("read_only", tp.ReadOnly);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadTableSubpartitionList(FbTransaction tran, SchemaType schemaType, List<TableSubpartition> list)
        {
            var tableName = string.Format("{0}_tab_subpartitions", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@table_name, @partition_name, @subpartition_name, @high_value, @high_value_length," +
                " @partition_position, @subpartition_position, @tablespace_name, @logging, @compression, @compress_for, @interval, @indexing, @read_only)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ts in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("table_name", ts.TableName);
                cmd.Parameters.AddWithValue("partition_name", ts.PartitionName);
                cmd.Parameters.AddWithValue("subpartition_name", ts.SubpartitionName);
                cmd.Parameters.AddWithValue("high_value", ts.HighValue);
                cmd.Parameters.AddWithValue("high_value_length", ts.HighValueLength);
                cmd.Parameters.AddWithValue("partition_position", ts.PartitionPosition);
                cmd.Parameters.AddWithValue("subpartition_position", ts.SubpartitionPosition);
                cmd.Parameters.AddWithValue("tablespace_name", ts.TablespaceName);
                cmd.Parameters.AddWithValue("logging", ts.Logging);
                cmd.Parameters.AddWithValue("compression", ts.Compression);
                cmd.Parameters.AddWithValue("compress_for", ts.CompressFor);
                cmd.Parameters.AddWithValue("interval", ts.Interval);
                cmd.Parameters.AddWithValue("indexing", ts.Indexing);
                cmd.Parameters.AddWithValue("read_only", ts.ReadOnly);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadViewList(FbTransaction tran, SchemaType schemaType, List<View> list)
        {
            var tableName = string.Format("{0}_views", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@view_name, @text_length, @text, @text_vc, @type_text, @oid_text," +
                " @view_type_owner, @view_type, @superview_name, @read_only, @bequeath, @default_collation)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var vi in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("view_name", vi.ViewName);
                cmd.Parameters.AddWithValue("text_length", vi.TextLength);
                cmd.Parameters.AddWithValue("text", vi.Text);
                cmd.Parameters.AddWithValue("text_vc", vi.TextVC);
                cmd.Parameters.AddWithValue("type_text", vi.TypeText);
                cmd.Parameters.AddWithValue("oid_text", vi.OidText);
                cmd.Parameters.AddWithValue("view_type_owner", vi.ViewTypeOwner);
                cmd.Parameters.AddWithValue("view_type", vi.ViewType);
                cmd.Parameters.AddWithValue("superview_name", vi.SuperviewName);
                cmd.Parameters.AddWithValue("read_only", vi.ReadOnly);
                cmd.Parameters.AddWithValue("bequeath", vi.Bequeath);
                cmd.Parameters.AddWithValue("default_collation", vi.DefaultCollation);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadMaterializedViewList(FbTransaction tran, SchemaType schemaType, List<MaterializedView> list)
        {
            var tableName = string.Format("{0}_mviews", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@mview_name, @container_name, @query, @query_len, @updatable, @update_log, @master_rollback_seg, @master_link," +
                " @rewrite_enabled, @rewrite_capability, @refresh_mode, @refresh_method, @build_mode, @fast_refreshable, @use_no_index, @default_collation)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var mv in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("mview_name", mv.MViewName);
                cmd.Parameters.AddWithValue("container_name", mv.ContainerName);
                cmd.Parameters.AddWithValue("query", mv.Query);
                cmd.Parameters.AddWithValue("query_len", mv.QueryLen);
                cmd.Parameters.AddWithValue("updatable", mv.Updatable);
                cmd.Parameters.AddWithValue("update_log", mv.UpdateLog);
                cmd.Parameters.AddWithValue("master_rollback_seg", mv.MasterRollbackSeg);
                cmd.Parameters.AddWithValue("master_link", mv.MasterLink);
                cmd.Parameters.AddWithValue("rewrite_enabled", mv.RewriteEnabled);
                cmd.Parameters.AddWithValue("rewrite_capability", mv.RewriteCapability);
                cmd.Parameters.AddWithValue("refresh_mode", mv.RefreshMode);
                cmd.Parameters.AddWithValue("refresh_method", mv.RefreshMethod);
                cmd.Parameters.AddWithValue("build_mode", mv.BuildMode);
                cmd.Parameters.AddWithValue("fast_refreshable", mv.FastRefreshable);
                cmd.Parameters.AddWithValue("use_no_index", mv.UseNoIndex);
                cmd.Parameters.AddWithValue("default_collation", mv.DefaultCollation);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadSequenceList(FbTransaction tran, SchemaType schemaType, List<Sequence> list)
        {
            var tableName = string.Format("{0}_sequences", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@sequence_name, @min_value, @max_value, @increment_by, @cycle_flag," +
                " @order_flag, @cache_size, @scale_flag, @extend_flag, @sharded_flag, @session_flag, @keep_value)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var se in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("sequence_name", se.SequenceName);
                cmd.Parameters.AddWithValue("min_value", se.MinValue);
                cmd.Parameters.AddWithValue("max_value", se.MaxValue);
                cmd.Parameters.AddWithValue("increment_by", se.IncrementBy);
                cmd.Parameters.AddWithValue("cycle_flag", se.CycleFlag);
                cmd.Parameters.AddWithValue("order_flag", se.OrderFlag);
                cmd.Parameters.AddWithValue("cache_size", se.CacheSize);
                cmd.Parameters.AddWithValue("scale_flag", se.ScaleFlag);
                cmd.Parameters.AddWithValue("extend_flag", se.ExtendFlag);
                cmd.Parameters.AddWithValue("sharded_flag", se.ShardedFlag);
                cmd.Parameters.AddWithValue("session_flag", se.SessionFlag);
                cmd.Parameters.AddWithValue("keep_value", se.KeepValue);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadTableIndexList(FbTransaction tran, SchemaType schemaType, List<TableIndex> list)
        {
            var tableName = string.Format("{0}_table_indexes", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@index_name, @index_type, @table_name, @uniqueness, @compression, @prefix_length, @tablespace_name," +
                " @include_column, @logging, @status, @degree, @partitioned, @temporary, @duration)";
            
            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ti in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("index_name", ti.IndexName);
                cmd.Parameters.AddWithValue("index_type", ti.IndexType);
                cmd.Parameters.AddWithValue("table_name", ti.TableName);
                cmd.Parameters.AddWithValue("uniqueness", ti.Uniqueness);
                cmd.Parameters.AddWithValue("compression", ti.Compression);
                cmd.Parameters.AddWithValue("prefix_length", ti.PrefixLength);
                cmd.Parameters.AddWithValue("tablespace_name", ti.TablespaceName);
                cmd.Parameters.AddWithValue("include_column", ti.IncludeColumn);
                cmd.Parameters.AddWithValue("logging", ti.Logging);
                cmd.Parameters.AddWithValue("status", ti.Status);
                cmd.Parameters.AddWithValue("degree", ti.Degree);
                cmd.Parameters.AddWithValue("partitioned", ti.Partitioned);
                cmd.Parameters.AddWithValue("temporary", ti.Temporary);
                cmd.Parameters.AddWithValue("duration", ti.Duration);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadIndexColumnList(FbTransaction tran, SchemaType schemaType, List<IndexColumn> list)
        {
            var tableName = string.Format("{0}_ind_columns", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@index_name, @table_owner, @table_name, @column_name, @column_position," +
                " @column_length, @col_char_length, @descend, @collated_column_id)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ic in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("index_name", ic.IndexName);
                cmd.Parameters.AddWithValue("table_owner", ic.TableOwner);
                cmd.Parameters.AddWithValue("table_name", ic.TableName);
                cmd.Parameters.AddWithValue("column_name", ic.ColumnName);
                cmd.Parameters.AddWithValue("column_position", ic.ColumnPosition);
                cmd.Parameters.AddWithValue("column_length", ic.ColumnLength);
                cmd.Parameters.AddWithValue("col_char_length", ic.CharLength);
                cmd.Parameters.AddWithValue("descend", ic.Descend);
                cmd.Parameters.AddWithValue("collated_column_id", ic.CollatedColumnId);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadPartitionedIndexList(FbTransaction tran, SchemaType schemaType, List<PartitionedIndex> list)
        {
            var tableName = string.Format("{0}_part_indexes", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@index_name, @table_name, @partitioning_type, @subpartitioning_type," +
                " @partition_count, @def_subpartition_count, @partitioning_key_count, @subpartitioning_key_count, @locality, @alignment," +
                " @def_tablespace_name, @def_logging, @def_parameters, @interval, @autolist, @interval_subpartition, @autolist_subpartition)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var pi in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("index_name", pi.IndexName);
                cmd.Parameters.AddWithValue("table_name", pi.TableName);
                cmd.Parameters.AddWithValue("partitioning_type", pi.PartitioningType);
                cmd.Parameters.AddWithValue("subpartitioning_type", pi.SubpartitioningType);
                cmd.Parameters.AddWithValue("partition_count", pi.PartitionCount);
                cmd.Parameters.AddWithValue("def_subpartition_count", pi.DefSubpartitionCount);
                cmd.Parameters.AddWithValue("partitioning_key_count", pi.PartitioningKeyCount);
                cmd.Parameters.AddWithValue("subpartitioning_key_count", pi.SubpartitioningKeyCount);
                cmd.Parameters.AddWithValue("locality", pi.Locality);
                cmd.Parameters.AddWithValue("alignment", pi.Alignment);
                cmd.Parameters.AddWithValue("def_tablespace_name", pi.DefTablespaceName);
                cmd.Parameters.AddWithValue("def_logging", pi.DefLogging);
                cmd.Parameters.AddWithValue("def_parameters", pi.DefParameters);
                cmd.Parameters.AddWithValue("interval", pi.Interval);
                cmd.Parameters.AddWithValue("autolist", pi.Autolist);
                cmd.Parameters.AddWithValue("interval_subpartition", pi.IntervalSubpartition);
                cmd.Parameters.AddWithValue("autolist_subpartition", pi.AutolistSubpartition);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadIndexPartitionList(FbTransaction tran, SchemaType schemaType, List<IndexPartition> list)
        {
            var tableName = string.Format("{0}_ind_partitions", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@index_name, @composite, @partition_name, @subpartition_count," +
                " @high_value, @high_value_length, @partition_position, @status, @tablespace_name, @logging, @compression, @parameters, @interval)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ip in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("index_name", ip.IndexName);
                cmd.Parameters.AddWithValue("composite", ip.Composite);
                cmd.Parameters.AddWithValue("partition_name", ip.PartitionName);
                cmd.Parameters.AddWithValue("subpartition_count", ip.SubpartitionCount);
                cmd.Parameters.AddWithValue("high_value", ip.HighValue);
                cmd.Parameters.AddWithValue("high_value_length", ip.HighValueLength);
                cmd.Parameters.AddWithValue("partition_position", ip.PartitionPosition);
                cmd.Parameters.AddWithValue("status", ip.Status);
                cmd.Parameters.AddWithValue("tablespace_name", ip.TablespaceName);
                cmd.Parameters.AddWithValue("logging", ip.Logging);
                cmd.Parameters.AddWithValue("compression", ip.Compression);
                cmd.Parameters.AddWithValue("parameters", ip.Parameters);
                cmd.Parameters.AddWithValue("interval", ip.Interval);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadIndexSubpartitionList(FbTransaction tran, SchemaType schemaType, List<IndexSubpartition> list)
        {
            var tableName = string.Format("{0}_ind_subpartitions", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@index_name, @partition_name, @subpartition_name, @high_value, @high_value_length, @partition_position," +
                " @subpartition_position, @status, @tablespace_name, @logging, @compression, @parameters, @interval)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var ip in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("index_name", ip.IndexName);
                cmd.Parameters.AddWithValue("partition_name", ip.PartitionName);
                cmd.Parameters.AddWithValue("subpartition_name", ip.SubpartitionName);
                cmd.Parameters.AddWithValue("high_value", ip.HighValue);
                cmd.Parameters.AddWithValue("high_value_length", ip.HighValueLength);
                cmd.Parameters.AddWithValue("partition_position", ip.PartitionPosition);
                cmd.Parameters.AddWithValue("subpartition_position", ip.SubpartitionPosition);
                cmd.Parameters.AddWithValue("status", ip.Status);
                cmd.Parameters.AddWithValue("tablespace_name", ip.TablespaceName);
                cmd.Parameters.AddWithValue("logging", ip.Logging);
                cmd.Parameters.AddWithValue("compression", ip.Compression);
                cmd.Parameters.AddWithValue("parameters", ip.Parameters);
                cmd.Parameters.AddWithValue("interval", ip.Interval);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadSourceList(FbTransaction tran, SchemaType schemaType, List<Source> list)
        {
            var tableName = string.Format("{0}_source", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@source_name, @source_type, @line, @text)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var so in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("source_name", so.Name);
                cmd.Parameters.AddWithValue("source_type", so.Type);
                cmd.Parameters.AddWithValue("line", so.Line);
                cmd.Parameters.AddWithValue("text", so.Text);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        public void LoadSourceSynthesis(FbTransaction tran, SchemaType schemaType)
        {
            var tableName = string.Format("{0}_source_synthesis", GetPrefix(schemaType));

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            const string sqlInsert = "INSERT INTO {0} VALUES(@source_name, @source_type, @text)";
            var cmdInsert = new FbCommand(string.Format(sqlInsert, tableName), tran.Connection, tran);
            cmdInsert.Prepare();

            const string sqlSelect = "SELECT source_name, source_type, line, text FROM {0}_source ORDER BY source_name, source_type, line";
            var cmdSelect = new FbCommand(string.Format(sqlSelect, GetPrefix(schemaType)), tran.Connection, tran);

            string currentName = string.Empty;
            string currentType = string.Empty;
            string currentText = string.Empty;

            using (var dr = cmdSelect.ExecuteReader())
            {
                while (dr.Read())
                {
                    var so = new Source();
                    so.Name = (string)dr["source_name"];
                    so.Type = (string)dr["source_type"];
                    so.Line = (int)dr["line"];
                    so.Text = dr["text"] is DBNull ? null : (string)dr["text"];

                    if (so.Name != currentName || so.Type != currentType)
                    {
                        if (!string.IsNullOrEmpty(currentName) && !string.IsNullOrEmpty(currentType))
                        {
                            cmdInsert.Parameters.Clear();
                            cmdInsert.Parameters.AddWithValue("source_name", currentName);
                            cmdInsert.Parameters.AddWithValue("source_type", currentType);
                            cmdInsert.Parameters.AddWithValue("text", currentText);
                            cmdInsert.ExecuteNonQuery();
                        }
                        currentName = so.Name;
                        currentType = so.Type;
                        currentText = so.Text;
                    }
                    else
                    {
                        currentText += so.Text;
                    }
                }

                if (!string.IsNullOrEmpty(currentName) && !string.IsNullOrEmpty(currentType))
                {
                    cmdInsert.Parameters.Clear();
                    cmdInsert.Parameters.AddWithValue("source_name", currentName);
                    cmdInsert.Parameters.AddWithValue("source_type", currentType);
                    cmdInsert.Parameters.AddWithValue("text", currentText);
                    cmdInsert.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadTriggerList(FbTransaction tran, SchemaType schemaType, List<Trigger> list)
        {
            var tableName = string.Format("{0}_triggers", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@trigger_name, @trigger_type, @triggering_event, @table_owner, @base_object_type, @table_name, @column_name," +
                " @referencing_names, @when_clause, @status, @description, @action_type, @trigger_body, @before_statement," +
                " @before_row, @after_row, @after_statement, @instead_of_row, @fire_once, @apply_server_only)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var tr in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("trigger_name", tr.TriggerName);
                cmd.Parameters.AddWithValue("trigger_type", tr.TriggerType);
                cmd.Parameters.AddWithValue("triggering_event", tr.TriggeringEvent);
                cmd.Parameters.AddWithValue("table_owner", tr.TableOwner);
                cmd.Parameters.AddWithValue("base_object_type", tr.BaseObjectType);
                cmd.Parameters.AddWithValue("table_name", tr.TableName);
                cmd.Parameters.AddWithValue("column_name", tr.ColumnName);
                cmd.Parameters.AddWithValue("referencing_names", tr.ReferencingNames);
                cmd.Parameters.AddWithValue("when_clause", tr.WhenClause);
                cmd.Parameters.AddWithValue("status", tr.Status);
                cmd.Parameters.AddWithValue("description", tr.Description);
                cmd.Parameters.AddWithValue("action_type", tr.ActionType);
                cmd.Parameters.AddWithValue("trigger_body", tr.TriggerBody);
                cmd.Parameters.AddWithValue("before_statement", tr.BeforeStatement);
                cmd.Parameters.AddWithValue("before_row", tr.BeforeRow);
                cmd.Parameters.AddWithValue("after_row", tr.AfterRow);
                cmd.Parameters.AddWithValue("after_statement", tr.AfterStatement);
                cmd.Parameters.AddWithValue("instead_of_row", tr.InsteadOfRow);
                cmd.Parameters.AddWithValue("fire_once", tr.FireOnce);
                cmd.Parameters.AddWithValue("apply_server_only", tr.ApplyServerOnly);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="schemaType"></param>
        /// <param name="list"></param>
        public void LoadClusterList(FbTransaction tran, SchemaType schemaType, List<Cluster> list)
        {
            var tableName = string.Format("{0}_clusters", GetPrefix(schemaType));

            const string sql = "INSERT INTO {0} VALUES(@cluster_name, @tablespace_name, @cluster_type, @clu_function," +
                " @hashkeys, @degree, @cache, @single_table, @dependencies)";

            // preliminary purge of the table
            (new FbCommand(string.Format("DELETE FROM {0}", tableName), tran.Connection, tran)).ExecuteNonQuery();

            // data insertion
            var cmd = new FbCommand(string.Format(sql, tableName), tran.Connection, tran);
            cmd.Prepare();

            foreach (var cl in list)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("cluster_name", cl.ClusterName);
                cmd.Parameters.AddWithValue("tablespace_name", cl.TablespaceName);
                cmd.Parameters.AddWithValue("cluster_type", cl.ClusterType);
                cmd.Parameters.AddWithValue("clu_function", cl.Function);
                cmd.Parameters.AddWithValue("hashkeys", cl.Hashkeys);
                cmd.Parameters.AddWithValue("degree", cl.Degree);
                cmd.Parameters.AddWithValue("cache", cl.Cache);
                cmd.Parameters.AddWithValue("single_table", cl.SingleTable);
                cmd.Parameters.AddWithValue("dependencies", cl.Dependencies);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
