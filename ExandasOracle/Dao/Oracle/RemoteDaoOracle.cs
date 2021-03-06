using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Dao.Oracle
{
    public class RemoteDaoOracle : AbstractDaoOracle, IRemoteDao
    {
        public RemoteDaoOracle(string connectionString) : base(connectionString)
        {
        }

        private static string GetPrefix(bool DBAViews)
        {
            return DBAViews ? "dba" : "all";
        }

        public bool CheckConnection(bool DBAViews)
        {
            bool result = false;
            using (OracleConnection conn = GetOracleConnection())
            {
                try
                {
                    conn.Open();
                    if (DBAViews)
                    {
                        try
                        {
                            const string sql = "SELECT count(*) FROM dba_tables";
                            var cmd = new OracleCommand(sql, conn);
                            using (var dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    result = true;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw new ApplicationException(Strings.UnableToAccess);
                        }
                    }
                    else
                    {
                        result = true;
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<Table> GetTableList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<Table>();

            const string root = "SELECT table_name, tablespace_name, cluster_name, iot_name, status, logging, degree, partitioned," +
                " iot_type, temporary, nested, duration, cluster_owner, compression, compress_for, dropped, read_only, clustering," +
                " has_identity, container_data, default_collation, external" +
                " FROM {0}_tables WHERE owner = :owner ORDER BY table_name";
            string sql = string.Format(root, GetPrefix(DBAViews));
            
            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ta = new Table();
                    ta.TableName = (string)dr["table_name"];
                    ta.TablespaceName = dr["tablespace_name"] is DBNull ? null : (string)dr["tablespace_name"];
                    ta.ClusterName = dr["cluster_name"] is DBNull ? null : (string)dr["cluster_name"];
                    ta.IOTName = dr["iot_name"] is DBNull ? null : (string)dr["iot_name"];
                    ta.Status = dr["status"] is DBNull ? null : (string)dr["status"];
                    ta.Logging = dr["logging"] is DBNull ? null : (string)dr["logging"];
                    ta.Degree = dr["degree"] is DBNull ? null : (string)dr["degree"];
                    ta.Partitioned = dr["partitioned"] is DBNull ? null : (string)dr["partitioned"];
                    ta.IOTType = dr["iot_type"] is DBNull ? null : (string)dr["iot_type"];
                    ta.Temporary = dr["temporary"] is DBNull ? null : (string)dr["temporary"];
                    ta.Nested = dr["nested"] is DBNull ? null : (string)dr["nested"];
                    ta.Duration = dr["duration"] is DBNull ? null : (string)dr["duration"];
                    ta.ClusterOwner = dr["cluster_owner"] is DBNull ? null : (string)dr["cluster_owner"];
                    ta.Compression = dr["compression"] is DBNull ? null : (string)dr["compression"];
                    ta.CompressFor = dr["compress_for"] is DBNull ? null : (string)dr["compress_for"];
                    ta.Dropped = dr["dropped"] is DBNull ? null : (string)dr["dropped"];
                    ta.ReadOnly = dr["read_only"] is DBNull ? null : (string)dr["read_only"];
                    ta.Clustering = dr["clustering"] is DBNull ? null : (string)dr["clustering"];
                    ta.HasIdentity = dr["has_identity"] is DBNull ? null : (string)dr["has_identity"];
                    ta.ContainerData = dr["container_data"] is DBNull ? null : (string)dr["container_data"];
                    ta.DefaultCollation = dr["default_collation"] is DBNull ? null : (string)dr["default_collation"];
                    ta.External = dr["external"] is DBNull ? null : (string)dr["external"];
                    list.Add(ta);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<TableComment> GetTableCommentList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<TableComment>();

            const string root = "SELECT table_name, comments" +
                " FROM {0}_tab_comments WHERE owner = :owner AND table_type = 'TABLE' ORDER BY table_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var tc = new TableComment();
                    tc.TableName = (string)dr["table_name"];
                    tc.Comments = dr["comments"] is DBNull ? null : (string)dr["comments"];
                    list.Add(tc);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<TableColumn> GetTableColumnList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<TableColumn>();

            const string root = "SELECT tc.table_name, column_name, data_type, data_type_mod, data_type_owner, data_length, data_precision, data_scale, nullable, column_id," +
                " default_length, data_default, char_length, char_used, hidden_column, virtual_column, default_on_null, identity_column, collation" +
                " FROM {0}_tab_cols tc" +
                " JOIN {0}_tables t ON tc.owner = t.owner AND tc.table_name = t.table_name" +
                " WHERE user_generated = 'YES' AND tc.owner = :owner ORDER BY tc.table_name, column_id";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            // cf. https://community.oracle.com/tech/developers/discussion/4000467/odp-net-does-not-support-reading-long-type-from-database
            cmd.InitialLONGFetchSize = -1;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var tc = new TableColumn();
                    tc.TableName = (string)dr["table_name"];
                    tc.ColumnName = (string)dr["column_name"];
                    tc.DataType = dr["data_type"] is DBNull ? null : (string)dr["data_type"];
                    tc.DataTypeMod = dr["data_type_mod"] is DBNull ? null : (string)dr["data_type_mod"];
                    tc.DataTypeOwner = dr["data_type_owner"] is DBNull ? null : (string)dr["data_type_owner"];
                    tc.DataLength = (decimal)dr["data_length"];
                    tc.DataPrecision = dr["data_precision"] is DBNull ? null : (decimal?)dr["data_precision"];
                    tc.DataScale = dr["data_scale"] is DBNull ? null : (decimal?)dr["data_scale"];
                    tc.Nullable = dr["nullable"] is DBNull ? null : (string)dr["nullable"];
                    tc.ColumnId = dr["column_id"] is DBNull ? null : (decimal?)dr["column_id"];
                    tc.DefaultLength = dr["default_length"] is DBNull ? null : (decimal?)dr["default_length"];
                    tc.DataDefault = dr["data_default"] is DBNull ? null : (string)dr["data_default"];
                    tc.CharLength = dr["char_length"] is DBNull ? null : (decimal?)dr["char_length"];
                    tc.CharUsed = dr["char_used"] is DBNull ? null : (string)dr["char_used"];
                    tc.HiddenColumn = dr["hidden_column"] is DBNull ? null : (string)dr["hidden_column"];
                    tc.VirtualColumn = dr["virtual_column"] is DBNull ? null : (string)dr["virtual_column"];
                    tc.DefaultOnNull = dr["default_on_null"] is DBNull ? null : (string)dr["default_on_null"];
                    tc.IdentityColumn = dr["identity_column"] is DBNull ? null : (string)dr["identity_column"];
                    tc.Collation = dr["collation"] is DBNull ? null : (string)dr["collation"];
                    list.Add(tc);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<IdentityColumn> GetIdentityColumnList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<IdentityColumn>();

            const string root = "SELECT table_name, column_name, generation_type, sequence_name, identity_options" +
                " FROM {0}_tab_identity_cols" +
                " WHERE owner = :owner ORDER BY table_name, column_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ic = new IdentityColumn();
                    ic.TableName = (string)dr["table_name"];
                    ic.ColumnName = (string)dr["column_name"];
                    ic.GenerationType = dr["generation_type"] is DBNull ? null : (string)dr["generation_type"];
                    ic.SequenceName = (string)dr["sequence_name"];
                    ic.IdentityOptions = dr["identity_options"] is DBNull ? null : (string)dr["identity_options"];
                    list.Add(ic);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<ColumnComment> GetColumnCommentList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<ColumnComment>();

            const string root = "SELECT table_name, column_name, comments" +
                " FROM {0}_col_comments" +
                " WHERE owner = :owner AND table_name NOT LIKE 'BIN$%'" +
                " ORDER BY table_name, column_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var cc = new ColumnComment();
                    cc.TableName = (string)dr["table_name"];
                    cc.ColumnName = (string)dr["column_name"];
                    cc.Comments = dr["comments"] is DBNull ? null : (string)dr["comments"];
                    list.Add(cc);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<PrimaryKey> GetPrimaryKeyList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<PrimaryKey>();

            const string root = "SELECT constraint_name, table_name, status, deferrable, deferred, validated, rely, index_owner, index_name, invalid, view_related" +
                " FROM {0}_constraints" +
                " WHERE owner = :owner AND constraint_type = 'P' AND generated = 'USER NAME'" +
                " ORDER BY table_name, constraint_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            cmd.InitialLONGFetchSize = -1;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var pk = new PrimaryKey();
                    pk.ConstraintName = (string)dr["constraint_name"];
                    pk.TableName = (string)dr["table_name"];
                    pk.Status = dr["status"] is DBNull ? null : (string)dr["status"];
                    pk.Deferrable = dr["deferrable"] is DBNull ? null : (string)dr["deferrable"];
                    pk.Deferred = dr["deferred"] is DBNull ? null : (string)dr["deferred"];
                    pk.Validated = dr["validated"] is DBNull ? null : (string)dr["validated"];
                    pk.Rely = dr["rely"] is DBNull ? null : (string)dr["rely"];
                    pk.IndexOwner = dr["index_owner"] is DBNull ? null : (string)dr["index_owner"];
                    pk.IndexName = dr["index_name"] is DBNull ? null : (string)dr["index_name"];
                    pk.Invalid = dr["invalid"] is DBNull ? null : (string)dr["invalid"];
                    pk.ViewRelated = dr["view_related"] is DBNull ? null : (string)dr["view_related"];
                    list.Add(pk);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<Unique> GetUniqueList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<Unique>();

            const string root = "SELECT constraint_name, table_name, status, deferrable, deferred, validated, rely, index_owner, index_name, invalid, view_related" +
                " FROM {0}_constraints" +
                " WHERE owner = :owner AND constraint_type = 'U' AND generated = 'USER NAME'" +
                " ORDER BY table_name, constraint_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            cmd.InitialLONGFetchSize = -1;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var un = new Unique();
                    un.ConstraintName = (string)dr["constraint_name"];
                    un.TableName = (string)dr["table_name"];
                    un.Status = dr["status"] is DBNull ? null : (string)dr["status"];
                    un.Deferrable = dr["deferrable"] is DBNull ? null : (string)dr["deferrable"];
                    un.Deferred = dr["deferred"] is DBNull ? null : (string)dr["deferred"];
                    un.Validated = dr["validated"] is DBNull ? null : (string)dr["validated"];
                    un.Rely = dr["rely"] is DBNull ? null : (string)dr["rely"];
                    un.IndexOwner = dr["index_owner"] is DBNull ? null : (string)dr["index_owner"];
                    un.IndexName = dr["index_name"] is DBNull ? null : (string)dr["index_name"];
                    un.Invalid = dr["invalid"] is DBNull ? null : (string)dr["invalid"];
                    un.ViewRelated = dr["view_related"] is DBNull ? null : (string)dr["view_related"];
                    list.Add(un);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<ForeignKey> GetForeignKeyList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<ForeignKey>();

            const string root = "SELECT constraint_name, table_name, r_owner, r_constraint_name, delete_rule, status, deferrable, deferred, validated, invalid, view_related" +
                " FROM {0}_constraints" +
                " WHERE owner = :owner AND constraint_type = 'R' AND generated = 'USER NAME'" +
                " ORDER BY table_name, constraint_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            cmd.InitialLONGFetchSize = -1;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var fk = new ForeignKey();
                    fk.ConstraintName = (string)dr["constraint_name"];
                    fk.TableName = (string)dr["table_name"];
                    fk.ROwner = dr["r_owner"] is DBNull ? null : (string)dr["r_owner"];
                    fk.RConstraintName = dr["r_constraint_name"] is DBNull ? null : (string)dr["r_constraint_name"];
                    fk.DeleteRule = dr["delete_rule"] is DBNull ? null : (string)dr["delete_rule"];
                    fk.Status = dr["status"] is DBNull ? null : (string)dr["status"];
                    fk.Deferrable = dr["deferrable"] is DBNull ? null : (string)dr["deferrable"];
                    fk.Deferred = dr["deferred"] is DBNull ? null : (string)dr["deferred"];
                    fk.Validated = dr["validated"] is DBNull ? null : (string)dr["validated"];
                    fk.Invalid = dr["invalid"] is DBNull ? null : (string)dr["invalid"];
                    fk.ViewRelated = dr["view_related"] is DBNull ? null : (string)dr["view_related"];
                    list.Add(fk);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<Check> GetCheckList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<Check>();

            const string root = "SELECT constraint_name, table_name, search_condition, search_condition_vc, status, deferrable, deferred, validated, invalid, view_related" +
                " FROM {0}_constraints" +
                " WHERE owner = :owner AND constraint_type = 'C' AND generated = 'USER NAME'" +
                " ORDER BY table_name, constraint_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            cmd.InitialLONGFetchSize = -1;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ch = new Check();
                    ch.ConstraintName = (string)dr["constraint_name"];
                    ch.TableName = (string)dr["table_name"];
                    ch.SearchCondition = dr["search_condition"] is DBNull ? null : (string)dr["search_condition"];
                    ch.SearchConditionVC = dr["search_condition_vc"] is DBNull ? null : (string)dr["search_condition_vc"];
                    ch.Status = dr["status"] is DBNull ? null : (string)dr["status"];
                    ch.Deferrable = dr["deferrable"] is DBNull ? null : (string)dr["deferrable"];
                    ch.Deferred = dr["deferred"] is DBNull ? null : (string)dr["deferred"];
                    ch.Validated = dr["validated"] is DBNull ? null : (string)dr["validated"];
                    ch.Invalid = dr["invalid"] is DBNull ? null : (string)dr["invalid"];
                    ch.ViewRelated = dr["view_related"] is DBNull ? null : (string)dr["view_related"];
                    list.Add(ch);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<Constraint> GetConstraintList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<Constraint>();

            const string root = "SELECT constraint_name, constraint_type, table_name, search_condition, search_condition_vc, status, deferrable, deferred, validated, invalid, view_related" +
                " FROM {0}_constraints" +
                " WHERE owner = :owner" +
                " AND (constraint_type NOT IN ('P', 'U', 'R', 'C') OR constraint_type IS NULL) AND generated = 'USER NAME'" +
                " ORDER BY table_name, constraint_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            cmd.InitialLONGFetchSize = -1;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var co = new Constraint();
                    co.ConstraintName = (string)dr["constraint_name"];
                    co.ConstraintType = dr["constraint_type"] is DBNull ? null : (string)dr["constraint_type"];
                    co.TableName = (string)dr["table_name"];
                    co.SearchCondition = dr["search_condition"] is DBNull ? null : (string)dr["search_condition"];
                    co.SearchConditionVC = dr["search_condition_vc"] is DBNull ? null : (string)dr["search_condition_vc"];
                    co.Status = dr["status"] is DBNull ? null : (string)dr["status"];
                    co.Deferrable = dr["deferrable"] is DBNull ? null : (string)dr["deferrable"];
                    co.Deferred = dr["deferred"] is DBNull ? null : (string)dr["deferred"];
                    co.Validated = dr["validated"] is DBNull ? null : (string)dr["validated"];
                    co.Invalid = dr["invalid"] is DBNull ? null : (string)dr["invalid"];
                    co.ViewRelated = dr["view_related"] is DBNull ? null : (string)dr["view_related"];
                    list.Add(co);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<ConstraintColumn> GetConstraintColumnList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<ConstraintColumn>();

            const string root = "SELECT cc.constraint_name, cc.table_name, substr(column_name, 1, 128) column_name, cc.position" +
                " FROM {0}_cons_columns cc" +
                " JOIN {0}_constraints c ON cc.owner = c.owner AND cc.table_name = c.table_name AND cc.constraint_name = c.constraint_name" +
                " WHERE cc.owner = :owner AND c.generated = 'USER NAME'" +
                " ORDER BY table_name, constraint_name, column_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var cc = new ConstraintColumn();
                    cc.ConstraintName = (string)dr["constraint_name"];
                    cc.TableName = (string)dr["table_name"];
                    cc.ColumnName = (string)dr["column_name"];
                    cc.Position = dr["position"] is DBNull ? null : (decimal?)dr["position"];
                    list.Add(cc);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<PartitionedTable> GetPartitionedTableList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<PartitionedTable>();

            const string root = "SELECT table_name, partitioning_type, subpartitioning_type, partition_count, def_subpartition_count, partitioning_key_count, subpartitioning_key_count," +
                " status, def_tablespace_name, def_logging, def_compression, def_compress_for, ref_ptn_constraint_name, interval, autolist," +
                " interval_subpartition, autolist_subpartition, is_nested, def_indexing, def_read_only" +
                " FROM {0}_part_tables" +
                " WHERE owner = :owner" +
                " ORDER BY table_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var pt = new PartitionedTable();
                    pt.TableName = (string)dr["table_name"];
                    pt.PartitioningType = dr["partitioning_type"] is DBNull ? null : (string)dr["partitioning_type"];
                    pt.SubpartitioningType = dr["subpartitioning_type"] is DBNull ? null : (string)dr["subpartitioning_type"];
                    pt.PartitionCount = (decimal)dr["partition_count"];
                    pt.DefSubpartitionCount = dr["def_subpartition_count"] is DBNull ? null : (decimal?)dr["def_subpartition_count"];
                    pt.PartitioningKeyCount = (decimal)dr["partitioning_key_count"];
                    pt.SubpartitioningKeyCount = dr["subpartitioning_key_count"] is DBNull ? null : (decimal?)dr["subpartitioning_key_count"];
                    pt.Status = dr["status"] is DBNull ? null : (string)dr["status"];
                    pt.DefTablespaceName = dr["def_tablespace_name"] is DBNull ? null : (string)dr["def_tablespace_name"];
                    pt.DefLogging = dr["def_logging"] is DBNull ? null : (string)dr["def_logging"];
                    pt.DefCompression = dr["def_compression"] is DBNull ? null : (string)dr["def_compression"];
                    pt.DefCompressFor = dr["def_compress_for"] is DBNull ? null : (string)dr["def_compress_for"];
                    pt.RefPtnConstraintName = dr["ref_ptn_constraint_name"] is DBNull ? null : (string)dr["ref_ptn_constraint_name"];
                    pt.Interval = dr["interval"] is DBNull ? null : (string)dr["interval"];
                    pt.Autolist = dr["autolist"] is DBNull ? null : (string)dr["autolist"];
                    pt.IntervalSubpartition = dr["interval_subpartition"] is DBNull ? null : (string)dr["interval_subpartition"];
                    pt.AutolistSubpartition = dr["autolist_subpartition"] is DBNull ? null : (string)dr["autolist_subpartition"];
                    pt.IsNested = dr["is_nested"] is DBNull ? null : (string)dr["is_nested"];
                    pt.DefIndexing = dr["def_indexing"] is DBNull ? null : (string)dr["def_indexing"];
                    pt.DefReadOnly = dr["def_read_only"] is DBNull ? null : (string)dr["def_read_only"];
                    list.Add(pt);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<TablePartition> GetTablePartitionList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<TablePartition>();

            const string root = "SELECT table_name, composite, partition_name, subpartition_count, high_value, high_value_length, partition_position," +
                " tablespace_name, logging, compression, compress_for, is_nested, parent_table_partition, interval, indexing, read_only" +
                " FROM {0}_tab_partitions" +
                " WHERE table_owner = :table_owner" +
                " ORDER BY table_name, partition_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("table_owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var tp = new TablePartition();
                    tp.TableName = (string)dr["table_name"];
                    tp.Composite = dr["composite"] is DBNull ? null : (string)dr["composite"];
                    tp.PartitionName = (string)dr["partition_name"];
                    tp.SubpartitionCount = dr["subpartition_count"] is DBNull ? null : (decimal?)dr["subpartition_count"];
                    tp.HighValue = dr["high_value"] is DBNull ? null : (string)dr["high_value"];
                    tp.HighValueLength = dr["high_value_length"] is DBNull ? null : (decimal?)dr["high_value_length"];
                    tp.PartitionPosition = dr["partition_position"] is DBNull ? null : (decimal?)dr["partition_position"];
                    tp.TablespaceName = dr["tablespace_name"] is DBNull ? null : (string)dr["tablespace_name"];
                    tp.Logging = dr["logging"] is DBNull ? null : (string)dr["logging"];
                    tp.Compression = dr["compression"] is DBNull ? null : (string)dr["compression"];
                    tp.CompressFor = dr["compress_for"] is DBNull ? null : (string)dr["compress_for"];
                    tp.IsNested = dr["is_nested"] is DBNull ? null : (string)dr["is_nested"];
                    tp.ParentTablePartition = dr["parent_table_partition"] is DBNull ? null : (string)dr["parent_table_partition"];
                    tp.Interval = dr["interval"] is DBNull ? null : (string)dr["interval"];
                    tp.Indexing = dr["indexing"] is DBNull ? null : (string)dr["indexing"];
                    tp.ReadOnly = dr["read_only"] is DBNull ? null : (string)dr["read_only"];
                    list.Add(tp);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<TableSubpartition> GetTableSubpartitionList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<TableSubpartition>();

            const string root = "SELECT table_name, partition_name, subpartition_name, high_value, high_value_length, partition_position, subpartition_position," +
                " tablespace_name, logging, compression, compress_for, interval, indexing, read_only" +
                " FROM {0}_tab_subpartitions" +
                " WHERE table_owner = :table_owner" +
                " ORDER BY table_name, partition_name, subpartition_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("table_owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ts = new TableSubpartition();
                    ts.TableName = (string)dr["table_name"];
                    ts.PartitionName = (string)dr["partition_name"];
                    ts.SubpartitionName = (string)dr["subpartition_name"];
                    ts.HighValue = dr["high_value"] is DBNull ? null : (string)dr["high_value"];
                    ts.HighValueLength = dr["high_value_length"] is DBNull ? null : (decimal?)dr["high_value_length"];
                    ts.PartitionPosition = dr["partition_position"] is DBNull ? null : (decimal?)dr["partition_position"];
                    ts.SubpartitionPosition = dr["subpartition_position"] is DBNull ? null : (decimal?)dr["subpartition_position"];
                    ts.TablespaceName = dr["tablespace_name"] is DBNull ? null : (string)dr["tablespace_name"];
                    ts.Logging = dr["logging"] is DBNull ? null : (string)dr["logging"];
                    ts.Compression = dr["compression"] is DBNull ? null : (string)dr["compression"];
                    ts.CompressFor = dr["compress_for"] is DBNull ? null : (string)dr["compress_for"];
                    ts.Interval = dr["interval"] is DBNull ? null : (string)dr["interval"];
                    ts.Indexing = dr["indexing"] is DBNull ? null : (string)dr["indexing"];
                    ts.ReadOnly = dr["read_only"] is DBNull ? null : (string)dr["read_only"];
                    list.Add(ts);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<View> GetViewList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<View>();

            const string root = "SELECT view_name, text_length, text, text_vc, substr(type_text, 1, 128) type_text, substr(oid_text, 1, 128) oid_text, view_type_owner, view_type," +
                " superview_name, read_only, bequeath, default_collation" +
                " FROM {0}_views WHERE owner = :owner ORDER BY view_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;
            
            cmd.InitialLONGFetchSize = -1;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var vi = new View();
                    vi.ViewName = (string)dr["view_name"];
                    vi.TextLength = dr["text_length"] is DBNull ? null : (decimal?)dr["text_length"];
                    vi.Text = dr["text"] is DBNull ? null : (string)dr["text"];
                    vi.TextVC = dr["text_vc"] is DBNull ? null : (string)dr["text_vc"];
                    vi.TypeText = dr["type_text"] is DBNull ? null : (string)dr["type_text"];
                    vi.OidText = dr["oid_text"] is DBNull ? null : (string)dr["oid_text"];
                    vi.ViewTypeOwner = dr["view_type_owner"] is DBNull ? null : (string)dr["view_type_owner"];
                    vi.ViewType = dr["view_type"] is DBNull ? null : (string)dr["view_type"];
                    vi.SuperviewName = dr["superview_name"] is DBNull ? null : (string)dr["superview_name"];
                    vi.ReadOnly = dr["read_only"] is DBNull ? null : (string)dr["read_only"];
                    vi.Bequeath = dr["bequeath"] is DBNull ? null : (string)dr["bequeath"];
                    vi.DefaultCollation = dr["default_collation"] is DBNull ? null : (string)dr["default_collation"];
                    list.Add(vi);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<ViewComment> GetViewCommentList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<ViewComment>();

            const string root = "SELECT table_name view_name, comments" +
                " FROM {0}_tab_comments WHERE owner = :owner AND table_type = 'VIEW' ORDER BY table_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var vc = new ViewComment();
                    vc.ViewName = (string)dr["view_name"];
                    vc.Comments = dr["comments"] is DBNull ? null : (string)dr["comments"];
                    list.Add(vc);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<ViewColumn> GetViewColumnList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<ViewColumn>();

            const string root = "SELECT tc.table_name, column_name, data_type, data_type_mod, data_type_owner, data_length, data_precision, data_scale, nullable, column_id," +
                " default_length, data_default, char_length, char_used, hidden_column, virtual_column, default_on_null, identity_column, collation" +
                " FROM {0}_tab_cols tc" +
                " JOIN {0}_views v ON tc.owner = v.owner AND tc.table_name = v.view_name" +
                " WHERE user_generated = 'YES' AND tc.owner = :owner ORDER BY tc.table_name, column_id";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            cmd.InitialLONGFetchSize = -1;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var vc = new ViewColumn();
                    vc.TableName = (string)dr["table_name"];
                    vc.ColumnName = (string)dr["column_name"];
                    vc.DataType = dr["data_type"] is DBNull ? null : (string)dr["data_type"];
                    vc.DataTypeMod = dr["data_type_mod"] is DBNull ? null : (string)dr["data_type_mod"];
                    vc.DataTypeOwner = dr["data_type_owner"] is DBNull ? null : (string)dr["data_type_owner"];
                    vc.DataLength = (decimal)dr["data_length"];
                    vc.DataPrecision = dr["data_precision"] is DBNull ? null : (decimal?)dr["data_precision"];
                    vc.DataScale = dr["data_scale"] is DBNull ? null : (decimal?)dr["data_scale"];
                    vc.Nullable = dr["nullable"] is DBNull ? null : (string)dr["nullable"];
                    vc.ColumnId = dr["column_id"] is DBNull ? null : (decimal?)dr["column_id"];
                    vc.DefaultLength = dr["default_length"] is DBNull ? null : (decimal?)dr["default_length"];
                    vc.DataDefault = dr["data_default"] is DBNull ? null : (string)dr["data_default"];
                    vc.CharLength = dr["char_length"] is DBNull ? null : (decimal?)dr["char_length"];
                    vc.CharUsed = dr["char_used"] is DBNull ? null : (string)dr["char_used"];
                    vc.HiddenColumn = dr["hidden_column"] is DBNull ? null : (string)dr["hidden_column"];
                    vc.VirtualColumn = dr["virtual_column"] is DBNull ? null : (string)dr["virtual_column"];
                    vc.DefaultOnNull = dr["default_on_null"] is DBNull ? null : (string)dr["default_on_null"];
                    vc.IdentityColumn = dr["identity_column"] is DBNull ? null : (string)dr["identity_column"];
                    vc.Collation = dr["collation"] is DBNull ? null : (string)dr["collation"];
                    list.Add(vc);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<MaterializedView> GetMaterializedViewList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<MaterializedView>();

            const string root = "SELECT mview_name, container_name, query, query_len, updatable, update_log, master_rollback_seg, master_link," +
                " rewrite_enabled, rewrite_capability, refresh_mode, refresh_method, build_mode, fast_refreshable, use_no_index, default_collation" +
                " FROM {0}_mviews WHERE owner = :owner ORDER BY mview_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            cmd.InitialLONGFetchSize = -1;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var mv = new MaterializedView();
                    mv.MViewName = (string)dr["mview_name"];
                    mv.ContainerName = (string)dr["container_name"];
                    mv.Query = dr["query"] is DBNull ? null : (string)dr["query"];
                    mv.QueryLen = dr["query_len"] is DBNull ? null : (decimal?)dr["query_len"];
                    mv.Updatable = dr["updatable"] is DBNull ? null : (string)dr["updatable"];
                    mv.UpdateLog = dr["update_log"] is DBNull ? null : (string)dr["update_log"];
                    mv.MasterRollbackSeg = dr["master_rollback_seg"] is DBNull ? null : (string)dr["master_rollback_seg"];
                    mv.MasterLink = dr["master_link"] is DBNull ? null : (string)dr["master_link"];
                    mv.RewriteEnabled = dr["rewrite_enabled"] is DBNull ? null : (string)dr["rewrite_enabled"];
                    mv.RewriteCapability = dr["rewrite_capability"] is DBNull ? null : (string)dr["rewrite_capability"];
                    mv.RefreshMode = dr["refresh_mode"] is DBNull ? null : (string)dr["refresh_mode"];
                    mv.RefreshMethod = dr["refresh_method"] is DBNull ? null : (string)dr["refresh_method"];
                    mv.BuildMode = dr["build_mode"] is DBNull ? null : (string)dr["build_mode"];
                    mv.FastRefreshable = dr["fast_refreshable"] is DBNull ? null : (string)dr["fast_refreshable"];
                    mv.UseNoIndex = dr["use_no_index"] is DBNull ? null : (string)dr["use_no_index"];
                    mv.DefaultCollation = dr["default_collation"] is DBNull ? null : (string)dr["default_collation"];
                    list.Add(mv);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<MaterializedViewComment> GetMaterializedViewCommentList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<MaterializedViewComment>();

            const string root = "SELECT mview_name, comments" +
                " FROM {0}_mview_comments WHERE owner = :owner ORDER BY mview_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var mc = new MaterializedViewComment();
                    mc.MViewName = (string)dr["mview_name"];
                    mc.Comments = dr["comments"] is DBNull ? null : (string)dr["comments"];
                    list.Add(mc);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<Sequence> GetSequenceList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<Sequence>();

            const string root = "SELECT s.sequence_name, min_value, max_value, increment_by, cycle_flag, order_flag," +
                " cache_size, scale_flag, extend_flag, sharded_flag, session_flag, keep_value" +
                " FROM {0}_sequences s" +
                " LEFT JOIN {0}_tab_identity_cols id ON s.sequence_owner = id.owner AND s.sequence_name = id.sequence_name" +
                " WHERE id.sequence_name IS NULL AND s.sequence_owner = :sequence_owner ORDER BY sequence_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("sequence_owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var se = new Sequence();
                    se.SequenceName = (string)dr["sequence_name"];
                    se.MinValue = dr["min_value"] is DBNull ? null : (decimal?)dr["min_value"];
                    se.MaxValue = dr["max_value"] is DBNull ? null : (decimal?)dr["max_value"];
                    se.IncrementBy = (decimal)dr["increment_by"];
                    se.CycleFlag = dr["cycle_flag"] is DBNull ? null : (string)dr["cycle_flag"];
                    se.OrderFlag = dr["order_flag"] is DBNull ? null : (string)dr["order_flag"];
                    se.CacheSize = (decimal)dr["cache_size"];
                    se.ScaleFlag = dr["scale_flag"] is DBNull ? null : (string)dr["scale_flag"];
                    se.ExtendFlag = dr["extend_flag"] is DBNull ? null : (string)dr["extend_flag"];
                    se.ShardedFlag = dr["sharded_flag"] is DBNull ? null : (string)dr["sharded_flag"];
                    se.SessionFlag = dr["session_flag"] is DBNull ? null : (string)dr["session_flag"];
                    se.KeepValue = dr["keep_value"] is DBNull ? null : (string)dr["keep_value"];
                    list.Add(se);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<TableIndex> GetTableIndexList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<TableIndex>();

            const string root = "SELECT index_name, index_type, table_name, uniqueness, compression, prefix_length, tablespace_name," +
                " include_column, logging, status, degree, partitioned, temporary, duration" +
                " FROM {0}_indexes" +
                " WHERE owner = :owner AND table_type = 'TABLE' AND generated = 'N' ORDER BY index_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ti = new TableIndex();
                    ti.IndexName = (string)dr["index_name"];
                    ti.IndexType = dr["index_type"] is DBNull ? null : (string)dr["index_type"];
                    ti.TableName = (string)dr["table_name"];
                    ti.Uniqueness = dr["uniqueness"] is DBNull ? null : (string)dr["uniqueness"];
                    ti.Compression = dr["compression"] is DBNull ? null : (string)dr["compression"];
                    ti.PrefixLength = dr["prefix_length"] is DBNull ? null : (decimal?)dr["prefix_length"];
                    ti.TablespaceName = dr["tablespace_name"] is DBNull ? null : (string)dr["tablespace_name"];
                    ti.IncludeColumn = dr["include_column"] is DBNull ? null : (decimal?)dr["include_column"];
                    ti.Logging = dr["logging"] is DBNull ? null : (string)dr["logging"];
                    ti.Status = dr["status"] is DBNull ? null : (string)dr["status"];
                    ti.Degree = dr["degree"] is DBNull ? null : (string)dr["degree"];
                    ti.Partitioned = dr["partitioned"] is DBNull ? null : (string)dr["partitioned"];
                    ti.Temporary = dr["temporary"] is DBNull ? null : (string)dr["temporary"];
                    ti.Duration = dr["duration"] is DBNull ? null : (string)dr["duration"];
                    list.Add(ti);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<IndexColumn> GetIndexColumnList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<IndexColumn>();

            const string root = "SELECT index_name, table_owner, table_name, substr(column_name, 1, 128) column_name," +
                " column_position, column_length, char_length, descend, collated_column_id" +
                " FROM {0}_ind_columns" +
                " WHERE index_owner = :owner AND column_name NOT LIKE 'SYS_NC%$' ORDER BY index_name, column_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ic = new IndexColumn();
                    ic.IndexName = (string)dr["index_name"];
                    ic.TableOwner = (string)dr["table_owner"];
                    ic.TableName = (string)dr["table_name"];
                    ic.ColumnName = (string)dr["column_name"];
                    ic.ColumnPosition = (decimal)dr["column_position"];
                    ic.ColumnLength = (decimal)dr["column_length"];
                    ic.CharLength = dr["char_length"] is DBNull ? null : (decimal?)dr["char_length"];
                    ic.Descend = dr["descend"] is DBNull ? null : (string)dr["descend"];
                    ic.CollatedColumnId = dr["collated_column_id"] is DBNull ? null : (decimal?)dr["collated_column_id"];
                    list.Add(ic);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<IndexExpression> GetIndexExpressionList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<IndexExpression>();

            const string root = "SELECT index_name, table_owner, table_name, column_expression, column_position" +
                " FROM {0}_ind_expressions" +
                " WHERE index_owner = :index_owner ORDER BY index_name, column_position";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("index_owner", OracleDbType.Varchar2).Value = schema;

            cmd.InitialLONGFetchSize = -1;
            
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ie = new IndexExpression();
                    ie.IndexName = (string)dr["index_name"];
                    ie.TableOwner = (string)dr["table_owner"];
                    ie.TableName = (string)dr["table_name"];
                    ie.ColumnExpression = dr["column_expression"] is DBNull ? null : (string)dr["column_expression"];
                    ie.ColumnPosition = (decimal)dr["column_position"];
                    list.Add(ie);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<PartitionedIndex> GetPartitionedIndexList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<PartitionedIndex>();

            const string root = "SELECT index_name, table_name, partitioning_type, subpartitioning_type, partition_count, def_subpartition_count," +
                " partitioning_key_count, subpartitioning_key_count, locality, alignment, def_tablespace_name, def_logging, def_parameters," +
                " interval, autolist, interval_subpartition, autolist_subpartition" +
                " FROM {0}_part_indexes" +
                " WHERE owner = :owner ORDER BY index_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var pi = new PartitionedIndex();
                    pi.IndexName = (string)dr["index_name"];
                    pi.TableName = (string)dr["table_name"];
                    pi.PartitioningType = dr["partitioning_type"] is DBNull ? null : (string)dr["partitioning_type"];
                    pi.SubpartitioningType = dr["subpartitioning_type"] is DBNull ? null : (string)dr["subpartitioning_type"];
                    pi.PartitionCount = (decimal)dr["partition_count"];
                    pi.DefSubpartitionCount = dr["def_subpartition_count"] is DBNull ? null : (decimal?)dr["def_subpartition_count"];
                    pi.PartitioningKeyCount = (decimal)dr["partitioning_key_count"];
                    pi.SubpartitioningKeyCount = dr["subpartitioning_key_count"] is DBNull ? null : (decimal?)dr["subpartitioning_key_count"];
                    pi.Locality = dr["locality"] is DBNull ? null : (string)dr["locality"];
                    pi.Alignment = dr["alignment"] is DBNull ? null : (string)dr["alignment"];
                    pi.DefTablespaceName = dr["def_tablespace_name"] is DBNull ? null : (string)dr["def_tablespace_name"];
                    pi.DefLogging = dr["def_logging"] is DBNull ? null : (string)dr["def_logging"];
                    pi.DefParameters = dr["def_parameters"] is DBNull ? null : (string)dr["def_parameters"];
                    pi.Interval = dr["interval"] is DBNull ? null : (string)dr["interval"];
                    pi.Autolist = dr["autolist"] is DBNull ? null : (string)dr["autolist"];
                    pi.IntervalSubpartition = dr["interval_subpartition"] is DBNull ? null : (string)dr["interval_subpartition"];
                    pi.AutolistSubpartition = dr["autolist_subpartition"] is DBNull ? null : (string)dr["autolist_subpartition"];
                    list.Add(pi);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<IndexPartition> GetIndexPartitionList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<IndexPartition>();

            const string root = "SELECT index_name, composite, partition_name, subpartition_count, high_value, high_value_length," +
                " partition_position, status, tablespace_name, logging, compression, parameters, interval" +
                " FROM {0}_ind_partitions" +
                " WHERE index_owner = :index_owner ORDER BY index_name, partition_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("index_owner", OracleDbType.Varchar2).Value = schema;
            
            cmd.InitialLONGFetchSize = -1;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ip = new IndexPartition();
                    ip.IndexName = (string)dr["index_name"];
                    ip.Composite = dr["composite"] is DBNull ? null : (string)dr["composite"];
                    ip.PartitionName = (string)dr["partition_name"];
                    ip.SubpartitionCount = dr["subpartition_count"] is DBNull ? null : (decimal?)dr["subpartition_count"];
                    ip.HighValue = dr["high_value"] is DBNull ? null : (string)dr["high_value"];
                    ip.HighValueLength = dr["high_value_length"] is DBNull ? null : (decimal?)dr["high_value_length"];
                    ip.PartitionPosition = dr["partition_position"] is DBNull ? null : (decimal?)dr["partition_position"];
                    ip.Status = dr["status"] is DBNull ? null : (string)dr["status"];
                    ip.TablespaceName = dr["tablespace_name"] is DBNull ? null : (string)dr["tablespace_name"];
                    ip.Logging = dr["logging"] is DBNull ? null : (string)dr["logging"];
                    ip.Compression = dr["compression"] is DBNull ? null : (string)dr["compression"];
                    ip.Parameters = dr["parameters"] is DBNull ? null : (string)dr["parameters"];
                    ip.Interval = dr["interval"] is DBNull ? null : (string)dr["interval"];
                    list.Add(ip);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<IndexSubpartition> GetIndexSubpartitionList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<IndexSubpartition>();

            const string root = "SELECT index_name, partition_name, subpartition_name, high_value, high_value_length, partition_position," +
                " subpartition_position, status, tablespace_name, logging, compression, parameters, interval" +
                " FROM {0}_ind_subpartitions" +
                " WHERE index_owner = :index_owner ORDER BY index_name, partition_name, subpartition_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("index_owner", OracleDbType.Varchar2).Value = schema;

            cmd.InitialLONGFetchSize = -1;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ip = new IndexSubpartition();
                    ip.IndexName = (string)dr["index_name"];
                    ip.PartitionName = (string)dr["partition_name"];
                    ip.SubpartitionName = (string)dr["subpartition_name"];
                    ip.HighValue = dr["high_value"] is DBNull ? null : (string)dr["high_value"];
                    ip.HighValueLength = dr["high_value_length"] is DBNull ? null : (decimal?)dr["high_value_length"];
                    ip.PartitionPosition = dr["partition_position"] is DBNull ? null : (decimal?)dr["partition_position"];
                    ip.SubpartitionPosition = dr["subpartition_position"] is DBNull ? null : (decimal?)dr["subpartition_position"];
                    ip.Status = dr["status"] is DBNull ? null : (string)dr["status"];
                    ip.TablespaceName = dr["tablespace_name"] is DBNull ? null : (string)dr["tablespace_name"];
                    ip.Logging = dr["logging"] is DBNull ? null : (string)dr["logging"];
                    ip.Compression = dr["compression"] is DBNull ? null : (string)dr["compression"];
                    ip.Parameters = dr["parameters"] is DBNull ? null : (string)dr["parameters"];
                    ip.Interval = dr["interval"] is DBNull ? null : (string)dr["interval"];
                    list.Add(ip);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<Source> GetSourceList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<Source>();

            const string root = "SELECT name, type, line, text" +
                " FROM {0}_source" +
                " WHERE owner = :owner ORDER BY name, type, line";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var so = new Source();
                    so.Name = (string)dr["name"];
                    so.Type = (string)dr["type"];
                    so.Line = (decimal)dr["line"];
                    so.Text = dr["text"] is DBNull ? null : (string)dr["text"];
                    list.Add(so);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<Trigger> GetTriggerList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<Trigger>();

            const string root = "SELECT trigger_name, trigger_type, triggering_event, table_owner, base_object_type, table_name, column_name," +
                " referencing_names, when_clause, status, description, action_type, trigger_body, before_statement," +
                " before_row, after_row, after_statement, instead_of_row, fire_once, apply_server_only" +
                " FROM {0}_triggers" +
                " WHERE owner = :owner ORDER BY trigger_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            cmd.InitialLONGFetchSize = -1;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var tr = new Trigger();
                    tr.TriggerName = (string)dr["trigger_name"];
                    tr.TriggerType = dr["trigger_type"] is DBNull ? null : (string)dr["trigger_type"];
                    tr.TriggeringEvent = dr["triggering_event"] is DBNull ? null : (string)dr["triggering_event"];
                    tr.TableOwner = dr["table_owner"] is DBNull ? null : (string)dr["table_owner"];
                    tr.BaseObjectType = dr["base_object_type"] is DBNull ? null : (string)dr["base_object_type"];
                    tr.TableName = dr["table_name"] is DBNull ? null : (string)dr["table_name"];
                    tr.ColumnName = dr["column_name"] is DBNull ? null : (string)dr["column_name"];
                    tr.ReferencingNames = dr["referencing_names"] is DBNull ? null : (string)dr["referencing_names"];
                    tr.WhenClause = dr["when_clause"] is DBNull ? null : (string)dr["when_clause"];
                    tr.Status = dr["status"] is DBNull ? null : (string)dr["status"];
                    tr.Description = dr["description"] is DBNull ? null : (string)dr["description"];
                    tr.ActionType = dr["action_type"] is DBNull ? null : (string)dr["action_type"];
                    tr.TriggerBody = dr["trigger_body"] is DBNull ? null : (string)dr["trigger_body"];
                    tr.BeforeStatement = dr["before_statement"] is DBNull ? null : (string)dr["before_statement"];
                    tr.BeforeRow = dr["before_row"] is DBNull ? null : (string)dr["before_row"];
                    tr.AfterRow = dr["after_row"] is DBNull ? null : (string)dr["after_row"];
                    tr.AfterStatement = dr["after_statement"] is DBNull ? null : (string)dr["after_statement"];
                    tr.InsteadOfRow = dr["instead_of_row"] is DBNull ? null : (string)dr["instead_of_row"];
                    tr.FireOnce = dr["fire_once"] is DBNull ? null : (string)dr["fire_once"];
                    tr.ApplyServerOnly = dr["apply_server_only"] is DBNull ? null : (string)dr["apply_server_only"];
                    list.Add(tr);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<Cluster> GetClusterList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<Cluster>();

            const string root = "SELECT cluster_name, tablespace_name, cluster_type, function, hashkeys, degree, cache, single_table, dependencies" +
                " FROM {0}_clusters" +
                " WHERE owner = :owner ORDER BY cluster_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var cl = new Cluster();
                    cl.ClusterName = (string)dr["cluster_name"];
                    cl.TablespaceName = (string)dr["tablespace_name"];
                    cl.ClusterType = dr["cluster_type"] is DBNull ? null : (string)dr["cluster_type"];
                    cl.Function = dr["function"] is DBNull ? null : (string)dr["function"];
                    cl.Hashkeys = dr["hashkeys"] is DBNull ? null : (decimal?)dr["hashkeys"];
                    cl.Degree = dr["degree"] is DBNull ? null : (string)dr["degree"];
                    cl.Cache = dr["cache"] is DBNull ? null : (string)dr["cache"];
                    cl.SingleTable = dr["single_table"] is DBNull ? null : (string)dr["single_table"];
                    cl.Dependencies = dr["dependencies"] is DBNull ? null : (string)dr["dependencies"];
                    list.Add(cl);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<ClusterColumn> GetClusterColumnList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<ClusterColumn>();

            const string root = "SELECT tc.table_name, column_name, data_type, data_type_mod, data_type_owner, data_length, data_precision, data_scale, nullable, column_id," +
                " default_length, data_default, char_length, char_used, hidden_column, virtual_column, default_on_null, identity_column, collation" +
                " FROM {0}_tab_cols tc" +
                " JOIN {0}_clusters c ON tc.owner = c.owner AND tc.table_name = c.cluster_name" +
                " WHERE user_generated = 'YES' AND tc.owner = :owner ORDER BY tc.table_name, column_id";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            cmd.InitialLONGFetchSize = -1;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var cc = new ClusterColumn();
                    cc.TableName = (string)dr["table_name"];
                    cc.ColumnName = (string)dr["column_name"];
                    cc.DataType = dr["data_type"] is DBNull ? null : (string)dr["data_type"];
                    cc.DataTypeMod = dr["data_type_mod"] is DBNull ? null : (string)dr["data_type_mod"];
                    cc.DataTypeOwner = dr["data_type_owner"] is DBNull ? null : (string)dr["data_type_owner"];
                    cc.DataLength = (decimal)dr["data_length"];
                    cc.DataPrecision = dr["data_precision"] is DBNull ? null : (decimal?)dr["data_precision"];
                    cc.DataScale = dr["data_scale"] is DBNull ? null : (decimal?)dr["data_scale"];
                    cc.Nullable = dr["nullable"] is DBNull ? null : (string)dr["nullable"];
                    cc.ColumnId = dr["column_id"] is DBNull ? null : (decimal?)dr["column_id"];
                    cc.DefaultLength = dr["default_length"] is DBNull ? null : (decimal?)dr["default_length"];
                    cc.DataDefault = dr["data_default"] is DBNull ? null : (string)dr["data_default"];
                    cc.CharLength = dr["char_length"] is DBNull ? null : (decimal?)dr["char_length"];
                    cc.CharUsed = dr["char_used"] is DBNull ? null : (string)dr["char_used"];
                    cc.HiddenColumn = dr["hidden_column"] is DBNull ? null : (string)dr["hidden_column"];
                    cc.VirtualColumn = dr["virtual_column"] is DBNull ? null : (string)dr["virtual_column"];
                    cc.DefaultOnNull = dr["default_on_null"] is DBNull ? null : (string)dr["default_on_null"];
                    cc.IdentityColumn = dr["identity_column"] is DBNull ? null : (string)dr["identity_column"];
                    cc.Collation = dr["collation"] is DBNull ? null : (string)dr["collation"];
                    list.Add(cc);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<ClusterColumnMapping> GetClusterColumnMappingList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<ClusterColumnMapping>();

            string sql;
            OracleCommand cmd;

            // this test is needed because all_clu_columns view does not exist
            if (DBAViews)
            {
                sql = "SELECT cluster_name, clu_column_name, table_name, tab_column_name" +
                    " FROM dba_clu_columns" +
                    " WHERE owner = :owner" +
                    " ORDER BY cluster_name, clu_column_name, table_name";

                cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;
            }
            else
            {
                sql = "SELECT cluster_name, clu_column_name, table_name, tab_column_name" +
                    " FROM user_clu_columns" +
                    " ORDER BY cluster_name, clu_column_name, table_name";
                
                cmd = new OracleCommand(sql, conn);
            }

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ccm = new ClusterColumnMapping();
                    ccm.ClusterName = (string)dr["cluster_name"];
                    ccm.CluColumnName = (string)dr["clu_column_name"];
                    ccm.TableName = (string)dr["table_name"];
                    ccm.TabColumnName = dr["tab_column_name"] is DBNull ? null : (string)dr["tab_column_name"];
                    list.Add(ccm);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<ClusterIndex> GetClusterIndexList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<ClusterIndex>();

            string sql;
            OracleCommand cmd;

            // this switch is necessary because the all_indexes view does not display cluster type indexes
            // and we are therefore obliged to use the user_indexes view
            // this is probably an Oracle bug
            if (DBAViews)
            {
                sql = "SELECT index_name, index_type, table_name, uniqueness, compression, prefix_length, tablespace_name," +
                    " include_column, logging, status, degree, partitioned, temporary, duration" +
                    " FROM dba_indexes" +
                    " WHERE owner = :owner AND table_type = 'CLUSTER' AND generated = 'N' ORDER BY index_name";
                cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;
            }
            else
            {
                sql = "SELECT index_name, index_type, table_name, uniqueness, compression, prefix_length, tablespace_name," +
                    " include_column, logging, status, degree, partitioned, temporary, duration" +
                    " FROM user_indexes" +
                    " WHERE table_type = 'CLUSTER' AND generated = 'N' ORDER BY index_name";
                cmd = new OracleCommand(sql, conn);
            }

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ci = new ClusterIndex();
                    ci.IndexName = (string)dr["index_name"];
                    ci.IndexType = dr["index_type"] is DBNull ? null : (string)dr["index_type"];
                    ci.TableName = (string)dr["table_name"];
                    ci.Uniqueness = dr["uniqueness"] is DBNull ? null : (string)dr["uniqueness"];
                    ci.Compression = dr["compression"] is DBNull ? null : (string)dr["compression"];
                    ci.PrefixLength = dr["prefix_length"] is DBNull ? null : (decimal?)dr["prefix_length"];
                    ci.TablespaceName = dr["tablespace_name"] is DBNull ? null : (string)dr["tablespace_name"];
                    ci.IncludeColumn = dr["include_column"] is DBNull ? null : (decimal?)dr["include_column"];
                    ci.Logging = dr["logging"] is DBNull ? null : (string)dr["logging"];
                    ci.Status = dr["status"] is DBNull ? null : (string)dr["status"];
                    ci.Degree = dr["degree"] is DBNull ? null : (string)dr["degree"];
                    ci.Partitioned = dr["partitioned"] is DBNull ? null : (string)dr["partitioned"];
                    ci.Temporary = dr["temporary"] is DBNull ? null : (string)dr["temporary"];
                    ci.Duration = dr["duration"] is DBNull ? null : (string)dr["duration"];
                    list.Add(ci);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<OracleType> GetOracleTypeList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<OracleType>();

            const string root = "SELECT type_name, typecode, attributes, methods, predefined, incomplete, final," +
                " instantiable, persistable, supertype_owner, supertype_name, local_attributes, local_methods" +
                " FROM {0}_types" +
                " WHERE owner = :owner ORDER BY type_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var ot = new OracleType();
                    ot.TypeName = (string)dr["type_name"];
                    ot.Typecode = dr["type_code"] is DBNull ? null : (string)dr["type_code"];
                    ot.Attributes = dr["attributes"] is DBNull ? null : (decimal?)dr["attributes"];
                    ot.Methods = dr["methods"] is DBNull ? null : (decimal?)dr["methods"];
                    ot.Predefined = dr["predefined"] is DBNull ? null : (string)dr["predefined"];
                    ot.Incomplete = dr["incomplete"] is DBNull ? null : (string)dr["incomplete"];
                    ot.Final = dr["final"] is DBNull ? null : (string)dr["final"];
                    ot.Instantiable = dr["instantiable"] is DBNull ? null : (string)dr["instantiable"];
                    ot.Persistable = dr["persistable"] is DBNull ? null : (string)dr["persistable"];
                    ot.SupertypeOwner = dr["supertype_owner"] is DBNull ? null : (string)dr["supertype_owner"];
                    ot.SupertypeName = dr["supertype_name"] is DBNull ? null : (string)dr["supertype_name"];
                    ot.LocalAttributes = dr["local_attributes"] is DBNull ? null : (decimal?)dr["local_attributes"];
                    ot.LocalMethods = dr["local_methods"] is DBNull ? null : (decimal?)dr["local_methods"];
                    list.Add(ot);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<DatabaseLink> GetDatabaseLinkList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<DatabaseLink>();

            const string root = "SELECT db_link, username, host, shard_internal, valid" +
                " FROM {0}_db_links" +
                " WHERE owner = :owner ORDER BY db_link";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var dl = new DatabaseLink();
                    dl.DbLink = (string)dr["db_link"];
                    dl.Username = dr["username"] is DBNull ? null : (string)dr["username"];
                    dl.Host = dr["host"] is DBNull ? null : (string)dr["host"];
                    dl.ShardInternal = dr["shard_internal"] is DBNull ? null : (string)dr["shard_internal"];
                    dl.Valid = dr["valid"] is DBNull ? null : (string)dr["valid"];
                    list.Add(dl);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<ObjectPrivilege> GetObjectPrivilegeList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<ObjectPrivilege>();

            string sql;
            if (DBAViews)
            {
                sql = "SELECT grantee, owner table_schema, table_name, privilege, grantable, hierarchy, common, type, inherited" +
                    " FROM dba_tab_privs" +
                    " WHERE grantor = :grantor ORDER BY grantee, table_name, privilege, inherited";
            }
            else
            {
                sql = "SELECT grantee, table_schema, table_name, privilege, grantable, hierarchy, common, type, inherited" +
                    " FROM all_tab_privs" +
                    " WHERE grantor = :grantor ORDER BY grantee, table_name, privilege, inherited";
            }
            
            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("grantor", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var op = new ObjectPrivilege();
                    op.Grantee = (string)dr["grantee"];
                    op.TableSchema = (string)dr["table_schema"];
                    op.TableName = (string)dr["table_name"];
                    op.Privilege = (string)dr["privilege"];
                    op.Grantable = dr["grantable"] is DBNull ? null : (string)dr["grantable"];
                    op.Hierarchy = dr["hierarchy"] is DBNull ? null : (string)dr["hierarchy"];
                    op.Common = dr["common"] is DBNull ? null : (string)dr["common"];
                    op.Type = dr["type"] is DBNull ? null : (string)dr["type"];
                    op.Inherited = (string)dr["inherited"];
                    list.Add(op);
                }
            }
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="schema"></param>
        /// <param name="DBAViews"></param>
        /// <returns></returns>
        public List<Synonym> GetSynonymList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<Synonym>();

            const string root = "SELECT owner, synonym_name, table_owner, table_name, db_link" +
                " FROM {0}_synonyms" +
                " WHERE owner IN ('PUBLIC', :owner) AND table_owner = :owner" +
                " ORDER BY owner, synonym_name";
            string sql = string.Format(root, GetPrefix(DBAViews));

            var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = schema;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sy = new Synonym();
                    sy.Owner = (string)dr["owner"];
                    sy.SynonymName = (string)dr["synonym_name"];
                    sy.TableOwner = dr["table_owner"] is DBNull ? null : (string)dr["table_owner"];
                    sy.TableName = dr["table_name"] is DBNull ? null : (string)dr["table_name"];
                    sy.DbLink = dr["db_link"] is DBNull ? null : (string)dr["db_link"];
                    list.Add(sy);
                }
            }
            return list;
        }

    }
}
