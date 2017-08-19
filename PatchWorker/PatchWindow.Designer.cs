namespace PatchWorker
{
    partial class PatchWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatchWindow));
            this.PatchStatus = new System.Windows.Forms.StatusStrip();
            this.savePatchDialog = new System.Windows.Forms.SaveFileDialog();
            this.openPatchDialog = new System.Windows.Forms.OpenFileDialog();
            this.PatchMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addInputUnitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addModiferUnitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addOutputUnitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PatchToolStrip = new System.Windows.Forms.ToolStrip();
            this.newPatchToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openPatchToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.savePatchToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.PatchMenuStrip.SuspendLayout();
            this.PatchToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PatchStatus
            // 
            this.PatchStatus.Location = new System.Drawing.Point(0, 339);
            this.PatchStatus.Name = "PatchStatus";
            this.PatchStatus.Size = new System.Drawing.Size(584, 22);
            this.PatchStatus.TabIndex = 8;
            this.PatchStatus.Text = "statusStrip1";
            // 
            // savePatchDialog
            // 
            this.savePatchDialog.DefaultExt = "pwk";
            // 
            // openPatchDialog
            // 
            this.openPatchDialog.DefaultExt = "pwk";
            // 
            // PatchMenuStrip
            // 
            this.PatchMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.unitMenuItem,
            this.helpToolStripMenuItem});
            this.PatchMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.PatchMenuStrip.Name = "PatchMenuStrip";
            this.PatchMenuStrip.Size = new System.Drawing.Size(584, 24);
            this.PatchMenuStrip.TabIndex = 9;
            this.PatchMenuStrip.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileMenuItem,
            this.openFileMenuItem,
            this.toolStripSeparator,
            this.saveFileMenuItem,
            this.saveAsFileMenuItem,
            this.toolStripSeparator1,
            this.exitFileMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "&File";
            // 
            // newFileMenuItem
            // 
            this.newFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newFileMenuItem.Image")));
            this.newFileMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newFileMenuItem.Name = "newFileMenuItem";
            this.newFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newFileMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newFileMenuItem.Text = "&New Patch";
            this.newFileMenuItem.Click += new System.EventHandler(this.patchNewMenuItem_Click);
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openFileMenuItem.Image")));
            this.openFileMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFileMenuItem.Name = "openFileMenuItem";
            this.openFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileMenuItem.Size = new System.Drawing.Size(179, 22);
            this.openFileMenuItem.Text = "&Open Patch";
            this.openFileMenuItem.Click += new System.EventHandler(this.patchLoadMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(176, 6);
            // 
            // saveFileMenuItem
            // 
            this.saveFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveFileMenuItem.Image")));
            this.saveFileMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveFileMenuItem.Name = "saveFileMenuItem";
            this.saveFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveFileMenuItem.Size = new System.Drawing.Size(179, 22);
            this.saveFileMenuItem.Text = "&Save Patch";
            this.saveFileMenuItem.Click += new System.EventHandler(this.patchSaveMenuItem_Click);
            // 
            // saveAsFileMenuItem
            // 
            this.saveAsFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveAsFileMenuItem.Name = "saveAsFileMenuItem";
            this.saveAsFileMenuItem.Size = new System.Drawing.Size(179, 22);
            this.saveAsFileMenuItem.Text = "Save Patch &As";
            this.saveAsFileMenuItem.Click += new System.EventHandler(this.patchSaveAsMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // exitFileMenuItem
            // 
            this.exitFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitFileMenuItem.Name = "exitFileMenuItem";
            this.exitFileMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exitFileMenuItem.Text = "E&xit";
            this.exitFileMenuItem.Click += new System.EventHandler(this.fileExitMenuItem_Click);
            // 
            // unitMenuItem
            // 
            this.unitMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addInputUnitMenuItem,
            this.addModiferUnitMenuItem,
            this.addOutputUnitMenuItem});
            this.unitMenuItem.Name = "unitMenuItem";
            this.unitMenuItem.Size = new System.Drawing.Size(41, 20);
            this.unitMenuItem.Text = "&Unit";
            // 
            // addInputUnitMenuItem
            // 
            this.addInputUnitMenuItem.Name = "addInputUnitMenuItem";
            this.addInputUnitMenuItem.Size = new System.Drawing.Size(169, 22);
            this.addInputUnitMenuItem.Text = "Add &Input Unit";
            this.addInputUnitMenuItem.Click += new System.EventHandler(this.addInputUnitMenuItem_Click);
            // 
            // addModiferUnitMenuItem
            // 
            this.addModiferUnitMenuItem.Name = "addModiferUnitMenuItem";
            this.addModiferUnitMenuItem.Size = new System.Drawing.Size(169, 22);
            this.addModiferUnitMenuItem.Text = "Add &Modifier Unit";
            this.addModiferUnitMenuItem.Click += new System.EventHandler(this.addModifierUnitMenuItem_Click);
            // 
            // addOutputUnitMenuItem
            // 
            this.addOutputUnitMenuItem.Name = "addOutputUnitMenuItem";
            this.addOutputUnitMenuItem.Size = new System.Drawing.Size(169, 22);
            this.addOutputUnitMenuItem.Text = "Add &Output Unit";
            this.addOutputUnitMenuItem.Click += new System.EventHandler(this.addOutputUnitMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.helpAboutMenuItem_Click);
            // 
            // PatchToolStrip
            // 
            this.PatchToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.PatchToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPatchToolStripButton,
            this.openPatchToolStripButton,
            this.savePatchToolStripButton,
            this.toolStripSeparator2});
            this.PatchToolStrip.Location = new System.Drawing.Point(0, 24);
            this.PatchToolStrip.Name = "PatchToolStrip";
            this.PatchToolStrip.Size = new System.Drawing.Size(584, 25);
            this.PatchToolStrip.TabIndex = 10;
            this.PatchToolStrip.Text = "toolStrip1";
            // 
            // newPatchToolStripButton
            // 
            this.newPatchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newPatchToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newPatchToolStripButton.Image")));
            this.newPatchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newPatchToolStripButton.Name = "newPatchToolStripButton";
            this.newPatchToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newPatchToolStripButton.Text = "&New Patch";
            this.newPatchToolStripButton.Click += new System.EventHandler(this.patchNewMenuItem_Click);
            // 
            // openPatchToolStripButton
            // 
            this.openPatchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openPatchToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openPatchToolStripButton.Image")));
            this.openPatchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openPatchToolStripButton.Name = "openPatchToolStripButton";
            this.openPatchToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openPatchToolStripButton.Text = "&Open Patch";
            this.openPatchToolStripButton.Click += new System.EventHandler(this.patchLoadMenuItem_Click);
            // 
            // savePatchToolStripButton
            // 
            this.savePatchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.savePatchToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("savePatchToolStripButton.Image")));
            this.savePatchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.savePatchToolStripButton.Name = "savePatchToolStripButton";
            this.savePatchToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.savePatchToolStripButton.Text = "&Save Patch";
            this.savePatchToolStripButton.Click += new System.EventHandler(this.patchSaveMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // PatchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.PatchToolStrip);
            this.Controls.Add(this.PatchStatus);
            this.Controls.Add(this.PatchMenuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.PatchMenuStrip;
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "PatchWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PatchWorker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PatchWindow_FormClosed);
            this.PatchMenuStrip.ResumeLayout(false);
            this.PatchMenuStrip.PerformLayout();
            this.PatchToolStrip.ResumeLayout(false);
            this.PatchToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip PatchStatus;
        private System.Windows.Forms.SaveFileDialog savePatchDialog;
        private System.Windows.Forms.OpenFileDialog openPatchDialog;
        private System.Windows.Forms.MenuStrip PatchMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addInputUnitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addModiferUnitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addOutputUnitMenuItem;
        private System.Windows.Forms.ToolStrip PatchToolStrip;
        private System.Windows.Forms.ToolStripButton newPatchToolStripButton;
        private System.Windows.Forms.ToolStripButton openPatchToolStripButton;
        private System.Windows.Forms.ToolStripButton savePatchToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

