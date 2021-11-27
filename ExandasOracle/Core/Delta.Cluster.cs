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
        private void DeltaCluster(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.cluster_name FROM src_clusters s" +
                " LEFT JOIN tgt_clusters t USING(cluster_name)" +
                " WHERE t.cluster_name IS NULL" +
                " ORDER BY cluster_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "CLUSTER", (string)dr["cluster_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.cluster_name FROM tgt_clusters t" +
                " LEFT JOIN src_clusters s USING(cluster_name)" +
                " WHERE s.cluster_name IS NULL" +
                " ORDER BY cluster_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "CLUSTER", (string)dr["cluster_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_clusters";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceCluster = new Cluster
                    {
                        ClusterName = (string)dr["cluster_name"],
                        TablespaceName = (string)dr["src_tablespace_name"],
                        ClusterType = dr["src_cluster_type"] is DBNull ? null : (string)dr["src_cluster_type"],
                        Function = dr["src_clu_function"] is DBNull ? null : (string)dr["src_clu_function"],
                        Hashkeys = dr["src_hashkeys"] is DBNull ? null : (int?)dr["src_hashkeys"],
                        Degree = dr["src_degree"] is DBNull ? null : (string)dr["src_degree"],
                        Cache = dr["src_cache"] is DBNull ? null : (string)dr["src_cache"],
                        SingleTable = dr["src_single_table"] is DBNull ? null : (string)dr["src_single_table"],
                        Dependencies = dr["src_dependencies"] is DBNull ? null : (string)dr["src_dependencies"],
                    };
                    var targetCluster = new Cluster
                    {
                        ClusterName = (string)dr["cluster_name"],
                        TablespaceName = (string)dr["tgt_tablespace_name"],
                        ClusterType = dr["tgt_cluster_type"] is DBNull ? null : (string)dr["tgt_cluster_type"],
                        Function = dr["tgt_clu_function"] is DBNull ? null : (string)dr["tgt_clu_function"],
                        Hashkeys = dr["tgt_hashkeys"] is DBNull ? null : (int?)dr["tgt_hashkeys"],
                        Degree = dr["tgt_degree"] is DBNull ? null : (string)dr["tgt_degree"],
                        Cache = dr["tgt_cache"] is DBNull ? null : (string)dr["tgt_cache"],
                        SingleTable = dr["tgt_single_table"] is DBNull ? null : (string)dr["tgt_single_table"],
                        Dependencies = dr["tgt_dependencies"] is DBNull ? null : (string)dr["tgt_dependencies"],
                    };
                    sourceCluster.Compare(targetCluster, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
