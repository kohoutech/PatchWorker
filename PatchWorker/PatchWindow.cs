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

        //cons
        public PatchWindow()
        {
            //start up midi engine
            midiSystem = new MidiSystem();

            settings = new Settings(this);          //read prog settings & unit list in from config file

            InitializeComponent();

            //control panel goes just below menubar
            controlPanel = new ControlPanel(this);
            this.Controls.Add(controlPanel);
            controlPanel.Location = new Point(this.ClientRectangle.Left, PatchWndMenu.Bottom);
            //controlPanel.Size = new Size(this.ClientRectangle.Width, controlPanel.Height);
            this.Controls.Add(controlPanel);

            //patch canvas fills up entire client area between control panel & status bar
            canvas = new PatchCanvas(this);
            canvas.Location = new Point(this.ClientRectangle.Left, controlPanel.Bottom);
            canvas.Size = new Size(controlPanel.Width, PatchStatus.Top - controlPanel.Bottom);
            this.Controls.Add(canvas);
            canvasHidden = false;

            //set initial sizes
            this.MinimumSize = new System.Drawing.Size(controlPanel.Width, this.Size.Height - canvas.Height);
            this.Size = new Size(settings.patchWndWidth, settings.patchWndHeight);
            this.Location = new Point(settings.patchWndX, settings.patchWndY);

            patchFolder = settings.patchFolder;
            patchFilename = null;
            this.Text = "PatchWorker [new patch]";

            patchWork = new PatchWork(this);
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
                    //settings.rackHeight = this.ClientSize.Height - (this.AudimatMenu.Height + controlPanel.Height + this.AudimatStatus.Height);
                }
            }
        }

        //save settings & clean up on shut down
        private void PatchWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            midiSystem.shutdown();

            settings.patchWndX = this.Location.X;
            settings.patchWndY = this.Location.Y;
            settings.patchWndHeight = this.Height;
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

        private void loadPatch()
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

        public void addInputUnitMenuItem_Click(object sender, EventArgs e)
        {
            if (midiSystem.inputDevices.Count > 0)
            {
                InputUnitDialog unitdlg = new InputUnitDialog(this);
                unitdlg.ShowDialog();
                if (unitdlg.DialogResult == DialogResult.OK)
                {
                    //InputUnit inUnit = new InputUnit(patchworker, unitdlg.name, unitdlg.devName, unitdlg.chanNum, true);
                    //patchworker.addInputUnit(inUnit);
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
            if (midiSystem.outputDevices.Count > 0)
            {
                OutputUnitDialog unitdlg = new OutputUnitDialog(this);
                unitdlg.ShowDialog();
                if (unitdlg.DialogResult == DialogResult.OK)
                {
            //        OutputUnit outUnit = new OutputUnit(patchworker, unitdlg.name, unitdlg.devName, unitdlg.chanNum, unitdlg.progNum, true);
            //        patchworker.addOutputUnit(outUnit);
                }
            }
            else
            {
                String msg = "You can't add any output units because you \ndon't have any MIDI output devices installed!";
                MessageBox.Show(msg, "Zoinks!");
            }
        }

        //- help menu ----------------------------------------------------------------

        private void helpAboutMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Patchworker\nversion " + Settings.VERSION + "\n\xA9 Transonic Software 1997-2019\n" +
                "http://transonic.kohoutech.com";
            MessageBox.Show(msg, "About");
        }
    }
}

//Console.WriteLine("there's no sun in the shadow of the Wizard");
