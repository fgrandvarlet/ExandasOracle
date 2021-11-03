
namespace ExandasOracle.Forms
{
    partial class DeltaReportListForm
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
            this.bottomPanel.SuspendLayout();
            this.bottomFlowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(800, 52);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.bottomFlowLayoutPanel);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 377);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(800, 73);
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
            this.bottomFlowLayoutPanel.Size = new System.Drawing.Size(800, 73);
            this.bottomFlowLayoutPanel.TabIndex = 0;
            // 
            // doCancelButton
            // 
            this.doCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.doCancelButton.Location = new System.Drawing.Point(709, 3);
            this.doCancelButton.Name = "doCancelButton";
            this.doCancelButton.Size = new System.Drawing.Size(80, 23);
            this.doCancelButton.TabIndex = 1;
            this.doCancelButton.Text = "Annuler";
            this.doCancelButton.UseVisualStyleBackColor = true;
            // 
            // doOkButton
            // 
            this.doOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.doOkButton.Location = new System.Drawing.Point(623, 3);
            this.doOkButton.Name = "doOkButton";
            this.doOkButton.Size = new System.Drawing.Size(80, 23);
            this.doOkButton.TabIndex = 0;
            this.doOkButton.Text = "OK";
            this.doOkButton.UseVisualStyleBackColor = true;
            // 
            // fillPanel
            // 
            this.fillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillPanel.Location = new System.Drawing.Point(0, 52);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Padding = new System.Windows.Forms.Padding(8);
            this.fillPanel.Size = new System.Drawing.Size(800, 325);
            this.fillPanel.TabIndex = 2;
            // 
            // DeltaReportListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.fillPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "DeltaReportListForm";
            this.Text = "DeltaReportListForm";
            this.Load += new System.EventHandler(this.DeltaReportListForm_Load);
            this.bottomPanel.ResumeLayout(false);
            this.bottomFlowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.FlowLayoutPanel bottomFlowLayoutPanel;
        private System.Windows.Forms.Button doCancelButton;
        private System.Windows.Forms.Button doOkButton;
    }
}