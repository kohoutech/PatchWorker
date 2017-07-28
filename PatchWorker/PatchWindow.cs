/* ----------------------------------------------------------------------------
Patchworker : a midi patchbay
Copyright (C) 2005-2017  George E Greaney

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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PatchWorker.UI;
using PatchWorker.Graph;

using Transonic.MIDI.Engine;

namespace PatchWorker
{
    public partial class PatchWindow : Form
    {
        public PatchWorker patchworker;
        public PatchCanvas canvas;

        //cons
        public PatchWindow()
        {
            InitializeComponent();
            patchworker = new PatchWorker(this);
            canvas = new PatchCanvas(this);
            canvas.SetBounds(0, this.PatchMenu.Height, this.Width,
                this.Height - this.PatchMenu.Height - this.PatchStatus.Height);
            this.Controls.Add(canvas);
        }

        private void PatchWindow_Resize(object sender, EventArgs e)
        {
            //canvas.Size = this.ClientSize;
            canvas.SetBounds(0, this.PatchMenu.Height, this.Width, this.Height - this.PatchMenu.Height - this.PatchStatus.Height);
        }

        //save settings & clean up on shut down
        private void PatchWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            patchworker.saveUnitData();
            foreach (InputDevice indev in patchworker.midiSystem.inputDevices)
            {
                indev.stop();
                indev.close();
            }
            foreach (OutputDevice outdev in patchworker.midiSystem.outputDevices)
            {
                outdev.close();
            }
        }

        //- file menu -----------------------------------------------------------------

        private void fileExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

//- patch menu ----------------------------------------------------------------

        private void patchNewMenuItem_Click(object sender, EventArgs e)
        {

            canvas.clearPatch();
        }

        private void patchLoadMenuItem_Click(object sender, EventArgs e)
        {
            openPatchDialog.InitialDirectory = Application.StartupPath;
            openPatchDialog.ShowDialog();
            String patchfilename = openPatchDialog.FileName;
            if (patchfilename.Length > 0)
            {
                canvas.loadPatch(patchfilename);
            }
        }

        private void patchSaveMenuItem_Click(object sender, EventArgs e)
        {
            savePatchDialog.InitialDirectory = Application.StartupPath;
            savePatchDialog.ShowDialog();
            String patchfilename = savePatchDialog.FileName;
            if (patchfilename.Length > 0)
            {
                canvas.savePatch(patchfilename);
            }
        }

//- unit menu ---------------------------------------------------------------

        int inputItems;
        int modifierItems;
        int outputItems;

        public void initUnitMenu()
        {
            inputItems = 0;
            modifierItems = 0;
            outputItems = 0;
        }

        public void addUnitMenuItem(UnitData udata)
        {
            switch (udata.utype)
            {
                case UNITTYPE.INPUT:
                    addInputUnitMenuItem(udata);
                    break;
                case UNITTYPE.MODIFIER:
                    addModifierUnitMenuItem(udata);
                    break;
                case UNITTYPE.OUTPUT:
                    addOutputUnitMenuItem(udata);
                    break;
            }
        }

        public void addInputUnitMenuItem(UnitData udata)
        {
            ToolStripItem inputItem = new ToolStripMenuItem(udata.name);
            inputItem.Click += new EventHandler(unitSelectMenuItem_Click);
            inputItem.Tag = udata;
            udata.setMenuItem(inputItem);
            unitsMenuItem.DropDownItems.Insert((inputItems + 2), inputItem);
            inputItems++;
        }

        public void addModifierUnitMenuItem(UnitData udata)
        {
            ToolStripItem modifierItem = new ToolStripMenuItem(udata.name);
            modifierItem.Click += new EventHandler(unitSelectMenuItem_Click);
            modifierItem.Tag = udata;
            udata.setMenuItem(modifierItem);
            unitsMenuItem.DropDownItems.Insert((inputItems + modifierItems + 2), modifierItem);
            modifierItems++;
        }

        public void addOutputUnitMenuItem(UnitData udata)
        {
            ToolStripItem outputItem = new ToolStripMenuItem(udata.name);
            outputItem.Click += new EventHandler(unitSelectMenuItem_Click);
            outputItem.Tag = udata;
            udata.setMenuItem(outputItem);
            unitsMenuItem.DropDownItems.Insert((inputItems + modifierItems + outputItems + 2), outputItem);
            outputItems++;
        }

        //handler for all unit menu items
        private void unitSelectMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            UnitData udata = (UnitData)item.Tag;                //get unit def from menu item
            PatchUnit unit = patchworker.addUnitToPatch(udata);          //create patch unit from def & add it to graph
            canvas.addPatchBox(unit);                           //create unit view from unit model & add it to canvas
        }

        private void unitNewMenuItem_Click(object sender, EventArgs e)
        {
            UnitDataDialog unitdlg = new UnitDataDialog();
            unitdlg.patchworker = patchworker;
            unitdlg.setInitialData(null);      //new unit - no initial data
            unitdlg.ShowDialog();
            if (unitdlg.DialogResult == DialogResult.OK)
            {
                UnitData udata = unitdlg.udata;
                Console.WriteLine(" adding new unit data " + udata.name + " with dev = " + udata.devName + " and channel = " + udata.channelNum);
                patchworker.addUnitData(udata);
            }
        }

//- help menu ----------------------------------------------------------------

        private void helpAboutMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Patchworker\nversion 1.1.0\n" + 
                "\xA9 Transonic Software 1997-2017\n" + 
                "http://transonic.kohoutech.com";
            MessageBox.Show(msg, "About");
        }
    }
}

//  Console.WriteLine(" adding new unit data " + udata.name);
