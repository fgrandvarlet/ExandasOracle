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
    public partial class ConnectionParamsForm : Form, IDataFormManager
    {
        IDataFormManager _dataFormManager;
        ConnectionParams _connectionParams;
        bool _inserting;
        bool _updating;
        bool _updated;
        TitlePanel titlePanel;
        BottomCommandPanel bottomCommandPanel;

        public ConnectionParamsForm(ConnectionParams connectionParams)
        {
            InitializeComponent();

            this.Size = new Size(620, 520);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this._dataFormManager = (IDataFormManager)this;
            this._connectionParams = connectionParams;
            this._inserting = (this._connectionParams == null);

            // localization
            this.nameLabel.Text = Strings.ConnectionName;
            this.userLabel.Text = Strings.UserName;
            this.passwordLabel.Text = Strings.Password;
            this.hostLabel.Text = Strings.Hostname;
            this.serviceRadioButton.Text = Strings.ServiceName;
            this.DBAViewsCheckBox.Text = Strings.DBAViews;
            this.checkConnectionButton.Text = Strings.CheckingTheConnection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionParamsForm_Load(object sender, EventArgs e)
        {
            Text = Defs.APPLICATION_TITLE;

            titlePanel = new TitlePanel();
            titlePanel.Parent = topPanel;
            titlePanel.Dock = DockStyle.Fill;
            topPanel.Height = 62;
            titlePanel.titleLabel.Text = Strings.ServerConnectionDetail;

            bottomCommandPanel = new BottomCommandPanel(this);
            bottomCommandPanel.Parent = bottomPanel;
            bottomCommandPanel.Dock = DockStyle.Fill;
            bottomPanel.Height = 38;

            if (!this._inserting)
            {
                this._dataFormManager.DataToForm();
            }
            else
            {
                hostTextBox.Text = "localhost";
                portTextBox.Text = "1521";
                SIDTextBox.Text = "xe";
                SIDRadioButton.Checked = true;
            }

            // event handlers
            nameTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            userTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            passwordTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            hostTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            portTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            SIDTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            serviceTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            DBAViewsCheckBox.CheckedChanged += new EventHandler(this._dataFormManager.DataChanged);
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
            nameTextBox.Text = _connectionParams.Name;
            userTextBox.Text = _connectionParams.User;
            passwordTextBox.Text = _connectionParams.DecryptedPassword;
            hostTextBox.Text = _connectionParams.Host;
            portTextBox.Text = _connectionParams.Port.ToString();

            if (_connectionParams.SID != null)
            {
                SIDRadioButton.Checked = true;
                SIDTextBox.Text = _connectionParams.SID;
            }
            else
            {
                serviceRadioButton.Checked = true;
                serviceTextBox.Text = _connectionParams.Service;
            }

            if (_connectionParams.DBAViews)
            {
                DBAViewsCheckBox.CheckState = CheckState.Checked;
            }
            else
            {
                DBAViewsCheckBox.CheckState = CheckState.Unchecked;
            }
        }

        public void FormToData()
        {
            _connectionParams.Name = nameTextBox.Text.Trim();
            _connectionParams.User = userTextBox.Text.Trim();
            _connectionParams.DecryptedPassword = passwordTextBox.Text.Trim();
            _connectionParams.Host = hostTextBox.Text.Trim();

            if (int.TryParse(portTextBox.Text.Trim(), out int result))
            {
                _connectionParams.Port = result;
            }

            if (SIDRadioButton.Checked)
            {
                _connectionParams.SID = SIDTextBox.Text.Trim();
                _connectionParams.Service = null;
            }
            else
            {
                _connectionParams.SID = null;
                _connectionParams.Service = serviceTextBox.Text.Trim();
            }

            _connectionParams.DBAViews = DBAViewsCheckBox.Checked;
        }

        public bool ValidateDataForm()
        {
            bool result = true;
            string message = "";

            if (nameTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- " + Strings.ConnectionNameRequired + Environment.NewLine;
            }
            if (userTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- " + Strings.UserNameRequired + Environment.NewLine;
            }
            if (passwordTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- " + Strings.PasswordRequired + Environment.NewLine;
            }
            if (hostTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- " + Strings.HostnameRequired + Environment.NewLine;
            }
            if (portTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- " + Strings.PortRequired + Environment.NewLine;
            }
            if (SIDTextBox.Text.Trim().Length == 0 && serviceTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- " + Strings.SIDOrServiceNameRequired + Environment.NewLine;
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
                    _connectionParams = new ConnectionParams();
                    _connectionParams.Uid = Guid.NewGuid();
                    _dataFormManager.FormToData();
                    DaoFactory.Instance.GetConnectionParamsDao().Add(_connectionParams);
                }
                else
                {
                    _dataFormManager.FormToData();
                    DaoFactory.Instance.GetConnectionParamsDao().Save(_connectionParams);
                }
                _dataFormManager.Inserting = false;
                _dataFormManager.Updating = false;
                bottomCommandPanel.doApplyButton.Enabled = false;
                _dataFormManager.Updated = true;
                result = true;
            }
            catch (Exception ex)
            {
                if (_dataFormManager.Inserting)
                {
                    _connectionParams = null;
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

        private void SIDRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SIDRadioButton.Checked)
            {
                SIDTextBox.Enabled = true;
                serviceTextBox.Enabled = false;
            }
        }

        private void ServiceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (serviceRadioButton.Checked)
            {
                SIDTextBox.Enabled = false;
                serviceTextBox.Enabled = true;
            }
        }

        private void CheckConnectionButton_Click(object sender, EventArgs e)
        {
            if (_dataFormManager.ValidateDataForm())
            {
                if (_connectionParams == null)
                {
                    _connectionParams = new ConnectionParams();
                }
                _dataFormManager.FormToData();
                try
                {
                    using (var wc = new WaitCursor())
                    {
                        DaoFactory.GetRemoteDao(_connectionParams.ConnectionString).CheckConnection(_connectionParams.DBAViews);
                    }
                    checkConnectionButton.BackColor = Color.LightGreen;
                    MessageBox.Show(
                        Strings.SuccessfulConnection,
                        Strings.CheckingTheConnection,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception ex)
                {
                    checkConnectionButton.BackColor = Color.Salmon;
                    MessageBox.Show(
                        Strings.ConnectionFailed + Environment.NewLine + ex.Message,
                        Strings.CheckingTheConnection,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

    }
}
