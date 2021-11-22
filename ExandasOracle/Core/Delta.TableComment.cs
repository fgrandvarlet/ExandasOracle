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
		private void DeltaTableComment(FbConnection conn, List<DeltaReport> list)
		{
			string sql;
			FbCommand cmd;

			// property differences between source and target
			sql = "SELECT * FROM comp_tab_comments";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var sourceTableComment = new TableComment
					{
						TableName = (string)dr["table_name"],
						Comments = dr["src_comments"] is DBNull ? null : (string)dr["src_comments"],
					};
					var targetTableComment = new TableComment
					{
						TableName = (string)dr["table_name"],
						Comments = dr["tgt_comments"] is DBNull ? null : (string)dr["tgt_comments"],
					};
					sourceTableComment.Compare(targetTableComment, this._comparisonSet.Uid, list);
				}
			}
		}

	}
}
