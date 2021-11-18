
namespace ExandasOracle.Forms
{
    partial class CompactLocalDatabaseForm
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
            this.bottomFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.doCancelButton = new System.Windows.Forms.Button();
            this.doOkButton = new System.Windows.Forms.Button();
            this.fillPanel = new System.Windows.Forms.Panel();
            this.reportTextBox = new System.Windows.Forms.TextBox();
            this.compactLocalDatabaseButton = new System.Windows.Forms.Button();
            this.purgeComparisonReportCheckBox = new System.Windows.Forms.CheckBox();
            this.localDatabaseTextBox = new System.Windows.Forms.TextBox();
            this.localDatabaseLabel = new System.Windows.Forms.Label();
            this.bottomPanel.SuspendLayout();
            this.bottomFlowLayoutPanel.SuspendLayout();
            this.fillPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(750, 59);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.bottomFlowLayoutPanel);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 454);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(750, 65);
            this.bottomPanel.TabIndex = 1;
            // 
            // bottomFlowLayoutPanel
            // 
            this.bottomFlowLayoutPanel.Controls.Add(this.doCancelButton);
            this.bottomFlowLayoutPanel.Controls.Add(this.doOkButton);
            this.bottomFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.bottomFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.bottomFlowLayoutPanel.Name = "bottomFlowLayoutPanel";
            this.bottomFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.bottomFlowLayoutPanel.Size = new System.Drawing.Size(750, 65);
            this.bottomFlowLayoutPanel.TabIndex = 1;
            // 
            // doCancelButton
            // 
            this.doCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.doCancelButton.Location = new System.Drawing.Point(659, 3);
            this.doCancelButton.Name = "doCancelButton";
            this.doCancelButton.Size = new System.Drawing.Size(80, 23);
            this.doCancelButton.TabIndex = 1;
            this.doCancelButton.Text = "Annuler";
            this.doCancelButton.UseVisualStyleBackColor = true;
            // 
            // doOkButton
            // 
            this.doOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.doOkButton.Location = new System.Drawing.Point(573, 3);
            this.doOkButton.Name = "doOkButton";
            this.doOkButton.Size = new System.Drawing.Size(80, 23);
            this.doOkButton.TabIndex = 0;
            this.doOkButton.Text = "OK";
            this.doOkButton.UseVisualStyleBackColor = true;
            // 
            // fillPanel
            // 
            this.fillPanel.Controls.Add(this.reportTextBox);
            this.fillPanel.Controls.Add(this.compactLocalDatabaseButton);
            this.fillPanel.Controls.Add(this.purgeComparisonReportCheckBox);
            this.fillPanel.Controls.Add(this.localDatabaseTextBox);
            this.fillPanel.Controls.Add(this.localDatabaseLabel);
            this.fillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillPanel.Location = new System.Drawing.Point(0, 59);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Size = new System.Drawing.Size(750, 395);
            this.fillPanel.TabIndex = 2;
            // 
            // reportTextBox
            // 
            this.reportTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportTextBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportTextBox.Location = new System.Drawing.Point(158, 113);
            this.reportTextBox.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.reportTextBox.Multiline = true;
            this.reportTextBox.Name = "reportTextBox";
            this.reportTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.reportTextBox.Size = new System.Drawing.Size(580, 264);
            this.reportTextBox.TabIndex = 6;
            // 
            // compactLocalDatabaseButton
            // 
            this.compactLocalDatabaseButton.Location = new System.Drawing.Point(158, 75);
            this.compactLocalDatabaseButton.Name = "compactLocalDatabaseButton";
            this.compactLocalDatabaseButton.Size = new System.Drawing.Size(150, 23);
            this.compactLocalDatabaseButton.TabIndex = 5;
            this.compactLocalDatabaseButton.Text = "Compacter";
            this.compactLocalDatabaseButton.UseVisualStyleBackColor = true;
            this.compactLocalDatabaseButton.Click += new System.EventHandler(this.CompactLocalDatabaseButton_Click);
            // 
            // purgeComparisonReportCheckBox
            // 
            this.purgeComparisonReportCheckBox.AutoSize = true;
            this.purgeComparisonReportCheckBox.Location = new System.Drawing.Point(158, 50);
            this.purgeComparisonReportCheckBox.Name = "purgeComparisonReportCheckBox";
            this.purgeComparisonReportCheckBox.Size = new System.Drawing.Size(213, 19);
            this.purgeComparisonReportCheckBox.TabIndex = 4;
            this.purgeComparisonReportCheckBox.Text = "Purger les rapports de comparaison";
            this.purgeComparisonReportCheckBox.UseVisualStyleBackColor = true;
            // 
            // localDatabaseTextBox
            // 
            this.localDatabaseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.localDatabaseTextBox.Location = new System.Drawing.Point(159, 21);
            this.localDatabaseTextBox.Margin = new System.Windows.Forms.Padding(3, 18, 3, 3);
            this.localDatabaseTextBox.Name = "localDatabaseTextBox";
            this.localDatabaseTextBox.ReadOnly = true;
            this.localDatabaseTextBox.Size = new System.Drawing.Size(580, 23);
            this.localDatabaseTextBox.TabIndex = 3;
            // 
            // localDatabaseLabel
            // 
            this.localDatabaseLabel.Location = new System.Drawing.Point(12, 24);
            this.localDatabaseLabel.Name = "localDatabaseLabel";
            this.localDatabaseLabel.Size = new System.Drawing.Size(140, 23);
            this.localDatabaseLabel.TabIndex = 2;
            this.localDatabaseLabel.Text = "Base de données locale";
            // 
            // CompactLocalDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 519);
            this.Controls.Add(this.fillPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CompactLocalDatabaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CompactLocalDatabaseForm";
            this.Load += new System.EventHandler(this.CompactLocalDatabaseForm_Load);
            this.bottomPanel.ResumeLayout(false);
            this.bottomFlowLayoutPanel.ResumeLayout(false);
            this.fillPanel.ResumeLayout(false);
            this.fillPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.TextBox reportTextBox;
        private System.Windows.Forms.Button compactLocalDatabaseButton;
        private System.Windows.Forms.CheckBox purgeComparisonReportCheckBox;
        private System.Windows.Forms.TextBox localDatabaseTextBox;
        private System.Windows.Forms.Label localDatabaseLabel;
        private System.Windows.Forms.FlowLayoutPanel bottomFlowLayoutPanel;
        private System.Windows.Forms.Button doCancelButton;
        private System.Windows.Forms.Button doOkButton;
    }
}