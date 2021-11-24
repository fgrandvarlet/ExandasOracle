using System;
using System.Drawing;
using System.Windows.Forms;
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

            this.Size = new Size(620, 380);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.AcceptButton = this.doOkButton;
            this.CancelButton = this.doCancelButton;
            this.bottomPanel.Height = 38;

            // localization
            this.localDatabaseLabel.Text = Strings.LocalDatabase;
            this.purgeComparisonReportCheckBox.Text = Strings.PurgeComparisonReports;
            this.compactLocalDatabaseButton.Text = Strings.Compact;
            this.doCancelButton.Text = Strings.CancelButton;
        }

        private void CompactLocalDatabaseForm_Load(object sender, EventArgs e)
        {
            this.Text = Defs.APPLICATION_TITLE;

            titlePanel = new TitlePanel();
            titlePanel.Parent = topPanel;
            titlePanel.Dock = DockStyle.Fill;
            topPanel.Height = 48;
            titlePanel.titleLabel.Text = Strings.CompactLocalDatabase;
            
            this.localDatabaseTextBox.Text = DaoFactory.Instance.LocalDatabaseFullPath;
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

            try
            {
                var dao = DaoFactory.Instance.GetLocalDao();

                if (this.purgeComparisonReportCheckBox.Checked)
                {
                    this.reportTextBox.AppendText(Strings.PurgingComparisonReports + Environment.NewLine);
                    dao.PurgeDeltaReport();
                }
                this.reportTextBox.AppendText(Strings.PurgingMetadata + Environment.NewLine);
                dao.PurgeMetaDataTables();
                
                this.reportTextBox.AppendText(Strings.LocalDatabaseBackup + Environment.NewLine);
                this.BackupLocalDatabase();
                this.reportTextBox.AppendText(Strings.RestoringLocalDatabase + Environment.NewLine);
                this.RestoreLocalDatabase();
                this.reportTextBox.AppendText(Strings.CompactionCompleted);
            }
            catch (Exception ex)
            {
                Defs.ErrorDialog(ex.Message);
            }
        }

    }
}
