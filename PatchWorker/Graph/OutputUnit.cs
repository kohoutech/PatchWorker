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

using Transonic.MIDI;
using Transonic.MIDI.System;
using Transonic.Patch;
using PatchWorker.UI;
using PatchWorker.Dialogs;

namespace PatchWorker.Graph
{
    public class OutputUnit : PatchUnit
    {
        OutputDevice outDev;
        String outDevName;
        int channelNum;
        bool started;

        //user cons
        public OutputUnit(PatchWorker _patchworker, String name, String _outDevName, int _channel, int _progCount)
            : base(_patchworker, name)
        {
            outDevName = _outDevName;
            channelNum = _channel;
            progCount = _progCount;
            programmer = new Programmer(this);
            started = false;            
        }

        public override void editSettings()
        {
            OutputUnitDialog unitdlg = new OutputUnitDialog(patchworker, name, outDevName, channelNum, progCount);
            unitdlg.ShowDialog();
            if (unitdlg.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                name = unitdlg.name;
                if (!outDevName.Equals(unitdlg.devName))         //if we've changed input devices, restart new dev;
                {
                    stop();
                    outDevName = unitdlg.devName;
                    start();
                }
                channelNum = unitdlg.chanNum;
                progCount = unitdlg.progNum;
            }
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
                if (msg is ChannelMessage)       //if channel msg, route to output device
                {
                    ((ChannelMessage)msg).channel = channelNum - 1;
                }
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

        public static OutputUnit loadFromXML(PatchWorker patchworker, XmlNode unitNode)
        {
            OutputUnit result = null;
            String name = unitNode.Attributes["name"].Value;
            String devicename = unitNode.Attributes["devicename"].Value;
            int channel = Convert.ToInt32(unitNode.Attributes["channel"].Value);
            int progcount = Convert.ToInt32(unitNode.Attributes["progcount"].Value);

            OutputDevice outDev = patchworker.midiSystem.findOutputDevice(devicename);
            if (outDev != null)
            {
                result = new OutputUnit(patchworker, name, devicename, channel, progcount);
            }
            else
            {
                throw new PatchUnitLoadException(name, "no output device " + devicename + " found");
            }

            //load programmer for unit
            if (result != null)
            {
                foreach (XmlNode node in unitNode.ChildNodes)
                {
                    if (node.Name.Equals("programmer"))
                    {
                        result.programmer = Programmer.loadFromXML(result, node);
                    }
                }
            }
            return result;
        }

        public void saveToXML(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("outputunit");
            xmlWriter.WriteAttributeString("name", name);
            xmlWriter.WriteAttributeString("devicename", outDevName);
            xmlWriter.WriteAttributeString("channel", channelNum.ToString());
            xmlWriter.WriteAttributeString("progcount", progCount.ToString());
            if (programmer != null)
            {
                programmer.saveToXML(xmlWriter);
            }
            xmlWriter.WriteEndElement();
        }
    }
}

//Console.WriteLine("There is no sun in the shadow of the wizard");
