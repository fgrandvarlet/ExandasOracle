using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao.Firebird
{
    public class FilterSettingDaoFirebird : AbstractDaoFirebird, IFilterSettingDao
    {
        public FilterSettingDaoFirebird(string connectionString) : base(connectionString)
        {
        }

		protected override FbCommand CreateCommand(Criteria criteria)
		{
			var comparisonSet = (ComparisonSet)criteria.Entity;

			string sql;
			const string ROOT_SELECT = "SELECT id, entity, label, property" +
				" FROM filter_setting WHERE comparison_set_uid = @comparison_set_uid" +
				" {0} ORDER BY id";

			var cmd = new FbCommand();

			if (criteria.HasText)
			{
				const string WHERE_CLAUSE = "AND (upper(entity) LIKE @pattern OR upper(label) LIKE @pattern OR upper(property) LIKE @pattern)";

				sql = String.Format(ROOT_SELECT, WHERE_CLAUSE);
				cmd.CommandText = sql;
				cmd.Parameters.AddWithValue("comparison_set_uid", comparisonSet.Uid);
				cmd.Parameters.AddWithValue("pattern", criteria.Pattern.ToUpper());
			}
			else
			{
				sql = String.Format(ROOT_SELECT, string.Empty);
				cmd.CommandText = sql;
				cmd.Parameters.AddWithValue("comparison_set_uid", comparisonSet.Uid);
			}

			return cmd;
		}

		public FilterSetting Get(int id)
		{
			const string sql = "SELECT * FROM filtering_setting WHERE id = @id";
			FilterSetting fs = null;

			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				var cmd = new FbCommand(sql, conn);
				cmd.Parameters.AddWithValue("id", id);
				using (FbDataReader dr = cmd.ExecuteReader())
				{
					if (dr.Read())
					{
						fs = new FilterSetting();
						fs.Id = (int)dr["id"];
						fs.ComparisonSetUid = Guid.Parse((string)dr["comparison_set_uid"]);
						fs.Entity = (string)dr["entity"];
						fs.LabelId = dr["label_id"] is DBNull ? null : (short?)dr["label_id"];
						fs.Label = dr["label"] is DBNull ? null : (string)dr["label"];
						fs.Property = dr["property"] is DBNull ? null : (string)dr["property"];
					}
				}
			}
			return fs;
		}

		public void Add(FilterSetting fs)
		{
			const string sql = "INSERT INTO filter_setting(comparison_set_uid, entity, label_id, label, property)" +
				" VALUES(@comparison_set_uid, @entity, @label_id, @label, @property)";

			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				var cmd = new FbCommand(sql, conn);

				cmd.Parameters.AddWithValue("comparison_set_uid", fs.ComparisonSetUid);
				cmd.Parameters.AddWithValue("entity", fs.Entity);
				cmd.Parameters.AddWithValue("label_id", fs.LabelId);
				cmd.Parameters.AddWithValue("label", fs.Label);
				cmd.Parameters.AddWithValue("property", fs.Property);

				cmd.ExecuteNonQuery();
			}
		}

		public void Delete(FilterSetting fs)
		{
			const string sql = "DELETE FROM filter_setting WHERE id = @id";

			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				var cmd = new FbCommand(sql, conn);
				cmd.Parameters.AddWithValue("id", fs.Id);
				cmd.ExecuteNonQuery();
			}
		}

		public string GetFilteringWhereClause(Guid comparisonSetUid)
        {
			var statements = new List<string>();

			foreach (FilterSetting fs in GetList(comparisonSetUid))
            {
				statements.Add(fs.Predicate);
            }
			return string.Join(" AND ", statements);			
        }

		private List<FilterSetting> GetList(Guid comparisonSetUid)
		{
			var list = new List<FilterSetting>();

			const string sql = "SELECT * FROM filter_setting WHERE comparison_set_uid = @comparison_set_uid ORDER BY id";

			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				var cmd = new FbCommand(sql, conn);
				cmd.Parameters.AddWithValue("comparison_set_uid", comparisonSetUid);

				using (FbDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						var fs = new FilterSetting();
						fs.Id = (int)dr["id"];
						fs.ComparisonSetUid = Guid.Parse((string)dr["comparison_set_uid"]);
						fs.Entity = (string)dr["entity"];
						fs.LabelId = dr["label_id"] is DBNull ? null : (short?)dr["label_id"];
						fs.Label = dr["label"] is DBNull ? null : (string)dr["label"];
						fs.Property = dr["property"] is DBNull ? null : (string)dr["property"];

						list.Add(fs);
					}
				}
			}
			return list;
		}

	}
}
