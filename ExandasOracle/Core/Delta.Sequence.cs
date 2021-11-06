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
		private void DeltaSequence(FbConnection conn, List<DeltaReport> list)
		{
			string sql;
			FbCommand cmd;

			// phase 1 : source moins cible
			sql = "SELECT s.sequence_name FROM src_sequences s" +
				" LEFT JOIN tgt_sequences t USING(sequence_name)" +
				" WHERE t.sequence_name IS NULL" +
				" ORDER BY sequence_name";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var report = new DeltaReport(this._comparisonSet.Uid, "SEQUENCE", (string)dr["sequence_name"], Strings.ObjectInSource);
					list.Add(report);
				}
			}

			// phase 2 : cible moins source
			sql = "SELECT t.sequence_name FROM tgt_sequences t" +
				" LEFT JOIN src_sequences s USING(sequence_name)" +
				" WHERE s.sequence_name IS NULL" +
				" ORDER BY sequence_name";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var report = new DeltaReport(this._comparisonSet.Uid, "SEQUENCE", (string)dr["sequence_name"], Strings.ObjectInTarget);
					list.Add(report);
				}
			}

			// phase 3 : différences de propriétés entre source et cible
			sql = "SELECT * FROM comp_sequences";
			cmd = new FbCommand(sql, conn);

			using (FbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					var sourceSequence = new Sequence
					{
						SequenceName = (string)dr["sequence_name"],
						MinValue = dr["src_min_value"] is DBNull ? null : (long?)dr["src_min_value"],
						MaxValue = dr["src_max_value"] is DBNull ? null : (decimal?)Convert.ToDecimal((string)dr["src_max_value"]),
						IncrementBy = (int)dr["src_increment_by"],
						CycleFlag = dr["src_cycle_flag"] is DBNull ? null : (string)dr["src_cycle_flag"],
						OrderFlag = dr["src_order_flag"] is DBNull ? null : (string)dr["src_order_flag"],
						CacheSize = (int)dr["src_cache_size"],
						ScaleFlag = dr["src_scale_flag"] is DBNull ? null : (string)dr["src_scale_flag"],
						ExtendFlag = dr["src_extend_flag"] is DBNull ? null : (string)dr["src_extend_flag"],
						ShardedFlag = dr["src_sharded_flag"] is DBNull ? null : (string)dr["src_sharded_flag"],
						SessionFlag = dr["src_session_flag"] is DBNull ? null : (string)dr["src_session_flag"],
						KeepValue = dr["src_keep_value"] is DBNull ? null : (string)dr["src_keep_value"]
					};
					var targetSequence = new Sequence
					{
						SequenceName = (string)dr["sequence_name"],
						MinValue = dr["tgt_min_value"] is DBNull ? null : (long?)dr["tgt_min_value"],
						MaxValue = dr["tgt_max_value"] is DBNull ? null : (decimal?)Convert.ToDecimal((string)dr["tgt_max_value"]),
						IncrementBy = (int)dr["tgt_increment_by"],
						CycleFlag = dr["tgt_cycle_flag"] is DBNull ? null : (string)dr["tgt_cycle_flag"],
						OrderFlag = dr["tgt_order_flag"] is DBNull ? null : (string)dr["tgt_order_flag"],
						CacheSize = (int)dr["tgt_cache_size"],
						ScaleFlag = dr["tgt_scale_flag"] is DBNull ? null : (string)dr["tgt_scale_flag"],
						ExtendFlag = dr["tgt_extend_flag"] is DBNull ? null : (string)dr["tgt_extend_flag"],
						ShardedFlag = dr["tgt_sharded_flag"] is DBNull ? null : (string)dr["tgt_sharded_flag"],
						SessionFlag = dr["tgt_session_flag"] is DBNull ? null : (string)dr["tgt_session_flag"],
						KeepValue = dr["tgt_keep_value"] is DBNull ? null : (string)dr["tgt_keep_value"]
					};
					sourceSequence.Compare(targetSequence, this._comparisonSet.Uid, list);
				}
			}
		}

	}
}
