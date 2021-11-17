using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Services;

using ExandasOracle.Dao;

namespace ExandasOracle.Forms
{
    public partial class CompactLocalDatabaseForm : Form
    {
        public CompactLocalDatabaseForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FbBackup fbBackup = new FbBackup(DaoFactory.Instance.LocalConnectionString);
            fbBackup.Options = FbBackupFlags.IgnoreLimbo;
            fbBackup.Verbose = true;
            fbBackup.BackupFiles.Add(new FbBackupFile(DaoFactory.Instance.BackupFilePath, 2048));
            fbBackup.Execute();
            MessageBox.Show("opération BACKUP terminée");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FbRestore fbRestore = new FbRestore(DaoFactory.Instance.LocalConnectionString);
            fbRestore.BackupFiles.Add(new FbBackupFile(DaoFactory.Instance.BackupFilePath, 2048));
            fbRestore.Verbose = true;
            fbRestore.PageSize = 16384;
            fbRestore.Options = FbRestoreFlags.Create | FbRestoreFlags.Replace;
            fbRestore.Execute();
            MessageBox.Show("opération RESTORE terminée");
        }
    }
}
