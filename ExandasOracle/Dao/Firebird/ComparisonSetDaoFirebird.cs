using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao.Firebird
{
    public class ComparisonSetDaoFirebird : AbstractDaoFirebird, IComparisonSetDao
    {
        public ComparisonSetDaoFirebird(string connectionString) : base(connectionString)
        {
        }

		protected override FbCommand CreateCommand(Criteria criteria)
		{
			string sql;
			const string ROOT_SELECT = "SELECT cs.uid, cs.name, cp1.name AS connection1, cp2.name AS connection2, schema1, schema2, last_report_time" +
				" FROM comparison_set cs" +
				" JOIN connection_params cp1 ON connection1_uid = cp1.uid" +
				" JOIN connection_params cp2 ON connection2_uid = cp2.uid" +
				" {0} ORDER BY 2";

			var cmd = new FbCommand();

			if (criteria.HasText)
			{
				const string WHERE_CLAUSE = "WHERE upper(cs.name) LIKE @pattern OR upper(cp1.name) LIKE @pattern OR upper(cp2.name) LIKE @pattern" +
					" OR upper(schema1) LIKE @pattern OR upper(schema2) LIKE @pattern";

				sql = String.Format(ROOT_SELECT, WHERE_CLAUSE);
				cmd.CommandText = sql;
				cmd.Parameters.AddWithValue("pattern", criteria.Pattern.ToUpper());
			}
			else
			{
				sql = String.Format(ROOT_SELECT, string.Empty);
				cmd.CommandText = sql;
			}

			return cmd;
		}

		public ComparisonSet Get(Guid uid)
		{
			const string sql = "SELECT * FROM comparison_set WHERE uid = @uid";
			ComparisonSet cs = null;

			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				var cmd = new FbCommand(sql, conn);
				cmd.Parameters.AddWithValue("uid", uid);
				using (FbDataReader dr = cmd.ExecuteReader())
				{
					if (dr.Read())
					{
						cs = new ComparisonSet();
						cs.Uid = Guid.Parse((string)dr["uid"]);
						cs.Name = (string)dr["name"];
						cs.Connection1Uid = Guid.Parse((string)dr["connection1_uid"]);
						cs.Connection2Uid = Guid.Parse((string)dr["connection2_uid"]);
						cs.Schema1 = (string)dr["schema1"];
						cs.Schema2 = (string)dr["schema2"];
						cs.LastReportTime = dr["last_report_time"] is DBNull ? null : (DateTime?)dr["last_report_time"];
					}
				}
			}
			return cs;
		}

		public void Add(FbTransaction tran, ComparisonSet cs)
		{
			const string sql = "INSERT INTO comparison_set(uid, name, connection1_uid, connection2_uid, schema1, schema2)" +
				" VALUES(@uid, @name, @connection1_uid, @connection2_uid, @schema1, @schema2)";

			var cmd = new FbCommand(sql, tran.Connection, tran);

			cmd.Parameters.AddWithValue("uid", cs.Uid);
			cmd.Parameters.AddWithValue("name", cs.Name);
			cmd.Parameters.AddWithValue("connection1_uid", cs.Connection1Uid);
			cmd.Parameters.AddWithValue("connection2_uid", cs.Connection2Uid);
			cmd.Parameters.AddWithValue("schema1", cs.Schema1);
			cmd.Parameters.AddWithValue("schema2", cs.Schema2);

			cmd.ExecuteNonQuery();
		}

		public void Add(ComparisonSet cs)
		{
			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				FbTransaction tran = conn.BeginTransaction();
                try
                {
					Add(tran, cs);
					tran.Commit();
				}
                catch (Exception)
                {
					tran.Rollback();
					throw;
                }
			}
		}

		public void Save(ComparisonSet cs)
		{
			const string sql = "UPDATE comparison_set SET name = @name, connection1_uid = @connection1_uid, connection2_uid = @connection2_uid," +
				"schema1 = @schema1, schema2 = @schema2 WHERE uid = @uid";

			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				using (var cmd = new FbCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("name", cs.Name);
					cmd.Parameters.AddWithValue("connection1_uid", cs.Connection1Uid);
					cmd.Parameters.AddWithValue("connection2_uid", cs.Connection2Uid);
					cmd.Parameters.AddWithValue("schema1", cs.Schema1);
					cmd.Parameters.AddWithValue("schema2", cs.Schema2);
					cmd.Parameters.AddWithValue("uid", cs.Uid);

					cmd.ExecuteNonQuery();
				}
			}
		}

		public void Delete(ComparisonSet cs)
		{
			const string sql = "DELETE FROM comparison_set WHERE uid = @uid";

			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				using (var cmd = new FbCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("uid", cs.Uid);
					cmd.ExecuteNonQuery();
				}
			}
		}

		public List<ComparisonSet> GetList()
		{
			var list = new List<ComparisonSet>();

			const string sql = "SELECT * FROM comparison_set ORDER BY name";

			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				var cmd = new FbCommand(sql, conn);
				using (FbDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						var cs = new ComparisonSet();

						cs.Uid = Guid.Parse((string)dr["uid"]);
						cs.Name = (string)dr["name"];
						cs.Connection1Uid = Guid.Parse((string)dr["connection1_uid"]);
						cs.Connection2Uid = Guid.Parse((string)dr["connection2_uid"]);
						cs.Schema1 = (string)dr["schema1"];
						cs.Schema2 = (string)dr["schema2"];
						cs.LastReportTime = dr["last_report_time"] is DBNull ? null : (DateTime?)dr["last_report_time"];

						list.Add(cs);
					}
				}
			}
			return list;
		}

	}
}
