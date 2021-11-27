using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;
using ExandasOracle.Dao;

namespace ExandasOracle.Core
{
    public partial class Delta
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="list"></param>
		private void DeltaClusterColumnMapping(FbConnection conn, List<DeltaReport> list)
		{
			const string ENTITY = "CLUSTER COLUMN MAPPING";
			string sql;
			FbCommand cmd;

			// phase 1 : source minus target
			sql = "SELECT s.cluster_name, s.clu_column_name, s.table_name FROM src_clu_columns s" +
				" LEFT JOIN tgt_clu_columns t USING (cluster_name, clu_column_name, table_name)" +
				" JOIN common_clusters USING (cluster_name)" +
				" WHERE t.cluster_name IS NULL" +
				" ORDER BY cluster_name, clu_column_name, table_name";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var objectValue = string.Format("{0}->{1}", (string)dr["clu_column_name"], (string)dr["table_name"]);
					var report = new DeltaReport(this._comparisonSet.Uid, ENTITY, objectValue, (string)dr["cluster_name"], LabelId.ObjectInSourceNotInTarget);
					list.Add(report);
				}
			}

			// phase 2 : target minus source
			sql = "SELECT t.cluster_name, t.clu_column_name, t.table_name FROM tgt_clu_columns t" +
				" LEFT JOIN src_clu_columns s USING (cluster_name, clu_column_name, table_name)" +
				" JOIN common_clusters USING (cluster_name)" +
				" WHERE s.cluster_name IS NULL" +
				" ORDER BY cluster_name, clu_column_name, table_name";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var objectValue = string.Format("{0}->{1}", (string)dr["clu_column_name"], (string)dr["table_name"]);
					var report = new DeltaReport(this._comparisonSet.Uid, ENTITY, objectValue, (string)dr["cluster_name"], LabelId.ObjectInTargetNotInSource);
					list.Add(report);
				}
			}

			// phase 3 : property differences between source and target
			sql = "SELECT * FROM comp_clu_columns";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var sourceClusterColumnMapping = new ClusterColumnMapping
					{
						ClusterName = (string)dr["cluster_name"],
						CluColumnName = (string)dr["clu_column_name"],
						TableName = (string)dr["table_name"],
						TabColumnName = dr["src_tab_column_name"] is DBNull ? null : (string)dr["src_tab_column_name"],
					};
					var targetClusterColumnMapping = new ClusterColumnMapping
					{
						ClusterName = (string)dr["cluster_name"],
						CluColumnName = (string)dr["clu_column_name"],
						TableName = (string)dr["table_name"],
						TabColumnName = dr["tgt_tab_column_name"] is DBNull ? null : (string)dr["tgt_tab_column_name"],
					};
					sourceClusterColumnMapping.Compare(targetClusterColumnMapping, this._comparisonSet.Uid, list);
				}
			}
		}

	}
}
