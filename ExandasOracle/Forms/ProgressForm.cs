using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using ExandasOracle.Core;
using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Forms
{
    public partial class ProgressForm : Form
    {
        ComparisonSet _comparisonSet;
        bool _cancellationDone = false;

        public ProgressForm(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            this.Size = new Size(520, 260);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.CancelButton = noButton;
            this.AcceptButton = yesButton;

            this.topPanel.Height = 60;
            this.bottomFlowLayoutPanel.Height = 52;
            this.mainProgressBar.Visible = false;
            this.progressLabel.Visible = false;
            this.progressLabel.Text = null;
            this.cancelButton.Visible = false;

            this._comparisonSet = comparisonSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressForm_Load(object sender, EventArgs e)
        {
            Text = Defs.TITLE_FORM_PROGRESS;
            messageLabel.Text = Defs.MESSAGE_REPORT_GENERATOR;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            var comparisonSet = e.Argument as ComparisonSet;
            (new MetaDataLoader(comparisonSet)).Execute(worker, e);
            (new Delta(comparisonSet)).Execute(worker, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            mainProgressBar.Value = e.ProgressPercentage;
            progressLabel.Text = string.Format("{0} : {1} %", e.UserState.ToString(), e.ProgressPercentage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                progressLabel.Text = "Opération annulée.";
                mainProgressBar.Visible = false;
                this._cancellationDone = true;
                cancelButton.Text = "Fermer";
            }
            else if (e.Error != null)
            {
                mainProgressBar.Visible = false;
                MessageBox.Show(e.Error.Message, Strings.ExandasOracleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else
            {
                DialogResult = DialogResult.Yes;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YesButton_Click(object sender, EventArgs e)
        {
            mainProgressBar.Visible = true;
            mainProgressBar.Style = ProgressBarStyle.Continuous;
            progressLabel.Visible = true;
            cancelButton.Visible = true;
            yesButton.Visible = false;
            noButton.Visible = false;
            messageLabel.Text = Defs.MESSAGE_REPORT_GENERATOR_RUNNING;
            mainBackgroundWorker.RunWorkerAsync(_comparisonSet);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (this._cancellationDone)
            {
                this.Close();
            }
            else
            {
                if (mainBackgroundWorker.WorkerSupportsCancellation == true)
                {
                    mainBackgroundWorker.CancelAsync();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainBackgroundWorker.IsBusy)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                }
            }
        }

    }
}
