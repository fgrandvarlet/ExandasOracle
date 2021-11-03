using System;
using System.Windows.Forms;

using ExandasOracle.Core;
using ExandasOracle.Dao;
using ExandasOracle.Domain;
using ExandasOracle.Forms;
using ExandasOracle.Properties;

namespace ExandasOracle.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ConnectionParamsListPanel : ListPanel
    {
        DataGridViewTextBoxColumn uidColumn;
        DataGridViewLinkColumn nameColumn;
        DataGridViewTextBoxColumn usernameColumn;
        DataGridViewTextBoxColumn hostColumn;
        DataGridViewTextBoxColumn portColumn;
        DataGridViewTextBoxColumn sidColumn;
        DataGridViewTextBoxColumn serviceColumn;

        /// <summary>
        /// 
        /// </summary>
        public ConnectionParamsListPanel()
        {
            InitializeComponent();

            this.titleLabel.Text = Strings.ServerConnectionsList;
        }

		/// <summary>
		/// 
		/// </summary>
		protected override void InitMainDataGridView()
		{
			uidColumn = new DataGridViewTextBoxColumn();
			uidColumn.Name = "uid";
			uidColumn.DataPropertyName = "uid";
			uidColumn.HeaderText = Strings.ConnectionID;
			uidColumn.Visible = false;

			nameColumn = new DataGridViewLinkColumn();
			nameColumn.Name = "name";
			nameColumn.DataPropertyName = "name";
			nameColumn.HeaderText = Strings.Name;
			nameColumn.SortMode = DataGridViewColumnSortMode.Automatic;
			nameColumn.TrackVisitedState = false;
			nameColumn.LinkBehavior = LinkBehavior.HoverUnderline;
			nameColumn.Width = 200;

			usernameColumn = new DataGridViewTextBoxColumn();
			usernameColumn.Name = "username";
			usernameColumn.DataPropertyName = "username";
			usernameColumn.HeaderText = Strings.User;
			usernameColumn.Width = 150;

			hostColumn = new DataGridViewTextBoxColumn();
			hostColumn.Name = "host";
			hostColumn.DataPropertyName = "host";
			hostColumn.HeaderText = Strings.Host;
			hostColumn.Width = 250;

			portColumn = new DataGridViewTextBoxColumn();
			portColumn.Name = "port";
			portColumn.DataPropertyName = "port";
			portColumn.HeaderText = "Port";
			portColumn.Width = 60;

			sidColumn = new DataGridViewTextBoxColumn();
			sidColumn.Name = "sid";
			sidColumn.DataPropertyName = "sid";
			sidColumn.HeaderText = "SID";
			sidColumn.Width = 100;

			serviceColumn = new DataGridViewTextBoxColumn();
			serviceColumn.Name = "service";
			serviceColumn.DataPropertyName = "service";
			serviceColumn.HeaderText = "Service";
			serviceColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			DataGridViewColumn[] cols = { uidColumn, nameColumn, usernameColumn, hostColumn, portColumn, sidColumn, serviceColumn};
			mainDataGridView.Columns.AddRange(cols);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="criteria"></param>
		protected override void LoadData(Criteria criteria)
		{
			mainDataGridView.DataSource = DaoFactory.Instance.GetConnectionParamsDao().GetDataTable(criteria);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void AddToolStripButton_Click(object sender, EventArgs e)
		{
			using (var frm = new ConnectionParamsForm(null))
			{
				DialogResult dr = frm.ShowDialog(this);
				if (dr == DialogResult.OK) RunLookup();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void ModifyToolStripButton_Click(object sender, EventArgs e)
		{
			if (mainDataGridView.SelectedRows.Count == 0) return;
			DataGridViewRow row = mainDataGridView.SelectedRows[0];
			if (row != null)
			{
				Guid uid = Guid.Parse(row.Cells[uidColumn.Name].Value.ToString());
				ConnectionParams cp = DaoFactory.Instance.GetConnectionParamsDao().Get(uid);
				using (var frm = new ConnectionParamsForm(cp))
				{
					DialogResult dr = frm.ShowDialog(this);
					if (dr == DialogResult.OK) RunLookup();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void DeleteToolStripButton_Click(object sender, EventArgs e)
		{
			if (mainDataGridView.SelectedRows.Count == 0) return;
			DataGridViewRow row = mainDataGridView.SelectedRows[0];
			if (row != null)
			{
				try
				{
					Guid uid = (Guid)row.Cells[uidColumn.Name].Value;
					ConnectionParams cp = DaoFactory.Instance.GetConnectionParamsDao().Get(uid);

					string enregistrement = string.Format("[ {0} ]", cp.Name);
					if (Defs.ConfirmDeleteDialog(enregistrement))
					{
						DaoFactory.Instance.GetConnectionParamsDao().Delete(cp);
						RunLookup();
					}
				}
				catch (Exception ex)
				{
					Defs.ErrorDialog(ex.Message);
				}
			}
		}

	}
}
