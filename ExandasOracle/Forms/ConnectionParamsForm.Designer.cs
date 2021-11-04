
namespace ExandasOracle.Forms
{
    partial class ConnectionParamsForm
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.fillPanel = new System.Windows.Forms.Panel();
            this.checkConnectionButton = new System.Windows.Forms.Button();
            this.DBAViewsCheckBox = new System.Windows.Forms.CheckBox();
            this.serviceRadioButton = new System.Windows.Forms.RadioButton();
            this.serviceTextBox = new System.Windows.Forms.TextBox();
            this.SIDRadioButton = new System.Windows.Forms.RadioButton();
            this.SIDTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.hostLabel = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.userLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.fillPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(623, 81);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 449);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(623, 83);
            this.bottomPanel.TabIndex = 1;
            // 
            // fillPanel
            // 
            this.fillPanel.Controls.Add(this.checkConnectionButton);
            this.fillPanel.Controls.Add(this.DBAViewsCheckBox);
            this.fillPanel.Controls.Add(this.serviceRadioButton);
            this.fillPanel.Controls.Add(this.serviceTextBox);
            this.fillPanel.Controls.Add(this.SIDRadioButton);
            this.fillPanel.Controls.Add(this.SIDTextBox);
            this.fillPanel.Controls.Add(this.portLabel);
            this.fillPanel.Controls.Add(this.portTextBox);
            this.fillPanel.Controls.Add(this.hostLabel);
            this.fillPanel.Controls.Add(this.hostTextBox);
            this.fillPanel.Controls.Add(this.passwordLabel);
            this.fillPanel.Controls.Add(this.passwordTextBox);
            this.fillPanel.Controls.Add(this.userTextBox);
            this.fillPanel.Controls.Add(this.userLabel);
            this.fillPanel.Controls.Add(this.nameLabel);
            this.fillPanel.Controls.Add(this.nameTextBox);
            this.fillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillPanel.Location = new System.Drawing.Point(0, 81);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Padding = new System.Windows.Forms.Padding(8, 24, 16, 24);
            this.fillPanel.Size = new System.Drawing.Size(623, 368);
            this.fillPanel.TabIndex = 2;
            // 
            // checkConnectionButton
            // 
            this.checkConnectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkConnectionButton.Location = new System.Drawing.Point(152, 302);
            this.checkConnectionButton.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.checkConnectionButton.Name = "checkConnectionButton";
            this.checkConnectionButton.Size = new System.Drawing.Size(451, 23);
            this.checkConnectionButton.TabIndex = 18;
            this.checkConnectionButton.Text = "Vérification de la connexion";
            this.checkConnectionButton.UseVisualStyleBackColor = true;
            this.checkConnectionButton.Click += new System.EventHandler(this.CheckConnectionButton_Click);
            // 
            // DBAViewsCheckBox
            // 
            this.DBAViewsCheckBox.AutoSize = true;
            this.DBAViewsCheckBox.Checked = true;
            this.DBAViewsCheckBox.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.DBAViewsCheckBox.Location = new System.Drawing.Point(152, 260);
            this.DBAViewsCheckBox.Name = "DBAViewsCheckBox";
            this.DBAViewsCheckBox.Size = new System.Drawing.Size(77, 19);
            this.DBAViewsCheckBox.TabIndex = 17;
            this.DBAViewsCheckBox.Text = "Vues DBA";
            this.DBAViewsCheckBox.ThreeState = true;
            this.DBAViewsCheckBox.UseVisualStyleBackColor = true;
            // 
            // serviceRadioButton
            // 
            this.serviceRadioButton.AutoSize = true;
            this.serviceRadioButton.Location = new System.Drawing.Point(12, 217);
            this.serviceRadioButton.Name = "serviceRadioButton";
            this.serviceRadioButton.Size = new System.Drawing.Size(107, 19);
            this.serviceRadioButton.TabIndex = 14;
            this.serviceRadioButton.TabStop = true;
            this.serviceRadioButton.Text = "Nom de service";
            this.serviceRadioButton.UseVisualStyleBackColor = true;
            this.serviceRadioButton.CheckedChanged += new System.EventHandler(this.ServiceRadioButton_CheckedChanged);
            // 
            // serviceTextBox
            // 
            this.serviceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serviceTextBox.Location = new System.Drawing.Point(152, 216);
            this.serviceTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 18);
            this.serviceTextBox.Name = "serviceTextBox";
            this.serviceTextBox.Size = new System.Drawing.Size(452, 23);
            this.serviceTextBox.TabIndex = 13;
            // 
            // SIDRadioButton
            // 
            this.SIDRadioButton.AutoSize = true;
            this.SIDRadioButton.Location = new System.Drawing.Point(12, 188);
            this.SIDRadioButton.Name = "SIDRadioButton";
            this.SIDRadioButton.Size = new System.Drawing.Size(42, 19);
            this.SIDRadioButton.TabIndex = 12;
            this.SIDRadioButton.TabStop = true;
            this.SIDRadioButton.Text = "SID";
            this.SIDRadioButton.UseVisualStyleBackColor = true;
            this.SIDRadioButton.CheckedChanged += new System.EventHandler(this.SIDRadioButton_CheckedChanged);
            // 
            // SIDTextBox
            // 
            this.SIDTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SIDTextBox.Location = new System.Drawing.Point(152, 187);
            this.SIDTextBox.Name = "SIDTextBox";
            this.SIDTextBox.Size = new System.Drawing.Size(452, 23);
            this.SIDTextBox.TabIndex = 10;
            // 
            // portLabel
            // 
            this.portLabel.Location = new System.Drawing.Point(12, 162);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(134, 23);
            this.portLabel.TabIndex = 9;
            this.portLabel.Text = "Port";
            // 
            // portTextBox
            // 
            this.portTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portTextBox.Location = new System.Drawing.Point(152, 158);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(452, 23);
            this.portTextBox.TabIndex = 8;
            // 
            // hostLabel
            // 
            this.hostLabel.Location = new System.Drawing.Point(12, 132);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(134, 23);
            this.hostLabel.TabIndex = 7;
            this.hostLabel.Text = "Nom d\'hôte";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostTextBox.Location = new System.Drawing.Point(152, 129);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(452, 23);
            this.hostTextBox.TabIndex = 6;
            // 
            // passwordLabel
            // 
            this.passwordLabel.Location = new System.Drawing.Point(12, 103);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(134, 23);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Mot de passe";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Location = new System.Drawing.Point(152, 100);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(452, 23);
            this.passwordTextBox.TabIndex = 4;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // userTextBox
            // 
            this.userTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userTextBox.Location = new System.Drawing.Point(152, 72);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.Size = new System.Drawing.Size(452, 23);
            this.userTextBox.TabIndex = 3;
            // 
            // userLabel
            // 
            this.userLabel.Location = new System.Drawing.Point(12, 74);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(134, 23);
            this.userLabel.TabIndex = 2;
            this.userLabel.Text = "Nom utilisateur";
            // 
            // nameLabel
            // 
            this.nameLabel.Location = new System.Drawing.Point(10, 30);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(135, 23);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Nom de la connexion";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.Location = new System.Drawing.Point(152, 27);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 18);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(452, 23);
            this.nameTextBox.TabIndex = 0;
            // 
            // ConnectionParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 532);
            this.Controls.Add(this.fillPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ConnectionParamsForm";
            this.Text = "ConnectionParamsForm";
            this.Load += new System.EventHandler(this.ConnectionParamsForm_Load);
            this.fillPanel.ResumeLayout(false);
            this.fillPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.RadioButton serviceRadioButton;
        private System.Windows.Forms.TextBox serviceTextBox;
        private System.Windows.Forms.RadioButton SIDRadioButton;
        private System.Windows.Forms.TextBox SIDTextBox;
        private System.Windows.Forms.CheckBox DBAViewsCheckBox;
        private System.Windows.Forms.Button checkConnectionButton;
    }
}