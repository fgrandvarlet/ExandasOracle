
namespace ExandasOracle.Components
{
    partial class FilterSettingListPanel
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.topPanel = new System.Windows.Forms.Panel();
            this.addButton = new System.Windows.Forms.Button();
            this.propertyLabel = new System.Windows.Forms.Label();
            this.propertyComboBox = new System.Windows.Forms.ComboBox();
            this.labelLabel = new System.Windows.Forms.Label();
            this.labelComboBox = new System.Windows.Forms.ComboBox();
            this.enableLineButton = new System.Windows.Forms.Button();
            this.entityLabel = new System.Windows.Forms.Label();
            this.entityComboBox = new System.Windows.Forms.ComboBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.fillPanel = new System.Windows.Forms.Panel();
            this.mainDataGridView = new System.Windows.Forms.DataGridView();
            this.actionToolStrip = new System.Windows.Forms.ToolStrip();
            this.addToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.modifyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lookupToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.mainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.topPanel.SuspendLayout();
            this.fillPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).BeginInit();
            this.actionToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.addButton);
            this.topPanel.Controls.Add(this.propertyLabel);
            this.topPanel.Controls.Add(this.propertyComboBox);
            this.topPanel.Controls.Add(this.labelLabel);
            this.topPanel.Controls.Add(this.labelComboBox);
            this.topPanel.Controls.Add(this.enableLineButton);
            this.topPanel.Controls.Add(this.entityLabel);
            this.topPanel.Controls.Add(this.entityComboBox);
            this.topPanel.Controls.Add(this.titleLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(845, 144);
            this.topPanel.TabIndex = 2;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(719, 65);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "Ajouter";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // propertyLabel
            // 
            this.propertyLabel.Location = new System.Drawing.Point(490, 48);
            this.propertyLabel.Name = "propertyLabel";
            this.propertyLabel.Size = new System.Drawing.Size(124, 15);
            this.propertyLabel.TabIndex = 7;
            this.propertyLabel.Text = "Propriété";
            // 
            // propertyComboBox
            // 
            this.propertyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.propertyComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.propertyComboBox.FormattingEnabled = true;
            this.propertyComboBox.Location = new System.Drawing.Point(493, 66);
            this.propertyComboBox.Name = "propertyComboBox";
            this.propertyComboBox.Size = new System.Drawing.Size(220, 23);
            this.propertyComboBox.TabIndex = 4;
            // 
            // labelLabel
            // 
            this.labelLabel.Location = new System.Drawing.Point(264, 48);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(124, 15);
            this.labelLabel.TabIndex = 5;
            this.labelLabel.Text = "Libellé";
            // 
            // labelComboBox
            // 
            this.labelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.labelComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelComboBox.FormattingEnabled = true;
            this.labelComboBox.Location = new System.Drawing.Point(267, 66);
            this.labelComboBox.Name = "labelComboBox";
            this.labelComboBox.Size = new System.Drawing.Size(220, 23);
            this.labelComboBox.TabIndex = 3;
            this.labelComboBox.SelectedValueChanged += new System.EventHandler(this.LabelComboBox_SelectedValueChanged);
            // 
            // enableLineButton
            // 
            this.enableLineButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.enableLineButton.Image = global::ExandasOracle.Properties.Resources.B16x16_Folder_Inbox;
            this.enableLineButton.Location = new System.Drawing.Point(5, 66);
            this.enableLineButton.Name = "enableLineButton";
            this.enableLineButton.Size = new System.Drawing.Size(30, 23);
            this.enableLineButton.TabIndex = 1;
            this.enableLineButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.mainToolTip.SetToolTip(this.enableLineButton, "Activer l\'ajout d\'un enregistrement");
            this.enableLineButton.UseVisualStyleBackColor = true;
            this.enableLineButton.Click += new System.EventHandler(this.EnableLineButton_Click);
            // 
            // entityLabel
            // 
            this.entityLabel.Location = new System.Drawing.Point(38, 48);
            this.entityLabel.Name = "entityLabel";
            this.entityLabel.Size = new System.Drawing.Size(124, 15);
            this.entityLabel.TabIndex = 2;
            this.entityLabel.Text = "Entité";
            // 
            // entityComboBox
            // 
            this.entityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.entityComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.entityComboBox.FormattingEnabled = true;
            this.entityComboBox.Location = new System.Drawing.Point(41, 66);
            this.entityComboBox.Name = "entityComboBox";
            this.entityComboBox.Size = new System.Drawing.Size(220, 23);
            this.entityComboBox.TabIndex = 2;
            this.entityComboBox.SelectedValueChanged += new System.EventHandler(this.EntityComboBox_SelectedValueChanged);
            // 
            // titleLabel
            // 
            this.titleLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(845, 23);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "titleLabel";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fillPanel
            // 
            this.fillPanel.Controls.Add(this.mainDataGridView);
            this.fillPanel.Controls.Add(this.actionToolStrip);
            this.fillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillPanel.Location = new System.Drawing.Point(0, 144);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Size = new System.Drawing.Size(845, 295);
            this.fillPanel.TabIndex = 3;
            // 
            // mainDataGridView
            // 
            this.mainDataGridView.AllowUserToAddRows = false;
            this.mainDataGridView.AllowUserToDeleteRows = false;
            this.mainDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.mainDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mainDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.mainDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDataGridView.GridColor = System.Drawing.Color.LightGray;
            this.mainDataGridView.Location = new System.Drawing.Point(0, 25);
            this.mainDataGridView.MultiSelect = false;
            this.mainDataGridView.Name = "mainDataGridView";
            this.mainDataGridView.ReadOnly = true;
            this.mainDataGridView.RowTemplate.Height = 25;
            this.mainDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainDataGridView.Size = new System.Drawing.Size(845, 270);
            this.mainDataGridView.TabIndex = 1;
            // 
            // actionToolStrip
            // 
            this.actionToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.actionToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripButton,
            this.modifyToolStripButton,
            this.deleteToolStripButton,
            this.toolStripSeparator1,
            this.lookupToolStripComboBox,
            this.refreshToolStripButton});
            this.actionToolStrip.Location = new System.Drawing.Point(0, 0);
            this.actionToolStrip.Name = "actionToolStrip";
            this.actionToolStrip.Size = new System.Drawing.Size(845, 25);
            this.actionToolStrip.TabIndex = 0;
            this.actionToolStrip.Text = "toolStrip1";
            // 
            // addToolStripButton
            // 
            this.addToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addToolStripButton.Image = global::ExandasOracle.Properties.Resources.B16x16_Folder_Inbox;
            this.addToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addToolStripButton.Name = "addToolStripButton";
            this.addToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.addToolStripButton.Text = "addToolStripButton";
            this.addToolStripButton.ToolTipText = "Ajouter";
            // 
            // modifyToolStripButton
            // 
            this.modifyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.modifyToolStripButton.Image = global::ExandasOracle.Properties.Resources.B16x16_Edit;
            this.modifyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modifyToolStripButton.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.modifyToolStripButton.Name = "modifyToolStripButton";
            this.modifyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.modifyToolStripButton.Text = "modifyToolStripButton";
            this.modifyToolStripButton.ToolTipText = "Modifier";
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteToolStripButton.Image = global::ExandasOracle.Properties.Resources.B16x16_Cut;
            this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolStripButton.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.deleteToolStripButton.Text = "deleteToolStripButton";
            this.deleteToolStripButton.ToolTipText = "Supprimer";
            this.deleteToolStripButton.Click += new System.EventHandler(this.DeleteToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lookupToolStripComboBox
            // 
            this.lookupToolStripComboBox.Name = "lookupToolStripComboBox";
            this.lookupToolStripComboBox.Size = new System.Drawing.Size(180, 25);
            // 
            // refreshToolStripButton
            // 
            this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshToolStripButton.Image = global::ExandasOracle.Properties.Resources.refresh;
            this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshToolStripButton.Margin = new System.Windows.Forms.Padding(8, 1, 0, 2);
            this.refreshToolStripButton.Name = "refreshToolStripButton";
            this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.refreshToolStripButton.Text = "refreshToolStripButton";
            this.refreshToolStripButton.ToolTipText = "Rafraîchir";
            // 
            // FilterSettingListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fillPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FilterSettingListPanel";
            this.Size = new System.Drawing.Size(845, 439);
            this.Load += new System.EventHandler(this.FilterSettingListPanel_Load);
            this.topPanel.ResumeLayout(false);
            this.fillPanel.ResumeLayout(false);
            this.fillPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).EndInit();
            this.actionToolStrip.ResumeLayout(false);
            this.actionToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        public System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.DataGridView mainDataGridView;
        private System.Windows.Forms.ToolStrip actionToolStrip;
        private System.Windows.Forms.ToolStripButton addToolStripButton;
        private System.Windows.Forms.ToolStripButton modifyToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox lookupToolStripComboBox;
        private System.Windows.Forms.ToolStripButton refreshToolStripButton;
        private System.Windows.Forms.Label entityLabel;
        private System.Windows.Forms.ComboBox entityComboBox;
        private System.Windows.Forms.Button enableLineButton;
        private System.Windows.Forms.ToolTip mainToolTip;
        private System.Windows.Forms.Label propertyLabel;
        private System.Windows.Forms.ComboBox propertyComboBox;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.ComboBox labelComboBox;
        private System.Windows.Forms.Button addButton;
    }
}
