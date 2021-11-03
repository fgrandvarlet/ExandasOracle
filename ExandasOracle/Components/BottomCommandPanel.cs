using System;
using System.Windows.Forms;

using ExandasOracle.Forms;
using ExandasOracle.Properties;

namespace ExandasOracle.Components
{
    public partial class BottomCommandPanel : UserControl
    {
        IDataFormManager _dataFormManager;

        public BottomCommandPanel(IDataFormManager dataFormManager)
        {
            InitializeComponent();

            this._dataFormManager = dataFormManager;
            if (this._dataFormManager != null)
            {
                this._dataFormManager.Parent.AcceptButton = this.doOkButton;
                this._dataFormManager.Parent.CancelButton = this.doCancelButton;
            }

            // localization
            this.doCancelButton.Text = Strings.Cancel;
            this.doApplyButton.Text = Strings.Apply;
        }

        private void DoOkButton_Click(object sender, EventArgs e)
        {
            if (_dataFormManager != null)
            {
                if (_dataFormManager.Inserting || _dataFormManager.Updating)
                {
                    if (!_dataFormManager.ValidateDataForm())
                    {
                        _dataFormManager.Parent.DialogResult = DialogResult.None;
                        return;
                    }
                    if (!_dataFormManager.SaveData())
                    {
                        _dataFormManager.Parent.DialogResult = DialogResult.None;
                    }
                }
            }
        }

        private void DoApplyButton_Click(object sender, EventArgs e)
        {
            if (_dataFormManager != null)
            {
                if (_dataFormManager.Inserting || _dataFormManager.Updating)
                {
                    if (!_dataFormManager.ValidateDataForm())
                    {
                        return;
                    }
                    _dataFormManager.SaveData();
                    doCancelButton.DialogResult = DialogResult.OK;
                }
            }
        }

    }
}
