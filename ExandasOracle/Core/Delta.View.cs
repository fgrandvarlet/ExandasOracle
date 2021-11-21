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
        private void DeltaView(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.view_name FROM src_views s" +
                " LEFT JOIN tgt_views t USING(view_name)" +
                " WHERE t.view_name IS NULL" +
                " ORDER BY view_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "VIEW", (string)dr["view_name"], Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.view_name FROM tgt_views t" +
                " LEFT JOIN src_views s USING(view_name)" +
                " WHERE s.view_name IS NULL" +
                " ORDER BY view_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "VIEW", (string)dr["view_name"], Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_views";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceView = new View
                    {
                        ViewName = (string)dr["view_name"],
                        TextLength = dr["src_text_length"] is DBNull ? null : (int?)dr["src_text_length"],
                        Text = dr["src_text"] is DBNull ? null : (string)dr["src_text"],
                        TextVC = dr["src_text_vc"] is DBNull ? null : (string)dr["src_text_vc"],
                        TypeText = dr["src_type_text"] is DBNull ? null : (string)dr["src_type_text"],
                        OidText = dr["src_oid_text"] is DBNull ? null : (string)dr["src_oid_text"],
                        ViewTypeOwner = dr["src_view_type_owner"] is DBNull ? null : (string)dr["src_view_type_owner"],
                        ViewType = dr["src_view_type"] is DBNull ? null : (string)dr["src_view_type"],
                        SuperviewName = dr["src_superview_name"] is DBNull ? null : (string)dr["src_superview_name"],
                        ReadOnly = dr["src_read_only"] is DBNull ? null : (string)dr["src_read_only"],
                        Bequeath = dr["src_bequeath"] is DBNull ? null : (string)dr["src_bequeath"],
                        DefaultCollation = dr["src_default_collation"] is DBNull ? null : (string)dr["src_default_collation"],
                    };
                    var targetView = new View
                    {
                        ViewName = (string)dr["view_name"],
                        TextLength = dr["tgt_text_length"] is DBNull ? null : (int?)dr["tgt_text_length"],
                        Text = dr["tgt_text"] is DBNull ? null : (string)dr["tgt_text"],
                        TextVC = dr["tgt_text_vc"] is DBNull ? null : (string)dr["tgt_text_vc"],
                        TypeText = dr["tgt_type_text"] is DBNull ? null : (string)dr["tgt_type_text"],
                        OidText = dr["tgt_oid_text"] is DBNull ? null : (string)dr["tgt_oid_text"],
                        ViewTypeOwner = dr["tgt_view_type_owner"] is DBNull ? null : (string)dr["tgt_view_type_owner"],
                        ViewType = dr["tgt_view_type"] is DBNull ? null : (string)dr["tgt_view_type"],
                        SuperviewName = dr["tgt_superview_name"] is DBNull ? null : (string)dr["tgt_superview_name"],
                        ReadOnly = dr["tgt_read_only"] is DBNull ? null : (string)dr["tgt_read_only"],
                        Bequeath = dr["tgt_bequeath"] is DBNull ? null : (string)dr["tgt_bequeath"],
                        DefaultCollation = dr["tgt_default_collation"] is DBNull ? null : (string)dr["tgt_default_collation"],
                    };
                    sourceView.Compare(targetView, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
