using System;
using System.Windows.Forms;

using ExandasOracle.Dao;
using ExandasOracle.Domain;
using ExandasOracle.Properties;
using ExandasOracle.Reporting;

namespace ExandasOracle.Components
{
    public partial class DeltaReportListPanel : UserControl
    {
        ComparisonSet _comparisonSet;

        DataGridViewTextBoxColumn idColumn;
        DataGridViewTextBoxColumn entityColumn;
        DataGridViewTextBoxColumn objectColumn;
        DataGridViewTextBoxColumn parentObjectColumn;
        DataGridViewTextBoxColumn labelColumn;
        DataGridViewTextBoxColumn propertyColumn;
        DataGridViewTextBoxColumn sourceColumn;
        DataGridViewTextBoxColumn targetColumn;

        public DeltaReportListPanel(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            this._comparisonSet = comparisonSet;

            this.addToolStripButton.Visible = false;
            this.deleteToolStripButton.Visible = false;

            // localization
            this.exportExcelButton.Text = Strings.ExportExcel;
        }

        private void InitMainDataGridView()
        {
            idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "id";
            idColumn.DataPropertyName = "id";
            idColumn.HeaderText = "ID delta report";
            idColumn.Visible = false;

            /*
            nameColumn = new DataGridViewLinkColumn();
            nameColumn.Name = "name";
            nameColumn.DataPropertyName = "name";
            nameColumn.HeaderText = "Nom";
            nameColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            nameColumn.TrackVisitedState = false;
            nameColumn.LinkBehavior = LinkBehavior.HoverUnderline;
            nameColumn.Width = 200;
            */

            entityColumn = new DataGridViewTextBoxColumn();
            entityColumn.Name = "entity";
            entityColumn.DataPropertyName = "entity";
            entityColumn.HeaderText = Strings.Entity;
            entityColumn.Width = 200;

            objectColumn = new DataGridViewTextBoxColumn();
            objectColumn.Name = "object";
            objectColumn.DataPropertyName = "object";
            objectColumn.HeaderText = Strings.ObjectName;
            objectColumn.Width = 250;

            parentObjectColumn = new DataGridViewTextBoxColumn();
            parentObjectColumn.Name = "parent_object";
            parentObjectColumn.DataPropertyName = "parent_object";
            parentObjectColumn.HeaderText = Strings.ParentObject;
            parentObjectColumn.Width = 180;

            labelColumn = new DataGridViewTextBoxColumn();
            labelColumn.Name = "label";
            labelColumn.DataPropertyName = "label";
            labelColumn.HeaderText = Strings.Label;
            labelColumn.Width = 200;

            propertyColumn = new DataGridViewTextBoxColumn();
            propertyColumn.Name = "property";
            propertyColumn.DataPropertyName = "property";
            propertyColumn.HeaderText = Strings.Property;
            propertyColumn.Width = 200;

            sourceColumn = new DataGridViewTextBoxColumn();
            sourceColumn.Name = "source";
            sourceColumn.DataPropertyName = "source";
            sourceColumn.HeaderText = Strings.SourceValue;
            sourceColumn.Width = 200;

            targetColumn = new DataGridViewTextBoxColumn();
            targetColumn.Name = "target";
            targetColumn.DataPropertyName = "target";
            targetColumn.HeaderText = Strings.TargetValue;
            targetColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewColumn[] cols = { idColumn, entityColumn, objectColumn, parentObjectColumn, labelColumn, propertyColumn, sourceColumn, targetColumn };
            mainDataGridView.Columns.AddRange(cols);
        }

        private void LoadData(Criteria criteria)
        {
            mainDataGridView.DataSource = DaoFactory.Instance.GetDeltaReportDao().GetDataTable(criteria);
        }

        private void LoadData()
        {
            var criteria = new Criteria
            {
                Entity = this._comparisonSet
            };
            LoadData(criteria);
        }

        private void DeltaReportListPanel_Load(object sender, EventArgs e)
        {
            InitMainDataGridView();
            LoadData();
        }

        private void RunLookup()
        {
            var criteria = new Criteria
            {
                Entity = this._comparisonSet
            };
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


        private void ModifyToolStripButton_Click(object sender, EventArgs e)
        {
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

        private void ExportExcelButton_Click(object sender, EventArgs e)
        {
            ReportUtils.ExportToExcel(this._comparisonSet);
        }
    }
}
