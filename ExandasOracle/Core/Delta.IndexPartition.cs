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
        private void DeltaIndexPartition(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.index_name, s.partition_name FROM src_ind_partitions s" +
                " LEFT JOIN tgt_ind_partitions t USING(index_name, partition_name)" +
                " JOIN common_table_indexes USING(index_name)" +
                " WHERE t.index_name IS NULL " +
                " ORDER BY index_name, partition_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "INDEX PARTITION", (string)dr["partition_name"], (string)dr["index_name"], Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.index_name, t.partition_name FROM tgt_ind_partitions t" +
                " LEFT JOIN src_ind_partitions s USING(index_name, partition_name)" +
                " JOIN common_table_indexes USING(index_name)" +
                " WHERE s.index_name IS NULL " +
                " ORDER BY index_name, partition_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "INDEX PARTITION", (string)dr["partition_name"], (string)dr["index_name"], Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_ind_partitions";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceIndexPartition = new IndexPartition
                    {
                        IndexName = (string)dr["index_name"],
                        Composite = dr["src_composite"] is DBNull ? null : (string)dr["src_composite"],
                        PartitionName = (string)dr["partition_name"],
                        SubpartitionCount = dr["src_subpartition_count"] is DBNull ? null : (decimal?)dr["src_subpartition_count"],
                        HighValue = dr["src_high_value"] is DBNull ? null : (string)dr["src_high_value"],
                        HighValueLength = dr["src_high_value_length"] is DBNull ? null : (decimal?)dr["src_high_value_length"],
                        PartitionPosition = dr["src_partition_position"] is DBNull ? null : (decimal?)dr["src_partition_position"],
                        Status = dr["src_status"] is DBNull ? null : (string)dr["src_status"],
                        TablespaceName = dr["src_tablespace_name"] is DBNull ? null : (string)dr["src_tablespace_name"],
                        Logging = dr["src_logging"] is DBNull ? null : (string)dr["src_logging"],
                        Compression = dr["src_compression"] is DBNull ? null : (string)dr["src_compression"],
                        Parameters = dr["src_parameters"] is DBNull ? null : (string)dr["src_parameters"],
                        Interval = dr["src_interval"] is DBNull ? null : (string)dr["src_interval"],
                    };
                    var targetIndexPartition = new IndexPartition
                    {
                        IndexName = (string)dr["index_name"],
                        Composite = dr["tgt_composite"] is DBNull ? null : (string)dr["tgt_composite"],
                        PartitionName = (string)dr["partition_name"],
                        SubpartitionCount = dr["tgt_subpartition_count"] is DBNull ? null : (decimal?)dr["tgt_subpartition_count"],
                        HighValue = dr["tgt_high_value"] is DBNull ? null : (string)dr["tgt_high_value"],
                        HighValueLength = dr["tgt_high_value_length"] is DBNull ? null : (decimal?)dr["tgt_high_value_length"],
                        PartitionPosition = dr["tgt_partition_position"] is DBNull ? null : (decimal?)dr["tgt_partition_position"],
                        Status = dr["tgt_status"] is DBNull ? null : (string)dr["tgt_status"],
                        TablespaceName = dr["tgt_tablespace_name"] is DBNull ? null : (string)dr["tgt_tablespace_name"],
                        Logging = dr["tgt_logging"] is DBNull ? null : (string)dr["tgt_logging"],
                        Compression = dr["tgt_compression"] is DBNull ? null : (string)dr["tgt_compression"],
                        Parameters = dr["tgt_parameters"] is DBNull ? null : (string)dr["tgt_parameters"],
                        Interval = dr["tgt_interval"] is DBNull ? null : (string)dr["tgt_interval"],
                    };
                    sourceIndexPartition.Compare(targetIndexPartition, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
