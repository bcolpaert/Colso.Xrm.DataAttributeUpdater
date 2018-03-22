namespace Colso.Xrm.DataAttributeUpdater
{
    partial class DataUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataUpdater));
            this.viewImageList = new System.Windows.Forms.ImageList(this.components);
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbCloseThisTab = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRefreshEntities = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPreviewTransfer = new System.Windows.Forms.ToolStripButton();
            this.tsbTransferDashboards = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pnlBody = new System.Windows.Forms.TableLayoutPanel();
            this.gbAttributes = new System.Windows.Forms.GroupBox();
            this.lblNrSelected = new System.Windows.Forms.Label();
            this.grdAttributes = new System.Windows.Forms.DataGridView();
            this.btnFilter = new System.Windows.Forms.Button();
            this.gbEntities = new System.Windows.Forms.GroupBox();
            this.txtEntityFilter = new System.Windows.Forms.TextBox();
            this.lblEntityFilter = new System.Windows.Forms.Label();
            this.lvEntities = new System.Windows.Forms.ListView();
            this.clEntDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clEntLogicalName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clComment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkUpdate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.attDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attLogicalName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsMain.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.gbAttributes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAttributes)).BeginInit();
            this.gbEntities.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewImageList
            // 
            this.viewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("viewImageList.ImageStream")));
            this.viewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.viewImageList.Images.SetKeyName(0, "dashboard.gif");
            this.viewImageList.Images.SetKeyName(1, "dashboard_user.png");
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseThisTab,
            this.toolStripSeparator2,
            this.tsbRefreshEntities,
            this.toolStripSeparator3,
            this.btnPreviewTransfer,
            this.tsbTransferDashboards,
            this.toolStripSeparator1});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(800, 25);
            this.tsMain.TabIndex = 90;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbCloseThisTab
            // 
            this.tsbCloseThisTab.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tsbCloseThisTab.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseThisTab.Image")));
            this.tsbCloseThisTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseThisTab.Name = "tsbCloseThisTab";
            this.tsbCloseThisTab.Size = new System.Drawing.Size(55, 22);
            this.tsbCloseThisTab.Text = "Close";
            this.tsbCloseThisTab.Click += new System.EventHandler(this.tsbCloseThisTab_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRefreshEntities
            // 
            this.tsbRefreshEntities.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tsbRefreshEntities.Image = global::Colso.Xrm.DataAttributeUpdater.Properties.Resources.entities;
            this.tsbRefreshEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshEntities.Name = "tsbRefreshEntities";
            this.tsbRefreshEntities.Size = new System.Drawing.Size(107, 22);
            this.tsbRefreshEntities.Text = "Refresh Entities";
            this.tsbRefreshEntities.Click += new System.EventHandler(this.tsbRefreshEntities_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPreviewTransfer
            // 
            this.btnPreviewTransfer.Image = global::Colso.Xrm.DataAttributeUpdater.Properties.Resources.preview;
            this.btnPreviewTransfer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreviewTransfer.Name = "btnPreviewTransfer";
            this.btnPreviewTransfer.Size = new System.Drawing.Size(68, 22);
            this.btnPreviewTransfer.Text = "Preview";
            this.btnPreviewTransfer.Click += new System.EventHandler(this.btnPreviewTransfer_Click);
            // 
            // tsbTransferDashboards
            // 
            this.tsbTransferDashboards.Image = global::Colso.Xrm.DataAttributeUpdater.Properties.Resources.export;
            this.tsbTransferDashboards.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTransferDashboards.Name = "tsbTransferDashboards";
            this.tsbTransferDashboards.Size = new System.Drawing.Size(120, 22);
            this.tsbTransferDashboards.Text = "Update Attributes";
            this.tsbTransferDashboards.Click += new System.EventHandler(this.tsbTransferData_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // pnlBody
            // 
            this.pnlBody.ColumnCount = 2;
            this.pnlBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.875F));
            this.pnlBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.125F));
            this.pnlBody.Controls.Add(this.gbAttributes, 1, 0);
            this.pnlBody.Controls.Add(this.gbEntities, 0, 0);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 25);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.RowCount = 1;
            this.pnlBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlBody.Size = new System.Drawing.Size(800, 575);
            this.pnlBody.TabIndex = 105;
            // 
            // gbAttributes
            // 
            this.gbAttributes.Controls.Add(this.lblNrSelected);
            this.gbAttributes.Controls.Add(this.grdAttributes);
            this.gbAttributes.Controls.Add(this.btnFilter);
            this.gbAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAttributes.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.gbAttributes.Location = new System.Drawing.Point(330, 3);
            this.gbAttributes.Name = "gbAttributes";
            this.gbAttributes.Size = new System.Drawing.Size(467, 569);
            this.gbAttributes.TabIndex = 92;
            this.gbAttributes.TabStop = false;
            this.gbAttributes.Text = "Available attributes";
            // 
            // lblNrSelected
            // 
            this.lblNrSelected.AutoSize = true;
            this.lblNrSelected.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.lblNrSelected.Location = new System.Drawing.Point(6, 20);
            this.lblNrSelected.Name = "lblNrSelected";
            this.lblNrSelected.Size = new System.Drawing.Size(53, 13);
            this.lblNrSelected.TabIndex = 103;
            this.lblNrSelected.Text = "0 selected";
            // 
            // grdAttributes
            // 
            this.grdAttributes.AllowUserToAddRows = false;
            this.grdAttributes.AllowUserToDeleteRows = false;
            this.grdAttributes.AllowUserToResizeRows = false;
            this.grdAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdAttributes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAttributes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkUpdate,
            this.attDisplayName,
            this.attLogicalName,
            this.attType,
            this.attValue});
            this.grdAttributes.GridColor = System.Drawing.SystemColors.Window;
            this.grdAttributes.Location = new System.Drawing.Point(0, 44);
            this.grdAttributes.MultiSelect = false;
            this.grdAttributes.Name = "grdAttributes";
            this.grdAttributes.RowHeadersVisible = false;
            this.grdAttributes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdAttributes.Size = new System.Drawing.Size(467, 525);
            this.grdAttributes.TabIndex = 102;
            this.grdAttributes.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grdAttributes_CellValidating);
            this.grdAttributes.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAttributes_CellValueChanged);
            this.grdAttributes.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdAttributes_ColumnClick);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.Location = new System.Drawing.Point(402, 15);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(53, 23);
            this.btnFilter.TabIndex = 101;
            this.btnFilter.Text = "Filters";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // gbEntities
            // 
            this.gbEntities.Controls.Add(this.txtEntityFilter);
            this.gbEntities.Controls.Add(this.lblEntityFilter);
            this.gbEntities.Controls.Add(this.lvEntities);
            this.gbEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbEntities.Location = new System.Drawing.Point(3, 3);
            this.gbEntities.Name = "gbEntities";
            this.gbEntities.Size = new System.Drawing.Size(321, 569);
            this.gbEntities.TabIndex = 93;
            this.gbEntities.TabStop = false;
            this.gbEntities.Text = "Available Entities";
            // 
            // txtEntityFilter
            // 
            this.txtEntityFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntityFilter.Location = new System.Drawing.Point(48, 17);
            this.txtEntityFilter.Name = "txtEntityFilter";
            this.txtEntityFilter.Size = new System.Drawing.Size(267, 20);
            this.txtEntityFilter.TabIndex = 66;
            this.txtEntityFilter.TextChanged += new System.EventHandler(this.txtEntityFilter_TextChanged);
            // 
            // lblEntityFilter
            // 
            this.lblEntityFilter.AutoSize = true;
            this.lblEntityFilter.Location = new System.Drawing.Point(6, 22);
            this.lblEntityFilter.Name = "lblEntityFilter";
            this.lblEntityFilter.Size = new System.Drawing.Size(32, 13);
            this.lblEntityFilter.TabIndex = 65;
            this.lblEntityFilter.Text = "Filter:";
            // 
            // lvEntities
            // 
            this.lvEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clEntDisplayName,
            this.clEntLogicalName,
            this.clComment});
            this.lvEntities.FullRowSelect = true;
            this.lvEntities.HideSelection = false;
            this.lvEntities.Location = new System.Drawing.Point(0, 44);
            this.lvEntities.MultiSelect = false;
            this.lvEntities.Name = "lvEntities";
            this.lvEntities.Size = new System.Drawing.Size(321, 525);
            this.lvEntities.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvEntities.TabIndex = 64;
            this.lvEntities.UseCompatibleStateImageBehavior = false;
            this.lvEntities.View = System.Windows.Forms.View.Details;
            this.lvEntities.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvEntities_ColumnClick);
            this.lvEntities.SelectedIndexChanged += new System.EventHandler(this.lvEntities_SelectedIndexChanged);
            // 
            // clEntDisplayName
            // 
            this.clEntDisplayName.Text = "Display Name";
            this.clEntDisplayName.Width = 150;
            // 
            // clEntLogicalName
            // 
            this.clEntLogicalName.Text = "Logical Name";
            this.clEntLogicalName.Width = 150;
            // 
            // clComment
            // 
            this.clComment.Text = "Comment";
            this.clComment.Width = 120;
            // 
            // chkUpdate
            // 
            this.chkUpdate.HeaderText = "Update";
            this.chkUpdate.Name = "chkUpdate";
            this.chkUpdate.Width = 60;
            // 
            // attDisplayName
            // 
            this.attDisplayName.HeaderText = "Display Name";
            this.attDisplayName.Name = "attDisplayName";
            this.attDisplayName.ReadOnly = true;
            this.attDisplayName.Width = 150;
            // 
            // attLogicalName
            // 
            this.attLogicalName.HeaderText = "Logical Name";
            this.attLogicalName.Name = "attLogicalName";
            this.attLogicalName.ReadOnly = true;
            this.attLogicalName.Width = 150;
            // 
            // attType
            // 
            this.attType.HeaderText = "Type";
            this.attType.Name = "attType";
            this.attType.ReadOnly = true;
            // 
            // attValue
            // 
            this.attValue.HeaderText = "Value";
            this.attValue.Name = "attValue";
            this.attValue.Width = 200;
            // 
            // DataUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.tsMain);
            this.Name = "DataUpdater";
            this.Size = new System.Drawing.Size(800, 600);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.gbAttributes.ResumeLayout(false);
            this.gbAttributes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAttributes)).EndInit();
            this.gbEntities.ResumeLayout(false);
            this.gbEntities.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbCloseThisTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbRefreshEntities;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbTransferDashboards;
        private System.Windows.Forms.ImageList viewImageList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnPreviewTransfer;
        private System.Windows.Forms.TableLayoutPanel pnlBody;
        private System.Windows.Forms.GroupBox gbAttributes;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.GroupBox gbEntities;
        private System.Windows.Forms.TextBox txtEntityFilter;
        private System.Windows.Forms.Label lblEntityFilter;
        private System.Windows.Forms.ListView lvEntities;
        private System.Windows.Forms.ColumnHeader clEntDisplayName;
        private System.Windows.Forms.ColumnHeader clEntLogicalName;
        private System.Windows.Forms.ColumnHeader clComment;
        private System.Windows.Forms.DataGridView grdAttributes;
        private System.Windows.Forms.Label lblNrSelected;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn attDisplayName;
        private System.Windows.Forms.DataGridViewTextBoxColumn attLogicalName;
        private System.Windows.Forms.DataGridViewTextBoxColumn attType;
        private System.Windows.Forms.DataGridViewTextBoxColumn attValue;
    }
}
