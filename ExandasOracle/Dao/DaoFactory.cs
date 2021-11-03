using System.Configuration;
using System.IO;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Core;
using ExandasOracle.Dao.Firebird;
using ExandasOracle.Dao.Oracle;

namespace ExandasOracle.Dao
{
    enum LocalDatabaseContext
    {
        Undefined,
        Server,
        Embedded
    }

    public enum SchemaType
    {
        Source,
        Target
    }

    public sealed class DaoFactory
    {
        static DaoFactory instance = new DaoFactory();
        string _localConnectionString;
        const string _LOCAL_DATABASE_CONTEXT_SETTING_NAME = "LocalDatabaseContext";
        const string _DATABASE_CONTEXT_SERVER = "server";
        const string _DATABASE_CONTEXT_EMBEDDED = "embedded";
        LocalDatabaseContext _localDatabaseContext = LocalDatabaseContext.Undefined;

        /// <summary>
        /// 
        /// </summary>
        public static DaoFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private DaoFactory()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialization()
        {
            ReadConfiguration();
            //InitializeSQLiteLocalDatabase();
            InitializeReportDirectory();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReadConfiguration()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[_LOCAL_DATABASE_CONTEXT_SETTING_NAME].ToLower();
            switch (result)
            {
                case _DATABASE_CONTEXT_SERVER:
                    _localDatabaseContext = LocalDatabaseContext.Server;
                    break;
                case _DATABASE_CONTEXT_EMBEDDED:
                    _localDatabaseContext = LocalDatabaseContext.Embedded;
                    break;
                default:
                    throw new ConfigurationErrorsException("Invalid Application Setting : " + result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeReportDirectory()
        {
            if (! Directory.Exists(Defs.REPORTS_DIRECTORY))
            {
                Directory.CreateDirectory(Defs.REPORTS_DIRECTORY);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LocalConnectionString
        {
            get
            {
                if (_localConnectionString == null)
                {
                    var csb = new FbConnectionStringBuilder();
                    switch (_localDatabaseContext)
                    {
                        case LocalDatabaseContext.Embedded:
                            csb.Database = @"data\EXANDAS_ORACLE.FDB";
                            csb.UserID = "SYSDBA";
                            csb.Password = "masterkey";
                            csb.Charset = "UTF8";
                            csb.Dialect = 3;
                            csb.ServerType = FbServerType.Embedded;
                            break;
                        case LocalDatabaseContext.Server:
                            csb.DataSource = "localhost";
                            csb.Database = @"C:\FIREBIRD\EXANDAS_ORACLE\EXANDAS_ORACLE.FDB";
                            csb.UserID = "SYSDBA";
                            csb.Password = "masterkey";
                            csb.Charset = "UTF8";
                            csb.Dialect = 3;
                            csb.ServerType = FbServerType.Default;
                            //TODO csb.Pooling QUELLE VALEUR PAR DEFAUT QUELLES CONSEQUENCES ?
                            break;
                        default:
                            break;
                    }
                    _localConnectionString = csb.ConnectionString;
                }
                return _localConnectionString;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IConnectionParamsDao GetConnectionParamsDao()
        {
            return new ConnectionParamsDaoFirebird(LocalConnectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IComparisonSetDao GetComparisonSetDao()
        {
            return new ComparisonSetDaoFirebird(LocalConnectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IRemoteDao GetRemoteDao(string connectionString)
        {
            return new RemoteDaoOracle(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ILocalDao GetLocalDao()
        {
            return new LocalDaoFirebird(LocalConnectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDeltaReportDao GetDeltaReportDao()
        {
            return new DeltaReportDaoFirebird(LocalConnectionString);
        }

    }
}
