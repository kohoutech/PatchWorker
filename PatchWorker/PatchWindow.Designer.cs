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
            this.PatchWndMenu = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addInputUnitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addModiferUnitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addOutputUnitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.canvasMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideShowCanvasMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.midiMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allNotesOffMidiMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PatchWndMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // PatchStatus
            // 
            this.PatchStatus.Location = new System.Drawing.Point(0, 339);
            this.PatchStatus.Name = "PatchStatus";
            this.PatchStatus.Size = new System.Drawing.Size(384, 22);
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
            // PatchWndMenu
            // 
            this.PatchWndMenu.Location = new System.Drawing.Point(0, 0);
            this.PatchWndMenu.Name = "PatchWndMenu";
            this.PatchWndMenu.Size = new System.Drawing.Size(384, 24);
            this.PatchWndMenu.TabIndex = 9;
            this.PatchWndMenu.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileMenuItem,
            this.loadFileMenuItem,
            this.saveFileMenuItem,
            this.saveAsFileMenuItem,
            this.toolStripSeparator,
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
            this.newFileMenuItem.Size = new System.Drawing.Size(176, 22);
            this.newFileMenuItem.Text = "&New Patch";
            this.newFileMenuItem.Click += new System.EventHandler(this.patchNewMenuItem_Click);
            // 
            // loadFileMenuItem
            // 
            this.loadFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadFileMenuItem.Image")));
            this.loadFileMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadFileMenuItem.Name = "loadFileMenuItem";
            this.loadFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadFileMenuItem.Size = new System.Drawing.Size(176, 22);
            this.loadFileMenuItem.Text = "&Load Patch";
            this.loadFileMenuItem.Click += new System.EventHandler(this.patchLoadMenuItem_Click);
            // 
            // saveFileMenuItem
            // 
            this.saveFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveFileMenuItem.Image")));
            this.saveFileMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveFileMenuItem.Name = "saveFileMenuItem";
            this.saveFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveFileMenuItem.Size = new System.Drawing.Size(176, 22);
            this.saveFileMenuItem.Text = "&Save Patch";
            this.saveFileMenuItem.Click += new System.EventHandler(this.patchSaveMenuItem_Click);
            // 
            // saveAsFileMenuItem
            // 
            this.saveAsFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveAsFileMenuItem.Name = "saveAsFileMenuItem";
            this.saveAsFileMenuItem.Size = new System.Drawing.Size(176, 22);
            this.saveAsFileMenuItem.Text = "Save Patch &As";
            this.saveAsFileMenuItem.Click += new System.EventHandler(this.patchSaveAsMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(173, 6);
            // 
            // exitFileMenuItem
            // 
            this.exitFileMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitFileMenuItem.Name = "exitFileMenuItem";
            this.exitFileMenuItem.Size = new System.Drawing.Size(176, 22);
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
            // canvasMenuItem
            // 
            this.canvasMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideShowCanvasMenuItem});
            this.canvasMenuItem.Name = "canvasMenuItem";
            this.canvasMenuItem.Size = new System.Drawing.Size(57, 20);
            this.canvasMenuItem.Text = "&Canvas";
            // 
            // hideShowCanvasMenuItem
            // 
            this.hideShowCanvasMenuItem.Name = "hideShowCanvasMenuItem";
            this.hideShowCanvasMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hideShowCanvasMenuItem.Text = "&Show / Hide Canvas";
            this.hideShowCanvasMenuItem.Click += new System.EventHandler(this.hideShowCanvasMenuItem_Click);
            // 
            // midiMenuItem
            // 
            this.midiMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allNotesOffMidiMenuItem});
            this.midiMenuItem.Name = "midiMenuItem";
            this.midiMenuItem.Size = new System.Drawing.Size(43, 20);
            this.midiMenuItem.Text = "Midi";
            // 
            // allNotesOffMidiMenuItem
            // 
            this.allNotesOffMidiMenuItem.Name = "allNotesOffMidiMenuItem";
            this.allNotesOffMidiMenuItem.Size = new System.Drawing.Size(142, 22);
            this.allNotesOffMidiMenuItem.Text = "&All Notes Off";
            this.allNotesOffMidiMenuItem.Click += new System.EventHandler(this.allNotesOffMidiMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutHelpMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "&Help";
            // 
            // aboutHelpMenuItem
            // 
            this.aboutHelpMenuItem.Name = "aboutHelpMenuItem";
            this.aboutHelpMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutHelpMenuItem.Text = "&About...";
            this.aboutHelpMenuItem.Click += new System.EventHandler(this.helpAboutMenuItem_Click);
            // 
            // PatchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.PatchStatus);
            this.Controls.Add(this.PatchWndMenu);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.PatchWndMenu;
            this.Name = "PatchWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PatchWorker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PatchWindow_FormClosed);
            this.PatchWndMenu.ResumeLayout(false);
            this.PatchWndMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip PatchStatus;
        private System.Windows.Forms.SaveFileDialog savePatchDialog;
        private System.Windows.Forms.OpenFileDialog openPatchDialog;
        private System.Windows.Forms.MenuStrip PatchWndMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addInputUnitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addModiferUnitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutHelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addOutputUnitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem canvasMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideShowCanvasMenuItem;
        private System.Windows.Forms.ToolStripMenuItem midiMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allNotesOffMidiMenuItem;
    }
}

