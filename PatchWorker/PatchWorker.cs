/* ----------------------------------------------------------------------------
Patchworker : a midi patchbay
Copyright (C) 2005-2018  George E Greaney

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
using System.Xml;
using System.IO;
using System.Windows.Forms;

using Transonic.MIDI.System;
using Transonic.Patch;
using PatchWorker.Graph;
using PatchWorker.UI;

namespace PatchWorker
{
    public class PatchWorker
    {
        const String CONFIGFILENAME = "patchworker.cfg";

        public PatchWindow patchwin;            //the front end
        public MidiSystem midiSystem;           //the back end
        public static PatchWorker _patchworker;

        public List<PatchUnit> allUnitList;
        public List<InputUnit> inputUnitList;
        public List<ModifierUnit> modifierUnitList;
        public List<OutputUnit> outputUnitList;
        
        public List<PatchUnit> patchUnits;              //units that have been added to the patch graph

        public static PatchWorker getPatchWorker()
        {
            return _patchworker;
        }

        public PatchWorker(PatchWindow _patchwin)
        {
            if (_patchworker == null) _patchworker = this;
            patchwin = _patchwin;

            //start up midi engine
            midiSystem = new MidiSystem();

            //init unit lists
            allUnitList = new List<PatchUnit>();
            inputUnitList = new List<InputUnit>();
            modifierUnitList = new List<ModifierUnit>();
            outputUnitList = new List<OutputUnit>();

            //patch graph empty
            patchUnits = new List<PatchUnit>();

            registerLoaders();
        }

        public void shutdown(PatchWindow window)
        {
            saveConfig(window);
            midiSystem.shutdown();
        }

//- configuration load/save ---------------------------------------------------

        public void loadConfig(PatchWindow window)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(CONFIGFILENAME);

                foreach (XmlNode settingNode in xmlDoc.DocumentElement.ChildNodes)
                {
                    if (settingNode.Name.Equals("patchwindow"))
                    {
                        window.loadSettings(settingNode);
                    }

                    if (settingNode.Name.Equals("units"))
                    {
                        loadUnits(settingNode);
                    }
                }
            }
            catch (Exception ex)
            {
                //valid config file not found, ignore
            }
        }

        public void loadUnits(XmlNode unitsNode)
        {
            foreach (XmlNode unitNode in unitsNode.ChildNodes)
            {
                try
                {
                    if (unitNode.Name.Equals("inputunit"))
                    {
                        InputUnit inUnit = InputUnit.loadFromXML(this, unitNode);
                        addInputUnit(inUnit);
                    }
                    if (unitNode.Name.Equals("outputunit"))
                    {
                        OutputUnit outUnit = OutputUnit.loadFromXML(this, unitNode);
                        addOutputUnit(outUnit);
                    }
                    if (unitNode.Name.Equals("modiferunit"))
                    {
                        //not implemented yet
                    }
                }
                catch (PatchUnitLoadException ex)
                {
                    //need further error checking here
                }
            }

            //notify user of any disabled units
            int disabledcount = 0;
            String disabledList = null;
            foreach (PatchUnit unit in allUnitList)
            {
                if (!unit.enabled)
                {
                    disabledcount++;
                    if (disabledList != null)
                    {
                        disabledList = disabledList + "\n";
                    }
                    disabledList = disabledList + unit.name;
                }
            }
            if (disabledcount > 0)
            {
                String msg = "Couldn't load these units:\n" + disabledList +
               "\nto enable them, reconnect them and restart Patchworker\n" +
               "If you have removed them from your system, delete them from the menu";
                MessageBox.Show(msg, "Warning");
            }
        }

        public void saveConfig(PatchWindow window)
        {
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            XmlWriter xmlWriter = XmlWriter.Create(CONFIGFILENAME, settings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("patchworker");        
            xmlWriter.WriteAttributeString("version", "1.2.1");

            window.saveToXML(xmlWriter);
            saveUnits(xmlWriter);

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        public void saveUnits(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("units");
            foreach (InputUnit inunit in inputUnitList)
            {
                inunit.saveToXML(xmlWriter);
            }
            foreach (ModifierUnit modunit in modifierUnitList)
            {
                modunit.saveToXML(xmlWriter);
            }
            foreach (OutputUnit outunit in outputUnitList)
            {
                outunit.saveToXML(xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }

        public void registerLoaders()
        {
            PatchBox.registerBoxType("PatchUnitBox", new PatchUnitBoxLoader());
            PatchPanel.registerPanelType("InJackPanel", new InJackPanelLoader());
            PatchPanel.registerPanelType("OutJackPanel", new OutJackPanelLoader());
            PatchPanel.registerPanelType("ProgramPanel", new ProgramPanelLoader());
        }

//- units ---------------------------------------------------------------------

        public void addInputUnit(InputUnit unit)
        {
            inputUnitList.Add(unit);
            allUnitList.Add(unit);
            patchwin.addInputUnitToMenu(unit);
        }

        public void addModifierUnit(ModifierUnit unit)
        {
            modifierUnitList.Add(unit);
            allUnitList.Add(unit);
            patchwin.addModifierUnitToMenu(unit);
        }

        public void addOutputUnit(OutputUnit unit)
        {
            outputUnitList.Add(unit);
            allUnitList.Add(unit);
            patchwin.addOutputUnitToMenu(unit);
        }

        public PatchUnit findUnit(String unitName)
        {
            PatchUnit result = null;
            foreach (PatchUnit unit in allUnitList)
            {
                if (unit.name.Equals(unitName)) 
                {
                    result = unit;
                    break;
                }
            }
            return result;        
        }

        //add new unit to unit list & start it up
        public void addUnitToPatch(PatchUnit unit)
        {            
            patchUnits.Add(unit);
            if (unit is InputUnit || unit is OutputUnit)             //input & output units can only be added once
            {
                unit.menuItem.Enabled = false;                       //so we disable menu item while unit is in graph
            }

            unit.start();            
        }

        //connections should already have been removed when this is called
        public void removeUnitFromPatch(PatchUnit unit)
        {
            patchUnits.Remove(unit);            //remove the unit from the patch graph
            unit.menuItem.Enabled = true;       //re-enable menu items for input and output units            
        }
    }
}
