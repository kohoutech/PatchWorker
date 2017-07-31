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
using Transonic.MIDI.Engine;
using Transonic.Patch;
using PatchWorker.UI;

namespace PatchWorker.Graph
{
    public class OutputUnit : PatchUnit
    {
        OutputDevice outDev;
        String outDevName;
        int channelNum;
        public int progCount;
        bool started;

        //user cons
        public OutputUnit(PatchWorker _patchworker, String name, String _outDevName, int _channel, int _progNum)
            : base(_patchworker, name)
        {
            outDevName = _outDevName;
            channelNum = _channel;
            progCount = _progNum;
            started = false;            
        }

        public override List<PatchPanel> getPatchPanels(PatchBox _box)
        {
            List<PatchPanel> panels = new List<PatchPanel>();
            panels.Add(new ProgramPanel(_box));
            panels.Add(new InJackPanel(_box));
            return panels;
        }

//- operation ---------------------------------------------------------------

        public override void start()
        {
            if (!started)
            {
                outDev = patchworker.midiSystem.findOutputDevice(outDevName);
                outDev.open();
            }
        }

        public override void stop()
        {
            started = false;
        }

        //instead of sending msg to next unit in patch, we send it to the output device
        public override void processMidiMsg(Message msg)
        {
            if (outDev != null)
            {
                Console.WriteLine("OUTPUT UNIT: sending msg to output device " + outDev.devName);
                outDev.sendMessage(msg.getDataBytes());                    
            }
        }

        public void sendProgramChange(int progNum)
        {
            byte b1 = (byte)(progNum & 0x7f);
            PatchChangeMessage msg = new PatchChangeMessage(channelNum, b1);
            processMidiMsg(msg);
        }

//- persistance ---------------------------------------------------------------

        public static OutputUnit loadFromXML(PatchWorker _patchworker, XmlNode unitNode)
        {
            String name = unitNode.Attributes["name"].Value;
            String devicename = unitNode.Attributes["devicename"].Value;
            int channel = Convert.ToInt32(unitNode.Attributes["channel"].Value);
            int progcount = Convert.ToInt32(unitNode.Attributes["progcount"].Value);
            return new OutputUnit(_patchworker, name, devicename, channel, progcount);
        }

        public void saveToXML(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("outputunit");
            xmlWriter.WriteAttributeString("name", name);
            xmlWriter.WriteAttributeString("devicename", outDevName);
            xmlWriter.WriteAttributeString("channel", channelNum.ToString());
            xmlWriter.WriteAttributeString("progcount", progCount.ToString());
            xmlWriter.WriteEndElement();
        }
    }
}
