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
using System.IO;

using PatchWorker.UI;
using PatchWorker.Graph;
using PatchWorker.Dialogs;
using Transonic.Patch;
using Transonic.MIDI.System;
using Origami.ENAML;

namespace PatchWorker
{
    public partial class PatchWindow : Form
    {
        readonly Color CANVASCOLOR = Color.FromArgb(255, 130, 0);       //UT Orange!
        readonly Color PALETTECOLOR = Color.FromArgb(255, 165, 0);

        //the front end
        public ControlPanel controlPanel;
        public PatchCanvas canvas;

        //the back end
        public PatchWork patchWork;
        public MidiSystem midiSystem;

        public Settings settings;

        String patchFolder;
        String patchFilename;
        String pluginFolder;

        bool canvasHidden;
        public int curCanvasHeight;
        public int minHeight;

        public bool hasChanged;

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
            canvas = new PatchCanvas(patchWork);
            canvas.BackColor = CANVASCOLOR;
            canvas.setPaletteColor(PALETTECOLOR);
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
            pluginFolder = settings.pluginFolder;
            patchFilename = null;
            this.Text = "PatchWorker [new patch]";
            hasChanged = false;

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

        public void patchHasChanged()
        {
            if (!hasChanged)
            {
                this.Text = this.Text + " *";
            }
            hasChanged = true;
        }

        //save settings & clean up on shut down
        private void PatchWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            midiSystem.shutdown();

            settings.patchWndX = this.Location.X;
            settings.patchWndY = this.Location.Y;
            settings.patchWndWidth = this.Width;
            settings.patchFolder = (patchFolder != null) ? patchFolder : Application.StartupPath;
            settings.save();
        }

        //- file menu -----------------------------------------------------------------

        public void newPatch()
        {
            canvas.clearPatch();
            patchFilename = null;
            this.Text = "PatchWorker [new patch]";
            hasChanged = false;
        }

        public void loadPatch()
        {
#if (DEBUG)
            String filename = "patch2.pwp";
#else

            openPatchDialog.InitialDirectory = patchFolder;
            openPatchDialog.FileName = "";
            openPatchDialog.DefaultExt = "*.pwp";
            openPatchDialog.Filter = "patch files|*.pwp|All files|*.*";
            DialogResult result = openPatchDialog.ShowDialog();
            String filename = openPatchDialog.FileName;
            if ((result == DialogResult.Cancel) || (filename.Length == 0)) return;           //user canceled load dialog
#endif

            patchFilename = filename;
            canvas.loadPatch(patchFilename);
            this.Text = "PatchWorker [" + patchFilename + "]";
            hasChanged = false;
            patchFolder = Path.GetDirectoryName(Path.GetFullPath(filename));

        }

        public void savePatch(bool newName)
        {
            bool renamed = false;
            if (newName || patchFilename == null)
            {
                String filename = "";
                savePatchDialog.InitialDirectory = patchFolder;
                savePatchDialog.DefaultExt = "*.pwp";
                savePatchDialog.Filter = "patch files|*.pwp|All files|*.*";
                DialogResult result = savePatchDialog.ShowDialog();
                filename = savePatchDialog.FileName;
                if ((result == DialogResult.Cancel) || (filename.Length == 0)) return;           //user canceled save dialog

                //add default extention if filename doesn't have one
                if (!filename.Contains('.'))
                    filename = filename + ".pwp";

                patchFilename = filename;
                patchFolder = Path.GetDirectoryName(Path.GetFullPath(patchFilename));
                renamed = true;
            }
            canvas.savePatch(patchFilename);
            this.Text = "PatchWorker [" + patchFilename + "]";
            hasChanged = false;
            if (renamed)
            {
                String msg = "Current patch has been saved as\n " + Path.GetFileName(patchFilename);
                MessageBox.Show(msg, "Saved");
            }
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
#if (DEBUG)
            String filename = @"Plugin\PowerChord.dll";
#else
            openPatchDialog.InitialDirectory = pluginFolder;
            openPatchDialog.FileName = "";
            openPatchDialog.DefaultExt = "*.dll";
            openPatchDialog.Filter = "patchworker plugin files|*.dll|All files|*.*";
            DialogResult result = openPatchDialog.ShowDialog();
            String filename = openPatchDialog.FileName;
            if ((result == DialogResult.Cancel) || (filename.Length == 0)) return;           //user canceled plugin load dialog
#endif

            //we get back a modifier factory, but it gets thrown away if the plugin failed to load
            ModifierFactory modFact = new ModifierFactory(filename);
            if (modFact.enabled)
            {
                patchWork.addModiferFactory(modFact);
                patchWork.updateUnitList();
            }
            else
            {
                String msg = "Unable to load PatchWorker plugin: " + filename;
                MessageBox.Show(msg, "Plugin load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            pluginFolder = Path.GetDirectoryName(Path.GetFullPath(filename));
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
                    OutputUnit outUnit = new OutputUnit(unitdlg.name, unitdlg.devName, unitdlg.chanNum, unitdlg.progCount);
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

        //- midi menu ----------------------------------------------------------------

        private void allNotesOffMidiMenuItem_Click(object sender, EventArgs e)
        {
            patchWork.sendMidiPanicMessage();
        }

        //- help menu ----------------------------------------------------------------

        private void helpAboutMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Patchworker\nversion " + Settings.VERSION + "\n\xA9 Transonic Software 1995-2019\n" +
                "http://transonic.kohoutech.com";
            MessageBox.Show(msg, "About");
        }
    }
}

//Console.WriteLine("there's no sun in the shadow of the Wizard");
