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
        private void DeltaUnique(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name, s.constraint_name FROM src_uniques s" +
                " LEFT JOIN tgt_uniques t USING(table_name, constraint_name)" +
                " JOIN common_tables USING(table_name)" +
                " WHERE t.table_name IS NULL " +
                " ORDER BY table_name, constraint_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "UNIQUE", (string)dr["constraint_name"], (string)dr["table_name"], Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.table_name, t.constraint_name FROM tgt_uniques t" +
                " LEFT JOIN src_uniques s USING(table_name, constraint_name)" +
                " JOIN common_tables USING(table_name)" +
                " WHERE s.table_name IS NULL " +
                " ORDER BY table_name, constraint_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "UNIQUE", (string)dr["constraint_name"], (string)dr["table_name"], Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_uniques";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceUnique = new Unique
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Status = dr["src_status"] is DBNull ? null : (string)dr["src_status"],
                        Deferrable = dr["src_deferrable"] is DBNull ? null : (string)dr["src_deferrable"],
                        Deferred = dr["src_deferred"] is DBNull ? null : (string)dr["src_deferred"],
                        Validated = dr["src_validated"] is DBNull ? null : (string)dr["src_validated"],
                        Rely = dr["src_rely"] is DBNull ? null : (string)dr["src_rely"],
                        IndexOwner = dr["src_index_owner"] is DBNull ? null : (string)dr["src_index_owner"],
                        IndexName = dr["src_index_name"] is DBNull ? null : (string)dr["src_index_name"],
                        Invalid = dr["src_invalid"] is DBNull ? null : (string)dr["src_invalid"],
                        ViewRelated = dr["src_view_related"] is DBNull ? null : (string)dr["src_view_related"],
                    };
                    var targetUnique = new Unique
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        Status = dr["tgt_status"] is DBNull ? null : (string)dr["tgt_status"],
                        Deferrable = dr["tgt_deferrable"] is DBNull ? null : (string)dr["tgt_deferrable"],
                        Deferred = dr["tgt_deferred"] is DBNull ? null : (string)dr["tgt_deferred"],
                        Validated = dr["tgt_validated"] is DBNull ? null : (string)dr["tgt_validated"],
                        Rely = dr["tgt_rely"] is DBNull ? null : (string)dr["tgt_rely"],
                        IndexOwner = dr["tgt_index_owner"] is DBNull ? null : (string)dr["tgt_index_owner"],
                        IndexName = dr["tgt_index_name"] is DBNull ? null : (string)dr["tgt_index_name"],
                        Invalid = dr["tgt_invalid"] is DBNull ? null : (string)dr["tgt_invalid"],
                        ViewRelated = dr["tgt_view_related"] is DBNull ? null : (string)dr["tgt_view_related"],
                    };
                    sourceUnique.Compare(targetUnique, this._comparisonSet, list);
                }
            }
        }

    }
}
