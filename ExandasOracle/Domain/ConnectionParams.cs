using System;
using Oracle.ManagedDataAccess.Client;

using ExandasOracle.Core;
using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    public class ConnectionParams
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string DecryptedPassword
        {
            get
            {
                return CryptoUtil.DecryptIdentifier(Password, Resources.IDIOMATIC);
            }
            set
            {
                Password = CryptoUtil.EncryptIdentifier(value, Resources.IDIOMATIC);
            }
        }
        public string Host { get; set; }
        public int Port { get; set; }
        public string SID { get; set; }
        public string Service { get; set; }
        public bool DBAViews { get; set; }

        public string ConnectionString
        {
            // cf. https://www.connectionstrings.com/oracle/
            get
            {
                string dataSource;
                if (SID != null)
                {
                    dataSource = string.Format(
                        "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))(CONNECT_DATA = (SID = {2})))",
                        Host,
                        Port,
                        SID
                        );
                }
                else
                {
                    dataSource = string.Format(
                        "{0}:{1}/{2}",
                        Host,
                        Port,
                        Service
                        );
                }
                var csb = new OracleConnectionStringBuilder();
                csb.DataSource = dataSource;
                csb.UserID = User;
                csb.Password = DecryptedPassword;
                csb.Pooling = false;

                return csb.ConnectionString;
            }
        }

        public string FormattedString
        {
            get
            {
                if (SID != null)
                {
                    return string.Format(
                        "{0} [{1}@//{2}:{3}/{4}]",
                        Name,
                        User,
                        Host,
                        Port,
                        SID
                    );
                }
                else
                {
                    return string.Format(
                        "{0} [{1}@//{2}:{3}/{4}]",
                        Name,
                        User,
                        Host,
                        Port,
                        Service
                    );
                }
            }
        }

    }
}
