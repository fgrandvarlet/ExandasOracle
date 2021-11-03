using Oracle.ManagedDataAccess.Client;

namespace ExandasOracle.Dao.Oracle
{
    public abstract class AbstractDaoOracle
    {
        protected readonly string _connectionString;

        protected AbstractDaoOracle(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public OracleConnection GetOracleConnection()
        {
            return new OracleConnection(_connectionString);
        }

    }
}
