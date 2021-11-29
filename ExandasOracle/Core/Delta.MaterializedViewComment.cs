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
        private void DeltaMaterializedViewComment(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // property differences between source and target
            sql = "SELECT * FROM comp_mview_comments";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceMaterializedViewComment = new MaterializedViewComment
                    {
                        MViewName = (string)dr["mview_name"],
                        Comments = dr["src_comments"] is DBNull ? null : (string)dr["src_comments"],
                    };
                    var targetMaterializedViewComment = new MaterializedViewComment
                    {
                        MViewName = (string)dr["mview_name"],
                        Comments = dr["tgt_comments"] is DBNull ? null : (string)dr["tgt_comments"],
                    };
                    sourceMaterializedViewComment.Compare(targetMaterializedViewComment, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
