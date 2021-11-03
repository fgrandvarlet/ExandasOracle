using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ExandasOracle.Properties;


namespace ExandasOracle.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DelegateForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public DelegateForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            //string a = "oops";
            //string b = "oops";

            decimal? a = null;
            decimal? b = null;

            bool result = a.Equals(b);
            MessageBox.Show("résultat = " + result);
            */
            var s = Strings.Password;
            MessageBox.Show(s);
        }
    }
}
