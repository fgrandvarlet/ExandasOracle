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
		private void DeltaColumnComment(FbConnection conn, List<DeltaReport> list)
		{
			string sql;
			FbCommand cmd;

			// phase 1 : source minus target
			sql = "SELECT s.table_name, s.column_name FROM src_col_comments s" +
				" LEFT JOIN tgt_col_comments t USING(table_name, column_name)" +
				" JOIN common_tables_views USING(table_name)" +
				" WHERE t.table_name IS NULL" +
				" ORDER BY table_name, column_name";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var report = new DeltaReport(this._comparisonSet.Uid, "COLUMN_COMMENT", (string)dr["column_name"], (string)dr["table_name"], Strings.ObjectInSource);
					list.Add(report);
				}
			}

			// phase 2 : target minus source
			sql = "SELECT t.table_name, t.column_name FROM tgt_col_comments t" +
				" LEFT JOIN src_col_comments s USING(table_name, column_name)" +
				" JOIN common_tables_views USING(table_name)" +
				" WHERE s.table_name IS NULL" +
				" ORDER BY table_name, column_name";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var report = new DeltaReport(this._comparisonSet.Uid, "COLUMN_COMMENT", (string)dr["column_name"], (string)dr["table_name"], Strings.ObjectInTarget);
					list.Add(report);
				}
			}

			// phase 3 : property differences between source and target
			sql = "SELECT * FROM comp_col_comments";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var sourceColumnComment = new ColumnComment
					{
						TableName = (string)dr["table_name"],
						ColumnName = (string)dr["column_name"],
						Comments = dr["src_comments"] is DBNull ? null : (string)dr["src_comments"],
					};
					var targetColumnComment = new ColumnComment
					{
						TableName = (string)dr["table_name"],
						ColumnName = (string)dr["column_name"],
						Comments = dr["tgt_comments"] is DBNull ? null : (string)dr["tgt_comments"],
					};
					sourceColumnComment.Compare(targetColumnComment, this._comparisonSet.Uid, list);
				}
			}
		}

	}
}
