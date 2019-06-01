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
    public class PatchWork
    {
        private PatchWindow patchWnd;
        public PatchCanvas canvas;

        //unit lists
        public List<InputUnit> inputUnitList;
        public List<ModifierUnit> modifierUnitList;
        public List<OutputUnit> outputUnitList;

        public List<PatchUnit> patchUnits;              //units that have been added to the patch graph

        public PatchWork(PatchWindow _patchWindow)
        {
            patchWnd = _patchWindow;
            canvas = patchWnd.canvas;

            //init unit lists
            inputUnitList = new List<InputUnit>();
            modifierUnitList = new List<ModifierUnit>();
            outputUnitList = new List<OutputUnit>();

            //patch graph empty
            patchUnits = new List<PatchUnit>();

            //registerLoaders();
        }

        //- units  ------------------------------------------------------------

        public void addInputUnit(InputUnit inUnit)
        {
            inUnit.inputDev = patchWnd.midiSystem.findInputDevice(inUnit.indevName);
            inUnit.enabled = (inUnit.inputDev == null);
            inputUnitList.Add(inUnit);
        }

        public void addOutputUnit(OutputUnit outUnit)
        {
            outUnit.outDev = patchWnd.midiSystem.findOutputDevice(outUnit.outDevName);
            outUnit.enabled = (outUnit.inputDev == null);
            outputUnitList.Add(outUnit);
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

            List<String> inputlist = data.getSubpathKeys("input-units");
            foreach (String inputName in inputlist)
            {
                InputUnit inUnit = InputUnit.loadFromConfig(data, "input-units." + inputName);
                inUnit.inputDev = patchWnd.midiSystem.findInputDevice(inUnit.indevName);
#if (RELEASE)
                if (inUnit.inputDev == null)
                {
                    inUnit.enabled = false;
                    disabledcount++;
                    disabledList = disabledList + inUnit.name + "\n";
                }
#endif
                inputUnitList.Add(inUnit);
            }

            List<String> outputlist = data.getSubpathKeys("output-units");
            foreach (String outputName in outputlist)
            {
                OutputUnit outUnit = OutputUnit.loadFromConfig(data, "output-units." + outputName);
                outUnit.outDev = patchWnd.midiSystem.findOutputDevice(outUnit.outDevName);
#if (RELEASE)
                if (outUnit.outDev == null)
                {
                    outUnit.enabled = false;
                    disabledcount++;
                    disabledList = disabledList + outUnit.name + "\n";
                }
#endif
                outputUnitList.Add(outUnit);
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

        //- patches + units ---------------------------------------------------



        //public PatchUnit findUnit(String unitName)
        //{
        //    PatchUnit result = null;
        //    //foreach (PatchUnit unit in allUnitList)
        //    //{
        //    //    if (unit.name.Equals(unitName))
        //    //    {
        //    //        result = unit;
        //    //        break;
        //    //    }
        //    //}
        //    return result;
        //}

        public void addUnitToPatch(PatchUnit unit)
        {
            //throw new NotImplementedException();
        }

        ////add new unit to unit list & start it up
        //public void addUnitToPatch(PatchUnit unit)
        //{
        //    //patchUnits.Add(unit);
        //    //if (unit is InputUnit || unit is OutputUnit)             //input & output units can only be added once
        //    //{
        //    //    unit.menuItem.Enabled = false;                       //so we disable menu item while unit is in graph
        //    //}

        //    //unit.start();
        //}

        ////connections should already have been removed when this is called
        //public void removeUnitFromPatch(PatchUnit unit)
        //{
        //    //patchUnits.Remove(unit);            //remove the unit from the patch graph
        //    //unit.menuItem.Enabled = true;       //re-enable menu items for input and output units            
        //}

        //public void loadPatchBox(XmlNode boxNode)
        //{
        //    PatchBox box = PatchBox.loadFromXML(boxNode);
        //    if (box != null)
        //    {
        //        box.canvas = this;
        //        boxList.Add(box);
        //    }
        //}

        //public void loadPatchWire(XmlNode lineNode)
        //{
        //    PatchLine line = PatchLine.loadFromXML(this, lineNode);
        //    if (line != null)
        //    {
        //        lineList.Add(line);
        //    }
        //}

        public PatchCord addCordToPath(PatchUnit srcUnit, int srcJack, PatchUnit destUnit, int destJack)
        {
            return null;
        }

        //- patches -----------------------------------------------------------

        public void loadPatch(string patchFilename)
        {
            
        }

        public void savePatch(string patchFilename)
        {
            
        }

        //public void loadPatch(String patchFileName)
        //{
        //    clearPatch();       //start with a clean slate

        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.Load(patchFileName);

        //    foreach (XmlNode patchNode in xmlDoc.DocumentElement.ChildNodes)
        //    {
        //        if (patchNode.Name.Equals("boxes"))
        //        {
        //            foreach (XmlNode boxNode in patchNode.ChildNodes)
        //            {
        //                loadPatchBox(boxNode);
        //            }
        //        }
        //        if (patchNode.Name.Equals("connections"))
        //        {
        //            foreach (XmlNode lineNode in patchNode.ChildNodes)
        //            {
        //                loadPatchLine(lineNode);
        //            }
        //        }
        //    }

        //    //renumber boxes
        //    int boxNum = 0;
        //    foreach (PatchBox box in boxList)
        //    {
        //        box.boxNum = ++boxNum;
        //        box.RenumberPanels();
        //    }
        //    PatchBox.boxCount = boxNum;

        //    Invalidate();
        //}

        //public void savePatch(String patchFileName)
        //{
        //    var settings = new XmlWriterSettings();
        //    settings.OmitXmlDeclaration = true;
        //    settings.Indent = true;
        //    settings.NewLineOnAttributes = true;

        //    XmlWriter xmlWriter = XmlWriter.Create(patchFileName, settings);

        //    xmlWriter.WriteStartDocument();
        //    xmlWriter.WriteStartElement("patchworkerpatch");
        //    xmlWriter.WriteAttributeString("version", "1.1.0");

        //    xmlWriter.WriteStartElement("boxes");
        //    foreach (PatchBox box in boxList)
        //    {
        //        box.saveToXML(xmlWriter);
        //    }
        //    xmlWriter.WriteEndElement();

        //    xmlWriter.WriteStartElement("connections");
        //    foreach (PatchLine line in lineList)
        //    {
        //        line.saveToXML(xmlWriter);
        //    }
        //    xmlWriter.WriteEndElement();

        //    xmlWriter.WriteEndDocument();
        //    xmlWriter.Close();
        //}

        //public void registerLoaders()
        //{
        //    PatchBox.registerBoxType("PatchUnitBox", new PatchUnitBoxLoader());
        //    PatchPanel.registerPanelType("InJackPanel", new InJackPanelLoader());
        //    PatchPanel.registerPanelType("OutJackPanel", new OutJackPanelLoader());
        //    PatchPanel.registerPanelType("ProgramPanel", new ProgramPanelLoader());
        //}

        //- persistance ---------------------------------------------------------------

        //get source & dest box nums from XML file and get the matching boxes from the canvas
        //then get the panel nums from XML and get the matching panels from the boxes
        //having the source & dest panels. create a new line between them
        //this will create a connection in the backing model, call loadFromXML() on it with XML node to set its properties
        //public static PatchWire loadFromXML(PatchCanvas canvas, XmlNode lineNode)
        //{
        //    PatchWire line = null;
        //    try
        //    {
        //        int srcBoxNum = Convert.ToInt32(lineNode.Attributes["sourcebox"].Value);
        //        int srcPanelNum = Convert.ToInt32(lineNode.Attributes["sourcepanel"].Value);
        //        int destBoxNum = Convert.ToInt32(lineNode.Attributes["destbox"].Value);
        //        int destPanelNum = Convert.ToInt32(lineNode.Attributes["destpanel"].Value);

        //        PatchBox sourceBox = canvas.findPatchBox(srcBoxNum);
        //        PatchBox destBox = canvas.findPatchBox(destBoxNum);
        //        if (sourceBox != null && destBox != null)
        //        {
        //            PatchPanel sourcePanel = sourceBox.findPatchPanel(srcPanelNum);
        //            PatchPanel destPanel = destBox.findPatchPanel(destPanelNum);

        //            if (sourcePanel != null && destPanel != null)
        //            {
        //                line = new PatchWire(canvas, sourcePanel, destPanel);
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        throw new PatchLoadException();
        //    }

        //    return line;
        //}

        //public void saveToXML(XmlWriter xmlWriter)
        //{
        //    //save patch line attributes
        //    xmlWriter.WriteStartElement("connection");
        //    xmlWriter.WriteAttributeString("sourcebox", srcPanel.patchbox.boxNum.ToString());
        //    xmlWriter.WriteAttributeString("sourcepanel", srcPanel.panelNum.ToString());
        //    xmlWriter.WriteAttributeString("destbox", destPanel.patchbox.boxNum.ToString());
        //    xmlWriter.WriteAttributeString("destpanel", destPanel.panelNum.ToString());

        //    xmlWriter.WriteEndElement();
        //}

    }

    public class PatchLoadException : Exception
    {
    }


}
