
namespace ExandasOracle.Components
{
    partial class BottomCommandPanel
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
            this.commandFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.doApplyButton = new System.Windows.Forms.Button();
            this.doCancelButton = new System.Windows.Forms.Button();
            this.doOkButton = new System.Windows.Forms.Button();
            this.commandFlowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandFlowLayoutPanel
            // 
            this.commandFlowLayoutPanel.Controls.Add(this.doApplyButton);
            this.commandFlowLayoutPanel.Controls.Add(this.doCancelButton);
            this.commandFlowLayoutPanel.Controls.Add(this.doOkButton);
            this.commandFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.commandFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.commandFlowLayoutPanel.Name = "commandFlowLayoutPanel";
            this.commandFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.commandFlowLayoutPanel.Size = new System.Drawing.Size(456, 44);
            this.commandFlowLayoutPanel.TabIndex = 0;
            // 
            // doApplyButton
            // 
            this.doApplyButton.Enabled = false;
            this.doApplyButton.Location = new System.Drawing.Point(365, 3);
            this.doApplyButton.Name = "doApplyButton";
            this.doApplyButton.Size = new System.Drawing.Size(80, 23);
            this.doApplyButton.TabIndex = 0;
            this.doApplyButton.Text = "A&ppliquer";
            this.doApplyButton.UseVisualStyleBackColor = true;
            this.doApplyButton.Click += new System.EventHandler(this.DoApplyButton_Click);
            // 
            // doCancelButton
            // 
            this.doCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.doCancelButton.Location = new System.Drawing.Point(279, 3);
            this.doCancelButton.Name = "doCancelButton";
            this.doCancelButton.Size = new System.Drawing.Size(80, 23);
            this.doCancelButton.TabIndex = 1;
            this.doCancelButton.Text = "&Annuler";
            this.doCancelButton.UseVisualStyleBackColor = true;
            // 
            // doOkButton
            // 
            this.doOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.doOkButton.Location = new System.Drawing.Point(193, 3);
            this.doOkButton.Name = "doOkButton";
            this.doOkButton.Size = new System.Drawing.Size(80, 23);
            this.doOkButton.TabIndex = 2;
            this.doOkButton.Text = "&OK";
            this.doOkButton.UseVisualStyleBackColor = true;
            this.doOkButton.Click += new System.EventHandler(this.DoOkButton_Click);
            // 
            // BottomCommandPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.commandFlowLayoutPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BottomCommandPanel";
            this.Size = new System.Drawing.Size(456, 44);
            this.commandFlowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel commandFlowLayoutPanel;
        public System.Windows.Forms.Button doApplyButton;
        public System.Windows.Forms.Button doCancelButton;
        public System.Windows.Forms.Button doOkButton;
    }
}
