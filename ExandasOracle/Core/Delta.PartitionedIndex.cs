using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

namespace ExandasOracle.Core
{
    public partial class Delta
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="list"></param>
        private void DeltaPartitionedIndex(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // property differences between source and target
            sql = "SELECT * FROM comp_part_indexes";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourcePartitionedIndex = new PartitionedIndex
                    {
                        IndexName = (string)dr["index_name"],
                        TableName = (string)dr["table_name"],
                        PartitioningType = dr["src_partitioning_type"] is DBNull ? null : (string)dr["src_partitioning_type"],
                        SubpartitioningType = dr["src_subpartitioning_type"] is DBNull ? null : (string)dr["src_subpartitioning_type"],
                        PartitionCount = (decimal)dr["src_partition_count"],
                        DefSubpartitionCount = dr["src_def_subpartition_count"] is DBNull ? null : (decimal?)dr["src_def_subpartition_count"],
                        PartitioningKeyCount = (decimal)dr["src_partitioning_key_count"],
                        SubpartitioningKeyCount = dr["src_subpartitioning_key_count"] is DBNull ? null : (decimal?)dr["src_subpartitioning_key_count"],
                        Locality = dr["src_locality"] is DBNull ? null : (string)dr["src_locality"],
                        Alignment = dr["src_alignment"] is DBNull ? null : (string)dr["src_alignment"],
                        DefTablespaceName = dr["src_def_tablespace_name"] is DBNull ? null : (string)dr["src_def_tablespace_name"],
                        DefLogging = dr["src_def_logging"] is DBNull ? null : (string)dr["src_def_logging"],
                        DefParameters = dr["src_def_parameters"] is DBNull ? null : (string)dr["src_def_parameters"],
                        Interval = dr["src_interval"] is DBNull ? null : (string)dr["src_interval"],
                        Autolist = dr["src_autolist"] is DBNull ? null : (string)dr["src_autolist"],
                        IntervalSubpartition = dr["src_interval_subpartition"] is DBNull ? null : (string)dr["src_interval_subpartition"],
                        AutolistSubpartition = dr["src_autolist_subpartition"] is DBNull ? null : (string)dr["src_autolist_subpartition"],
                    };
                    var targetPartitionedIndex = new PartitionedIndex
                    {
                        IndexName = (string)dr["index_name"],
                        TableName = (string)dr["table_name"],
                        PartitioningType = dr["tgt_partitioning_type"] is DBNull ? null : (string)dr["tgt_partitioning_type"],
                        SubpartitioningType = dr["tgt_subpartitioning_type"] is DBNull ? null : (string)dr["tgt_subpartitioning_type"],
                        PartitionCount = (decimal)dr["tgt_partition_count"],
                        DefSubpartitionCount = dr["tgt_def_subpartition_count"] is DBNull ? null : (decimal?)dr["tgt_def_subpartition_count"],
                        PartitioningKeyCount = (decimal)dr["tgt_partitioning_key_count"],
                        SubpartitioningKeyCount = dr["tgt_subpartitioning_key_count"] is DBNull ? null : (decimal?)dr["tgt_subpartitioning_key_count"],
                        Locality = dr["tgt_locality"] is DBNull ? null : (string)dr["tgt_locality"],
                        Alignment = dr["tgt_alignment"] is DBNull ? null : (string)dr["tgt_alignment"],
                        DefTablespaceName = dr["tgt_def_tablespace_name"] is DBNull ? null : (string)dr["tgt_def_tablespace_name"],
                        DefLogging = dr["tgt_def_logging"] is DBNull ? null : (string)dr["tgt_def_logging"],
                        DefParameters = dr["tgt_def_parameters"] is DBNull ? null : (string)dr["tgt_def_parameters"],
                        Interval = dr["tgt_interval"] is DBNull ? null : (string)dr["tgt_interval"],
                        Autolist = dr["tgt_autolist"] is DBNull ? null : (string)dr["tgt_autolist"],
                        IntervalSubpartition = dr["tgt_interval_subpartition"] is DBNull ? null : (string)dr["tgt_interval_subpartition"],
                        AutolistSubpartition = dr["tgt_autolist_subpartition"] is DBNull ? null : (string)dr["tgt_autolist_subpartition"],
                    };
                    sourcePartitionedIndex.Compare(targetPartitionedIndex, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
