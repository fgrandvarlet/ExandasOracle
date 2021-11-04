
namespace ExandasOracle.Components
{
    partial class ComparisonSetUserControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectionLabel = new System.Windows.Forms.Label();
            this.connectionComboBox = new System.Windows.Forms.ComboBox();
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.userLabel = new System.Windows.Forms.Label();
            this.hostLabel = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.SIDTextBox = new System.Windows.Forms.TextBox();
            this.SIDRadioButton = new System.Windows.Forms.RadioButton();
            this.serviceRadioButton = new System.Windows.Forms.RadioButton();
            this.serviceTextBox = new System.Windows.Forms.TextBox();
            this.DBAViewsCheckBox = new System.Windows.Forms.CheckBox();
            this.schemaTextBox = new System.Windows.Forms.TextBox();
            this.schemaLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // connectionLabel
            // 
            this.connectionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.connectionLabel.Location = new System.Drawing.Point(10, 16);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(283, 23);
            this.connectionLabel.TabIndex = 0;
            this.connectionLabel.Text = "connectionLabel";
            // 
            // connectionComboBox
            // 
            this.connectionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.connectionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.connectionComboBox.FormattingEnabled = true;
            this.connectionComboBox.Location = new System.Drawing.Point(10, 42);
            this.connectionComboBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.connectionComboBox.Name = "connectionComboBox";
            this.connectionComboBox.Size = new System.Drawing.Size(284, 23);
            this.connectionComboBox.TabIndex = 1;
            // 
            // userTextBox
            // 
            this.userTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userTextBox.Location = new System.Drawing.Point(127, 88);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.ReadOnly = true;
            this.userTextBox.Size = new System.Drawing.Size(168, 23);
            this.userTextBox.TabIndex = 2;
            // 
            // userLabel
            // 
            this.userLabel.Location = new System.Drawing.Point(10, 91);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(110, 23);
            this.userLabel.TabIndex = 3;
            this.userLabel.Text = "Nom utilisateur";
            // 
            // hostLabel
            // 
            this.hostLabel.Location = new System.Drawing.Point(10, 120);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(110, 23);
            this.hostLabel.TabIndex = 4;
            this.hostLabel.Text = "Nom d\'hôte";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostTextBox.Location = new System.Drawing.Point(127, 117);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.ReadOnly = true;
            this.hostTextBox.Size = new System.Drawing.Size(168, 23);
            this.hostTextBox.TabIndex = 5;
            // 
            // portLabel
            // 
            this.portLabel.Location = new System.Drawing.Point(10, 149);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(110, 23);
            this.portLabel.TabIndex = 6;
            this.portLabel.Text = "Port";
            // 
            // portTextBox
            // 
            this.portTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portTextBox.Location = new System.Drawing.Point(127, 147);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.ReadOnly = true;
            this.portTextBox.Size = new System.Drawing.Size(168, 23);
            this.portTextBox.TabIndex = 7;
            // 
            // SIDTextBox
            // 
            this.SIDTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SIDTextBox.Location = new System.Drawing.Point(127, 175);
            this.SIDTextBox.Name = "SIDTextBox";
            this.SIDTextBox.ReadOnly = true;
            this.SIDTextBox.Size = new System.Drawing.Size(168, 23);
            this.SIDTextBox.TabIndex = 8;
            // 
            // SIDRadioButton
            // 
            this.SIDRadioButton.AutoSize = true;
            this.SIDRadioButton.Enabled = false;
            this.SIDRadioButton.Location = new System.Drawing.Point(10, 177);
            this.SIDRadioButton.Name = "SIDRadioButton";
            this.SIDRadioButton.Size = new System.Drawing.Size(42, 19);
            this.SIDRadioButton.TabIndex = 9;
            this.SIDRadioButton.TabStop = true;
            this.SIDRadioButton.Text = "SID";
            this.SIDRadioButton.UseVisualStyleBackColor = true;
            // 
            // serviceRadioButton
            // 
            this.serviceRadioButton.AutoSize = true;
            this.serviceRadioButton.Enabled = false;
            this.serviceRadioButton.Location = new System.Drawing.Point(10, 205);
            this.serviceRadioButton.Name = "serviceRadioButton";
            this.serviceRadioButton.Size = new System.Drawing.Size(107, 19);
            this.serviceRadioButton.TabIndex = 10;
            this.serviceRadioButton.TabStop = true;
            this.serviceRadioButton.Text = "Nom de service";
            this.serviceRadioButton.UseVisualStyleBackColor = true;
            // 
            // serviceTextBox
            // 
            this.serviceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serviceTextBox.Location = new System.Drawing.Point(127, 204);
            this.serviceTextBox.Name = "serviceTextBox";
            this.serviceTextBox.ReadOnly = true;
            this.serviceTextBox.Size = new System.Drawing.Size(168, 23);
            this.serviceTextBox.TabIndex = 11;
            // 
            // DBAViewsCheckBox
            // 
            this.DBAViewsCheckBox.AutoSize = true;
            this.DBAViewsCheckBox.Checked = true;
            this.DBAViewsCheckBox.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.DBAViewsCheckBox.Enabled = false;
            this.DBAViewsCheckBox.Location = new System.Drawing.Point(127, 238);
            this.DBAViewsCheckBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.DBAViewsCheckBox.Name = "DBAViewsCheckBox";
            this.DBAViewsCheckBox.Size = new System.Drawing.Size(77, 19);
            this.DBAViewsCheckBox.TabIndex = 12;
            this.DBAViewsCheckBox.Text = "Vues DBA";
            this.DBAViewsCheckBox.ThreeState = true;
            this.DBAViewsCheckBox.UseVisualStyleBackColor = true;
            // 
            // schemaTextBox
            // 
            this.schemaTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.schemaTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.schemaTextBox.Location = new System.Drawing.Point(127, 276);
            this.schemaTextBox.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.schemaTextBox.Name = "schemaTextBox";
            this.schemaTextBox.Size = new System.Drawing.Size(168, 23);
            this.schemaTextBox.TabIndex = 13;
            // 
            // schemaLabel
            // 
            this.schemaLabel.Location = new System.Drawing.Point(10, 279);
            this.schemaLabel.Name = "schemaLabel";
            this.schemaLabel.Size = new System.Drawing.Size(110, 23);
            this.schemaLabel.TabIndex = 14;
            this.schemaLabel.Text = "Schéma";
            // 
            // ComparisonSetUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.schemaLabel);
            this.Controls.Add(this.schemaTextBox);
            this.Controls.Add(this.DBAViewsCheckBox);
            this.Controls.Add(this.serviceTextBox);
            this.Controls.Add(this.serviceRadioButton);
            this.Controls.Add(this.SIDRadioButton);
            this.Controls.Add(this.SIDTextBox);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.hostTextBox);
            this.Controls.Add(this.hostLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.userTextBox);
            this.Controls.Add(this.connectionComboBox);
            this.Controls.Add(this.connectionLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ComparisonSetUserControl";
            this.Padding = new System.Windows.Forms.Padding(8, 16, 8, 0);
            this.Size = new System.Drawing.Size(306, 313);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label schemaLabel;
        public System.Windows.Forms.Label connectionLabel;
        public System.Windows.Forms.ComboBox connectionComboBox;
        public System.Windows.Forms.TextBox userTextBox;
        public System.Windows.Forms.TextBox hostTextBox;
        public System.Windows.Forms.TextBox portTextBox;
        public System.Windows.Forms.TextBox SIDTextBox;
        public System.Windows.Forms.RadioButton SIDRadioButton;
        public System.Windows.Forms.RadioButton serviceRadioButton;
        public System.Windows.Forms.TextBox serviceTextBox;
        public System.Windows.Forms.CheckBox DBAViewsCheckBox;
        public System.Windows.Forms.TextBox schemaTextBox;
    }
}
