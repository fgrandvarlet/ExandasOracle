using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Core
{
    public partial class Delta
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="list"></param>
        private void DeltaTable(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name FROM src_tables s" +
                " LEFT JOIN tgt_tables t USING(table_name)" +
                " WHERE t.table_name IS NULL" +
                " ORDER BY table_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "TABLE", (string)dr["table_name"], Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.table_name FROM tgt_tables t" +
                " LEFT JOIN src_tables s USING(table_name)" +
                " WHERE s.table_name IS NULL" +
                " ORDER BY table_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "TABLE", (string)dr["table_name"], Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_tables";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceTable = new Table
                    {
                        TableName = (string)dr["table_name"],
                        TablespaceName = dr["src_tablespace_name"] is DBNull ? null : (string)dr["src_tablespace_name"],
                        ClusterName = dr["src_cluster_name"] is DBNull ? null : (string)dr["src_cluster_name"],
                        IOTName = dr["src_iot_name"] is DBNull ? null : (string)dr["src_iot_name"],
                        Status = dr["src_status"] is DBNull ? null : (string)dr["src_status"],
                        Logging = dr["src_logging"] is DBNull ? null : (string)dr["src_logging"],
                        Degree = dr["src_degree"] is DBNull ? null : (string)dr["src_degree"],
                        Partitioned = dr["src_partitioned"] is DBNull ? null : (string)dr["src_partitioned"],
                        IOTType = dr["src_iot_type"] is DBNull ? null : (string)dr["src_iot_type"],
                        Temporary = dr["src_tab_temporary"] is DBNull ? null : (string)dr["src_tab_temporary"],
                        Nested = dr["src_nested"] is DBNull ? null : (string)dr["src_nested"],
                        Duration = dr["src_duration"] is DBNull ? null : (string)dr["src_duration"],
                        ClusterOwner = dr["src_cluster_owner"] is DBNull ? null : (string)dr["src_cluster_owner"],
                        Compression = dr["src_compression"] is DBNull ? null : (string)dr["src_compression"],
                        CompressFor = dr["src_compress_for"] is DBNull ? null : (string)dr["src_compress_for"],
                        Dropped = dr["src_dropped"] is DBNull ? null : (string)dr["src_dropped"],
                        ReadOnly = dr["src_read_only"] is DBNull ? null : (string)dr["src_read_only"],
                        Clustering = dr["src_clustering"] is DBNull ? null : (string)dr["src_clustering"],
                        HasIdentity = dr["src_has_identity"] is DBNull ? null : (string)dr["src_has_identity"],
                        ContainerData = dr["src_container_data"] is DBNull ? null : (string)dr["src_container_data"],
                        DefaultCollation = dr["src_default_collation"] is DBNull ? null : (string)dr["src_default_collation"],
                        External = dr["src_tab_external"] is DBNull ? null : (string)dr["src_tab_external"],
                    };
                    var targetTable = new Table
                    {
                        TableName = (string)dr["table_name"],
                        TablespaceName = dr["tgt_tablespace_name"] is DBNull ? null : (string)dr["tgt_tablespace_name"],
                        ClusterName = dr["tgt_cluster_name"] is DBNull ? null : (string)dr["tgt_cluster_name"],
                        IOTName = dr["tgt_iot_name"] is DBNull ? null : (string)dr["tgt_iot_name"],
                        Status = dr["tgt_status"] is DBNull ? null : (string)dr["tgt_status"],
                        Logging = dr["tgt_logging"] is DBNull ? null : (string)dr["tgt_logging"],
                        Degree = dr["tgt_degree"] is DBNull ? null : (string)dr["tgt_degree"],
                        Partitioned = dr["tgt_partitioned"] is DBNull ? null : (string)dr["tgt_partitioned"],
                        IOTType = dr["tgt_iot_type"] is DBNull ? null : (string)dr["tgt_iot_type"],
                        Temporary = dr["tgt_tab_temporary"] is DBNull ? null : (string)dr["tgt_tab_temporary"],
                        Nested = dr["tgt_nested"] is DBNull ? null : (string)dr["tgt_nested"],
                        Duration = dr["tgt_duration"] is DBNull ? null : (string)dr["tgt_duration"],
                        ClusterOwner = dr["tgt_cluster_owner"] is DBNull ? null : (string)dr["tgt_cluster_owner"],
                        Compression = dr["tgt_compression"] is DBNull ? null : (string)dr["tgt_compression"],
                        CompressFor = dr["tgt_compress_for"] is DBNull ? null : (string)dr["tgt_compress_for"],
                        Dropped = dr["tgt_dropped"] is DBNull ? null : (string)dr["tgt_dropped"],
                        ReadOnly = dr["tgt_read_only"] is DBNull ? null : (string)dr["tgt_read_only"],
                        Clustering = dr["tgt_clustering"] is DBNull ? null : (string)dr["tgt_clustering"],
                        HasIdentity = dr["tgt_has_identity"] is DBNull ? null : (string)dr["tgt_has_identity"],
                        ContainerData = dr["tgt_container_data"] is DBNull ? null : (string)dr["tgt_container_data"],
                        DefaultCollation = dr["tgt_default_collation"] is DBNull ? null : (string)dr["tgt_default_collation"],
                        External = dr["tgt_tab_external"] is DBNull ? null : (string)dr["tgt_tab_external"],
                    };
                    sourceTable.Compare(targetTable, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
