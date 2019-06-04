/* ----------------------------------------------------------------------------
Patchworker : a midi patchbay
Copyright (C) 1995-2019  George E Greaney

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
----------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using Transonic.Patch;
using PatchWorker.Graph;

namespace PatchWorker.UI
{
    public class ControlPanel : UserControl
    {
        public PatchWindow patchWnd;
        public Settings settings;

        //controls
        private Button btnSavePatch;
        private Button btnNewPatch;
        private Button btnLoadPatch;
        private Button btnSavePatchAs;
        private Button btnNewOutUnit;
        private Button btnNewInUnit;
        private Button btnNewModUnit;
        private Button btnPanic;
        private ToolTip controlPanelToolTip;
        private System.ComponentModel.IContainer components;
        private Button btnHide;


        public ControlPanel(PatchWindow _patchWnd)
        {
            patchWnd = _patchWnd;
            settings = patchWnd.settings;

            InitializeComponent();

            this.TabStop = false;

            ResizeRedraw = true;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.controlPanelToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnPanic = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnNewOutUnit = new System.Windows.Forms.Button();
            this.btnNewInUnit = new System.Windows.Forms.Button();
            this.btnNewModUnit = new System.Windows.Forms.Button();
            this.btnSavePatchAs = new System.Windows.Forms.Button();
            this.btnLoadPatch = new System.Windows.Forms.Button();
            this.btnNewPatch = new System.Windows.Forms.Button();
            this.btnSavePatch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPanic
            // 
            this.btnPanic.BackgroundImage = global::PatchWorker.Properties.Resources.panic;
            this.btnPanic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPanic.FlatAppearance.BorderSize = 0;
            this.btnPanic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPanic.Location = new System.Drawing.Point(241, 11);
            this.btnPanic.Name = "btnPanic";
            this.btnPanic.Size = new System.Drawing.Size(24, 24);
            this.btnPanic.TabIndex = 8;
            this.btnPanic.TabStop = false;
            this.controlPanelToolTip.SetToolTip(this.btnPanic, "all notes off");
            this.btnPanic.UseVisualStyleBackColor = true;
            this.btnPanic.Click += new System.EventHandler(this.btnPanic_Click);
            // 
            // btnHide
            // 
            this.btnHide.BackgroundImage = global::PatchWorker.Properties.Resources.show_hide;
            this.btnHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHide.FlatAppearance.BorderSize = 0;
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.Location = new System.Drawing.Point(217, 11);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(24, 24);
            this.btnHide.TabIndex = 7;
            this.btnHide.TabStop = false;
            this.controlPanelToolTip.SetToolTip(this.btnHide, "show / hide canvas");
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnNewOutUnit
            // 
            this.btnNewOutUnit.BackgroundImage = global::PatchWorker.Properties.Resources.new_output;
            this.btnNewOutUnit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewOutUnit.FlatAppearance.BorderSize = 0;
            this.btnNewOutUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewOutUnit.Location = new System.Drawing.Point(181, 11);
            this.btnNewOutUnit.Name = "btnNewOutUnit";
            this.btnNewOutUnit.Size = new System.Drawing.Size(24, 24);
            this.btnNewOutUnit.TabIndex = 6;
            this.btnNewOutUnit.TabStop = false;
            this.controlPanelToolTip.SetToolTip(this.btnNewOutUnit, "add output unit");
            this.btnNewOutUnit.UseVisualStyleBackColor = true;
            this.btnNewOutUnit.Click += new System.EventHandler(this.btnNewOutUnit_Click);
            // 
            // btnNewInUnit
            // 
            this.btnNewInUnit.BackgroundImage = global::PatchWorker.Properties.Resources.new_input;
            this.btnNewInUnit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewInUnit.FlatAppearance.BorderSize = 0;
            this.btnNewInUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewInUnit.Location = new System.Drawing.Point(133, 11);
            this.btnNewInUnit.Name = "btnNewInUnit";
            this.btnNewInUnit.Size = new System.Drawing.Size(24, 24);
            this.btnNewInUnit.TabIndex = 4;
            this.btnNewInUnit.TabStop = false;
            this.controlPanelToolTip.SetToolTip(this.btnNewInUnit, "add input unit");
            this.btnNewInUnit.UseVisualStyleBackColor = true;
            this.btnNewInUnit.Click += new System.EventHandler(this.btnNewInUnit_Click);
            // 
            // btnNewModUnit
            // 
            this.btnNewModUnit.BackgroundImage = global::PatchWorker.Properties.Resources.new_modifier;
            this.btnNewModUnit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewModUnit.FlatAppearance.BorderSize = 0;
            this.btnNewModUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewModUnit.Location = new System.Drawing.Point(157, 11);
            this.btnNewModUnit.Name = "btnNewModUnit";
            this.btnNewModUnit.Size = new System.Drawing.Size(24, 24);
            this.btnNewModUnit.TabIndex = 5;
            this.btnNewModUnit.TabStop = false;
            this.controlPanelToolTip.SetToolTip(this.btnNewModUnit, "add modifier unit");
            this.btnNewModUnit.UseVisualStyleBackColor = true;
            this.btnNewModUnit.Click += new System.EventHandler(this.btnNewModUnit_Click);
            // 
            // btnSavePatchAs
            // 
            this.btnSavePatchAs.BackgroundImage = global::PatchWorker.Properties.Resources.patch_save_as;
            this.btnSavePatchAs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSavePatchAs.FlatAppearance.BorderSize = 0;
            this.btnSavePatchAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePatchAs.Location = new System.Drawing.Point(97, 11);
            this.btnSavePatchAs.Name = "btnSavePatchAs";
            this.btnSavePatchAs.Size = new System.Drawing.Size(24, 24);
            this.btnSavePatchAs.TabIndex = 3;
            this.btnSavePatchAs.TabStop = false;
            this.controlPanelToolTip.SetToolTip(this.btnSavePatchAs, "save patch as");
            this.btnSavePatchAs.UseVisualStyleBackColor = true;
            this.btnSavePatchAs.Click += new System.EventHandler(this.btnSavePatchAs_Click);
            // 
            // btnLoadPatch
            // 
            this.btnLoadPatch.BackgroundImage = global::PatchWorker.Properties.Resources.patch_open;
            this.btnLoadPatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoadPatch.FlatAppearance.BorderSize = 0;
            this.btnLoadPatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadPatch.Location = new System.Drawing.Point(25, 11);
            this.btnLoadPatch.Name = "btnLoadPatch";
            this.btnLoadPatch.Size = new System.Drawing.Size(24, 24);
            this.btnLoadPatch.TabIndex = 0;
            this.btnLoadPatch.TabStop = false;
            this.controlPanelToolTip.SetToolTip(this.btnLoadPatch, "load patch");
            this.btnLoadPatch.UseVisualStyleBackColor = true;
            this.btnLoadPatch.Click += new System.EventHandler(this.btnLoadPatch_Click);
            // 
            // btnNewPatch
            // 
            this.btnNewPatch.BackgroundImage = global::PatchWorker.Properties.Resources.patch_new;
            this.btnNewPatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewPatch.FlatAppearance.BorderSize = 0;
            this.btnNewPatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewPatch.Location = new System.Drawing.Point(49, 11);
            this.btnNewPatch.Name = "btnNewPatch";
            this.btnNewPatch.Size = new System.Drawing.Size(24, 24);
            this.btnNewPatch.TabIndex = 1;
            this.btnNewPatch.TabStop = false;
            this.controlPanelToolTip.SetToolTip(this.btnNewPatch, "new patch");
            this.btnNewPatch.UseVisualStyleBackColor = true;
            this.btnNewPatch.Click += new System.EventHandler(this.btnNewPatch_Click);
            // 
            // btnSavePatch
            // 
            this.btnSavePatch.BackgroundImage = global::PatchWorker.Properties.Resources.patch_save;
            this.btnSavePatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSavePatch.FlatAppearance.BorderSize = 0;
            this.btnSavePatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePatch.Location = new System.Drawing.Point(73, 11);
            this.btnSavePatch.Name = "btnSavePatch";
            this.btnSavePatch.Size = new System.Drawing.Size(24, 24);
            this.btnSavePatch.TabIndex = 2;
            this.btnSavePatch.TabStop = false;
            this.controlPanelToolTip.SetToolTip(this.btnSavePatch, "save patch");
            this.btnSavePatch.UseVisualStyleBackColor = true;
            this.btnSavePatch.Click += new System.EventHandler(this.btnSavePatch_Click);
            // 
            // ControlPanel
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.btnPanic);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnNewOutUnit);
            this.Controls.Add(this.btnNewInUnit);
            this.Controls.Add(this.btnNewModUnit);
            this.Controls.Add(this.btnSavePatchAs);
            this.Controls.Add(this.btnLoadPatch);
            this.Controls.Add(this.btnNewPatch);
            this.Controls.Add(this.btnSavePatch);
            this.DoubleBuffered = true;
            this.Name = "ControlPanel";
            this.Size = new System.Drawing.Size(400, 45);
            this.ResumeLayout(false);

        }

        //- control handlers ----------------------------------------------------------

        private void btnLoadPatch_Click(object sender, EventArgs e)
        {
            patchWnd.loadPatch();
        }

        private void btnNewPatch_Click(object sender, EventArgs e)
        {
            patchWnd.newPatch();
        }

        private void btnSavePatch_Click(object sender, EventArgs e)
        {
            patchWnd.savePatch(false);
        }

        private void btnSavePatchAs_Click(object sender, EventArgs e)
        {
            patchWnd.savePatch(true);
        }

        private void btnNewInUnit_Click(object sender, EventArgs e)
        {
            patchWnd.addInputUnit();
        }

        private void btnNewModUnit_Click(object sender, EventArgs e)
        {
            patchWnd.addModifierUnit();
        }

        private void btnNewOutUnit_Click(object sender, EventArgs e)
        {
            patchWnd.addOutputUnit();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            patchWnd.hideCanvas();
        }

        private void btnPanic_Click(object sender, EventArgs e)
        {
            patchWnd.patchWork.sendMidiPanicMessage();
        }

        //- painting ------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //beveled edge
            Pen edgePen = new Pen(Color.Black, 2.0f);
            g.DrawLine(edgePen, 1, this.Height - 1, this.Width - 1, this.Height - 1);        //bottom
            g.DrawLine(edgePen, this.Width - 1, 1, this.Width - 1, this.Height - 1);         //right
        }
    }
}
