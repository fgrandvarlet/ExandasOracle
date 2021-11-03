using System;
using System.Collections.Generic;
using System.Text;
using Oracle.ManagedDataAccess.Client;

using ExandasOracle.Core;
using ExandasOracle.Properties;

namespace ExandasOracle.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionParams
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Uid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string User { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DecryptedPassword
        {
            // TODO Chronométrer via log4net ?
            get
            {
                return CryptoUtil.DecryptIdentifier(Password, Resources.IDIOMATIC);
            }
            set
            {
                Password = CryptoUtil.EncryptIdentifier(value, Resources.IDIOMATIC);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DBAViews { get; set; }

        /// <summary>
        /// 
        /// </summary>
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

                // TODO ETUDIER csb.IsReadOnly ET EVENTUELLEMENT AUTRES PROPRIETES
                csb.Pooling = false;

                // TODO SUPPRIMER
                // System.Windows.Forms.MessageBox.Show(csb.ConnectionString);
                return csb.ConnectionString;
            }
        }

        /// <summary>
        /// 
        /// </summary>
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
