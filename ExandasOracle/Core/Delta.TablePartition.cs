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
        private void DeltaTablePartition(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name, s.partition_name FROM src_tab_partitions s" +
                " LEFT JOIN tgt_tab_partitions t USING(table_name, partition_name)" +
                " JOIN common_tables USING(table_name)" +
                " WHERE t.table_name IS NULL " +
                " ORDER BY table_name, partition_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "TABLE PARTITION", (string)dr["partition_name"], (string)dr["table_name"], Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.table_name, t.partition_name FROM tgt_tab_partitions t" +
                " LEFT JOIN src_tab_partitions s USING(table_name, partition_name)" +
                " JOIN common_tables USING(table_name)" +
                " WHERE s.table_name IS NULL " +
                " ORDER BY table_name, partition_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "TABLE PARTITION", (string)dr["partition_name"], (string)dr["table_name"], Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_tab_partitions";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceTablePartition = new TablePartition
                    {
                        TableName = (string)dr["table_name"],
                        Composite = dr["src_composite"] is DBNull ? null : (string)dr["src_composite"],
                        PartitionName = (string)dr["partition_name"],
                        SubpartitionCount = dr["src_subpartition_count"] is DBNull ? null : (int?)dr["src_subpartition_count"],
                        HighValue = dr["src_high_value"] is DBNull ? null : (string)dr["src_high_value"],
                        HighValueLength = dr["src_high_value_length"] is DBNull ? null : (int?)dr["src_high_value_length"],
                        PartitionPosition = dr["src_partition_position"] is DBNull ? null : (int?)dr["src_partition_position"],
                        TablespaceName = dr["src_tablespace_name"] is DBNull ? null : (string)dr["src_tablespace_name"],
                        Logging = dr["src_logging"] is DBNull ? null : (string)dr["src_logging"],
                        Compression = dr["src_compression"] is DBNull ? null : (string)dr["src_compression"],
                        CompressFor = dr["src_compress_for"] is DBNull ? null : (string)dr["src_compress_for"],
                        IsNested = dr["src_is_nested"] is DBNull ? null : (string)dr["src_is_nested"],
                        ParentTablePartition = dr["src_parent_table_partition"] is DBNull ? null : (string)dr["src_parent_table_partition"],
                        Interval = dr["src_interval"] is DBNull ? null : (string)dr["src_interval"],
                        Indexing = dr["src_indexing"] is DBNull ? null : (string)dr["src_indexing"],
                        ReadOnly = dr["src_read_only"] is DBNull ? null : (string)dr["src_read_only"],
                    };
                    var targetTablePartition = new TablePartition
                    {
                        TableName = (string)dr["table_name"],
                        Composite = dr["tgt_composite"] is DBNull ? null : (string)dr["tgt_composite"],
                        PartitionName = (string)dr["partition_name"],
                        SubpartitionCount = dr["tgt_subpartition_count"] is DBNull ? null : (int?)dr["tgt_subpartition_count"],
                        HighValue = dr["tgt_high_value"] is DBNull ? null : (string)dr["tgt_high_value"],
                        HighValueLength = dr["tgt_high_value_length"] is DBNull ? null : (int?)dr["tgt_high_value_length"],
                        PartitionPosition = dr["tgt_partition_position"] is DBNull ? null : (int?)dr["tgt_partition_position"],
                        TablespaceName = dr["tgt_tablespace_name"] is DBNull ? null : (string)dr["tgt_tablespace_name"],
                        Logging = dr["tgt_logging"] is DBNull ? null : (string)dr["tgt_logging"],
                        Compression = dr["tgt_compression"] is DBNull ? null : (string)dr["tgt_compression"],
                        CompressFor = dr["tgt_compress_for"] is DBNull ? null : (string)dr["tgt_compress_for"],
                        IsNested = dr["tgt_is_nested"] is DBNull ? null : (string)dr["tgt_is_nested"],
                        ParentTablePartition = dr["tgt_parent_table_partition"] is DBNull ? null : (string)dr["tgt_parent_table_partition"],
                        Interval = dr["tgt_interval"] is DBNull ? null : (string)dr["tgt_interval"],
                        Indexing = dr["tgt_indexing"] is DBNull ? null : (string)dr["tgt_indexing"],
                        ReadOnly = dr["tgt_read_only"] is DBNull ? null : (string)dr["tgt_read_only"],
                    };
                    sourceTablePartition.Compare(targetTablePartition, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
