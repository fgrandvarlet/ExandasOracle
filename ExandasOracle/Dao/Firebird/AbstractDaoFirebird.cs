using System.Data;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao.Firebird
{
    public abstract class AbstractDaoFirebird
    {
        protected readonly string _connectionString;

        protected AbstractDaoFirebird(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public FbConnection GetFirebirdConnection()
        {
            return new FbConnection(_connectionString);
        }

        protected abstract FbCommand CreateCommand(Criteria criteria);

        public DataTable GetDataTable(Criteria criteria)
        {
            var ds = new DataSet();

            FbConnection conn = GetFirebirdConnection();
            try
            {
                conn.Open();
                var cmd = CreateCommand(criteria);
                cmd.Connection = conn;
                var da = new FbDataAdapter(cmd);
                da.Fill(ds);

                return ds.Tables[0];
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
