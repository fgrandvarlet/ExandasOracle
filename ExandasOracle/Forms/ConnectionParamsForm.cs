using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ExandasOracle.Components;
using ExandasOracle.Core;
using ExandasOracle.Dao;
using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ConnectionParamsForm : Form, IDataFormManager
    {
        // TODO REGLER TAB ORDER VIA PROPERTIES

        IDataFormManager _dataFormManager;
        ConnectionParams _connectionParams;
        bool _inserting;
        bool _updating;
        bool _updated;
        TitlePanel titlePanel;
        BottomCommandPanel bottomCommandPanel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionParams"></param>
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionParamsForm_Load(object sender, EventArgs e)
        {
            Text = Defs.TITLE_FORM_CONNECTION_PARAMS;

            titlePanel = new TitlePanel();
            titlePanel.Parent = topPanel;
            titlePanel.Dock = DockStyle.Fill;
            topPanel.Height = 62;
            titlePanel.titleLabel.Text = Defs.TITLE_FORM_CONNECTION_PARAMS;

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

            // gestionnaires d'évènement
            nameTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            userTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            passwordTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            hostTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            portTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            SIDTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            serviceTextBox.TextChanged += new EventHandler(this._dataFormManager.DataChanged);
            DBAViewsCheckBox.CheckedChanged += new EventHandler(this._dataFormManager.DataChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        Form IDataFormManager.Parent
        {
            get { return this; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Inserting
        {
            get { return _inserting; }
            set { _inserting = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Updating
        {
            get { return _updating; }
            set { _updating = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ValidateDataForm()
        {
            bool result = true;
            string message = "";

            if (nameTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- Nom de la connexion obligatoire" + Environment.NewLine;
            }
            if (userTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- Nom utilisateur obligatoire" + Environment.NewLine;
            }
            if (passwordTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- Mot de passe obligatoire" + Environment.NewLine;
            }
            if (hostTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- Hôte obligatoire" + Environment.NewLine;
            }
            if (portTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- Port obligatoire" + Environment.NewLine;
            }
            if (SIDTextBox.Text.Trim().Length == 0 && serviceTextBox.Text.Trim().Length == 0)
            {
                result = false;
                message += "- SID ou nom de service obligatoire" + Environment.NewLine;
            }
            if (DBAViewsCheckBox.CheckState == CheckState.Indeterminate)
            {
                result = false;
                message += "- Vues DBA doit être coché ou décoché" + Environment.NewLine;
            }

            if (!result)
            {
                Defs.ValidatingErrorDialog(message);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataChanged(object sender, EventArgs e)
        {
            _dataFormManager.Updating = true;
            bottomCommandPanel.doApplyButton.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SIDRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SIDRadioButton.Checked)
            {
                SIDTextBox.Enabled = true;
                serviceTextBox.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServiceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (serviceRadioButton.Checked)
            {
                SIDTextBox.Enabled = false;
                serviceTextBox.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        "Connexion réussie.",
                        "Vérification de la connexion",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception ex)
                {
                    checkConnectionButton.BackColor = Color.Salmon;
                    MessageBox.Show(
                        "Echec de la connexion." + Environment.NewLine + ex.Message,
                        "Vérification de la connexion",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }

        }

    }
}
