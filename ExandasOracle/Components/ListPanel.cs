using System;
using System.Windows.Forms;

using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Components
{
    public partial class ListPanel : UserControl
    {
        public ListPanel()
        {
            InitializeComponent();

            // localization
            addToolStripButton.ToolTipText = Strings.Add;
            modifyToolStripButton.ToolTipText = Strings.Modify;
            deleteToolStripButton.ToolTipText = Strings.Delete;
            refreshToolStripButton.ToolTipText = Strings.Refresh;
        }

        private void ListPanel_Load(object sender, EventArgs e)
        {
            InitMainDataGridView();
            LoadData();
        }

        protected virtual void InitMainDataGridView()
        {
        }

        protected virtual void LoadData()
        {
            LoadData(new Criteria());
        }

        protected virtual void LoadData(Criteria criteria)
        {
        }

        protected virtual void AddToolStripButton_Click(object sender, EventArgs e)
        {
        }

        protected virtual void ModifyToolStripButton_Click(object sender, EventArgs e)
        {
        }

        protected virtual void DeleteToolStripButton_Click(object sender, EventArgs e)
        {
        }

        private void MainDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsANonHeaderLinkCell(e)) ModifyToolStripButton_Click(sender, e);
        }

        private void MainDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) ModifyToolStripButton_Click(sender, e);
        }

        protected bool IsANonHeaderLinkCell(DataGridViewCellEventArgs cellEvent)
        {
            if (mainDataGridView.Columns[cellEvent.ColumnIndex] is DataGridViewLinkColumn &&
                cellEvent.RowIndex != -1)
                return true;
            return false;
        }

        protected virtual void RunLookup()
        {
            var criteria = new Criteria();
            string current = lookupToolStripComboBox.Text.Trim();
            if (current.Length > 0)
            {
                criteria.Text = current;
            }
            LoadData(criteria);
        }

        private void LookupToolStripComboBox_TextChanged(object sender, EventArgs e)
        {
            lookupTimer.Enabled = false;
            lookupTimer.Enabled = true;
        }

        private void LookupToolStripComboBox_Leave(object sender, EventArgs e)
        {
            string current = lookupToolStripComboBox.Text.Trim();
            if (current.Length > 0)
            {
                if (!lookupToolStripComboBox.Items.Contains(current))
                {
                    lookupToolStripComboBox.Items.Insert(0, current);
                }
            }
        }

        private void LookupTimer_Tick(object sender, EventArgs e)
        {
            lookupTimer.Enabled = false;
            RunLookup();
        }

        private void RefreshToolStripButton_Click(object sender, EventArgs e)
        {
            string current = lookupToolStripComboBox.Text.Trim();
            if (current.Length > 0)
            {
                if (!lookupToolStripComboBox.Items.Contains(current))
                {
                    lookupToolStripComboBox.Items.Insert(0, current);
                }
            }
            lookupToolStripComboBox.Text = null;
        }

    }
}
