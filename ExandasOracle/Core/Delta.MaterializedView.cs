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
        private void DeltaMaterializedView(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.mview_name FROM src_mviews s" +
                " LEFT JOIN tgt_mviews t USING(mview_name)" +
                " WHERE t.mview_name IS NULL" +
                " ORDER BY mview_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "MATERIALIZED VIEW", (string)dr["mview_name"], Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.mview_name FROM tgt_mviews t" +
                " LEFT JOIN src_mviews s USING(mview_name)" +
                " WHERE s.mview_name IS NULL" +
                " ORDER BY mview_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "MATERIALIZED VIEW", (string)dr["mview_name"], Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_mviews";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceMaterializedView = new MaterializedView
                    {
                        MViewName = (string)dr["mview_name"],
                        ContainerName = (string)dr["src_container_name"],
                        Query = dr["src_query"] is DBNull ? null : (string)dr["src_query"],
                        QueryLen = dr["src_query_len"] is DBNull ? null : (decimal?)dr["src_query_len"],
                        Updatable = dr["src_updatable"] is DBNull ? null : (string)dr["src_updatable"],
                        UpdateLog = dr["src_update_log"] is DBNull ? null : (string)dr["src_update_log"],
                        MasterRollbackSeg = dr["src_master_rollback_seg"] is DBNull ? null : (string)dr["src_master_rollback_seg"],
                        MasterLink = dr["src_master_link"] is DBNull ? null : (string)dr["src_master_link"],
                        RewriteEnabled = dr["src_rewrite_enabled"] is DBNull ? null : (string)dr["src_rewrite_enabled"],
                        RewriteCapability = dr["src_rewrite_capability"] is DBNull ? null : (string)dr["src_rewrite_capability"],
                        RefreshMode = dr["src_refresh_mode"] is DBNull ? null : (string)dr["src_refresh_mode"],
                        RefreshMethod = dr["src_refresh_method"] is DBNull ? null : (string)dr["src_refresh_method"],
                        BuildMode = dr["src_build_mode"] is DBNull ? null : (string)dr["src_build_mode"],
                        FastRefreshable = dr["src_fast_refreshable"] is DBNull ? null : (string)dr["src_fast_refreshable"],
                        UseNoIndex = dr["src_use_no_index"] is DBNull ? null : (string)dr["src_use_no_index"],
                        DefaultCollation = dr["src_default_collation"] is DBNull ? null : (string)dr["src_default_collation"],
                    };
                    var targetMaterializedView = new MaterializedView
                    {
                        MViewName = (string)dr["mview_name"],
                        ContainerName = (string)dr["tgt_container_name"],
                        Query = dr["tgt_query"] is DBNull ? null : (string)dr["tgt_query"],
                        QueryLen = dr["tgt_query_len"] is DBNull ? null : (decimal?)dr["tgt_query_len"],
                        Updatable = dr["tgt_updatable"] is DBNull ? null : (string)dr["tgt_updatable"],
                        UpdateLog = dr["tgt_update_log"] is DBNull ? null : (string)dr["tgt_update_log"],
                        MasterRollbackSeg = dr["tgt_master_rollback_seg"] is DBNull ? null : (string)dr["tgt_master_rollback_seg"],
                        MasterLink = dr["tgt_master_link"] is DBNull ? null : (string)dr["tgt_master_link"],
                        RewriteEnabled = dr["tgt_rewrite_enabled"] is DBNull ? null : (string)dr["tgt_rewrite_enabled"],
                        RewriteCapability = dr["tgt_rewrite_capability"] is DBNull ? null : (string)dr["tgt_rewrite_capability"],
                        RefreshMode = dr["tgt_refresh_mode"] is DBNull ? null : (string)dr["tgt_refresh_mode"],
                        RefreshMethod = dr["tgt_refresh_method"] is DBNull ? null : (string)dr["tgt_refresh_method"],
                        BuildMode = dr["tgt_build_mode"] is DBNull ? null : (string)dr["tgt_build_mode"],
                        FastRefreshable = dr["tgt_fast_refreshable"] is DBNull ? null : (string)dr["tgt_fast_refreshable"],
                        UseNoIndex = dr["tgt_use_no_index"] is DBNull ? null : (string)dr["tgt_use_no_index"],
                        DefaultCollation = dr["tgt_default_collation"] is DBNull ? null : (string)dr["tgt_default_collation"],
                    };
                    sourceMaterializedView.Compare(targetMaterializedView, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
