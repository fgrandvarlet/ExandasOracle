using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Components;
using ExandasOracle.Core;
using ExandasOracle.Dao;
using ExandasOracle.Domain;
using ExandasOracle.Native;
using ExandasOracle.Properties;

// TODO quid ExecuteReaderAsync (méthodes asynchrones)
// TODO quid procédure de maintenance des statistiques
//      cf. https://stackoverflow.com/questions/882223/does-firebird-need-manual-reindexing
// TODO internationalisation traduction IHM en anglais i18n
// TODO Exandas.Oracle.Admin pour purger la base de données locale ?
// TODO compléter les propriétés de l'objet de domaine Table
// TODO contrainte de clé étrangère ON DELETE CASCADE entre delta_report et comparison_set
// TODO utiliser un mot de passe différent du défaut (masterkey) pour SYSDBA ?? pertinent ??
// TODO utilitaire de sauvegarde et de purge de la base de données locale
// TODO REGLER TAB ORDER VIA PROPERTIES DANS TOUTES LES FORMS
// TODO vérifier unicité trigger_name (essayer de créer un 2ème trigger du même nom dans un même schéma sur un table différente)

namespace ExandasOracle.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainForm : Form
    {
        Dictionary<int, Action> _menuActionDict;

        UserControl currentControl;
        ConnectionParamsListPanel connectionParamsListPanel;
        ComparisonSetListPanel comparisonSetListPanel;

        const int _ID_CLOSE = 11;
        const int _ID_CONFIGURATION = 12;
        const int _ID_APROPOS = 13;
        const int _ID_CONNECTION_LIST = 101;
        const int _ID_COMPARISON_SET_LIST = 102;
        const int _ID_DELEGATE_FORM = 901;

        /// <summary>
        /// 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            
            this.Size = new Size(1300, 900);

            this._menuActionDict = BuildMenuActionDict();

            quitToolStripMenuItem.Tag = _ID_CLOSE;
            // TODO SUPPRIMER
            //configurationToolStripMenuItem.Tag = _ID_CONFIGURATION;
            connectionsLinkLabel.Tag = _ID_CONNECTION_LIST;
            comparisonSetsLinkLabel.Tag = _ID_COMPARISON_SET_LIST;
            aboutToolStripMenuItem.Tag = _ID_APROPOS;
            delegateFormsToolStripMenuItem.Tag = _ID_DELEGATE_FORM;

            // localization
            fileToolStripMenuItem.Text = Strings.File;
            helpToolStripMenuItem.Text = Strings.Help;
            connectionsLinkLabel.Text = Strings.ServerConnections;
            comparisonSetsLinkLabel.Text = Strings.ComparisonSets;

            Thread.Sleep(3000);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// 
        /// </summary>
        void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            // get our current "TopMost" value (ours will always be false though)
            bool top = TopMost;
            // make our form jump to the top of everything
            TopMost = true;
            // set it back to whatever it was
            TopMost = top;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = Defs.APPLICATION_TITLE;
            try
            {
                DaoFactory.Instance.Initialization();
                DoActionConnectionList();
                Activate();
            }
            catch (Exception ex)
            {
                Activate();
                // TODO TRADUIRE Defs.CAPTION_ERROR
                MessageBox.Show(ex.Message, Defs.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Dictionary<int, Action> BuildMenuActionDict()
        {
            var dict = new Dictionary<int, Action>();

            dict.Add(
                _ID_CLOSE,
                new Action(DoActionClose)
            );

            // TODO SUPPRIMER
            /*
            dict.Add(
                _ID_CONFIGURATION,
                new Action(DoActionConfiguration)
            );
            */
            dict.Add(
                _ID_APROPOS,
                new Action(DoActionAPropos)
            );
            dict.Add(
                _ID_CONNECTION_LIST,
                new Action(DoActionConnectionList)
            );
            dict.Add(
                _ID_COMPARISON_SET_LIST,
                new Action(DoActionComparisonSetList)
            );
            // TODO TEMP
            dict.Add(
                _ID_DELEGATE_FORM,
                new Action(DoActionDelegateForm)
            );

            return dict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userControl"></param>
        void ClearAndSwitch(UserControl userControl)
        {
            if (userControl == null || userControl == currentControl) return;

            NativeMethods.LockWindowUpdate(this.Handle);

            foreach (Control control in mainPanel.Controls)
            {
                if (userControl != control) mainPanel.Controls.Remove(control);
            }
            mainPanel.Controls.Add(userControl);
            userControl.Dock = DockStyle.Fill;
            currentControl = userControl;

            Refresh();
            NativeMethods.LockWindowUpdate(IntPtr.Zero);
        }

        void MenuAction_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            if (item != null)
            {
                if (item.Tag != null)
                {
                    int key = (int)item.Tag;
                    Action action = _menuActionDict[key];
                    action();
                }
            }
        }

        #region méthodes actions de menu

        void DoActionClose()
        {
            Close();
        }

        /*
        void DoActionConfiguration()
        {
            var credentials = DaoFactory.Instance.GetConfigurationDao().GetCredentials();
            using (var frm = new ConfigurationForm(credentials))
            {
                frm.ShowDialog();
            }
        }
        */

        void DoActionAPropos()
        {
            using (var frm = new AboutBox())
            {
                frm.ShowDialog(this);
            }
        }
        void DoActionConnectionList()
        {
            if (connectionParamsListPanel == null) connectionParamsListPanel = new ConnectionParamsListPanel();
            ClearAndSwitch(connectionParamsListPanel);
        }
        void DoActionComparisonSetList()
        {
            if (comparisonSetListPanel == null) comparisonSetListPanel = new ComparisonSetListPanel();
            ClearAndSwitch(comparisonSetListPanel);
        }
        void DoActionDelegateForm()
        {
            using (var frm = new DelegateForm())
            {
                frm.ShowDialog(this);
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // TODO SUPPRIMER
            var cs = DaoFactory.Instance.LocalConnectionString;

            /*
            using (var con = new FbConnection(cs))
            {
                con.Open();
                MessageBox.Show("Ok");
            }
            */

            var cp = new ConnectionParams();
            cp.Uid = Guid.NewGuid();
            cp.Name = "MA CONNEXION";
            cp.User = "MONUSER";
            cp.Password = "secret";
            cp.Host = "localhost";
            cp.Port = 1521;
            cp.SID = "ALPHA";
            cp.Service = "monservice";
            cp.DBAViews = true;

            var dao = DaoFactory.Instance.GetConnectionParamsDao();
            dao.Add(cp);
            MessageBox.Show("Ok");
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new AboutBox())
            {
                frm.ShowDialog(this);
            }
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var label = sender as LinkLabel;
            if (label != null)
            {
                if (label.Tag != null)
                {
                    int key = (int)label.Tag;
                    Action action = _menuActionDict[key];
                    action();
                }
            }
        }
    }
}
