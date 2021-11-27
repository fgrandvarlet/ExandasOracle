using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ExandasOracle.Components;
using ExandasOracle.Core;
using ExandasOracle.Domain;

namespace ExandasOracle.Forms
{
    public partial class FilterSettingListForm : Form
    {
        ComparisonSet _comparisonSet;
        FilterSettingListPanel filterSettingListPanel;
        TitlePanel titlePanel;

        public FilterSettingListForm(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            this.Size = new Size(900, 800);

            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.AcceptButton = this.doOkButton;
            this.CancelButton = this.doCancelButton;
            this.bottomPanel.Height = 38;

            this._comparisonSet = comparisonSet;
        }

        private void FilterSettingListForm_Load(object sender, EventArgs e)
        {
            this.Text = Defs.APPLICATION_TITLE;

            titlePanel = new TitlePanel();
            titlePanel.Parent = topPanel;
            titlePanel.Dock = DockStyle.Fill;
            topPanel.Height = 48;
            titlePanel.titleLabel.Text = "Paramètres de filtrage"; // Strings.ComparisonReport;

            filterSettingListPanel = new FilterSettingListPanel(this._comparisonSet);
            fillPanel.Controls.Add(filterSettingListPanel);
            filterSettingListPanel.Dock = DockStyle.Fill;

            filterSettingListPanel.titleLabel.Text = _comparisonSet.Name;
        }
    }
}
