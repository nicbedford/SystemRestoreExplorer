namespace SystemRestoreExplorer
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lvRestorePoints = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMount = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnmount = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.chkHideNewest = new System.Windows.Forms.CheckBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnUnmount = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnMount = new System.Windows.Forms.Button();
            this.pnlButtonBright3d = new System.Windows.Forms.Panel();
            this.pnlButtonDark3d = new System.Windows.Forms.Panel();
            this.lblTimeZoneTitle = new System.Windows.Forms.Label();
            this.lblTimeZone = new System.Windows.Forms.Label();
            this.lblDiskSpace = new System.Windows.Forms.Label();
            this.lblDiskSpaceTitle = new System.Windows.Forms.Label();
            this.pnl3dDark = new System.Windows.Forms.Panel();
            this.pnl3dBright = new System.Windows.Forms.Panel();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.pnlDockPadding = new System.Windows.Forms.Panel();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.timerUpdateCheck = new System.Windows.Forms.Timer(this.components);
            this.mnuContext.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.pnlDockPadding.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvRestorePoints
            // 
            this.lvRestorePoints.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvRestorePoints.ContextMenuStrip = this.mnuContext;
            this.lvRestorePoints.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvRestorePoints.FullRowSelect = true;
            this.lvRestorePoints.GridLines = true;
            this.lvRestorePoints.HideSelection = false;
            this.lvRestorePoints.Location = new System.Drawing.Point(21, 141);
            this.lvRestorePoints.MultiSelect = false;
            this.lvRestorePoints.Name = "lvRestorePoints";
            this.lvRestorePoints.Size = new System.Drawing.Size(522, 182);
            this.lvRestorePoints.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lvRestorePoints.TabIndex = 2;
            this.lvRestorePoints.UseCompatibleStateImageBehavior = false;
            this.lvRestorePoints.View = System.Windows.Forms.View.Details;
            this.lvRestorePoints.SelectedIndexChanged += new System.EventHandler(this.lvRestorePoints_SelectedIndexChanged);
            this.lvRestorePoints.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvRestorePoints_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date and Time";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 368;
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMount,
            this.tsmiUnmount,
            this.tsmiDelete});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(126, 70);
            // 
            // tsmiMount
            // 
            this.tsmiMount.Name = "tsmiMount";
            this.tsmiMount.Size = new System.Drawing.Size(125, 22);
            this.tsmiMount.Text = "&Mount";
            this.tsmiMount.Click += new System.EventHandler(this.btnMount_Click);
            // 
            // tsmiUnmount
            // 
            this.tsmiUnmount.Enabled = false;
            this.tsmiUnmount.Name = "tsmiUnmount";
            this.tsmiUnmount.Size = new System.Drawing.Size(125, 22);
            this.tsmiUnmount.Text = "&Unmount";
            this.tsmiUnmount.Click += new System.EventHandler(this.btnUnmount_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(125, 22);
            this.tsmiDelete.Text = "&Delete";
            this.tsmiDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // chkHideNewest
            // 
            this.chkHideNewest.AutoSize = true;
            this.chkHideNewest.Checked = true;
            this.chkHideNewest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHideNewest.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHideNewest.Location = new System.Drawing.Point(22, 333);
            this.chkHideNewest.Name = "chkHideNewest";
            this.chkHideNewest.Size = new System.Drawing.Size(242, 17);
            this.chkHideNewest.TabIndex = 3;
            this.chkHideNewest.Text = "&Hide restore points created in the last 5 days";
            this.chkHideNewest.UseVisualStyleBackColor = true;
            this.chkHideNewest.Visible = false;
            this.chkHideNewest.CheckedChanged += new System.EventHandler(this.chkHideNewest_CheckedChanged);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnUnmount);
            this.pnlButtons.Controls.Add(this.btnDelete);
            this.pnlButtons.Controls.Add(this.btnMount);
            this.pnlButtons.Controls.Add(this.pnlButtonBright3d);
            this.pnlButtons.Controls.Add(this.pnlButtonDark3d);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 380);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(564, 47);
            this.pnlButtons.TabIndex = 6;
            // 
            // btnUnmount
            // 
            this.btnUnmount.Enabled = false;
            this.btnUnmount.Location = new System.Drawing.Point(388, 12);
            this.btnUnmount.Name = "btnUnmount";
            this.btnUnmount.Size = new System.Drawing.Size(79, 23);
            this.btnUnmount.TabIndex = 12;
            this.btnUnmount.Text = "Unmount";
            this.btnUnmount.UseVisualStyleBackColor = true;
            this.btnUnmount.Click += new System.EventHandler(this.btnUnmount_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(473, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(79, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnMount
            // 
            this.btnMount.Enabled = false;
            this.btnMount.Location = new System.Drawing.Point(307, 12);
            this.btnMount.Name = "btnMount";
            this.btnMount.Size = new System.Drawing.Size(75, 23);
            this.btnMount.TabIndex = 11;
            this.btnMount.Text = "Mount";
            this.btnMount.UseVisualStyleBackColor = true;
            this.btnMount.Click += new System.EventHandler(this.btnMount_Click);
            // 
            // pnlButtonBright3d
            // 
            this.pnlButtonBright3d.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlButtonBright3d.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtonBright3d.Location = new System.Drawing.Point(0, 1);
            this.pnlButtonBright3d.Name = "pnlButtonBright3d";
            this.pnlButtonBright3d.Size = new System.Drawing.Size(564, 1);
            this.pnlButtonBright3d.TabIndex = 1;
            // 
            // pnlButtonDark3d
            // 
            this.pnlButtonDark3d.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlButtonDark3d.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtonDark3d.Location = new System.Drawing.Point(0, 0);
            this.pnlButtonDark3d.Name = "pnlButtonDark3d";
            this.pnlButtonDark3d.Size = new System.Drawing.Size(564, 1);
            this.pnlButtonDark3d.TabIndex = 2;
            // 
            // lblTimeZoneTitle
            // 
            this.lblTimeZoneTitle.AutoSize = true;
            this.lblTimeZoneTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeZoneTitle.Location = new System.Drawing.Point(19, 119);
            this.lblTimeZoneTitle.Name = "lblTimeZoneTitle";
            this.lblTimeZoneTitle.Size = new System.Drawing.Size(100, 13);
            this.lblTimeZoneTitle.TabIndex = 7;
            this.lblTimeZoneTitle.Text = "Current time zone: ";
            // 
            // lblTimeZone
            // 
            this.lblTimeZone.AutoSize = true;
            this.lblTimeZone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeZone.Location = new System.Drawing.Point(114, 119);
            this.lblTimeZone.Name = "lblTimeZone";
            this.lblTimeZone.Size = new System.Drawing.Size(84, 13);
            this.lblTimeZone.TabIndex = 8;
            this.lblTimeZone.Text = "Time zone name";
            // 
            // lblDiskSpace
            // 
            this.lblDiskSpace.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiskSpace.Location = new System.Drawing.Point(478, 119);
            this.lblDiskSpace.Name = "lblDiskSpace";
            this.lblDiskSpace.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDiskSpace.Size = new System.Drawing.Size(65, 13);
            this.lblDiskSpace.TabIndex = 13;
            this.lblDiskSpace.Text = "1000 MB";
            this.lblDiskSpace.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDiskSpaceTitle
            // 
            this.lblDiskSpaceTitle.AutoSize = true;
            this.lblDiskSpaceTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiskSpaceTitle.Location = new System.Drawing.Point(390, 119);
            this.lblDiskSpaceTitle.Name = "lblDiskSpaceTitle";
            this.lblDiskSpaceTitle.Size = new System.Drawing.Size(90, 13);
            this.lblDiskSpaceTitle.TabIndex = 12;
            this.lblDiskSpaceTitle.Text = "Disk space used: ";
            // 
            // pnl3dDark
            // 
            this.pnl3dDark.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnl3dDark.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl3dDark.Location = new System.Drawing.Point(0, 62);
            this.pnl3dDark.Name = "pnl3dDark";
            this.pnl3dDark.Size = new System.Drawing.Size(564, 1);
            this.pnl3dDark.TabIndex = 15;
            // 
            // pnl3dBright
            // 
            this.pnl3dBright.BackColor = System.Drawing.Color.White;
            this.pnl3dBright.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl3dBright.Location = new System.Drawing.Point(0, 379);
            this.pnl3dBright.Name = "pnl3dBright";
            this.pnl3dBright.Size = new System.Drawing.Size(564, 1);
            this.pnl3dBright.TabIndex = 16;
            // 
            // picIcon
            // 
            this.picIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
            this.picIcon.Location = new System.Drawing.Point(508, 4);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(52, 52);
            this.picIcon.TabIndex = 3;
            this.picIcon.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(19, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(485, 22);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Choose a restore point";
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblDescription.Location = new System.Drawing.Point(43, 26);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(446, 35);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "This tool allows you to mount and view or remove system restore points from your " +
                "computer, once a restore point is removed your system can no longer be restored " +
                "to that point in time.";
            // 
            // pnlDockPadding
            // 
            this.pnlDockPadding.BackColor = System.Drawing.SystemColors.Window;
            this.pnlDockPadding.Controls.Add(this.lblDescription);
            this.pnlDockPadding.Controls.Add(this.lblTitle);
            this.pnlDockPadding.Controls.Add(this.picIcon);
            this.pnlDockPadding.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDockPadding.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDockPadding.Location = new System.Drawing.Point(0, 0);
            this.pnlDockPadding.Name = "pnlDockPadding";
            this.pnlDockPadding.Padding = new System.Windows.Forms.Padding(8, 6, 4, 4);
            this.pnlDockPadding.Size = new System.Drawing.Size(564, 62);
            this.pnlDockPadding.TabIndex = 14;
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.Location = new System.Drawing.Point(19, 88);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(318, 13);
            this.lblInstructions.TabIndex = 17;
            this.lblInstructions.Text = "Click the restore point you want to remove and then click Delete.";
            // 
            // timerUpdateCheck
            // 
            this.timerUpdateCheck.Interval = 1000;
            this.timerUpdateCheck.Tick += new System.EventHandler(this.timerUpdateCheck_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 427);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.pnl3dDark);
            this.Controls.Add(this.pnlDockPadding);
            this.Controls.Add(this.pnl3dBright);
            this.Controls.Add(this.lblDiskSpace);
            this.Controls.Add(this.lblDiskSpaceTitle);
            this.Controls.Add(this.lblTimeZone);
            this.Controls.Add(this.lblTimeZoneTitle);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.chkHideNewest);
            this.Controls.Add(this.lvRestorePoints);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "System Restore Explorer";
            this.mnuContext.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.pnlDockPadding.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvRestorePoints;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.CheckBox chkHideNewest;
        protected internal System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Panel pnlButtonBright3d;
        private System.Windows.Forms.Panel pnlButtonDark3d;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblTimeZoneTitle;
        private System.Windows.Forms.Label lblTimeZone;
        private System.Windows.Forms.Label lblDiskSpace;
        private System.Windows.Forms.Label lblDiskSpaceTitle;
        private System.Windows.Forms.Button btnMount;
        private System.Windows.Forms.Panel pnl3dDark;
        private System.Windows.Forms.Panel pnl3dBright;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Panel pnlDockPadding;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.ContextMenuStrip mnuContext;
        private System.Windows.Forms.ToolStripMenuItem tsmiMount;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnmount;
        private System.Windows.Forms.Button btnUnmount;
        private System.Windows.Forms.Timer timerUpdateCheck;
    }
}

