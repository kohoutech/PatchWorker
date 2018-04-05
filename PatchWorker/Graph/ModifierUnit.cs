/* ----------------------------------------------------------------------------
Patchworker : a midi patchbay
Copyright (C) 2005-2017  George E Greaney

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

using Transonic.MIDI;
using Transonic.MIDI.System;
using Transonic.Patch;
using PatchWorker.UI;

namespace PatchWorker.Graph
{
    public class ModifierUnit : PatchUnit
    {
        //no special cons here
        public ModifierUnit(PatchWorker _patchworker, String name, bool enabled)
            : base(_patchworker, name, enabled)
        {            
        }

        public override List<PatchPanel> getPatchPanels(PatchBox _box)
        {
            List<PatchPanel> panels = new List<PatchPanel>();
            panels.Add(new ProgramPanel(_box));
            panels.Add(new InJackPanel(_box));
            panels.Add(new OutJackPanel(_box));
            return panels;
        }

        //just call parent method - for now
        public override void processMidiMsg(Message msg)
        {
            base.processMidiMsg(msg);
        }

//- persistance ---------------------------------------------------------------

        public void saveToXML(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("modifierunit");
            xmlWriter.WriteEndElement();
        }
    }
}
