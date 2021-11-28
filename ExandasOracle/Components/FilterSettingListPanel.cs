using System;
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

        DataGridViewTextBoxColumn idColumn;
        DataGridViewTextBoxColumn entityColumn;
        DataGridViewTextBoxColumn labelColumn;
        DataGridViewTextBoxColumn propertyColumn;

        public FilterSettingListPanel(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            this.entityComboBox.Enabled = false;
            this.labelComboBox.Enabled = false;
            this.propertyComboBox.Enabled = false;
            this.addButton.Enabled = false;

            this.addToolStripButton.Visible = false;
            this.modifyToolStripButton.Visible = false;

            this._comparisonSet = comparisonSet;
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
            entityColumn.Width = 223;

            labelColumn = new DataGridViewTextBoxColumn();
            labelColumn.Name = "label";
            labelColumn.DataPropertyName = "label";
            labelColumn.HeaderText = Strings.Label;
            labelColumn.Width = 223;

            propertyColumn = new DataGridViewTextBoxColumn();
            propertyColumn.Name = "property";
            propertyColumn.DataPropertyName = "property";
            propertyColumn.HeaderText = Strings.Property;
            propertyColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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

            // cf. https://stackoverflow.com/questions/6644837/selectedvaluechange-event-firing-during-form-load-in-a-windows-form-applicatio

            entityComboBox.SelectedValueChanged -= EntityComboBox_SelectedValueChanged;

            entityComboBox.DataSource = Defs.GetEntityReferenceList();
            entityComboBox.ValueMember = "Key";
            entityComboBox.DisplayMember = "Value";

            entityComboBox.SelectedValueChanged += EntityComboBox_SelectedValueChanged;

            labelComboBox.SelectedValueChanged -= LabelComboBox_SelectedValueChanged;

            labelComboBox.DataSource = Defs.GetLabelReferenceList();
            labelComboBox.ValueMember = "Key";
            labelComboBox.DisplayMember = "Value";

            labelComboBox.SelectedValueChanged += LabelComboBox_SelectedValueChanged;
        }

        private void EnableLineButton_Click(object sender, EventArgs e)
        {
            this.entityComboBox.Enabled = true;
        }

        private void EntityComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((string)entityComboBox.SelectedValue != Defs.EMPTY_ITEM_STRING)
            {
                EntityReference er = new EntityReference { Entity = (string)entityComboBox.SelectedValue };
                propertyComboBox.DataSource = Defs.GetPropertyReferenceListByEntity(er);
                propertyComboBox.ValueMember = "Key";
                propertyComboBox.DisplayMember = "Value";

                this.labelComboBox.Enabled = true;
                this.addButton.Enabled = true;
            }
            else
            {
                propertyComboBox.DataSource = null;

                this.labelComboBox.Enabled = false;
                this.addButton.Enabled = false;
            }
        }

        private void LabelComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((short)labelComboBox.SelectedValue == (short)LabelId.PropertyDifference)
            {
                this.propertyComboBox.Enabled = true;
            }
            else
            {
                this.propertyComboBox.Enabled = false;
                this.propertyComboBox.SelectedValue = Defs.EMPTY_ITEM_STRING;
            }
        }

        private bool ValidateData()
        {
            bool result = true;
            string message = "";

            if ((short)labelComboBox.SelectedValue == (short)LabelId.PropertyDifference && (string)propertyComboBox.SelectedValue == Defs.EMPTY_ITEM_STRING)
            {
                result = false;
                message += "- " + "Nom de propriété obligatoire dans ce contexte" + Environment.NewLine;
            }

            if (!result)
            {
                Defs.ValidatingErrorDialog(message);
            }
            return result;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (! ValidateData())
            {
                return;
            }

            string entity = null;
            short? labelId = null;
            string property = null;

            if ((string)entityComboBox.SelectedValue != Defs.EMPTY_ITEM_STRING)
            {
                entity = (string)entityComboBox.SelectedValue;
                if ((short)labelComboBox.SelectedValue != Defs.EMPTY_ITEM_SHORT)
                {
                    labelId = (short)labelComboBox.SelectedValue;
                    if ((string)propertyComboBox.SelectedValue != Defs.EMPTY_ITEM_STRING)
                    {
                        property = (string)propertyComboBox.SelectedValue;
                    }

                }
            }
            FilterSetting fs = new FilterSetting(this._comparisonSet.Uid, entity, labelId, property);

            try
            {
                DaoFactory.Instance.GetFilterSettingDao().Add(fs);
                entityComboBox.Enabled = false;
                labelComboBox.Enabled = false;
                propertyComboBox.Enabled = false;
                addButton.Enabled = false;
                RunLookup();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Strings.ExandasOracleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteToolStripButton_Click(object sender, EventArgs e)
        {
            if (mainDataGridView.SelectedRows.Count == 0) return;
            DataGridViewRow row = mainDataGridView.SelectedRows[0];
            if (row != null)
            {
                try
                {
                    int id = (int)row.Cells[idColumn.Name].Value;
                    FilterSetting fs = DaoFactory.Instance.GetFilterSettingDao().Get(id);

                    string record = string.Format("[ {0} ]", fs.Entity);
                    if (Defs.ConfirmDeleteDialog(record))
                    {
                        DaoFactory.Instance.GetFilterSettingDao().Delete(fs);
                        RunLookup();
                    }
                }
                catch (Exception ex)
                {
                    Defs.ErrorDialog(ex.Message);
                }
            }
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

    }
}