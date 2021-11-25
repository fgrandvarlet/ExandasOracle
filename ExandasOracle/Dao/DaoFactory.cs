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
        static readonly DaoFactory instance = new DaoFactory();
        
        const string _LOCAL_DATABASE_FILE_NAME = "EXANDAS_ORACLE.FDB";
        const string _BACKUP_FILE_NAME = "backup_EXANDAS_ORACLE.FBK";
        const string _LOCAL_DATABASE_CONTEXT_SETTING_NAME = "LocalDatabaseContext";
        const string _LOCAL_DATABASE_DIRECTORY_SETTING_NAME = "LocalDatabaseDirectory";
        const string _DATABASE_CONTEXT_SERVER = "server";
        const string _DATABASE_CONTEXT_EMBEDDED = "embedded";
        const string _LOCAL_DATABASE_USERID = "SYSDBA";
        const string _LOCAL_DATABASE_PASSWORD = "masterkey";

        LocalDatabaseContext _localDatabaseContext = LocalDatabaseContext.Undefined;
        string _localDatabaseDirectory;
        string _localConnectionString;

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

        public void Initialization()
        {
            ReadConfiguration();
            InitializeReportDirectory();
        }

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
            _localDatabaseDirectory = appSettings[_LOCAL_DATABASE_DIRECTORY_SETTING_NAME];
            if (_localDatabaseDirectory == null)
            {
                throw new ConfigurationErrorsException(_LOCAL_DATABASE_DIRECTORY_SETTING_NAME + " Setting Missing");
            }
        }

        private void InitializeReportDirectory()
        {
            if (! Directory.Exists(Defs.REPORTS_DIRECTORY))
            {
                Directory.CreateDirectory(Defs.REPORTS_DIRECTORY);
            }
        }

        public string LocalDatabaseDirectoryFullPath
        {
            get
            {
                return Path.GetFullPath(_localDatabaseDirectory);
            }
        }

        public string LocalDatabasePath
        {
            get
            {
                return Path.Combine(_localDatabaseDirectory, _LOCAL_DATABASE_FILE_NAME);
            }
        }

        public string LocalDatabaseFullPath
        {
            get
            {
                return Path.GetFullPath(LocalDatabasePath);
            }
        }

        public string LocalDatabaseSize
        {
            get
            {
                FileInfo fi = new FileInfo(LocalDatabasePath);
                decimal mo = (decimal)fi.Length / 1024 / 1024;
                return string.Format("{0:F1} Mo", mo);
            }
        }

        public string BackupFilePath
        {
            get
            {
                return Path.Combine(_localDatabaseDirectory, _BACKUP_FILE_NAME);
            }
        }

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
                            csb.Database = LocalDatabasePath;
                            csb.UserID = _LOCAL_DATABASE_USERID;
                            csb.Password = _LOCAL_DATABASE_PASSWORD;
                            csb.Charset = "UTF8";
                            csb.Dialect = 3;
                            csb.ServerType = FbServerType.Embedded;
                            csb.Pooling = false;
                            break;
                        case LocalDatabaseContext.Server:
                            csb.DataSource = "localhost";
                            csb.Database = LocalDatabasePath;
                            csb.UserID = _LOCAL_DATABASE_USERID;
                            csb.Password = _LOCAL_DATABASE_PASSWORD;
                            csb.Charset = "UTF8";
                            csb.Dialect = 3;
                            csb.ServerType = FbServerType.Default;
                            csb.Pooling = false;
                            break;
                        default:
                            break;
                    }
                    _localConnectionString = csb.ConnectionString;
                }
                return _localConnectionString;
            }
        }

        public IConnectionParamsDao GetConnectionParamsDao()
        {
            return new ConnectionParamsDaoFirebird(LocalConnectionString);
        }

        public IComparisonSetDao GetComparisonSetDao()
        {
            return new ComparisonSetDaoFirebird(LocalConnectionString);
        }

        public static IRemoteDao GetRemoteDao(string connectionString)
        {
            return new RemoteDaoOracle(connectionString);
        }

        public ILocalDao GetLocalDao()
        {
            return new LocalDaoFirebird(LocalConnectionString);
        }

        public IDeltaReportDao GetDeltaReportDao()
        {
            return new DeltaReportDaoFirebird(LocalConnectionString);
        }

        public IParameterDataDao GetParameterDataDao()
        {
            return new ParameterDataDaoFirebird(LocalConnectionString);
        }

    }
}
