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
        private void DeltaSourceSynthesis(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.source_name, s.source_type FROM src_source_synthesis s" +
                " LEFT JOIN tgt_source_synthesis t USING (source_name, source_type)" +
                " WHERE t.source_name IS NULL" +
                " ORDER BY source_name, source_type";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "SOURCE", (string)dr["source_name"], (string)dr["source_type"], Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.source_name, t.source_type FROM tgt_source_synthesis t" +
                " LEFT JOIN src_source_synthesis s USING (source_name, source_type)" +
                " WHERE s.source_name IS NULL" +
                " ORDER BY source_name, source_type";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "SOURCE", (string)dr["source_name"], (string)dr["source_type"], Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_source_synthesis";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceSourceSynthesis = new SourceSynthesis
                    {
                        Name = (string)dr["source_name"],
                        Type = (string)dr["source_type"],
                        Text = dr["src_text"] is DBNull ? null : (string)dr["src_text"]
                    };
                    var targetSourceSynthesis = new SourceSynthesis
                    {
                        Name = (string)dr["source_name"],
                        Type = (string)dr["source_type"],
                        Text = dr["tgt_text"] is DBNull ? null : (string)dr["tgt_text"]
                    };
                    sourceSourceSynthesis.Compare(targetSourceSynthesis, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
