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

using ExandasOracle.Components;
using ExandasOracle.Core;
using ExandasOracle.Dao;
using ExandasOracle.Properties;

namespace ExandasOracle.Forms
{
    public partial class CompactLocalDatabaseForm : Form
    {
        TitlePanel titlePanel;

        public CompactLocalDatabaseForm()
        {
            InitializeComponent();

            this.Size = new Size(620, 560);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.AcceptButton = this.doOkButton;
            this.CancelButton = this.doCancelButton;
            this.bottomPanel.Height = 38;
        }

        private void CompactLocalDatabaseForm_Load(object sender, EventArgs e)
        {
            this.Text = Defs.APPLICATION_TITLE;

            titlePanel = new TitlePanel();
            titlePanel.Parent = topPanel;
            titlePanel.Dock = DockStyle.Fill;
            topPanel.Height = 48;
            titlePanel.titleLabel.Text = "Compacter la base de données locale"; // TODO Strings.ComparisonReport;
            
            this.localDatabaseTextBox.Text = DaoFactory.Instance.LocalDatabasePath;
        }

        private void BackupLocalDatabase()
        {
            FbBackup fbBackup = new FbBackup(DaoFactory.Instance.LocalConnectionString);
            fbBackup.Options = FbBackupFlags.IgnoreLimbo;
            fbBackup.Verbose = true;
            fbBackup.BackupFiles.Add(new FbBackupFile(DaoFactory.Instance.BackupFilePath, 2048));
            fbBackup.Execute();
        }

        private void RestoreLocalDatabase()
        {
            FbRestore fbRestore = new FbRestore(DaoFactory.Instance.LocalConnectionString);
            fbRestore.BackupFiles.Add(new FbBackupFile(DaoFactory.Instance.BackupFilePath, 2048));
            fbRestore.Verbose = true;
            fbRestore.PageSize = 16384;
            fbRestore.Options = FbRestoreFlags.Create | FbRestoreFlags.Replace;
            fbRestore.Execute();
        }

        private void CompactLocalDatabaseButton_Click(object sender, EventArgs e)
        {
            this.reportTextBox.Clear();

            if (this.purgeComparisonReportCheckBox.Checked)
            {
                // appel méthode de purge
            }
            // appel méthode de purge metadata

            try
            {
                this.reportTextBox.AppendText("Sauvegarde de la base de données locale");
                this.BackupLocalDatabase();
                this.reportTextBox.AppendText("Restauration de la base de données locale");
                this.RestoreLocalDatabase();
                this.reportTextBox.AppendText("Compactage terminé");
            }
            catch (Exception ex)
            {
                Defs.ErrorDialog(ex.Message);
            }
        }

    }
}
