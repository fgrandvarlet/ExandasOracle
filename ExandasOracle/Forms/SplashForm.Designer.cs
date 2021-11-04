
namespace ExandasOracle.Forms
{
    partial class SplashForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fillPanel = new System.Windows.Forms.Panel();
            this.innerPanel = new System.Windows.Forms.Panel();
            this.versionLabel = new System.Windows.Forms.Label();
            this.titleLabel2 = new System.Windows.Forms.Label();
            this.titleLabel1 = new System.Windows.Forms.Label();
            this.splashPictureBox = new System.Windows.Forms.PictureBox();
            this.fillPanel.SuspendLayout();
            this.innerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splashPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // fillPanel
            // 
            this.fillPanel.BackColor = System.Drawing.Color.SteelBlue;
            this.fillPanel.Controls.Add(this.innerPanel);
            this.fillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillPanel.Location = new System.Drawing.Point(0, 0);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Padding = new System.Windows.Forms.Padding(16, 16, 16, 16);
            this.fillPanel.Size = new System.Drawing.Size(537, 335);
            this.fillPanel.TabIndex = 0;
            // 
            // innerPanel
            // 
            this.innerPanel.Controls.Add(this.versionLabel);
            this.innerPanel.Controls.Add(this.titleLabel2);
            this.innerPanel.Controls.Add(this.titleLabel1);
            this.innerPanel.Controls.Add(this.splashPictureBox);
            this.innerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.innerPanel.Location = new System.Drawing.Point(16, 16);
            this.innerPanel.Name = "innerPanel";
            this.innerPanel.Size = new System.Drawing.Size(505, 303);
            this.innerPanel.TabIndex = 0;
            // 
            // versionLabel
            // 
            this.versionLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.versionLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.versionLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.versionLabel.Location = new System.Drawing.Point(254, 273);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(251, 30);
            this.versionLabel.TabIndex = 3;
            this.versionLabel.Text = "Version";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // titleLabel2
            // 
            this.titleLabel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel2.Font = new System.Drawing.Font("Trebuchet MS", 18F);
            this.titleLabel2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.titleLabel2.Location = new System.Drawing.Point(254, 88);
            this.titleLabel2.Name = "titleLabel2";
            this.titleLabel2.Size = new System.Drawing.Size(251, 45);
            this.titleLabel2.TabIndex = 2;
            this.titleLabel2.Text = "Oracle";
            this.titleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleLabel1
            // 
            this.titleLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel1.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.titleLabel1.Location = new System.Drawing.Point(254, 0);
            this.titleLabel1.Name = "titleLabel1";
            this.titleLabel1.Size = new System.Drawing.Size(251, 88);
            this.titleLabel1.TabIndex = 1;
            this.titleLabel1.Text = "Exandas";
            this.titleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splashPictureBox
            // 
            this.splashPictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.splashPictureBox.Image = global::ExandasOracle.Properties.Resources.f6016_001;
            this.splashPictureBox.Location = new System.Drawing.Point(0, 0);
            this.splashPictureBox.Name = "splashPictureBox";
            this.splashPictureBox.Size = new System.Drawing.Size(254, 303);
            this.splashPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.splashPictureBox.TabIndex = 0;
            this.splashPictureBox.TabStop = false;
            // 
            // SplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 335);
            this.Controls.Add(this.fillPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashForm";
            this.fillPanel.ResumeLayout(false);
            this.innerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splashPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.Panel innerPanel;
        private System.Windows.Forms.PictureBox splashPictureBox;
        private System.Windows.Forms.Label titleLabel1;
        private System.Windows.Forms.Label titleLabel2;
        private System.Windows.Forms.Label versionLabel;
    }
}