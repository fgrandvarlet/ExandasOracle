﻿using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

using ExandasOracle.Domain;

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
                            throw new ApplicationException("Impossible d'accéder aux vues DBA");
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

            const string root = "SELECT table_name, tablespace_name FROM {0}_tables WHERE owner = :owner ORDER BY table_name";
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
        public List<View> GetViewList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<View>();

            const string root = "SELECT view_name, text_length, text, text_vc, type_text, oid_text, view_type_owner, view_type," +
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
        public List<IndexPartition> GetIndexPartitionList(OracleConnection conn, string schema, bool DBAViews)
        {
            var list = new List<IndexPartition>();

            const string root = "SELECT index_name, composite, partition_name, subpartition_count, high_value, high_value_length," +
                " partition_position, status, tablespace_name, logging, compression" +
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

    }
}