using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ExandasOracle.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataFormManager
    {
        /// <summary>
        /// 
        /// </summary>
        Form Parent { get; }

        /// <summary>
        /// 
        /// </summary>
        bool Inserting { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        bool Updating { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool Updated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        void DataToForm();
        
        /// <summary>
        /// 
        /// </summary>
        void FormToData();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool ValidateDataForm();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool SaveData();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataChanged(object sender, EventArgs e);

    }
}
