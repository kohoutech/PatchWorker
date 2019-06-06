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

using PatchWorker.UI;
using Transonic.Patch;
using Origami.ENAML;

namespace PatchWorker.Graph
{
    public class PatchWork : IPatchModel
    {
        public PatchWindow patchWnd;
        public PatchCanvas canvas;

        //unit lists
        public List<InputUnit> inputUnitList;
        public List<ModifierUnit> modifierUnitList;
        public List<OutputUnit> outputUnitList;
        Dictionary<String, PatchUnit> allUnitNames;     //for looking up a unit by its name

        public List<PatchUnit> patchUnits;              //units that have been added to the patch graph


        public PatchWork(PatchWindow _patchWindow)
        {
            patchWnd = _patchWindow;
            canvas = patchWnd.canvas;

            //init unit lists
            inputUnitList = new List<InputUnit>();
            modifierUnitList = new List<ModifierUnit>();
            outputUnitList = new List<OutputUnit>();
            allUnitNames = new Dictionary<string, PatchUnit>();

            //for testing purposes, adding modifier here, for now
            modifierUnitList.Add(new PowerChord("Power Chord"));

            //patch graph empty
            patchUnits = new List<PatchUnit>();
        }

        //- adding units  -----------------------------------------------------

        public void addInputUnit(InputUnit inUnit)
        {
            inUnit.patchWork = this;
            inUnit.inputDev = patchWnd.midiSystem.findInputDevice(inUnit.indevName);
            inUnit.enabled = (inUnit.inputDev == null);
            inputUnitList.Add(inUnit);
            allUnitNames.Add(inUnit.name, inUnit);
        }

        public void addOutputUnit(OutputUnit outUnit)
        {
            outUnit.patchWork = this;
            outUnit.outDev = patchWnd.midiSystem.findOutputDevice(outUnit.outDevName);
            outUnit.enabled = (outUnit.inputDev == null);
            outputUnitList.Add(outUnit);
            allUnitNames.Add(outUnit.name, outUnit);
        }

        public PatchUnit getUnit(String unitName)
        {
            PatchUnit result = null;
            if (allUnitNames.ContainsKey(unitName))
            {
                result = allUnitNames[unitName];
            }
            return result;
        }

        //wrap patch units in palette items so they can be displayed in canvas' palette
        public void updateUnitList()
        {
            List<PaletteItem> items = new List<PaletteItem>();
            foreach (InputUnit inUnit in inputUnitList)
            {
                PaletteItem item = new PaletteItem(inUnit.name);
                item.tag = inUnit;
                item.enabled = inUnit.enabled;
                inUnit.paletteItem = item;
                items.Add(item);
            }

            foreach (ModifierUnit modUnit in modifierUnitList)
            {
                PaletteItem item = new PaletteItem(modUnit.name);
                item.tag = modUnit;
                item.enabled = true;            //modifier units are always enabled
                modUnit.paletteItem = item;
                items.Add(item);
            }

            foreach (OutputUnit outUnit in outputUnitList)
            {
                PaletteItem item = new PaletteItem(outUnit.name);
                item.tag = outUnit;
                item.enabled = outUnit.enabled;
                outUnit.paletteItem = item;
                items.Add(item);
            }

            canvas.setPaletteItems(items);
        }

        public void loadUnits(EnamlData data)
        {
            int disabledcount = 0;
            String disabledList = "";

            List<String> inputlist = data.getPathKeys("input-units");
            foreach (String inputName in inputlist)
            {
                InputUnit inUnit = InputUnit.loadFromConfig(data, "input-units." + inputName);
                inUnit.patchWork = this;
                inUnit.inputDev = patchWnd.midiSystem.findInputDevice(inUnit.indevName);
#if (!DEBUG)
                if (inUnit.inputDev == null)
                {
                    inUnit.enabled = false;
                    disabledcount++;
                    disabledList = disabledList + inUnit.name + "\n";
                }
#endif
                inputUnitList.Add(inUnit);
                allUnitNames.Add(inUnit.name, inUnit);
            }

            List<String> outputlist = data.getPathKeys("output-units");
            foreach (String outputName in outputlist)
            {
                OutputUnit outUnit = OutputUnit.loadFromConfig(data, "output-units." + outputName);
                outUnit.patchWork = this;
                outUnit.outDev = patchWnd.midiSystem.findOutputDevice(outUnit.outDevName);
#if (!DEBUG)
                if (outUnit.outDev == null)
                {
                    outUnit.enabled = false;
                    disabledcount++;
                    disabledList = disabledList + outUnit.name + "\n";
                }
#endif
                outputUnitList.Add(outUnit);
                allUnitNames.Add(outUnit.name, outUnit);
            }

            //notify user of any disabled units
            if (disabledcount > 0)
            {
                String msg = "Couldn't load these units:\n" + disabledList +
               "to enable them, reconnect them and restart Patchworker\n" +
               "If you have removed them from your system, delete them from the menu";
                MessageBox.Show(msg, "Warning");
            }
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

        //- patch management --------------------------------------------------

        //add new input/output unit to unit list & start it up
        public void addIOUnitToPatch(PatchUnit unit)
        {
            patchUnits.Add(unit);
            canvas.disablePaletteItem(unit.paletteItem);    //so we disable menu item while unit is in graph
            unit.start();
        }

        //handle modifier units differently
        public PatchUnit addModiferToPatch(PatchUnit unit)
        {
            ModifierUnit modUnit = ((ModifierUnit)unit).copyUnit();
            modUnit.patchWork = this;

            patchUnits.Add(modUnit);
            return modUnit;
        }

        //connections should already have been removed when this is called
        public void removeUnitFromPatch(PatchUnit unit)
        {
            patchUnits.Remove(unit);                            //remove the unit from the patch graph
            if (unit is InputUnit || unit is OutputUnit)
            {
                canvas.enablePaletteItem(unit.paletteItem);     //re-enable menu items for input and output units            
            }
            unit.stop();
        }

        public PatchCord addCordToPatch(PatchUnit srcUnit, String srcJack, PatchUnit destUnit, String destJack)
        {
            return srcUnit.connectDest(destUnit);
        }

        public void removeCordFromPatch(PatchCord cord)
        {
            PatchUnit source = cord.srcUnit;
            source.disconnectCord(cord);
        }

        public void sendMidiPanicMessage()
        {
            foreach (PatchUnit unit in patchUnits)
            {
                if (unit is OutputUnit)
                {
                    OutputUnit outunit = (OutputUnit)unit;

                    outunit.sendAllNotesOff();
                }
            }
        }

        //- IPatchModel interface ----------------------------------------------

        public PatchBox getPatchBox(PaletteItem item)
        {
            PatchUnit unit = (PatchUnit)item.tag;               //get patch unit obj from menu item
            if (unit is ModifierUnit)
            {
                unit = addModiferToPatch(unit);                 //add new modifier unit to graph
            }
            else
            {
                addIOUnitToPatch(unit);                         //add input/output unit to graph
            }
            PatchUnitBox newBox = new PatchUnitBox(unit);       //create new patch box from unit
            return newBox;
        }

        public void removePatchBox(PatchBox box)
        {
            removeUnitFromPatch(((PatchUnitBox)box).unit);
        }

        public PatchWire getPatchWire(PatchPanel source, PatchPanel dest)
        {
            //connect source & dest units in graph
            PatchUnit srcUnit = ((PatchUnitBox)source.patchbox).unit;
            String srcJack = source.panelName;
            PatchUnit destUnit = ((PatchUnitBox)dest.patchbox).unit;
            String destJack = source.panelName;
            PatchCord patchCord = addCordToPatch(srcUnit, srcJack, destUnit, destJack);

            PatchUnitWire newWire = new PatchUnitWire(source, dest, patchCord);    //create new patch wire from connection
            return newWire;
        }

        public void removePatchWire(PatchWire wire)
        {
            removeCordFromPatch(((PatchUnitWire)wire).patchCord);
        }

        public void loadPatchData(EnamlData data)
        {
            string version = data.getStringValue("patchworker-version", Settings.VERSION);
        }

        public PatchBox loadPatchBox(EnamlData data, String boxPath)
        {
            String name = data.getStringValue(boxPath + ".name", "");
            PatchUnit unit = getUnit(name);                             //get patch unit obj from patch file name
            if (unit is ModifierUnit)
            {
                unit = addModiferToPatch(unit);                         //add new modifier unit to graph
            }
            else
            {
                addIOUnitToPatch(unit);                                 //add input/output unit to graph
            }
            PatchUnitBox newBox = new PatchUnitBox(unit);               //create new patch box from unit

            if (unit is OutputUnit || unit is ModifierUnit)
            {
                int progNum = data.getIntValue(boxPath + ".program", 0);
                ProgramPanel progPanel = (ProgramPanel)newBox.getPanel("programmer");
                progPanel.setProgram(progNum);
            }

            return newBox;
        }

        //does the same thing as <getPatchWire> for now so <data> and <path> are unused
        //could load additional data from the patch file if the model should expand
        public PatchWire loadPatchWire(EnamlData data, String path, PatchPanel source, PatchPanel dest)
        {
            PatchWire newWire = getPatchWire(source, dest);
            return newWire;
        }

        public void savePatchData(EnamlData data)
        {
            data.setStringValue("patchworker-version", Settings.VERSION);
        }

        public void savePatchBox(EnamlData data, string path, PatchBox box)
        {
            PatchUnitBox unitBox = (PatchUnitBox)box;
            PatchUnit unit = unitBox.unit;
            data.setStringValue(path + ".name", unit.name);

            if (unit is OutputUnit || unit is ModifierUnit)
            {
                ProgramPanel progPanel = (ProgramPanel)unitBox.getPanel("programmer");
                data.setIntValue(path + ".program", progPanel.progNum);
            }
        }

        //doesn't store any additional data for now
        public void savePatchWire(EnamlData data, string path, PatchWire wire)
        {
        }

        public void patchHasChanged()
        {
            patchWnd.patchHasChanged();
        }
    }
}
