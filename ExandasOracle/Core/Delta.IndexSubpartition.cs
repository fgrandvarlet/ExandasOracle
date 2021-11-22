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
        private void DeltaIndexSubpartition(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.index_name, s.partition_name, s.subpartition_name FROM src_ind_subpartitions s" +
                " LEFT JOIN tgt_ind_subpartitions t USING(index_name, partition_name, subpartition_name)" +
                " JOIN common_ind_partitions USING(index_name, partition_name)" +
                " WHERE t.index_name IS NULL" +
                " ORDER BY index_name, partition_name, subpartition_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var parentObject = string.Format("{0}.{1}", (string)dr["index_name"], (string)dr["partition_name"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "INDEX SUBPARTITION", (string)dr["subpartition_name"], parentObject, Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.index_name, t.partition_name, t.subpartition_name FROM tgt_ind_subpartitions t" +
                " LEFT JOIN src_ind_subpartitions s USING(index_name, partition_name, subpartition_name)" +
                " JOIN common_ind_partitions USING(index_name, partition_name)" +
                " WHERE s.index_name IS NULL" +
                " ORDER BY index_name, partition_name, subpartition_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var parentObject = string.Format("{0}.{1}", (string)dr["index_name"], (string)dr["partition_name"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "INDEX SUBPARTITION", (string)dr["subpartition_name"], parentObject, Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_ind_subpartitions";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceIndexSubpartition = new IndexSubpartition
                    {
                        IndexName = (string)dr["index_name"],
                        PartitionName = (string)dr["partition_name"],
                        SubpartitionName = (string)dr["subpartition_name"],
                        HighValue = dr["src_high_value"] is DBNull ? null : (string)dr["src_high_value"],
                        HighValueLength = dr["src_high_value_length"] is DBNull ? null : (int?)dr["src_high_value_length"],
                        PartitionPosition = dr["src_partition_position"] is DBNull ? null : (int?)dr["src_partition_position"],
                        SubpartitionPosition = dr["src_subpartition_position"] is DBNull ? null : (int?)dr["src_subpartition_position"],
                        Status = dr["src_status"] is DBNull ? null : (string)dr["src_status"],
                        TablespaceName = dr["src_tablespace_name"] is DBNull ? null : (string)dr["src_tablespace_name"],
                        Logging = dr["src_logging"] is DBNull ? null : (string)dr["src_logging"],
                        Compression = dr["src_compression"] is DBNull ? null : (string)dr["src_compression"],
                        Parameters = dr["src_parameters"] is DBNull ? null : (string)dr["src_parameters"],
                        Interval = dr["src_interval"] is DBNull ? null : (string)dr["src_interval"],
                    };
                    var targetIndexSubpartition = new IndexSubpartition
                    {
                        IndexName = (string)dr["index_name"],
                        PartitionName = (string)dr["partition_name"],
                        SubpartitionName = (string)dr["subpartition_name"],
                        HighValue = dr["tgt_high_value"] is DBNull ? null : (string)dr["tgt_high_value"],
                        HighValueLength = dr["tgt_high_value_length"] is DBNull ? null : (int?)dr["tgt_high_value_length"],
                        PartitionPosition = dr["tgt_partition_position"] is DBNull ? null : (int?)dr["tgt_partition_position"],
                        SubpartitionPosition = dr["tgt_subpartition_position"] is DBNull ? null : (int?)dr["tgt_subpartition_position"],
                        Status = dr["tgt_status"] is DBNull ? null : (string)dr["tgt_status"],
                        TablespaceName = dr["tgt_tablespace_name"] is DBNull ? null : (string)dr["tgt_tablespace_name"],
                        Logging = dr["tgt_logging"] is DBNull ? null : (string)dr["tgt_logging"],
                        Compression = dr["tgt_compression"] is DBNull ? null : (string)dr["tgt_compression"],
                        Parameters = dr["tgt_parameters"] is DBNull ? null : (string)dr["tgt_parameters"],
                        Interval = dr["tgt_interval"] is DBNull ? null : (string)dr["tgt_interval"],
                    };
                    sourceIndexSubpartition.Compare(targetIndexSubpartition, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
