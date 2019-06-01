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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

using PatchWorker.UI;
using PatchWorker.Graph;
using PatchWorker.Dialogs;
using Transonic.Patch;
using Transonic.MIDI.System;

namespace PatchWorker
{
    public partial class PatchWindow : Form, IPatchView
    {
        //the front end
        public ControlPanel controlPanel;
        public PatchCanvas canvas;

        //the back end
        public PatchWork patchWork;
        public MidiSystem midiSystem;

        public Settings settings;

        String patchFolder;
        String patchFilename;

        bool canvasHidden;
        public int curCanvasHeight;
        public int minHeight;

        //cons
        public PatchWindow()
        {
            patchWork = new PatchWork(this);

            //start up midi engine
            midiSystem = new MidiSystem();

            settings = new Settings(this);          //read prog settings & unit list in from config file

            InitializeComponent();

            //control panel goes just below menubar
            controlPanel = new ControlPanel(this);
            controlPanel.Location = new Point(this.ClientRectangle.Left, PatchWndMenu.Bottom);
            //controlPanel.Size = new Size(this.ClientRectangle.Width, controlPanel.Height);
            this.Controls.Add(controlPanel);

            //patch canvas fills up entire client area between control panel & status bar
            canvas = new PatchCanvas(this);
            canvas.BackColor = Color.FromArgb(255, 140, 0);
            canvas.Location = new Point(this.ClientRectangle.Left, controlPanel.Bottom);
            canvas.Size = new Size(controlPanel.Width, PatchStatus.Top - controlPanel.Bottom);
            this.Controls.Add(canvas);
            canvasHidden = false;

            //set initial sizes
            minHeight = this.Size.Height - canvas.Height;
            this.MinimumSize = new System.Drawing.Size(controlPanel.Width, minHeight);
            this.Size = new Size(settings.patchWndWidth, settings.patchWndHeight);
            this.Location = new Point(settings.patchWndX, settings.patchWndY);

            patchFolder = settings.patchFolder;
            patchFilename = null;
            this.Text = "PatchWorker [new patch]";

            //wire the parts together
            patchWork.canvas = canvas;
            patchWork.updateUnitList();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (controlPanel != null)
            {
                controlPanel.Size = new Size(this.ClientSize.Width, controlPanel.Height);
            }

            if (canvas != null)
            {
                canvas.Size = new Size(this.ClientSize.Width, PatchStatus.Top - controlPanel.Bottom);
                if (!canvasHidden)
                {
                    settings.patchWndHeight = this.Height;
                }
            }
        }

        //save settings & clean up on shut down
        private void PatchWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            midiSystem.shutdown();

            settings.patchWndX = this.Location.X;
            settings.patchWndY = this.Location.Y;
            settings.patchWndWidth = this.Width;
            settings.patchFolder = patchFolder;
            settings.save();
        }

        //- file menu -----------------------------------------------------------------

        public void newPatch()
        {
            canvas.clearPatch();
            patchFilename = null;
            this.Text = "PatchWorker [new patch]";
        }

        public void loadPatch()
        {
            openPatchDialog.InitialDirectory = patchFolder;
            openPatchDialog.DefaultExt = "*.pwp";
            openPatchDialog.Filter = "patch files|*.pwp|All files|*.*";
            openPatchDialog.ShowDialog();
            String filename = openPatchDialog.FileName;
            if (filename.Length > 0)
            {
                patchFilename = filename;
                patchWork.loadPatch(patchFilename);
                this.Text = "PatchWorker [" + patchFilename + "]";
                patchFolder = Path.GetDirectoryName(filename);
            }
        }

        public void savePatch(bool newName)
        {
            if (newName || patchFilename == null)
            {
                String filename = "";
                savePatchDialog.InitialDirectory = patchFolder;
                savePatchDialog.DefaultExt = "*.pwp";
                savePatchDialog.Filter = "patch files|*.pwp|All files|*.*";
                savePatchDialog.ShowDialog();
                filename = savePatchDialog.FileName;
                if (filename.Length == 0) return;           //user canceled save dialog

                //add default extention if filename doesn't have one
                if (!filename.Contains('.'))
                    filename = filename + ".pwp";

                patchFilename = filename;
                patchFolder = Path.GetDirectoryName(patchFilename);
            }
            patchWork.savePatch(patchFilename);
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

        public void addInputUnit()
        {
            if (midiSystem.inputDevices.Count > 0)
            {
                InputUnitDialog unitdlg = new InputUnitDialog(this);
                unitdlg.Icon = this.Icon;
                unitdlg.ShowDialog();
                if (unitdlg.DialogResult == DialogResult.OK)
                {
                    InputUnit inUnit = new InputUnit(unitdlg.name, unitdlg.devName, unitdlg.chanNum);
                    patchWork.addInputUnit(inUnit);
                    patchWork.updateUnitList();
                }
            }
            else
            {
                String msg = "You can't add any input units because you \ndon't have any MIDI input devices installed!";
                MessageBox.Show(msg, "Zoinks!");
            }
        }

        public void addModifierUnit()
        {
            String msg = "Patchworker modifers are still in development. Anon! Anon!";
            MessageBox.Show(msg, "In the works");
        }

        public void addOutputUnit()
        {
            if (midiSystem.outputDevices.Count > 0)
            {
                OutputUnitDialog unitdlg = new OutputUnitDialog(this);
                unitdlg.Icon = this.Icon;
                unitdlg.ShowDialog();
                if (unitdlg.DialogResult == DialogResult.OK)
                {
                    OutputUnit outUnit = new OutputUnit(unitdlg.name, unitdlg.devName, unitdlg.chanNum);
                    patchWork.addOutputUnit(outUnit);
                    patchWork.updateUnitList();
                }
            }
            else
            {
                String msg = "You can't add any output units because you \ndon't have any MIDI output devices installed!";
                MessageBox.Show(msg, "Zoinks!");
            }
        }

        private void addInputUnitMenuItem_Click(object sender, EventArgs e)
        {
            addInputUnit();
        }

        private void addModifierUnitMenuItem_Click(object sender, EventArgs e)
        {
            addModifierUnit();
        }

        private void addOutputUnitMenuItem_Click(object sender, EventArgs e)
        {
            addOutputUnit();
        }

        //- canvas menu ----------------------------------------------------------------

        public void hideCanvas()
        {
            if (!canvasHidden)
            {
                canvasHidden = true;
                curCanvasHeight = this.ClientSize.Height;
                this.ClientSize = new System.Drawing.Size(canvas.Size.Width, minHeight);
                this.MaximumSize = new System.Drawing.Size(Int32.MaxValue, minHeight);
            }
            else
            {
                canvasHidden = false;
                this.MaximumSize = new System.Drawing.Size(0, 0);
                this.ClientSize = new System.Drawing.Size(canvas.Size.Width, curCanvasHeight);
            }
        }

        private void hideShowCanvasMenuItem_Click(object sender, EventArgs e)
        {
            hideCanvas();
        }

        //- help menu ----------------------------------------------------------------

        private void helpAboutMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Patchworker\nversion " + Settings.VERSION + "\n\xA9 Transonic Software 1995-2019\n" +
                "http://transonic.kohoutech.com";
            MessageBox.Show(msg, "About");
        }

        //- IPatchView interface ----------------------------------------------

        public PatchBox getPatchBox(PaletteItem item)
        {
            PatchUnit unit = (PatchUnit)item.tag;               //get patch unit obj from menu item
            patchWork.addUnitToPatch(unit);                     //add patch unit to graph
            PatchUnitBox newBox = new PatchUnitBox(unit);       //create new patch box from unit
            return newBox;
        }

        public PatchWire getPatchWire(PatchPanel source, PatchPanel dest)
        {
            //connect source & dest units in graph
            PatchUnit srcUnit = ((PatchUnitBox)source.patchbox).unit;
            int srcJack = source.jackNum;
            PatchUnit destUnit = ((PatchUnitBox)dest.patchbox).unit;
            int destJack = source.jackNum;
            PatchCord patchCord = patchWork.addCordToPath(srcUnit, srcJack, destUnit, destJack);

            PatchUnitWire newWire = new PatchUnitWire(source, dest, patchCord);    //create new patch wire from connection
            return newWire;
        }
    }
}

//Console.WriteLine("there's no sun in the shadow of the Wizard");
