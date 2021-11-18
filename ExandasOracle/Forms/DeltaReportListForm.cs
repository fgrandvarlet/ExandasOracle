using System;
using System.Drawing;
using System.Windows.Forms;

using ExandasOracle.Components;
using ExandasOracle.Core;
using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Forms
{
    public partial class DeltaReportListForm : Form
    {
        ComparisonSet _comparisonSet;
        DeltaReportListPanel deltaReportListPanel;
        TitlePanel titlePanel;

        public DeltaReportListForm(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            this.Size = new Size(resolution.Width - 80, resolution.Height - 80);

            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimizeBox = false;
            this.AcceptButton = this.doOkButton;
            this.CancelButton = this.doCancelButton;
            this.bottomPanel.Height = 38;

            this._comparisonSet = comparisonSet;
        }

        private void DeltaReportListForm_Load(object sender, EventArgs e)
        {
            this.Text = Defs.APPLICATION_TITLE;

            titlePanel = new TitlePanel();
            titlePanel.Parent = topPanel;
            titlePanel.Dock = DockStyle.Fill;
            topPanel.Height = 48;
            titlePanel.titleLabel.Text = Strings.ComparisonReport;
            
            deltaReportListPanel = new DeltaReportListPanel(this._comparisonSet);
            fillPanel.Controls.Add(deltaReportListPanel);
            deltaReportListPanel.Dock = DockStyle.Fill;

            var lastReportTimeText = "";
            if (_comparisonSet.LastReportTime.HasValue)
            {
                lastReportTimeText = string.Format("[{0}]", _comparisonSet.LastReportTime.Value.ToString("f"));
            }
            deltaReportListPanel.titleLabel.Text = string.Format("{0} {1}", _comparisonSet.Name, lastReportTimeText);
        }

    }
}
