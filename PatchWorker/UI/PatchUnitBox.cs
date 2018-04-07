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
using System.Drawing;
using System.Xml;

using Transonic.Patch;
using PatchWorker.Graph;

namespace PatchWorker.UI
{
    class PatchUnitBox : PatchBox
    {
        public PatchUnit unit;

        //cons for loading from XML, panels will be loaded by bases class from XML
        public PatchUnitBox()
            : base()
        {
            boxType = "PatchUnitBox";
        }

        public PatchUnitBox(PatchUnit _unit) : this()
        {
            unit = _unit;
            title = unit.name;
            List<PatchPanel> panelList = unit.getPatchPanels(this);
            foreach (PatchPanel panel in panelList)
            {
                addPanel(panel, false);
            }
        }

        //removing this box from the canvas, re-enable unit's entry in unit menu
        public override void delete()
        {
            unit.menuItem.Enabled = true;
        }

//- settings methods -------------------------------------------------------------

        //called by canvas when user double clicks on title bar
        public override void onTitleDoubleClick()
        {
            unit.editSettings();
            title = unit.name;
            unit.menuItem.Text = unit.name;
            canvas.Invalidate();            
        }

//- persistance ---------------------------------------------------------------

        public override void loadAttributesFromXML(XmlNode boxNode)
        {
            base.loadAttributesFromXML(boxNode);

            //find unit box's unit from unit name
            String unitName = boxNode.Attributes["unitname"].Value;
            PatchWorker pw = PatchWorker.getPatchWorker();
            unit = pw.findUnit(unitName);
            if (unit != null)
            {
                pw.addUnitToPatch(unit);               //add patch unit to graph
            }
        }

        public override void saveAttributesToXML(XmlWriter xmlWriter)
        {
            base.saveAttributesToXML(xmlWriter);
            xmlWriter.WriteAttributeString("unitname", unit.name);
        }
    }

//- unit box loader class --------------------------------------------------------

    public class PatchUnitBoxLoader : PatchBoxLoader
    {
        public override PatchBox loadFromXML(XmlNode boxNode)
        {
            //create unit box & set attrs
            PatchUnitBox box = new PatchUnitBox();
            box.loadAttributesFromXML(boxNode);
            if (box.unit == null)
                return null;
            return box;
        }
    }
}
