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

using PatchWorker.Graph;
using Transonic.MIDI.System;
using Origami.ENAML;

namespace PatchWorker
{
    public class Settings
    {
        public static String VERSION = "1.3.0";
        const String CONFIGFILENAME = "patchworker.cfg";

        PatchWindow patchWnd;

        //global settings
        public int patchWndX;
        public int patchWndY;
        public int patchWndHeight;
        public int patchWndWidth;
        public String patchFolder;

        //unit lists
        public List<PatchUnit> allUnitList;
        public List<InputUnit> inputUnitList;
        public List<ModifierUnit> modifierUnitList;
        public List<OutputUnit> outputUnitList;

        public Settings(PatchWindow _patchWnd)
        {
            patchWnd = _patchWnd;

            EnamlData data = new EnamlData(CONFIGFILENAME);

            string version = data.getStringValue("version", VERSION);
            patchWndX = data.getIntValue("global-settings.patch-window.x", 100);
            patchWndY = data.getIntValue("global-settings.patch-window.y", 100);
            patchWndWidth = data.getIntValue("global-settings.patch-window.width", 400);
            patchWndHeight = data.getIntValue("global-settings.patch-window.height", 400);
            patchFolder = data.getStringValue("global-settings.patch-window.patch-folder", Application.StartupPath);

            //init unit lists
            allUnitList = new List<PatchUnit>();
            inputUnitList = new List<InputUnit>();
            modifierUnitList = new List<ModifierUnit>();
            outputUnitList = new List<OutputUnit>();

            loadUnits(data);
        }

        public void loadUnits(EnamlData data)
        {
            List<String> inputlist = data.getSubpathKeys("input-units");
            foreach (String inputName in inputlist)
            {
                InputUnit inUnit = InputUnit.loadFromConfig(data, "input-units." + inputName);
                inputUnitList.Add(inUnit);
            }

            List<String> outputlist = data.getSubpathKeys("output-units");
            foreach (String outputName in outputlist)
            {
                OutputUnit outUnit = OutputUnit.loadFromConfig(data, "output-units." + outputName);
                outputUnitList.Add(outUnit);
            }

            //                InputDevice inDev = patchworker.midiSystem.findInputDevice(devicename);
            //    bool enabled = (inDev != null);

            //    OutputDevice outDev = patchworker.midiSystem.findOutputDevice(devicename);
            //    bool enabled = (outDev != null);

            //    //notify user of any disabled units
            //    int disabledcount = 0;
            //    String disabledList = null;
            //    foreach (PatchUnit unit in allUnitList)
            //    {
            //        if (!unit.enabled)
            //        {
            //            disabledcount++;
            //            if (disabledList != null)
            //            {
            //                disabledList = disabledList + "\n";
            //            }
            //            disabledList = disabledList + unit.name;
            //        }
            //    }
            //    if (disabledcount > 0)
            //    {
            //        String msg = "Couldn't load these units:\n" + disabledList +
            //       "\nto enable them, reconnect them and restart Patchworker\n" +
            //       "If you have removed them from your system, delete them from the menu";
            //        MessageBox.Show(msg, "Warning");
            //    }
        }

        public void save()
        {
            EnamlData data = new EnamlData(CONFIGFILENAME);

            data.setStringValue("version", VERSION);
            data.setIntValue("global-settings.patch-window.x", patchWndX);
            data.setIntValue("global-settings.patch-window.y", patchWndY);
            data.setIntValue("global-settings.patch-window.width", patchWndWidth);
            data.setIntValue("global-settings.patch-window.height", patchWndHeight);
            data.setStringValue("global-settings.patch-window.patch-folder", patchFolder);

            saveUnits(data);

            data.saveToFile();
        }

        public void saveUnits(EnamlData data)
        {
            int count = 1;
                foreach (InputUnit inunit in inputUnitList)
                {
                    String path = "input-units.unit-" + count.ToString().PadLeft(3, '0');
                    inunit.saveToConfig(data, path);
                    count++;
                }

                count = 1;
                foreach (OutputUnit outunit in outputUnitList)
                {
                    String path = "output-units.unit-" + count.ToString().PadLeft(3, '0');
                    outunit.saveToConfig(data, path);
                    count++;
                }            
        }

        //public void registerLoaders()
        //{
        //    PatchBox.registerBoxType("PatchUnitBox", new PatchUnitBoxLoader());
        //    PatchPanel.registerPanelType("InJackPanel", new InJackPanelLoader());
        //    PatchPanel.registerPanelType("OutJackPanel", new OutJackPanelLoader());
        //    PatchPanel.registerPanelType("ProgramPanel", new ProgramPanelLoader());
        //}

    }
}
