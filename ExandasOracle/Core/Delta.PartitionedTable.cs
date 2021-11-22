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
        private void DeltaPartitionedTable(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // property differences between source and target
            sql = "SELECT * FROM comp_part_tables";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourcePartitionedTable = new PartitionedTable
                    {
                        TableName = (string)dr["table_name"],
                        PartitioningType = dr["src_partitioning_type"] is DBNull ? null : (string)dr["src_partitioning_type"],
                        SubpartitioningType = dr["src_subpartitioning_type"] is DBNull ? null : (string)dr["src_subpartitioning_type"],
                        PartitionCount = (int)dr["src_partition_count"],
                        DefSubpartitionCount = dr["src_def_subpartition_count"] is DBNull ? null : (int?)dr["src_def_subpartition_count"],
                        PartitioningKeyCount = (int)dr["src_partitioning_key_count"],
                        SubpartitioningKeyCount = dr["src_subpartitioning_key_count"] is DBNull ? null : (int?)dr["src_subpartitioning_key_count"],
                        Status = dr["src_status"] is DBNull ? null : (string)dr["src_status"],
                        DefTablespaceName = dr["src_def_tablespace_name"] is DBNull ? null : (string)dr["src_def_tablespace_name"],
                        DefLogging = dr["src_def_logging"] is DBNull ? null : (string)dr["src_def_logging"],
                        DefCompression = dr["src_def_compression"] is DBNull ? null : (string)dr["src_def_compression"],
                        DefCompressFor = dr["src_def_compress_for"] is DBNull ? null : (string)dr["src_def_compress_for"],
                        RefPtnConstraintName = dr["src_ref_ptn_constraint_name"] is DBNull ? null : (string)dr["src_ref_ptn_constraint_name"],
                        Interval = dr["src_interval"] is DBNull ? null : (string)dr["src_interval"],
                        Autolist = dr["src_autolist"] is DBNull ? null : (string)dr["src_autolist"],
                        IntervalSubpartition = dr["src_interval_subpartition"] is DBNull ? null : (string)dr["src_interval_subpartition"],
                        AutolistSubpartition = dr["src_autolist_subpartition"] is DBNull ? null : (string)dr["src_autolist_subpartition"],
                        IsNested = dr["src_is_nested"] is DBNull ? null : (string)dr["src_is_nested"],
                        DefIndexing = dr["src_def_indexing"] is DBNull ? null : (string)dr["src_def_indexing"],
                        DefReadOnly = dr["src_def_read_only"] is DBNull ? null : (string)dr["src_def_read_only"],
                    };
                    var targetPartitionedTable = new PartitionedTable
                    {
                        TableName = (string)dr["table_name"],
                        PartitioningType = dr["tgt_partitioning_type"] is DBNull ? null : (string)dr["tgt_partitioning_type"],
                        SubpartitioningType = dr["tgt_subpartitioning_type"] is DBNull ? null : (string)dr["tgt_subpartitioning_type"],
                        PartitionCount = (int)dr["tgt_partition_count"],
                        DefSubpartitionCount = dr["tgt_def_subpartition_count"] is DBNull ? null : (int?)dr["tgt_def_subpartition_count"],
                        PartitioningKeyCount = (int)dr["tgt_partitioning_key_count"],
                        SubpartitioningKeyCount = dr["tgt_subpartitioning_key_count"] is DBNull ? null : (int?)dr["tgt_subpartitioning_key_count"],
                        Status = dr["tgt_status"] is DBNull ? null : (string)dr["tgt_status"],
                        DefTablespaceName = dr["tgt_def_tablespace_name"] is DBNull ? null : (string)dr["tgt_def_tablespace_name"],
                        DefLogging = dr["tgt_def_logging"] is DBNull ? null : (string)dr["tgt_def_logging"],
                        DefCompression = dr["tgt_def_compression"] is DBNull ? null : (string)dr["tgt_def_compression"],
                        DefCompressFor = dr["tgt_def_compress_for"] is DBNull ? null : (string)dr["tgt_def_compress_for"],
                        RefPtnConstraintName = dr["tgt_ref_ptn_constraint_name"] is DBNull ? null : (string)dr["tgt_ref_ptn_constraint_name"],
                        Interval = dr["tgt_interval"] is DBNull ? null : (string)dr["tgt_interval"],
                        Autolist = dr["tgt_autolist"] is DBNull ? null : (string)dr["tgt_autolist"],
                        IntervalSubpartition = dr["tgt_interval_subpartition"] is DBNull ? null : (string)dr["tgt_interval_subpartition"],
                        AutolistSubpartition = dr["tgt_autolist_subpartition"] is DBNull ? null : (string)dr["tgt_autolist_subpartition"],
                        IsNested = dr["tgt_is_nested"] is DBNull ? null : (string)dr["tgt_is_nested"],
                        DefIndexing = dr["tgt_def_indexing"] is DBNull ? null : (string)dr["tgt_def_indexing"],
                        DefReadOnly = dr["tgt_def_read_only"] is DBNull ? null : (string)dr["tgt_def_read_only"],
                    };
                    sourcePartitionedTable.Compare(targetPartitionedTable, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
