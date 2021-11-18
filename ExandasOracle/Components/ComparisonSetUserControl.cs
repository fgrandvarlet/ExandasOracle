using System.Windows.Forms;

using ExandasOracle.Properties;

namespace ExandasOracle.Components
{
    public partial class ComparisonSetUserControl : UserControl
    {
        public ComparisonSetUserControl()
        {
            InitializeComponent();

            // localization
            this.userLabel.Text = Strings.UserName;
            this.hostLabel.Text = Strings.Hostname;
            this.serviceRadioButton.Text = Strings.ServiceName;
            this.schemaLabel.Text = Strings.Schema;
        }

    }
}
