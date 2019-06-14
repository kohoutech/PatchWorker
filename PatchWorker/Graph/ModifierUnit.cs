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
//using System.Reflection;

using PatchWorker.UI;
using PatchWorker.Plugin;
using Transonic.MIDI;
using Transonic.MIDI.System;
using Transonic.Patch;

//modifier unit base class

namespace PatchWorker.Graph
{
    public class ModifierUnit : PatchUnit, IPatchModifer
    {
        public ModifierFactory modFact;
        public IPatchPlugin plugin;
        public int modNum;

        public ModifierUnit(ModifierFactory _modFact, IPatchPlugin _plugin, int _modNum) :
            base(_modFact.plugName + "-" + (_modNum).ToString().PadLeft(3, '0'))
        {
            modFact = _modFact;
            plugin = _plugin;
            modNum = _modNum;
        }

        public override List<PatchPanel> getPatchPanels(PatchBox _box)
        {
            List<PatchPanel> panels = new List<PatchPanel>();
            int incount = plugin.getInputCount();
            for (int i = 1; i <= incount; i++)
            {
                panels.Add(new InJackPanel(_box, "input " + i));
            }
            int outcount = plugin.getInputCount();
            for (int i = 1; i <= outcount; i++)
            {
                panels.Add(new OutJackPanel(_box, "output " + i));
            }
            return panels;
        }

        //show plugin's dialog when user clicks on modifier unit's box's title
        public override void editSettings()
        {
                String dlgTitle = modFact.plugName + "-" + (modNum).ToString().PadLeft(3, '0');     //fx Power Chord-001
                plugin.showPluginDialog(dlgTitle);
        }

        //- midi handling -----------------------------------------------------

        //send MIDI data to plugin
        //NOTE: send and receive MIDI data as bytes so we don't need to match the MIDI library 
        //assembly version in both patchworker and the plugin
        public override void processMidiMsg(Message msg)
        {
            byte[] msgData = msg.getDataBytes();
            plugin.handleMidiMessage(msgData);
        }

        //receive MIDI data back from plugin and send it to next units in patch
        //NOTE: sending & receiving MIDI data to/from the plugin can be asynchronous
        //for example, an chord arpeggiator would send note on/off msgs back to the modifier on a timer
        //without the modifier having to send any msgs to the plugin after the initial note down msgs
        public void sendMidiMsg(byte[] msgData)
        {
            Message msg = Message.getMessage(msgData);
            base.processMidiMsg(msg);
        }

        //- persistance -------------------------------------------------------

        //loading plugin specific data from the patch is handled by the modifier factory before it creates this modifier unit

        public void saveUnitToPatch(Origami.ENAML.EnamlData data, string path)
        {
            plugin.saveToPatch(data, path);
        }
    }
}
