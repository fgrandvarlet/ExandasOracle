using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ExandasOracle.Components;
using ExandasOracle.Domain;

namespace ExandasOracle.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DeltaReportListForm : Form
    {
        ComparisonSet _comparisonSet;
        DeltaReportListPanel _deltaReportListPanel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparisonSet"></param>
        public DeltaReportListForm(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            this.Size = new Size(1280, 820);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimizeBox = false;
            this.AcceptButton = this.doOkButton;
            this.CancelButton = this.doCancelButton;
            this.bottomPanel.Height = 38;

            this._comparisonSet = comparisonSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeltaReportListForm_Load(object sender, EventArgs e)
        {
            this._deltaReportListPanel = new DeltaReportListPanel(this._comparisonSet);
            fillPanel.Controls.Add(this._deltaReportListPanel);
            this._deltaReportListPanel.Dock = DockStyle.Fill;
        }
    }
}
