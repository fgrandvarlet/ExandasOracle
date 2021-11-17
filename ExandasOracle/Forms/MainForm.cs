using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        const int _ID_LOCAL_DATABASE_SIZE = 21;
        const int _ID_COMPACT_LOCAL_DATABASE = 22;
        const int _ID_ABOUT = 31;
        const int _ID_CONNECTION_LIST = 101;
        const int _ID_COMPARISON_SET_LIST = 102;

        public MainForm()
        {
            InitializeComponent();
            
            this.Size = new Size(1300, 900);

            this._menuActionDict = BuildMenuActionDict();

            quitToolStripMenuItem.Tag = _ID_CLOSE;
            localDatabaseSizeToolStripMenuItem.Tag = _ID_LOCAL_DATABASE_SIZE;
            compactLocalDatabaseToolStripMenuItem.Tag = _ID_COMPACT_LOCAL_DATABASE;
            aboutToolStripMenuItem.Tag = _ID_ABOUT;
            connectionsLinkLabel.Tag = _ID_CONNECTION_LIST;
            comparisonSetsLinkLabel.Tag = _ID_COMPARISON_SET_LIST;
            
            // localization
            fileToolStripMenuItem.Text = Strings.File;
            quitToolStripMenuItem.Text = Strings.Quit;
            helpToolStripMenuItem.Text = Strings.Help;
            aboutToolStripMenuItem.Text = Strings.AboutMenu;
            connectionsLinkLabel.Text = Strings.ServerConnections;
            comparisonSetsLinkLabel.Text = Strings.ComparisonSets;

            Thread.Sleep(3000);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

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
                MessageBox.Show(ex.Message, Strings.ExandasOracleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        Dictionary<int, Action> BuildMenuActionDict()
        {
            var dict = new Dictionary<int, Action>();

            dict.Add(_ID_CLOSE, new Action(DoActionClose));
            dict.Add(_ID_LOCAL_DATABASE_SIZE, new Action(DoActionLocalDatabaseSize));
            dict.Add(_ID_COMPACT_LOCAL_DATABASE, new Action(DoActionCompactLocalDatabase));
            dict.Add(_ID_ABOUT, new Action(DoActionAbout));
            dict.Add(_ID_CONNECTION_LIST, new Action(DoActionConnectionList));
            dict.Add(_ID_COMPARISON_SET_LIST, new Action(DoActionComparisonSetList));

            return dict;
        }

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

        #region menu actions methods

        void DoActionClose()
        {
            Close();
        }
        void DoActionLocalDatabaseSize()
        {
            var message = Strings.LocalDatabaseColon + Environment.NewLine +
                Path.GetFullPath(DaoFactory.Instance.LocalDatabasePath) + Environment.NewLine + Environment.NewLine +
                Strings.FileSizeColon + DaoFactory.Instance.LocalDatabaseSize;
            MessageBox.Show(message, Defs.APPLICATION_TITLE);
        }
        void DoActionCompactLocalDatabase()
        {
            using (var frm = new CompactLocalDatabaseForm())
            {
                frm.ShowDialog(this);
            }
        }
        void DoActionAbout()
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

        #endregion

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is LinkLabel label)
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
