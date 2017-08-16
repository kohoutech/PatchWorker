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
//using System.Windows.Forms;

using Transonic.MIDI;
using Transonic.MIDI.System;
using Transonic.Patch;
using PatchWorker.UI;
using PatchWorker.Dialogs;

namespace PatchWorker.Graph
{
    public class InputUnit : PatchUnit
    {
        String indevName;
        int channelNum;
        bool started;

        //cons
        public InputUnit(PatchWorker _patchworker, String name, String _indevName, int _channel)
            : base(_patchworker, name)
        {
            indevName = _indevName;
            channelNum = _channel;
            started = false;
        }

        public override void editSettings()
        {
            InputUnitDialog unitdlg = new InputUnitDialog(patchworker, name, indevName, channelNum);
            unitdlg.ShowDialog();
            if (unitdlg.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                name = unitdlg.name;
                if (!indevName.Equals(unitdlg.devName))         //if we've changed input devices, restart new dev;
                {
                    stop();
                    indevName = unitdlg.devName;
                    start();
                }
                channelNum = unitdlg.chanNum;
            }
        }

        public override List<PatchPanel> getPatchPanels(PatchBox _box)
        {
            List<PatchPanel> panels = new List<PatchPanel>();
            panels.Add(new OutJackPanel(_box));
            return panels;
        }

//- operation ---------------------------------------------------------------

        public override void start()
        {
            if (!started)
            {
#if(!DEBUG)
                inputDev = patchworker.midiSystem.findInputDevice(indevName);
                inputDev.connectUnit(this);
                inputDev.open();
                inputDev.start();              //open device & start receiving input
#endif
                started = true;
            }            
        }

        public override void stop()
        {
#if(!DEBUG)
            inputDev.stop();
#endif
            started = false;
        }

        public override void receiveMessage(byte[] data)
        {
            Message msg = Message.getMessage(data, 0);                  //convert incoming bytes into midi message

            if ((msg.msgClass == Message.MESSAGECLASS.CHANNEL) &&       //filter channel msgs by channel num
                (((ChannelMessage)msg).channel == (channelNum - 1))  || 
                (msg.msgClass == Message.MESSAGECLASS.SYSTEM))
            {
                processMidiMsg(msg);                                    //and send it on its way
            }
        }

        public override void processMidiMsg(Message msg)
        {
            base.processMidiMsg(msg);
        }

//- persistance ---------------------------------------------------------------

        public static InputUnit loadFromXML(PatchWorker patchworker, XmlNode unitNode)
        {
            String name = unitNode.Attributes["name"].Value;
            String devicename = unitNode.Attributes["devicename"].Value;
            int channel = Convert.ToInt32(unitNode.Attributes["channel"].Value);

#if(!DEBUG)
            InputDevice inDev = patchworker.midiSystem.findInputDevice(devicename);
            if (inDev != null)
            {
                return new InputUnit(patchworker, name, devicename, channel);
            }
            else
            {
                throw new PatchUnitLoadException(name, "no input device " + devicename + " found");
            }
#else
            return new InputUnit(patchworker, name, devicename, channel);
#endif
        }

        public void saveToXML(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("inputunit");
            xmlWriter.WriteAttributeString("name", name);
            xmlWriter.WriteAttributeString("devicename", indevName);
            xmlWriter.WriteAttributeString("channel", channelNum.ToString());
            xmlWriter.WriteEndElement();
        }
    }
}
