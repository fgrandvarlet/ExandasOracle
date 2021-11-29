using System;
using System.Drawing;
using System.Windows.Forms;

using ExandasOracle.Components;
using ExandasOracle.Core;
using ExandasOracle.Dao;
using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Forms
{
    public partial class ComparisonSetForm : Form, IDataFormManager
    {
        IDataFormManager _dataFormManager;
        ComparisonSet _comparisonSet;
        bool _inserting;
        bool _updating;
        bool _updated;
        TitlePanel titlePanel;
        BottomCommandPanel bottomCommandPanel;

        public ComparisonSetForm(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            this.Size = new Size(980, 670);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this._dataFormManager = (IDataFormManager)this;
            this._comparisonSet = comparisonSet;
            this._inserting = (this._comparisonSet == null);

            this.comparisonSetUserControl1.connectionLabel.Text = Strings.SourceServerConnection;
            this.comparisonSetUserControl2.connectionLabel.Text = Strings.TargetServerConnection;
            this.comparisonSetUserControl1.connectionLabel.ForeColor = Color.DarkGreen;
            this.comparisonSetUserControl2.connectionLabel.ForeColor = Color.DarkRed;

            // localization
            this.nameLabel.Text = Strings.ComparisonSetName;
            this.lastReportTimeLabel.Text = Strings.LastComparisonReport;
            this.filteringButton.Text = Strings.FilterSettings;
            this.deltaReportButton.Text = Strings.ViewComparisonReport;
            this.generateReportButton.Text = Strings.GenerateComparisonReport;
        }

        private void ComparisonSetForm_Load(object sender, EventArgs e)
        {
            Text = Defs.APPLICATION_TITLE;

            titlePanel = new TitlePanel();
            titlePanel.Parent = topPanel;
            titlePanel.Dock = DockStyle.Fill;
            topPanel.Height = 62;
            titlePanel.titleLabel.Text = Strings.ComparisonSetDetail;

            bottomCommandPanel = new BottomCommandPanel(this);
            bottomCommandPanel.Parent = bottomPanel;
            bottomCommandPanel.Dock = DockStyle.Fill;
            bottomPanel.Height = 38;

            innerBottomPanel.Height = 100;

            // initialisation listes déroulantes
            comparisonSetUserControl1.connectionComboBox.DataSource = Defs.GetConnectionReferenceList();
            comparisonSetUserControl1.connectionComboBox.ValueMember = "Key";
            comparisonSetUserControl1.connectionComboBox.DisplayMember = "Value";

            comparisonSetUserControl2.connectionComboBox.DataSource = Defs.GetConnectionReferenceList();
            comparisonSetUserControl2.connectionComboBox.ValueMember = "Key";
            comparisonSetUserControl2.connectionComboBox.DisplayMember = "Value";

            if (!this._inserting)
            {
                this._dataFormManager.DataToForm();
            }
            else
            {
                deltaReportButton.Enabled = false;
                filteringButton.Enabled = false;
            }

            // gestionnaires d'évènement
            nameTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            comparisonSetUserControl1.connectionComboBox.SelectedIndexChanged += new EventHandler(this._dataFormManager.DataChanged);
            comparisonSetUserControl1.connectionComboBox.SelectedIndexChanged += new EventHandler(this.ConnectionComboBoxDataChanged);
            comparisonSetUserControl2.connectionComboBox.SelectedIndexChanged += new EventHandler(this._dataFormManager.DataChanged);
            comparisonSetUserControl2.connectionComboBox.SelectedIndexChanged += new EventHandler(this.ConnectionComboBoxDataChanged);
            comparisonSetUserControl1.schemaTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            comparisonSetUserControl2.schemaTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
        }

        Form IDataFormManager.Parent
        {
            get { return this; }
        }

        public bool Inserting
        {
            get { return _inserting; }
            set { _inserting = value; }
        }

        public bool Updating
        {
            get { return _updating; }
            set { _updating = value; }
        }

        public bool Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }

        public void DataToForm()
        {
            nameTextBox.Text = _comparisonSet.Name;
            if (_comparisonSet.LastReportTime.HasValue)
            {
                lastReportTimeTextBox.Text = _comparisonSet.LastReportTime.Value.ToString("f");
            }
            
            comparisonSetUserControl1.connectionComboBox.SelectedValue = _comparisonSet.Connection1Uid;
            comparisonSetUserControl2.connectionComboBox.SelectedValue = _comparisonSet.Connection2Uid;

            ConnectionComboBoxDataChanged(comparisonSetUserControl1.connectionComboBox, null);
            ConnectionComboBoxDataChanged(comparisonSetUserControl2.connectionComboBox, null);

            comparisonSetUserControl1.schemaTextBox.Text = _comparisonSet.Schema1;
            comparisonSetUserControl2.schemaTextBox.Text = _comparisonSet.Schema2;

            deltaReportButton.Enabled = _comparisonSet.LastReportTime != null;
        }

        public void FormToData()
        {
            _comparisonSet.Name = nameTextBox.Text.Trim();

            _comparisonSet.Connection1Uid = (Guid)comparisonSetUserControl1.connectionComboBox.SelectedValue;
            _comparisonSet.Connection2Uid = (Guid)comparisonSetUserControl2.connectionComboBox.SelectedValue;

            _comparisonSet.Schema1 = comparisonSetUserControl1.schemaTextBox.Text.Trim();
            _comparisonSet.Schema2 = comparisonSetUserControl2.schemaTextBox.Text.Trim();
         }

        public bool ValidateDataForm()
        {
            bool result = true;
            string message = "";

            if (nameTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- " + Strings.ComparisonSetNameRequired + Environment.NewLine;
            }

            var v = (Guid)comparisonSetUserControl1.connectionComboBox.SelectedValue;
            if (v == Defs.EMPTY_ITEM_GUID)
            {
                result = false;
                message += "- " + Strings.SourceServerConnectionRequired + Environment.NewLine;
            }

            v = (Guid)comparisonSetUserControl2.connectionComboBox.SelectedValue;
            if (v == Defs.EMPTY_ITEM_GUID)
            {
                result = false;
                message += "- " + Strings.TargetServerConnectionRequired + Environment.NewLine;
            }

            if (comparisonSetUserControl1.schemaTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- " + Strings.SourceSchemaRequired + Environment.NewLine;
            }

            if (comparisonSetUserControl2.schemaTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- " + Strings.TargetSchemaRequired + Environment.NewLine;
            }

            if (!result)
            {
                Defs.ValidatingErrorDialog(message);
            }
            return result;
        }

        public bool SaveData()
        {
            bool result = false;
            try
            {
                if (_dataFormManager.Inserting)
                {
                    _comparisonSet = new ComparisonSet();
                    _comparisonSet.Uid = Guid.NewGuid();
                    _dataFormManager.FormToData();
                    DaoFactory.Instance.GetComparisonSetDao().Add(_comparisonSet);
                }
                else
                {
                    _dataFormManager.FormToData();
                    DaoFactory.Instance.GetComparisonSetDao().Save(_comparisonSet);
                }
                _dataFormManager.Inserting = false;
                _dataFormManager.Updating = false;
                bottomCommandPanel.doApplyButton.Enabled = false;
                _dataFormManager.Updated = true;
                filteringButton.Enabled = true;
                result = true;
            }
            catch (Exception ex)
            {
                if (_dataFormManager.Inserting)
                {
                    _comparisonSet = null;
                }
                Defs.ErrorDialog(ex.Message);
            }
            return result;
        }

        public void DataChanged(object sender, EventArgs e)
        {
            _dataFormManager.Updating = true;
            bottomCommandPanel.doApplyButton.Enabled = true;
        }

        void ConnectionComboBoxDataChanged(object sender, EventArgs e)
        {
            if (sender == comparisonSetUserControl1.connectionComboBox)
            {
                var uid = (Guid)comparisonSetUserControl1.connectionComboBox.SelectedValue;
                if (uid == Defs.EMPTY_ITEM_GUID)
                {
                    comparisonSetUserControl1.userTextBox.Text = null;
                    comparisonSetUserControl1.hostTextBox.Text = null;
                    comparisonSetUserControl1.portTextBox.Text = null;
                    comparisonSetUserControl1.SIDTextBox.Text = null;
                    comparisonSetUserControl1.serviceTextBox.Text = null;
                    comparisonSetUserControl1.SIDRadioButton.Checked = false;
                    comparisonSetUserControl1.serviceRadioButton.Checked = false;
                    comparisonSetUserControl1.DBAViewsCheckBox.CheckState = CheckState.Indeterminate;
                }
                else
                {
                    var cp = DaoFactory.Instance.GetConnectionParamsDao().Get(uid);
                    if (cp != null)
                    {
                        comparisonSetUserControl1.userTextBox.Text = cp.User;
                        comparisonSetUserControl1.hostTextBox.Text = cp.Host;
                        comparisonSetUserControl1.portTextBox.Text = cp.Port.ToString();
                        comparisonSetUserControl1.SIDTextBox.Text = cp.SID;
                        comparisonSetUserControl1.serviceTextBox.Text = cp.Service;
                        if (cp.SID != null)
                        {
                            comparisonSetUserControl1.SIDRadioButton.Checked = true;
                        }
                        else
                        {
                            comparisonSetUserControl1.serviceRadioButton.Checked = true;
                        }
                        comparisonSetUserControl1.DBAViewsCheckBox.Checked = cp.DBAViews;
                    }
                }
            }
            if (sender == comparisonSetUserControl2.connectionComboBox)
            {
                var uid = (Guid)comparisonSetUserControl2.connectionComboBox.SelectedValue;
                if (uid == Defs.EMPTY_ITEM_GUID)
                {
                    comparisonSetUserControl2.userTextBox.Text = null;
                    comparisonSetUserControl2.hostTextBox.Text = null;
                    comparisonSetUserControl2.portTextBox.Text = null;
                    comparisonSetUserControl2.SIDTextBox.Text = null;
                    comparisonSetUserControl2.serviceTextBox.Text = null;
                    comparisonSetUserControl2.SIDRadioButton.Checked = false;
                    comparisonSetUserControl2.serviceRadioButton.Checked = false;
                    comparisonSetUserControl2.DBAViewsCheckBox.CheckState = CheckState.Indeterminate;
                }
                else
                {
                    var cp = DaoFactory.Instance.GetConnectionParamsDao().Get(uid);
                    if (cp != null)
                    {
                        comparisonSetUserControl2.userTextBox.Text = cp.User;
                        comparisonSetUserControl2.hostTextBox.Text = cp.Host;
                        comparisonSetUserControl2.portTextBox.Text = cp.Port.ToString();
                        comparisonSetUserControl2.SIDTextBox.Text = cp.SID;
                        comparisonSetUserControl2.serviceTextBox.Text = cp.Service;
                        if (cp.SID != null)
                        {
                            comparisonSetUserControl2.SIDRadioButton.Checked = true;
                        }
                        else
                        {
                            comparisonSetUserControl2.serviceRadioButton.Checked = true;
                        }
                        comparisonSetUserControl2.DBAViewsCheckBox.Checked = cp.DBAViews;
                    }
                }
            }
        }

        private void GenerateReportButton_Click(object sender, EventArgs e)
        {
            if (_dataFormManager != null)
            {
                if (_dataFormManager.Inserting || _dataFormManager.Updating)
                {
                    if (!_dataFormManager.ValidateDataForm())
                    {
                        return;
                    }
                    _dataFormManager.SaveData();
                }
                using (var frm = new ProgressForm(this._comparisonSet))
                {
                    var dialogResult = frm.ShowDialog(this);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // mise à jour de la date du dernier rapport de comparaison dans le formulaire
                        var cs = DaoFactory.Instance.GetComparisonSetDao().Get(this._comparisonSet.Uid);
                        lastReportTimeTextBox.Text = cs.LastReportTime.Value.ToString("f");
                        this._comparisonSet.LastReportTime = cs.LastReportTime;
                        deltaReportButton.Enabled = _comparisonSet.LastReportTime != null;

                        using (var frmResult = new DeltaReportListForm(this._comparisonSet))
                        {
                            frmResult.ShowDialog(this);
                        }
                    }
                }
            }
        }

        private void DeltaReportButton_Click(object sender, EventArgs e)
        {
            using (var frm = new DeltaReportListForm(this._comparisonSet))
            {
                frm.ShowDialog(this);
            }
        }

        private void FilteringButton_Click(object sender, EventArgs e)
        {
            using (var frm = new FilterSettingListForm(this._comparisonSet))
            {
                frm.ShowDialog(this);
            }
        }
    }
}
