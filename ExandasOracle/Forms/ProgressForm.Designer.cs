
namespace ExandasOracle.Forms
{
    partial class ProgressForm
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
            this.messageLabel = new System.Windows.Forms.Label();
            this.bottomFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.noButton = new System.Windows.Forms.Button();
            this.yesButton = new System.Windows.Forms.Button();
            this.fillPanel = new System.Windows.Forms.Panel();
            this.progressLabel = new System.Windows.Forms.Label();
            this.mainProgressBar = new System.Windows.Forms.ProgressBar();
            this.mainBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.cancelButton = new System.Windows.Forms.Button();
            this.topPanel.SuspendLayout();
            this.bottomFlowLayoutPanel.SuspendLayout();
            this.fillPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.Window;
            this.topPanel.Controls.Add(this.messageLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Padding = new System.Windows.Forms.Padding(16);
            this.topPanel.Size = new System.Drawing.Size(632, 66);
            this.topPanel.TabIndex = 0;
            // 
            // messageLabel
            // 
            this.messageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLabel.Location = new System.Drawing.Point(16, 16);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(600, 34);
            this.messageLabel.TabIndex = 0;
            this.messageLabel.Text = "messageLabel";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // bottomFlowLayoutPanel
            // 
            this.bottomFlowLayoutPanel.Controls.Add(this.noButton);
            this.bottomFlowLayoutPanel.Controls.Add(this.yesButton);
            this.bottomFlowLayoutPanel.Controls.Add(this.cancelButton);
            this.bottomFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.bottomFlowLayoutPanel.Location = new System.Drawing.Point(0, 204);
            this.bottomFlowLayoutPanel.Name = "bottomFlowLayoutPanel";
            this.bottomFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 8, 8, 0);
            this.bottomFlowLayoutPanel.Size = new System.Drawing.Size(632, 55);
            this.bottomFlowLayoutPanel.TabIndex = 1;
            // 
            // noButton
            // 
            this.noButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.noButton.Location = new System.Drawing.Point(541, 11);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(80, 23);
            this.noButton.TabIndex = 0;
            this.noButton.Text = "Non";
            this.noButton.UseVisualStyleBackColor = true;
            // 
            // yesButton
            // 
            this.yesButton.Location = new System.Drawing.Point(455, 11);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(80, 23);
            this.yesButton.TabIndex = 1;
            this.yesButton.Text = "Oui";
            this.yesButton.UseVisualStyleBackColor = true;
            this.yesButton.Click += new System.EventHandler(this.YesButton_Click);
            // 
            // fillPanel
            // 
            this.fillPanel.BackColor = System.Drawing.SystemColors.Window;
            this.fillPanel.Controls.Add(this.progressLabel);
            this.fillPanel.Controls.Add(this.mainProgressBar);
            this.fillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillPanel.Location = new System.Drawing.Point(0, 66);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Padding = new System.Windows.Forms.Padding(16);
            this.fillPanel.Size = new System.Drawing.Size(632, 138);
            this.fillPanel.TabIndex = 2;
            // 
            // progressLabel
            // 
            this.progressLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressLabel.Location = new System.Drawing.Point(16, 39);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(600, 30);
            this.progressLabel.TabIndex = 1;
            this.progressLabel.Text = "progressLabel";
            this.progressLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // mainProgressBar
            // 
            this.mainProgressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainProgressBar.Location = new System.Drawing.Point(16, 16);
            this.mainProgressBar.Name = "mainProgressBar";
            this.mainProgressBar.Size = new System.Drawing.Size(600, 23);
            this.mainProgressBar.TabIndex = 0;
            // 
            // mainBackgroundWorker
            // 
            this.mainBackgroundWorker.WorkerReportsProgress = true;
            this.mainBackgroundWorker.WorkerSupportsCancellation = true;
            this.mainBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MainBackgroundWorker_DoWork);
            this.mainBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.MainBackgroundWorker_ProgressChanged);
            this.mainBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.MainBackgroundWorker_RunWorkerCompleted);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(369, 11);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(80, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 259);
            this.Controls.Add(this.fillPanel);
            this.Controls.Add(this.bottomFlowLayoutPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "ProgressForm";
            this.Text = "ProgressForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressForm_FormClosing);
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            this.topPanel.ResumeLayout(false);
            this.bottomFlowLayoutPanel.ResumeLayout(false);
            this.fillPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.FlowLayoutPanel bottomFlowLayoutPanel;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.ProgressBar mainProgressBar;
        private System.ComponentModel.BackgroundWorker mainBackgroundWorker;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Button cancelButton;
    }
}