using System;
using System.Windows.Forms;

using ExandasOracle.Dao;
using ExandasOracle.Domain;

namespace ExandasOracle.Forms
{
    // TODO SUPPRIMER A LA FIN
    public partial class DebugForm : Form
    {
        ComparisonSet _comparisonSet;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparisonSet"></param>
        public DebugForm(ComparisonSet comparisonSet)
        {
            InitializeComponent();

            this._comparisonSet = comparisonSet;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            //MessageBox.Show(this._comparisonSet.ToString());
            this._comparisonSet.Connection1 = DaoFactory.Instance.GetConnectionParamsDao().Get(this._comparisonSet.Connection1Uid);
            this._comparisonSet.Connection2 = DaoFactory.Instance.GetConnectionParamsDao().Get(this._comparisonSet.Connection2Uid);

            var dao = DaoFactory.GetRemoteDao(this._comparisonSet.Connection1.ConnectionString);
            var conn = dao.GetOracleConnection();
            try
            {
                conn.Open();

                var list = dao.GetTableList(conn, this._comparisonSet.Schema1, this._comparisonSet.Connection1.DBAViews);
                foreach (Table t in list)
                {
                    listBox1.Items.Add(t.TableName + " - " + t.TablespaceName);
                }

                var list2 = dao.GetTableColumnList(conn, this._comparisonSet.Schema1, this._comparisonSet.Connection1.DBAViews);
                foreach (TableColumn tc in list2)
                {
                    listBox1.Items.Add(tc.TableName + " - " + tc.ColumnName + " - " + tc.DataDefault);
                }

                MessageBox.Show("avant connection.close()");
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            var loader = new MetaDataLoader(this._comparisonSet);
            loader.Execute();
            MessageBox.Show("Exécution terminée");
            */
        }
    }
}
