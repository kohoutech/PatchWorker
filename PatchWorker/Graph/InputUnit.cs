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
//using System.Windows.Forms;

using PatchWorker.UI;
using PatchWorker.Dialogs;
using Transonic.MIDI;
using Transonic.MIDI.System;
using Transonic.Patch;
using Origami.ENAML;

namespace PatchWorker.Graph
{
    public class InputUnit : PatchUnit
    {
        String indevName;
        int channelNum;
        bool started;

        //cons
        public InputUnit(String name, String _indevName, int _channel)
            : base(name)
        {
            indevName = _indevName;
            channelNum = _channel;
            started = false;
        }

        public override void editSettings()
        {
            //InputUnitDialog unitdlg = new InputUnitDialog(patchworker, name, indevName, channelNum);
            InputUnitDialog unitdlg = null;
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
                //inputDev = patchworker.midiSystem.findInputDevice(indevName);
                inputDev.connectUnit(this);
                inputDev.open();
                inputDev.start();              //open device & start receiving input
                started = true;
            }
        }

        public override void stop()
        {
            inputDev.stop();
            started = false;
        }

        public override void receiveMessage(byte[] data)
        {
            Message msg = Message.getMessage(data);                  //convert incoming bytes into midi message

            if ((msg is ChannelMessage) &&                              //filter channel msgs by channel num
                (((ChannelMessage)msg).channel == (channelNum - 1)) ||
                (msg is SystemMessage))
            {
                processMidiMsg(msg);                                    //and send it on its way
            }
        }

        public override void processMidiMsg(Message msg)
        {
            base.processMidiMsg(msg);
        }

        //- persistance ---------------------------------------------------------------

        public static InputUnit loadFromConfig(EnamlData data, String path)
        {
            String name = data.getStringValue(path + ".name", "no name");
            String devicename = data.getStringValue(path + ".device-name", "no name");
            int channel = data.getIntValue(path + ".channel", 0);

            return new InputUnit(name, devicename, channel);
        }

        public void saveToConfig(EnamlData data, String path)
        {
            data.setStringValue(path + ".name", name);
            data.setStringValue(path + ".device-name", indevName);
            data.setIntValue(path + ".channel", channelNum);
        }
    }
}

//Console.WriteLine("there's no sun in the shadow of the wizard");
