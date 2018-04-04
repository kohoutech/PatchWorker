/* ----------------------------------------------------------------------------
Patchworker : a midi patchbay
Copyright (C) 1995-2018  George E Greaney

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
using System.Xml;

using PatchWorker.UI;
using PatchWorker.Graph;
using PatchWorker.Dialogs;
using Transonic.MIDI.System;
using Transonic.Patch;

namespace PatchWorker
{
    public partial class PatchWindow : Form, IPatchView
    {
        public PatchWorker patchworker;
        public PatchCanvas canvas;
        String patchFilename;

        int inputUnitMenuItems;
        int outputUnitMenuItems;

        //cons
        public PatchWindow()
        {
            patchworker = new PatchWorker(this);

            InitializeComponent();

            inputUnitMenuItems = 0;
            outputUnitMenuItems = 0;
            patchworker.loadConfig(this);

            canvas = new PatchCanvas(this);
            canvas.Dock = DockStyle.Fill;
            this.Controls.Add(canvas);

            patchFilename = null;
            this.Text = "PatchWorker [new patch]";
        }

        public void loadSettings(XmlNode windowNode)
        {
            int posX = Convert.ToInt32(windowNode.Attributes["posX"].Value);
            int posY = Convert.ToInt32(windowNode.Attributes["posY"].Value);
            this.Location = new Point(posX, posY);
            int width = Convert.ToInt32(windowNode.Attributes["width"].Value);
            int height = Convert.ToInt32(windowNode.Attributes["height"].Value);
            this.Size = new Size(width, height);
        }

        //save settings & clean up on shut down
        private void PatchWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            patchworker.shutdown(this);
        }

        public void saveToXML(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("patchwindow");
            xmlWriter.WriteAttributeString("posX", this.Location.X.ToString());
            xmlWriter.WriteAttributeString("posY", this.Location.Y.ToString());
            xmlWriter.WriteAttributeString("width", this.Width.ToString());
            xmlWriter.WriteAttributeString("height", this.Height.ToString());
            xmlWriter.WriteEndElement();
        }

//- file menu -----------------------------------------------------------------

        public void newPatch()
        {
            canvas.clearPatch();
            patchFilename = null;
            this.Text = "PatchWorker [new patch]";
        }

        private void loadPatch()
        {
            openPatchDialog.InitialDirectory = Application.StartupPath;
            openPatchDialog.DefaultExt = "*.pwp";
            openPatchDialog.Filter = "patch files|*.pwp|All files|*.*";
            openPatchDialog.ShowDialog();
            String filename = openPatchDialog.FileName;
            if (filename.Length > 0)
            {
                patchFilename = filename;
                canvas.loadPatch(patchFilename);
                this.Text = "PatchWorker [" + patchFilename + "]";
            }
        }

        public void savePatch(bool newName)
        {
            if (newName || patchFilename == null)
            {
                String filename = "";
                savePatchDialog.InitialDirectory = Application.StartupPath;
                savePatchDialog.DefaultExt = "*.pwp";
                savePatchDialog.Filter = "patch files|*.pwp|All files|*.*";
                savePatchDialog.ShowDialog();
                filename = savePatchDialog.FileName;
                if (filename.Length == 0) return;

                //add default extention if filename doesn't have one
                if (!filename.Contains('.'))
                    filename = filename + ".pwp";
                patchFilename = filename;
            }
            canvas.savePatch(patchFilename);
            String msg = "Current patch has been saved as\n " + patchFilename;
            MessageBox.Show(msg, "Saved");
            this.Text = "PatchWorker [" + patchFilename + "]";            
        }

        private void patchNewMenuItem_Click(object sender, EventArgs e)
        {
            newPatch();
        }

        private void patchLoadMenuItem_Click(object sender, EventArgs e)
        {
            loadPatch();
        }

        private void patchSaveMenuItem_Click(object sender, EventArgs e)
        {
            savePatch(false);
        }

        private void patchSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            savePatch(true);
        }

        private void fileExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

//- unit menu ---------------------------------------------------------------

        public void addInputUnitToMenu(PatchUnit unit)
        {
            ToolStripItem inputItem = new ToolStripMenuItem(unit.name);
            inputItem.Click += new EventHandler(unitSelectMenuItem_Click);
            inputItem.Tag = unit;
            unit.setMenuItem(inputItem);
            if (inputUnitMenuItems == 0)
            {
                unitMenuItem.DropDownItems.Add(new ToolStripSeparator());
                inputUnitMenuItems++;
            }
            unitMenuItem.DropDownItems.Insert((3 + inputUnitMenuItems), inputItem);
            inputUnitMenuItems++;
        }

        public void addModifierUnitToMenu(PatchUnit unit)
        {
        }

        public void addOutputUnitToMenu(PatchUnit unit)
        {
            ToolStripItem inputItem = new ToolStripMenuItem(unit.name);
            inputItem.Click += new EventHandler(unitSelectMenuItem_Click);
            inputItem.Tag = unit;
            unit.setMenuItem(inputItem);
            if (outputUnitMenuItems == 0)
            {
                unitMenuItem.DropDownItems.Add(new ToolStripSeparator());
                outputUnitMenuItems++;
            }
            unitMenuItem.DropDownItems.Insert((3 + inputUnitMenuItems + outputUnitMenuItems), inputItem);
            outputUnitMenuItems++;
        }

        public void addInputUnitMenuItem_Click(object sender, EventArgs e)
        {
            if (patchworker.midiSystem.inputDevices.Count > 0)
            {
                InputUnitDialog unitdlg = new InputUnitDialog(patchworker);
                unitdlg.ShowDialog();
                if (unitdlg.DialogResult == DialogResult.OK)
                {
                    InputUnit inUnit = new InputUnit(patchworker, unitdlg.name, unitdlg.devName, unitdlg.chanNum);
                    patchworker.addInputUnit(inUnit);
                }
            }
            else
            {
                String msg = "You can't add any input units because you \ndon't have any MIDI input devices installed!";
                MessageBox.Show(msg, "Zoinks!");
            }
        }

        public void addModifierUnitMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Patchworker modifers are still in development. Anon! Anon!";
            MessageBox.Show(msg, "In the works");
        }

        public void addOutputUnitMenuItem_Click(object sender, EventArgs e)
        {
            if (patchworker.midiSystem.outputDevices.Count > 0)
            {
                OutputUnitDialog unitdlg = new OutputUnitDialog(patchworker);
                unitdlg.ShowDialog();
                if (unitdlg.DialogResult == DialogResult.OK)
                {
                    OutputUnit outUnit = new OutputUnit(patchworker, unitdlg.name, unitdlg.devName, unitdlg.chanNum, unitdlg.progNum);
                    patchworker.addOutputUnit(outUnit);
                }
            }
            else
            {
                String msg = "You can't add any output units because you \ndon't have any MIDI output devices installed!";
                MessageBox.Show(msg, "Zoinks!");
            }
        }

        //handler for all unit menu items
        private void unitSelectMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            PatchUnit unit = (PatchUnit)item.Tag;                      //get patch unit obj from menu item

            patchworker.addUnitToPatch(unit);               //add patch unit to graph
            PatchUnitBox box = new PatchUnitBox(unit);      //create new patch box from unit
            canvas.addPatchBox(box);                        //and add it to canvas
        }

//- help menu ----------------------------------------------------------------

        private void helpAboutMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Patchworker\nversion 1.2.0\n" + 
                "\xA9 Transonic Software 1997-2018\n" + 
                "http://transonic.kohoutech.com";
            MessageBox.Show(msg, "About");
        }
    }
}

//Console.WriteLine("there's no sun in the shadow of the Wizard");
