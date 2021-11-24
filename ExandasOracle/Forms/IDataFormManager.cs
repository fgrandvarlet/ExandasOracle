using System;
using System.Windows.Forms;

namespace ExandasOracle.Forms
{
    public interface IDataFormManager
    {
        Form Parent { get; }
        bool Inserting { get; set; }
        bool Updating { get; set; }
        bool Updated { get; set; }

        void DataToForm();
        
        void FormToData();

        bool ValidateDataForm();

        bool SaveData();

        void DataChanged(object sender, EventArgs e);

    }
}
