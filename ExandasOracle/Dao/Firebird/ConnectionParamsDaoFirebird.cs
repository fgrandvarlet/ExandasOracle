using System;
using System.Collections.Generic;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao.Firebird
{
	/// <summary>
	/// 
	/// </summary>
    public class ConnectionParamsDaoFirebird : AbstractDaoFirebird, IConnectionParamsDao
    {
        /// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		public ConnectionParamsDaoFirebird(string connectionString) : base(connectionString)
        {
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		protected override FbCommand CreateCommand(Criteria criteria)
		{
			string sql;
			const string ROOT_SELECT = "SELECT uid, name, username, host, port, sid, service" +
				" FROM connection_params" +
				" {0} ORDER BY 2";

			var cmd = new FbCommand();

			if (criteria.HasText)
			{
				const string WHERE_CLAUSE = "WHERE upper(name) LIKE @pattern OR upper(username) LIKE @pattern OR upper(host) LIKE @pattern OR upper(port) LIKE @pattern" +
					" OR upper(sid) LIKE @pattern OR upper(service) LIKE @pattern";

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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="uid"></param>
		/// <returns></returns>
		public ConnectionParams Get(Guid uid)
		{
			const string sql = "SELECT * FROM connection_params WHERE uid = @uid";
			ConnectionParams cp = null;

			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				var cmd = new FbCommand(sql, conn);
				cmd.Parameters.AddWithValue("uid", uid);
				using (FbDataReader dr = cmd.ExecuteReader())
				{
					if (dr.Read())
					{
						cp = new ConnectionParams();
						cp.Uid = Guid.Parse((string)dr["uid"]);
						cp.Name = (string)dr["name"];
						cp.User = (string)dr["username"];
						cp.Password = (string)dr["password"];
						cp.Host = (string)dr["host"];
						cp.Port = (int)dr["port"];
						cp.SID = dr["sid"] is DBNull ? null : (string)dr["sid"];
						cp.Service = dr["service"] is DBNull ? null : (string)dr["service"];
						cp.DBAViews = (short)dr["dbaviews"] == 1 ? true : false;
					}
				}
			}
			return cp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cp"></param>
		public void Add(ConnectionParams cp)
		{
			const string sql = "INSERT INTO connection_params(uid, name, username, password, host, port, sid, service, dbaviews)" +
				" VALUES(@uid, @name, @username, @password, @host, @port, @sid, @service, @dbaviews)";

			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				var cmd = new FbCommand(sql, conn);

				cmd.Parameters.AddWithValue("uid", cp.Uid);
				cmd.Parameters.AddWithValue("name", cp.Name);
				cmd.Parameters.AddWithValue("username", cp.User);
				cmd.Parameters.AddWithValue("password", cp.Password);
				cmd.Parameters.AddWithValue("host", cp.Host);
				cmd.Parameters.AddWithValue("port", cp.Port);
				cmd.Parameters.AddWithValue("sid", cp.SID);
				cmd.Parameters.AddWithValue("service", cp.Service);
				cmd.Parameters.AddWithValue("dbaviews", cp.DBAViews);

				cmd.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cp"></param>
		public void Save(ConnectionParams cp)
		{
			const string sql = "UPDATE connection_params SET name = @name, username = @username, password = @password, host = @host, port = @port," +
				" sid = @sid, service = @service, dbaviews = @dbaviews WHERE uid = @uid";

			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				var cmd = new FbCommand(sql, conn);

				cmd.Parameters.AddWithValue("name", cp.Name);
				cmd.Parameters.AddWithValue("username", cp.User);
				cmd.Parameters.AddWithValue("password", cp.Password);
				cmd.Parameters.AddWithValue("host", cp.Host);
				cmd.Parameters.AddWithValue("port", cp.Port);
				cmd.Parameters.AddWithValue("sid", cp.SID);
				cmd.Parameters.AddWithValue("service", cp.Service);
				cmd.Parameters.AddWithValue("dbaviews", cp.DBAViews);
				cmd.Parameters.AddWithValue("uid", cp.Uid);

				cmd.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cp"></param>
		public void Delete(ConnectionParams cp)
		{
			const string sql = "DELETE FROM connection_params WHERE uid = @id";

			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				var cmd = new FbCommand(sql, conn);
				cmd.Parameters.AddWithValue("uid", cp.Uid);
				cmd.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<ConnectionParams> GetList()
		{
			var list = new List<ConnectionParams>();

			const string sql = "SELECT * FROM connection_params ORDER BY name";

			using (FbConnection conn = GetFirebirdConnection())
			{
				conn.Open();
				var cmd = new FbCommand(sql, conn);
				using (FbDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						var cp = new ConnectionParams();

						cp.Uid = Guid.Parse((string)dr["uid"]);
						cp.Name = (string)dr["name"];
						cp.User = (string)dr["username"];
						cp.Password = (string)dr["password"];
						cp.Host = (string)dr["host"];
						cp.Port = (int)dr["port"];
						cp.SID = dr["sid"] is DBNull ? null : (string)dr["sid"];
						cp.Service = dr["service"] is DBNull ? null : (string)dr["service"];
						cp.DBAViews = (short)dr["dbaviews"] == 1 ? true : false;

						list.Add(cp);
					}
				}
			}
			return list;
		}

	}
}
