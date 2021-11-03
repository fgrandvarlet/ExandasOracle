using System;
using System.Windows.Forms;

using ExandasOracle.Core;
using ExandasOracle.Domain;
using ExandasOracle.Dao;
using ExandasOracle.Forms;
using ExandasOracle.Properties;

namespace ExandasOracle.Components
{
    public partial class ComparisonSetListPanel : ListPanel
    {
        DataGridViewTextBoxColumn uidColumn;
        DataGridViewLinkColumn nameColumn;
        DataGridViewTextBoxColumn connection1Column;
        DataGridViewTextBoxColumn connection2Column;
        DataGridViewTextBoxColumn schema1Column;
        DataGridViewTextBoxColumn schema2Column;
        DataGridViewTextBoxColumn lastReportTimeColumn;

        public ComparisonSetListPanel()
        {
            InitializeComponent();

            this.titleLabel.Text = Strings.ComparisonSetList;
        }

        protected override void InitMainDataGridView()
        {
            uidColumn = new DataGridViewTextBoxColumn();
            uidColumn.Name = "uid";
            uidColumn.DataPropertyName = "uid";
            uidColumn.HeaderText = Strings.ComparisonSetID;
            uidColumn.Visible = false;

            nameColumn = new DataGridViewLinkColumn();
            nameColumn.Name = "name";
            nameColumn.DataPropertyName = "name";
            nameColumn.HeaderText = Strings.Name;
            nameColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            nameColumn.TrackVisitedState = false;
            nameColumn.LinkBehavior = LinkBehavior.HoverUnderline;
            nameColumn.Width = 300;

            connection1Column = new DataGridViewTextBoxColumn();
            connection1Column.Name = "connection1";
            connection1Column.DataPropertyName = "connection1";
            connection1Column.HeaderText = Strings.SourceConnection;
            connection1Column.Width = 150;

            connection2Column = new DataGridViewTextBoxColumn();
            connection2Column.Name = "connection2";
            connection2Column.DataPropertyName = "connection2";
            connection2Column.HeaderText = Strings.TargetConnection;
            connection2Column.Width = 150;

            schema1Column = new DataGridViewTextBoxColumn();
            schema1Column.Name = "schema1";
            schema1Column.DataPropertyName = "schema1";
            schema1Column.HeaderText = Strings.SourceSchema;
            schema1Column.Width = 120;

            schema2Column = new DataGridViewTextBoxColumn();
            schema2Column.Name = "schema2";
            schema2Column.DataPropertyName = "schema2";
            schema2Column.HeaderText = Strings.TargetSchema;
            schema2Column.Width = 120;

            lastReportTimeColumn = new DataGridViewTextBoxColumn();
            lastReportTimeColumn.Name = "last_report_time";
            lastReportTimeColumn.DataPropertyName = "last_report_time";
            lastReportTimeColumn.HeaderText = Strings.LastReportTime;
            lastReportTimeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewColumn[] cols = { uidColumn, nameColumn, connection1Column, connection2Column, schema1Column, schema2Column, lastReportTimeColumn };
            mainDataGridView.Columns.AddRange(cols);
        }

        protected override void LoadData(Criteria criteria)
        {
            mainDataGridView.DataSource = DaoFactory.Instance.GetComparisonSetDao().GetDataTable(criteria);
        }

        protected override void AddToolStripButton_Click(object sender, EventArgs e)
        {
            using (var frm = new ComparisonSetForm(null))
            {
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK) RunLookup();
            }
        }

        protected override void ModifyToolStripButton_Click(object sender, EventArgs e)
        {
            if (mainDataGridView.SelectedRows.Count <= 0) return;
            DataGridViewRow row = mainDataGridView.SelectedRows[0];
            if (row != null)
            {
                var uid = Guid.Parse(row.Cells[uidColumn.Name].Value.ToString());
                ComparisonSet cs = DaoFactory.Instance.GetComparisonSetDao().Get(uid);
                using (var frm = new ComparisonSetForm(cs))
                {
                    DialogResult dr = frm.ShowDialog(this);
                    if (dr == DialogResult.OK) RunLookup();
                }
            }
        }

        protected override void DeleteToolStripButton_Click(object sender, EventArgs e)
        {
            if (mainDataGridView.SelectedRows.Count == 0) return;
            DataGridViewRow row = mainDataGridView.SelectedRows[0];
            if (row != null)
            {
                try
                {
                    var uid = Guid.Parse(row.Cells[uidColumn.Name].Value.ToString());
                    ComparisonSet cs = DaoFactory.Instance.GetComparisonSetDao().Get(uid);

                    string enregistrement = string.Format("[ {0} ]", cs.Name);
                    if (Defs.ConfirmDeleteDialog(enregistrement))
                    {
                        DaoFactory.Instance.GetComparisonSetDao().Delete(cs);
                        RunLookup();
                    }
                }
                catch (Exception ex)
                {
                    Defs.ErrorDialog(ex.Message);
                }
            }
        }

    }
}
