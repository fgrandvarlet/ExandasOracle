using System;
using System.Windows.Forms;

using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ListPanel : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ListPanel()
        {
            InitializeComponent();

            // localization
            addToolStripButton.ToolTipText = Strings.Add;
            modifyToolStripButton.ToolTipText = Strings.Modify;
            deleteToolStripButton.ToolTipText = Strings.Delete;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListPanel_Load(object sender, EventArgs e)
        {
            InitMainDataGridView();
            LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void InitMainDataGridView()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void LoadData()
        {
            LoadData(new Criteria());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        protected virtual void LoadData(Criteria criteria)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void AddToolStripButton_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ModifyToolStripButton_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void DeleteToolStripButton_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsANonHeaderLinkCell(e)) ModifyToolStripButton_Click(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) ModifyToolStripButton_Click(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellEvent"></param>
        /// <returns></returns>
        protected bool IsANonHeaderLinkCell(DataGridViewCellEventArgs cellEvent)
        {
            if (mainDataGridView.Columns[cellEvent.ColumnIndex] is DataGridViewLinkColumn &&
                cellEvent.RowIndex != -1)
                return true;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LookupToolStripComboBox_TextChanged(object sender, EventArgs e)
        {
            lookupTimer.Enabled = false;
            lookupTimer.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LookupTimer_Tick(object sender, EventArgs e)
        {
            lookupTimer.Enabled = false;
            RunLookup();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
