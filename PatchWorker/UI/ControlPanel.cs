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
            this.btnSavePatch = new System.Windows.Forms.Button();
            this.btnNewPatch = new System.Windows.Forms.Button();
            this.btnLoadPatch = new System.Windows.Forms.Button();
            this.btnSavePatchAs = new System.Windows.Forms.Button();
            this.btnNewOutUnit = new System.Windows.Forms.Button();
            this.btnNewInUnit = new System.Windows.Forms.Button();
            this.btnNewModUnit = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSavePatch
            // 
            this.btnSavePatch.Location = new System.Drawing.Point(73, 11);
            this.btnSavePatch.Name = "btnSavePatch";
            this.btnSavePatch.Size = new System.Drawing.Size(24, 24);
            this.btnSavePatch.TabIndex = 2;
            this.btnSavePatch.TabStop = false;
            this.btnSavePatch.Text = "S";
            this.btnSavePatch.UseVisualStyleBackColor = true;
            this.btnSavePatch.Click += new System.EventHandler(this.btnSavePatch_Click);
            // 
            // btnNewPatch
            // 
            this.btnNewPatch.Location = new System.Drawing.Point(49, 11);
            this.btnNewPatch.Name = "btnNewPatch";
            this.btnNewPatch.Size = new System.Drawing.Size(24, 24);
            this.btnNewPatch.TabIndex = 1;
            this.btnNewPatch.TabStop = false;
            this.btnNewPatch.Text = "N";
            this.btnNewPatch.UseVisualStyleBackColor = true;
            this.btnNewPatch.Click += new System.EventHandler(this.btnNewPatch_Click);
            // 
            // btnLoadPatch
            // 
            this.btnLoadPatch.Location = new System.Drawing.Point(25, 11);
            this.btnLoadPatch.Name = "btnLoadPatch";
            this.btnLoadPatch.Size = new System.Drawing.Size(24, 24);
            this.btnLoadPatch.TabIndex = 0;
            this.btnLoadPatch.TabStop = false;
            this.btnLoadPatch.Text = "L";
            this.btnLoadPatch.UseVisualStyleBackColor = true;
            this.btnLoadPatch.Click += new System.EventHandler(this.btnLoadPatch_Click);
            // 
            // btnSavePatchAs
            // 
            this.btnSavePatchAs.Location = new System.Drawing.Point(97, 11);
            this.btnSavePatchAs.Name = "btnSavePatchAs";
            this.btnSavePatchAs.Size = new System.Drawing.Size(24, 24);
            this.btnSavePatchAs.TabIndex = 3;
            this.btnSavePatchAs.TabStop = false;
            this.btnSavePatchAs.Text = "A";
            this.btnSavePatchAs.UseVisualStyleBackColor = true;
            this.btnSavePatchAs.Click += new System.EventHandler(this.btnSavePatchAs_Click);
            // 
            // btnNewOutUnit
            // 
            this.btnNewOutUnit.Location = new System.Drawing.Point(181, 11);
            this.btnNewOutUnit.Name = "btnNewOutUnit";
            this.btnNewOutUnit.Size = new System.Drawing.Size(24, 24);
            this.btnNewOutUnit.TabIndex = 6;
            this.btnNewOutUnit.TabStop = false;
            this.btnNewOutUnit.Text = "O";
            this.btnNewOutUnit.UseVisualStyleBackColor = true;
            this.btnNewOutUnit.Click += new System.EventHandler(this.btnNewOutUnit_Click);
            // 
            // btnNewInUnit
            // 
            this.btnNewInUnit.Location = new System.Drawing.Point(133, 11);
            this.btnNewInUnit.Name = "btnNewInUnit";
            this.btnNewInUnit.Size = new System.Drawing.Size(24, 24);
            this.btnNewInUnit.TabIndex = 4;
            this.btnNewInUnit.TabStop = false;
            this.btnNewInUnit.Text = "I";
            this.btnNewInUnit.UseVisualStyleBackColor = true;
            this.btnNewInUnit.Click += new System.EventHandler(this.btnNewInUnit_Click);
            // 
            // btnNewModUnit
            // 
            this.btnNewModUnit.Location = new System.Drawing.Point(157, 11);
            this.btnNewModUnit.Name = "btnNewModUnit";
            this.btnNewModUnit.Size = new System.Drawing.Size(24, 24);
            this.btnNewModUnit.TabIndex = 5;
            this.btnNewModUnit.TabStop = false;
            this.btnNewModUnit.Text = "M";
            this.btnNewModUnit.UseVisualStyleBackColor = true;
            this.btnNewModUnit.Click += new System.EventHandler(this.btnNewModUnit_Click);
            // 
            // btnHide
            // 
            this.btnHide.Location = new System.Drawing.Point(217, 11);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(24, 24);
            this.btnHide.TabIndex = 7;
            this.btnHide.TabStop = false;
            this.btnHide.Text = "H";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // ControlPanel
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(117)))), ((int)(((byte)(24)))));
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
            this.Size = new System.Drawing.Size(400, 44);
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

        //- painting ------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //beveled edge
            g.DrawLine(Pens.SteelBlue, 1, this.Height - 1, this.Width - 1, this.Height - 1);        //bottom
            g.DrawLine(Pens.SteelBlue, this.Width - 1, 1, this.Width - 1, this.Height - 1);         //right
        }
    }
}
