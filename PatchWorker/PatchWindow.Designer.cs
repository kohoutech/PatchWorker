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
            this.PatchMenu = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchLoadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PatchStatus = new System.Windows.Forms.StatusStrip();
            this.savePatchDialog = new System.Windows.Forms.SaveFileDialog();
            this.openPatchDialog = new System.Windows.Forms.OpenFileDialog();
            this.PatchMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // PatchMenu
            // 
            this.PatchMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.patchMenuItem,
            this.unitsMenuItem,
            this.helpMenuItem});
            this.PatchMenu.Location = new System.Drawing.Point(0, 0);
            this.PatchMenu.Name = "PatchMenu";
            this.PatchMenu.Size = new System.Drawing.Size(584, 24);
            this.PatchMenu.TabIndex = 7;
            this.PatchMenu.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileExitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "&File";
            // 
            // fileExitMenuItem
            // 
            this.fileExitMenuItem.Name = "fileExitMenuItem";
            this.fileExitMenuItem.Size = new System.Drawing.Size(92, 22);
            this.fileExitMenuItem.Text = "E&xit";
            this.fileExitMenuItem.Click += new System.EventHandler(this.fileExitMenuItem_Click);
            // 
            // patchMenuItem
            // 
            this.patchMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patchNewMenuItem,
            this.patchLoadMenuItem,
            this.patchSaveMenuItem});
            this.patchMenuItem.Name = "patchMenuItem";
            this.patchMenuItem.Size = new System.Drawing.Size(49, 20);
            this.patchMenuItem.Text = "&Patch";
            // 
            // patchNewMenuItem
            // 
            this.patchNewMenuItem.Name = "patchNewMenuItem";
            this.patchNewMenuItem.Size = new System.Drawing.Size(152, 22);
            this.patchNewMenuItem.Text = "New Patch";
            this.patchNewMenuItem.Click += new System.EventHandler(this.patchNewMenuItem_Click);
            // 
            // patchLoadMenuItem
            // 
            this.patchLoadMenuItem.Name = "patchLoadMenuItem";
            this.patchLoadMenuItem.Size = new System.Drawing.Size(152, 22);
            this.patchLoadMenuItem.Text = "&Load Patch";
            this.patchLoadMenuItem.Click += new System.EventHandler(this.patchLoadMenuItem_Click);
            // 
            // patchSaveMenuItem
            // 
            this.patchSaveMenuItem.Name = "patchSaveMenuItem";
            this.patchSaveMenuItem.Size = new System.Drawing.Size(152, 22);
            this.patchSaveMenuItem.Text = "Save Patch";
            this.patchSaveMenuItem.Click += new System.EventHandler(this.patchSaveMenuItem_Click);
            // 
            // unitsMenuItem
            // 
            this.unitsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unitNewMenuItem,
            this.toolStripMenuItem1});
            this.unitsMenuItem.Name = "unitsMenuItem";
            this.unitsMenuItem.Size = new System.Drawing.Size(41, 20);
            this.unitsMenuItem.Text = "&Unit";
            // 
            // unitNewMenuItem
            // 
            this.unitNewMenuItem.Name = "unitNewMenuItem";
            this.unitNewMenuItem.Size = new System.Drawing.Size(123, 22);
            this.unitNewMenuItem.Text = "&New Unit";
            this.unitNewMenuItem.Click += new System.EventHandler(this.unitNewMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(120, 6);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpAboutMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "&Help";
            // 
            // helpAboutMenuItem
            // 
            this.helpAboutMenuItem.Name = "helpAboutMenuItem";
            this.helpAboutMenuItem.Size = new System.Drawing.Size(152, 22);
            this.helpAboutMenuItem.Text = "&About...";
            this.helpAboutMenuItem.Click += new System.EventHandler(this.helpAboutMenuItem_Click);
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
            // PatchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.PatchStatus);
            this.Controls.Add(this.PatchMenu);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.PatchMenu;
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "PatchWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PatchWorker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PatchWindow_FormClosed);
            this.Resize += new System.EventHandler(this.PatchWindow_Resize);
            this.PatchMenu.ResumeLayout(false);
            this.PatchMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip PatchMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patchMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unitsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unitNewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpAboutMenuItem;
        private System.Windows.Forms.StatusStrip PatchStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem patchLoadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patchSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patchNewMenuItem;
        private System.Windows.Forms.SaveFileDialog savePatchDialog;
        private System.Windows.Forms.OpenFileDialog openPatchDialog;
    }
}

