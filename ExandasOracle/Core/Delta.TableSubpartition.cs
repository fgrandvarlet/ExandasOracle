using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;
using ExandasOracle.Dao;

namespace ExandasOracle.Core
{
    public partial class Delta
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="list"></param>
        private void DeltaTableSubpartition(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name, s.partition_name, s.subpartition_name FROM src_tab_subpartitions s" +
                " LEFT JOIN tgt_tab_subpartitions t USING(table_name, partition_name, subpartition_name)" +
                " JOIN common_tab_partitions USING(table_name, partition_name)" +
                " WHERE t.table_name IS NULL" +
                " ORDER BY table_name, partition_name, subpartition_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var parentObject = string.Format("{0}.{1}", (string)dr["table_name"], (string)dr["partition_name"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "TABLE SUBPARTITION", (string)dr["subpartition_name"], parentObject, LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.table_name, t.partition_name, t.subpartition_name FROM tgt_tab_subpartitions t" +
                " LEFT JOIN src_tab_subpartitions s USING(table_name, partition_name, subpartition_name)" +
                " JOIN common_tab_partitions USING(table_name, partition_name)" +
                " WHERE s.table_name IS NULL" +
                " ORDER BY table_name, partition_name, subpartition_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var parentObject = string.Format("{0}.{1}", (string)dr["table_name"], (string)dr["partition_name"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "TABLE SUBPARTITION", (string)dr["subpartition_name"], parentObject, LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_tab_subpartitions";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceTableSubpartition = new TableSubpartition
                    {
                        TableName = (string)dr["table_name"],
                        PartitionName = (string)dr["partition_name"],
                        SubpartitionName = (string)dr["subpartition_name"],
                        HighValue = dr["src_high_value"] is DBNull ? null : (string)dr["src_high_value"],
                        HighValueLength = dr["src_high_value_length"] is DBNull ? null : (int?)dr["src_high_value_length"],
                        PartitionPosition = dr["src_partition_position"] is DBNull ? null : (int?)dr["src_partition_position"],
                        SubpartitionPosition = dr["src_subpartition_position"] is DBNull ? null : (int?)dr["src_subpartition_position"],
                        TablespaceName = dr["src_tablespace_name"] is DBNull ? null : (string)dr["src_tablespace_name"],
                        Logging = dr["src_logging"] is DBNull ? null : (string)dr["src_logging"],
                        Compression = dr["src_compression"] is DBNull ? null : (string)dr["src_compression"],
                        CompressFor = dr["src_compress_for"] is DBNull ? null : (string)dr["src_compress_for"],
                        Interval = dr["src_interval"] is DBNull ? null : (string)dr["src_interval"],
                        Indexing = dr["src_indexing"] is DBNull ? null : (string)dr["src_indexing"],
                        ReadOnly = dr["src_read_only"] is DBNull ? null : (string)dr["src_read_only"],
                    };
                    var targetTableSubpartition = new TableSubpartition
                    {
                        TableName = (string)dr["table_name"],
                        PartitionName = (string)dr["partition_name"],
                        SubpartitionName = (string)dr["subpartition_name"],
                        HighValue = dr["tgt_high_value"] is DBNull ? null : (string)dr["tgt_high_value"],
                        HighValueLength = dr["tgt_high_value_length"] is DBNull ? null : (int?)dr["tgt_high_value_length"],
                        PartitionPosition = dr["tgt_partition_position"] is DBNull ? null : (int?)dr["tgt_partition_position"],
                        SubpartitionPosition = dr["tgt_subpartition_position"] is DBNull ? null : (int?)dr["tgt_subpartition_position"],
                        TablespaceName = dr["tgt_tablespace_name"] is DBNull ? null : (string)dr["tgt_tablespace_name"],
                        Logging = dr["tgt_logging"] is DBNull ? null : (string)dr["tgt_logging"],
                        Compression = dr["tgt_compression"] is DBNull ? null : (string)dr["tgt_compression"],
                        CompressFor = dr["tgt_compress_for"] is DBNull ? null : (string)dr["tgt_compress_for"],
                        Interval = dr["tgt_interval"] is DBNull ? null : (string)dr["tgt_interval"],
                        Indexing = dr["tgt_indexing"] is DBNull ? null : (string)dr["tgt_indexing"],
                        ReadOnly = dr["tgt_read_only"] is DBNull ? null : (string)dr["tgt_read_only"],
                    };
                    sourceTableSubpartition.Compare(targetTableSubpartition, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
