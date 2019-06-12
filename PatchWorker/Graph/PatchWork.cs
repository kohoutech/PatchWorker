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
        public List<ModifierFactory> modifierFactoryList;
        public List<OutputUnit> outputUnitList;

        Dictionary<String, PatchUnit> IOUnitNames;     //for looking up a input/output unit by its name
        Dictionary<String, ModifierFactory> ModFactNames;

        public List<PatchUnit> patchUnits;              //units that have been added to the patch graph
        
        public PatchWork(PatchWindow _patchWindow)
        {
            patchWnd = _patchWindow;
            canvas = patchWnd.canvas;

            //init unit lists
            inputUnitList = new List<InputUnit>();
            modifierFactoryList = new List<ModifierFactory>();
            outputUnitList = new List<OutputUnit>();
            IOUnitNames = new Dictionary<string, PatchUnit>();
            ModFactNames = new Dictionary<string, ModifierFactory>();

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
            IOUnitNames.Add(inUnit.name, inUnit);
        }

        public void addModiferFactory(ModifierFactory modFact)
        {
            modFact.patchWork = this;
            modifierFactoryList.Add(modFact);
            ModFactNames.Add(modFact.plugName, modFact);
        }

        public void addOutputUnit(OutputUnit outUnit)
        {
            outUnit.patchWork = this;
            outUnit.outDev = patchWnd.midiSystem.findOutputDevice(outUnit.outDevName);
            outUnit.enabled = (outUnit.inputDev == null);
            outputUnitList.Add(outUnit);
            IOUnitNames.Add(outUnit.name, outUnit);
        }

        //this finds a unit from the unit's name read from a patch file
        public PatchUnit getUnit(String unitName)
        {
            PatchUnit result = null;
            if (IOUnitNames.ContainsKey(unitName))
            {
                result = IOUnitNames[unitName];
            }
            return result;
        }

        //and this does the same for modifier factories
        private ModifierFactory getModFactory(string factName)
        {
            ModifierFactory result = null;
            if (ModFactNames.ContainsKey(factName))
            {
                result = ModFactNames[factName];
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

            foreach (ModifierFactory modFact in modifierFactoryList)
            {
                PaletteItem item = new PaletteItem(modFact.plugName);
                item.tag = modFact;
                item.enabled = modFact.enabled;
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
            String disabledUnits = "";
            String disabledPlugins = "";

            //input units
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
                    disabledUnits = disabledUnits + inUnit.name + "\n";
                }
#endif
                inputUnitList.Add(inUnit);
                IOUnitNames.Add(inUnit.name, inUnit);
            }

            //modifier units
            List<String> modlist = data.getPathKeys("modifier-units");
            foreach (String modName in modlist)
            {
                ModifierFactory modFact = ModifierFactory.loadFromConfig(data, "modifier-units." + modName);
                modFact.patchWork = this;
#if (!DEBUG)
                if (!modFact.enabled)
                {
                    disabledcount++;
                    disabledPlugins = disabledPlugins + modFact.plugName + "\n";
                }
#endif
                modifierFactoryList.Add(modFact);
                ModFactNames.Add(modFact.plugName, modFact);
            }

            //output units
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
                    disabledUnits = disabledUnits + outUnit.name + "\n";
                }
#endif
                outputUnitList.Add(outUnit);
                IOUnitNames.Add(outUnit.name, outUnit);
            }

            //notify user of any disabled units or plugins
            if (disabledcount > 0)
            {
                String msg = "";
                if (disabledUnits.Length > 0)
                {
                    msg += ("Couldn't load these units:\n" + disabledUnits);
                }
                if (disabledPlugins.Length > 0)
                {
                    msg += ("Couldn't load these modifiers:\n" + disabledPlugins);
                }
                msg += "to enable units, reconnect them and restart Patchworker\n" +
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
            foreach (ModifierFactory modFact in modifierFactoryList)
            {
                String path = "modifier-units.unit-" + count.ToString().PadLeft(3, '0');
                modFact.saveToConfig(data, path);
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

        //add new patch unit to unit list & start it up
        public void addUnitToPatch(PatchUnit unit)
        {
            patchUnits.Add(unit);
            if (unit is InputUnit || unit is OutputUnit)
            {
                canvas.disablePaletteItem(unit.paletteItem);    //we disable menu item for i/o units while unit is in graph
            }
            unit.start();
        }

        //connections should already have been removed when this is called
        public void removeUnitFromPatch(PatchUnit unit)
        {
            patchUnits.Remove(unit);                            //remove the unit from the patch graph
            if (unit is InputUnit || unit is OutputUnit)
            {
                canvas.enablePaletteItem(unit.paletteItem);     //re-enable menu items for input and output units            
            }
            if (unit is ModifierUnit)
            {
                ((ModifierUnit)unit).plugin.closePluginDialog();       //close plugin's dialog if its open
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
            PatchUnit unit = null;
            if (item.tag is ModifierFactory)
            {
                ModifierFactory modFact = (ModifierFactory)item.tag;
                unit = modFact.newModifierUnit();
            }
            else
            {
                unit = (PatchUnit)item.tag;                             //get patch unit obj from menu item

            }
            addUnitToPatch(unit);                                       //add patch unit to graph
            PatchUnitBox newBox = new PatchUnitBox(unit);               //create new patch box from unit
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

        //loading data from patch files -----------------------------

        public void loadPatchData(EnamlData data)
        {
            string version = data.getStringValue("patchworker-version", Settings.VERSION);
        }

        //load a unit by name from the patch file and create a patch unit box for that unit
        public PatchBox loadPatchBox(EnamlData data, String boxPath)
        {
            String name = data.getStringValue(boxPath + ".name", "");
            PatchUnit unit = getUnit(name);                             //get patch unit obj from name in patch file
            if (unit == null)
            {                
                ModifierFactory modFact = getModFactory(name);                      //if not found in i/o units, must be a modifier
                int modNum = data.getIntValue(boxPath + ".mod-num", 1);
                unit = modFact.loadUnitFromPatch(data, boxPath, modNum);
            }
            addUnitToPatch(unit);                                       //add patch unit to graph
            PatchUnitBox newBox = new PatchUnitBox(unit);               //create new patch box from unit

            if (unit is OutputUnit)
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

        //saving data from patch files ------------------------------

        public void savePatchData(EnamlData data)
        {
            data.setStringValue("patchworker-version", Settings.VERSION);
        }

        public void savePatchBox(EnamlData data, string path, PatchBox box)
        {
            PatchUnitBox unitBox = (PatchUnitBox)box;
            PatchUnit unit = unitBox.unit;

            if (unit is ModifierUnit)
            {
                ModifierUnit modUnit = (ModifierUnit)unit;
                data.setStringValue(path + ".name", modUnit.modFact.plugName);      //use the modifier plugin's name
                data.setIntValue(path + ".mod-num", modUnit.modNum);
                modUnit.saveUnitToPatch(data, path);
            }
            else
            {
                data.setStringValue(path + ".name", unit.name);
            }

            if (unit is OutputUnit)
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

        //called when the canvas clears all the boxes and wires
        public void patchHasBeenCleared()
        {
            //reset unit count for all factories
            foreach (ModifierFactory modFact in modifierFactoryList)
            {
                modFact.resetUnitCount();
            }            
        }
    }

}

//Console.WriteLine("there's no sun in the shadow of the wizard");
