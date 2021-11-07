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

			// property differences between source and target
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
