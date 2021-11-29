using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao.Firebird
{
    public class DeltaReportDaoFirebird : AbstractDaoFirebird, IDeltaReportDao
    {
        public DeltaReportDaoFirebird(string connectionString) : base(connectionString)
        {
        }

        protected override FbCommand CreateCommand(Criteria criteria)
        {
            var comparisonSet = (ComparisonSet)criteria.Entity;
            var filterStatements = DaoFactory.Instance.GetFilterSettingDao().GetFilteringWhereClause(comparisonSet.Uid);
            // TODO A FINALISER

            System.Windows.Forms.MessageBox.Show(filterStatements);

            string sql;
            const string ROOT_SELECT = "SELECT id, entity, object, parent_object, label, property, source, target" +
                " FROM delta_report WHERE comparison_set_uid = @comparison_set_uid" +
                " {0} {1} ORDER BY id";

            var cmd = new FbCommand();

            if (criteria.HasText)
            {
                const string WHERE_CLAUSE = "AND (upper(entity) LIKE @pattern OR upper(object) LIKE @pattern OR upper(parent_object) LIKE @pattern OR upper(label) LIKE @pattern" +
                    " OR upper(property) LIKE @pattern)";

                sql = String.Format(ROOT_SELECT, filterStatements, WHERE_CLAUSE);
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("comparison_set_uid", comparisonSet.Uid);
                cmd.Parameters.AddWithValue("pattern", criteria.Pattern.ToUpper());
            }
            else
            {
                sql = String.Format(ROOT_SELECT, filterStatements, string.Empty);
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("comparison_set_uid", comparisonSet.Uid);
            }

            return cmd;
        }

        public void LoadDeltaReportList(FbConnection conn, Guid comparisonSetUid, List<DeltaReport> list)
        {
            FbTransaction tran = conn.BeginTransaction();
            try
            {
                var nowDateTime = DateTime.Now;

                // phase 1 : suppression préalable des enregistrements correspondant au comparisonSetUid
                const string sql = "DELETE FROM delta_report WHERE comparison_set_uid = @comparison_set_uid";
                var cmd = new FbCommand(sql, conn, tran);
                cmd.Parameters.AddWithValue("comparison_set_uid", comparisonSetUid);
                cmd.ExecuteNonQuery();

                // phase 2 : ajout des enregistrements de la liste
                const string sqlInsert = "INSERT INTO delta_report(comparison_set_uid, entity, object, parent_object, label_id, label, property, source, target)" +
                    "VALUES(@comparison_set_uid, @entity, @object, @parent_object, @label_id, @label, @property, @source, @target)";
                var cmdInsert = new FbCommand(sqlInsert, conn, tran);
                cmdInsert.Prepare();

                foreach (var dr in list)
                {
                    cmdInsert.Parameters.Clear();
                    cmdInsert.Parameters.AddWithValue("comparison_set_uid", dr.ComparisonSetUid);
                    cmdInsert.Parameters.AddWithValue("entity", dr.Entity);
                    cmdInsert.Parameters.AddWithValue("object", dr.ObjectValue);
                    cmdInsert.Parameters.AddWithValue("parent_object", dr.ParentObject);
                    cmdInsert.Parameters.AddWithValue("label_id", dr.LabelId);
                    cmdInsert.Parameters.AddWithValue("label", dr.Label);
                    cmdInsert.Parameters.AddWithValue("property", dr.Property);
                    cmdInsert.Parameters.AddWithValue("source", dr.Source);
                    cmdInsert.Parameters.AddWithValue("target", dr.Target);
                    cmdInsert.ExecuteNonQuery();
                }

                // phase 3 : mise à jour du ComparisonSet colonne last_report_time
                const string sqlUpdate = "UPDATE comparison_set SET last_report_time = @last_report_time WHERE uid = @uid";
                var cmdUpdate = new FbCommand(sqlUpdate, conn, tran);
                cmdUpdate.Parameters.AddWithValue("last_report_time", nowDateTime);
                cmdUpdate.Parameters.AddWithValue("uid", comparisonSetUid);
                cmdUpdate.ExecuteNonQuery();

                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                throw;
            }
        }

    }
}
