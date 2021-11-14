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
        private void DeltaObjectPrivilege(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // TODO ? vue common_objects ? union sur autres common_*

            // phase 1 : source minus target
            sql = "SELECT s.grantee, s.table_name, s.privilege, s.inherited FROM src_tab_privs s" +
                " LEFT JOIN tgt_tab_privs t USING(grantee, table_name, privilege, inherited)" +
                " WHERE t.grantee IS NULL" +
                " ORDER BY grantee, table_name, privilege, inherited";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var objectValue = string.Format("{0}/{1}@{2}", (string)dr["privilege"], (string)dr["table_name"], (string)dr["grantee"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "OBJECT PRIVILEGE", objectValue, Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.grantee, t.table_name, t.privilege, t.inherited FROM tgt_tab_privs t" +
                " LEFT JOIN src_tab_privs s USING(grantee, table_name, privilege, inherited)" +
                " WHERE s.grantee IS NULL" +
                " ORDER BY grantee, table_name, privilege, inherited";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var objectValue = string.Format("{0}/{1}@{2}", (string)dr["privilege"], (string)dr["table_name"], (string)dr["grantee"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "OBJECT PRIVILEGE", objectValue, Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_tab_privs";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceObjectPrivilege = new ObjectPrivilege
                    {
                        Grantee = (string)dr["grantee"],
                        TableSchema = (string)dr["src_table_schema"],
                        TableName = (string)dr["table_name"],
                        Privilege = (string)dr["privilege"],
                        Grantable = dr["src_grantable"] is DBNull ? null : (string)dr["src_grantable"],
                        Hierarchy = dr["src_hierarchy"] is DBNull ? null : (string)dr["src_hierarchy"],
                        Common = dr["src_common"] is DBNull ? null : (string)dr["src_common"],
                        Type = dr["src_type"] is DBNull ? null : (string)dr["src_type"],
                        Inherited = (string)dr["inherited"],
                    };
                    var targetObjectPrivilege = new ObjectPrivilege
                    {
                        Grantee = (string)dr["grantee"],
                        TableSchema = (string)dr["tgt_table_schema"],
                        TableName = (string)dr["table_name"],
                        Privilege = (string)dr["privilege"],
                        Grantable = dr["tgt_grantable"] is DBNull ? null : (string)dr["tgt_grantable"],
                        Hierarchy = dr["tgt_hierarchy"] is DBNull ? null : (string)dr["tgt_hierarchy"],
                        Common = dr["tgt_common"] is DBNull ? null : (string)dr["tgt_common"],
                        Type = dr["tgt_type"] is DBNull ? null : (string)dr["tgt_type"],
                        Inherited = (string)dr["inherited"],
                    };
                    sourceObjectPrivilege.Compare(targetObjectPrivilege, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
