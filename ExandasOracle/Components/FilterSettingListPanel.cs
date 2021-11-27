using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ExandasOracle.Core;
using ExandasOracle.Dao;
using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Components
{
    public partial class FilterSettingListPanel : UserControl
    {
        ComparisonSet _comparisonSet;
        IReferenceDao _referenceDao;

        DataGridViewTextBoxColumn idColumn;
        DataGridViewTextBoxColumn entityColumn;
        DataGridViewTextBoxColumn labelColumn;
        DataGridViewTextBoxColumn propertyColumn;

        public FilterSettingListPanel(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            this.entityComboBox.Enabled = false;
            this.addButton.Enabled = false;

            this._comparisonSet = comparisonSet;

            this._referenceDao = DaoFactory.Instance.GetReferenceDao();
        }

        private void InitMainDataGridView()
        {
            idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "id";
            idColumn.DataPropertyName = "id";
            idColumn.HeaderText = "ID filter setting";
            idColumn.Visible = false;

            entityColumn = new DataGridViewTextBoxColumn();
            entityColumn.Name = "entity";
            entityColumn.DataPropertyName = "entity";
            entityColumn.HeaderText = Strings.Entity;
            entityColumn.Width = 200;

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

            DataGridViewColumn[] cols = { idColumn, entityColumn, labelColumn, propertyColumn };
            mainDataGridView.Columns.AddRange(cols);
        }

        private void LoadData(Criteria criteria)
        {
            mainDataGridView.DataSource = DaoFactory.Instance.GetFilterSettingDao().GetDataTable(criteria);
        }

        private void LoadData()
        {
            var criteria = new Criteria
            {
                Entity = this._comparisonSet
            };
            LoadData(criteria);
        }

        private void FilterSettingListPanel_Load(object sender, EventArgs e)
        {
            InitMainDataGridView();
            LoadData();

            entityComboBox.DataSource = Defs.GetEntityReferenceList();
            entityComboBox.ValueMember = "Key";
            entityComboBox.DisplayMember = "Value";
        }

        private void EnableLineButton_Click(object sender, EventArgs e)
        {
            this.entityComboBox.Enabled = true;
        }

        private void EntityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO
        }
    }
}
