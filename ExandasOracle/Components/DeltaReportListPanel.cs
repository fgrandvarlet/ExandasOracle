using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;

using OfficeOpenXml;

using ExandasOracle.Core;
using ExandasOracle.Dao;
using ExandasOracle.Domain;
using ExandasOracle.Reporting;

namespace ExandasOracle.Components
{
    /// <summary>
    /// 
    /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparisonSet"></param>
        public DeltaReportListPanel(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            // TODO CHANGER
            //this.titleLabel.Text = Defs.TITLE_LIST_CONNECTION_PARAMS;

            this._comparisonSet = comparisonSet;
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
            entityColumn.HeaderText = "Entité";
            entityColumn.Width = 150;

            objectColumn = new DataGridViewTextBoxColumn();
            objectColumn.Name = "object";
            objectColumn.DataPropertyName = "object";
            objectColumn.HeaderText = "Nom d'objet";
            objectColumn.Width = 150;

            parentObjectColumn = new DataGridViewTextBoxColumn();
            parentObjectColumn.Name = "parent_object";
            parentObjectColumn.DataPropertyName = "parent_object";
            parentObjectColumn.HeaderText = "Objet parent";
            parentObjectColumn.Width = 150;

            labelColumn = new DataGridViewTextBoxColumn();
            labelColumn.Name = "label";
            labelColumn.DataPropertyName = "label";
            labelColumn.HeaderText = "Libellé";
            labelColumn.Width = 150;

            propertyColumn = new DataGridViewTextBoxColumn();
            propertyColumn.Name = "property";
            propertyColumn.DataPropertyName = "property";
            propertyColumn.HeaderText = "Propriété";
            propertyColumn.Width = 150;

            sourceColumn = new DataGridViewTextBoxColumn();
            sourceColumn.Name = "source";
            sourceColumn.DataPropertyName = "source";
            sourceColumn.HeaderText = "Valeur source";
            sourceColumn.Width = 150;

            targetColumn = new DataGridViewTextBoxColumn();
            targetColumn.Name = "target";
            targetColumn.DataPropertyName = "target";
            targetColumn.HeaderText = "Valeur cible";
            targetColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewColumn[] cols = { idColumn, entityColumn, objectColumn, parentObjectColumn, labelColumn, propertyColumn, sourceColumn, targetColumn };
            mainDataGridView.Columns.AddRange(cols);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeltaReportListPanel_Load(object sender, EventArgs e)
        {
            InitMainDataGridView();
            LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
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

        private void Button1_Click(object sender, EventArgs e)
        {
            // TODO
            // https://github.com/EPPlusSoftware/EPPlus/wiki/Getting-Started
            // https://github.com/EPPlusSoftware/EPPlus/wiki/LoadFromDataReader

            /*
            //Creates a blank workbook. Use the using statment, so the package is disposed when we are done.
            using (var p = new ExcelPackage())
            {
                //A workbook must have at least on cell, so lets add one... 
                var ws = p.Workbook.Worksheets.Add("MySheet");
                //To set values in the spreadsheet use the Cells indexer.
                ws.Cells["A1"].Value = "This is cell A1";
                //Save the new workbook. We haven't specified the filename so use the Save as method.
                p.SaveAs(new FileInfo(@"myworkbook.xlsx"));
            }
            */
            ReportUtils.ExportToExcel(this._comparisonSet);
        }
    }
}
