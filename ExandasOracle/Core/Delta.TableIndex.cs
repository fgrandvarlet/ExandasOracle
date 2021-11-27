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
		private void DeltaTableIndex(FbConnection conn, List<DeltaReport> list)
		{
			const string ENTITY = "TABLE INDEX";
			string sql;
			FbCommand cmd;

			// phase 1 : source minus target
			sql = "SELECT s.index_name, s.table_name FROM src_table_indexes s" +
				" LEFT JOIN tgt_table_indexes t USING (index_name, table_name)" +
				" JOIN common_tables USING (table_name)" +
				" WHERE t.index_name IS NULL" +
				" ORDER BY table_name, index_name";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var report = new DeltaReport(this._comparisonSet.Uid, ENTITY, (string)dr["index_name"], (string)dr["table_name"], LabelId.ObjectInSourceNotInTarget);
					list.Add(report);
				}
			}

			// phase 2 : target minus source
			sql = "SELECT t.index_name, t.table_name FROM tgt_table_indexes t" +
				" LEFT JOIN src_table_indexes s USING (index_name, table_name)" +
				" JOIN common_tables USING (table_name)" +
				" WHERE s.index_name IS NULL" +
				" ORDER BY table_name, index_name";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var report = new DeltaReport(this._comparisonSet.Uid, ENTITY, (string)dr["index_name"], (string)dr["table_name"], LabelId.ObjectInTargetNotInSource);
					list.Add(report);
				}
			}

			// phase 3 : property differences between source and target
			sql = "SELECT * FROM comp_table_indexes";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var sourceTableIndex = new TableIndex
					{
						IndexName = (string)dr["index_name"],
						IndexType = dr["src_index_type"] is DBNull ? null : (string)dr["src_index_type"],
						TableName = (string)dr["table_name"],
						Uniqueness = dr["src_uniqueness"] is DBNull ? null : (string)dr["src_uniqueness"],
						Compression = dr["src_compression"] is DBNull ? null : (string)dr["src_compression"],
						PrefixLength = dr["src_prefix_length"] is DBNull ? null : (int?)dr["src_prefix_length"],
						TablespaceName = dr["src_tablespace_name"] is DBNull ? null : (string)dr["src_tablespace_name"],
						IncludeColumn = dr["src_include_column"] is DBNull ? null : (int?)dr["src_include_column"],
						Logging = dr["src_logging"] is DBNull ? null : (string)dr["src_logging"],
						Status = dr["src_status"] is DBNull ? null : (string)dr["src_status"],
						Degree = dr["src_degree"] is DBNull ? null : (string)dr["src_degree"],
						Partitioned = dr["src_partitioned"] is DBNull ? null : (string)dr["src_partitioned"],
						Temporary = dr["src_temporary"] is DBNull ? null : (string)dr["src_temporary"],
						Duration = dr["src_duration"] is DBNull ? null : (string)dr["src_duration"]
					};
					var targetTableIndex = new TableIndex
					{
						IndexName = (string)dr["index_name"],
						IndexType = dr["tgt_index_type"] is DBNull ? null : (string)dr["tgt_index_type"],
						TableName = (string)dr["table_name"],
						Uniqueness = dr["tgt_uniqueness"] is DBNull ? null : (string)dr["tgt_uniqueness"],
						Compression = dr["tgt_compression"] is DBNull ? null : (string)dr["tgt_compression"],
						PrefixLength = dr["tgt_prefix_length"] is DBNull ? null : (int?)dr["tgt_prefix_length"],
						TablespaceName = dr["tgt_tablespace_name"] is DBNull ? null : (string)dr["tgt_tablespace_name"],
						IncludeColumn = dr["tgt_include_column"] is DBNull ? null : (int?)dr["tgt_include_column"],
						Logging = dr["tgt_logging"] is DBNull ? null : (string)dr["tgt_logging"],
						Status = dr["tgt_status"] is DBNull ? null : (string)dr["tgt_status"],
						Degree = dr["tgt_degree"] is DBNull ? null : (string)dr["tgt_degree"],
						Partitioned = dr["tgt_partitioned"] is DBNull ? null : (string)dr["tgt_partitioned"],
						Temporary = dr["tgt_temporary"] is DBNull ? null : (string)dr["tgt_temporary"],
						Duration = dr["tgt_duration"] is DBNull ? null : (string)dr["tgt_duration"]
					};
					sourceTableIndex.Compare(targetTableIndex, this._comparisonSet.Uid, list);
				}
			}
		}

	}
}
