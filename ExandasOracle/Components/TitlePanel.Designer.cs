
namespace ExandasOracle.Components
{
    partial class TitlePanel
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
            this.bottomLineLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bottomLineLabel
            // 
            this.bottomLineLabel.BackColor = System.Drawing.Color.Salmon;
            this.bottomLineLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomLineLabel.Location = new System.Drawing.Point(0, 65);
            this.bottomLineLabel.Name = "bottomLineLabel";
            this.bottomLineLabel.Size = new System.Drawing.Size(225, 2);
            this.bottomLineLabel.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.Cornsilk;
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Padding = new System.Windows.Forms.Padding(8, 0, 0, 12);
            this.titleLabel.Size = new System.Drawing.Size(225, 65);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "titleLabel";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.titleLabel.UseCompatibleTextRendering = true;
            // 
            // TitlePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.bottomLineLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TitlePanel";
            this.Size = new System.Drawing.Size(225, 67);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label bottomLineLabel;
        public System.Windows.Forms.Label titleLabel;
    }
}
