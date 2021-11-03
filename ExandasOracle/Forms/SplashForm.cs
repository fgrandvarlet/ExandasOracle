using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace ExandasOracle.Forms
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();

            int width = 500;
            int height = 280;
            this.Size = new Size(width, height);

            this.splashPictureBox.Width = (int)((double)1280 / 1165 * this.splashPictureBox.Height);

            this.titleLabel1.Height = 80;
            this.titleLabel2.Height = 60;

            this.versionLabel.Text = string.Format("Version {0}", AssemblyVersion);
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

    }
}
