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

using Transonic.MIDI;
using Transonic.MIDI.System;
using Transonic.Patch;
using PatchWorker.UI;
using PatchWorker.Dialogs;
using Origami.ENAML;

namespace PatchWorker.Graph
{
    public class OutputUnit : PatchUnit
    {
        public OutputDevice outDev;
        public String outDevName;
        public int channelNum;
        public bool started;

        //user cons
        public OutputUnit(String name, String _outDevName, int _channel, int _progCount)
            : base(name)
        {
            outDevName = _outDevName;
            channelNum = _channel;
            progCount = _progCount;
            programmer = new Programmer(this);
            started = false;
        }

        public override void editSettings()
        {
            OutputUnitDialog unitdlg = new OutputUnitDialog(patchWork.patchWnd, name, outDevName, channelNum, progCount);
            unitdlg.Icon = patchWork.patchWnd.Icon;
            unitdlg.ShowDialog();
            if (unitdlg.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!name.Equals(unitdlg.name))
                {
                    name = unitdlg.name;
                    paletteItem.name = name;
                    paletteItem.itembox.Text = name;
                    paletteItem.itembox.Invalidate();            //update name in palette entry
                }
                if (!outDevName.Equals(unitdlg.devName))         //if we've changed input devices, restart new dev;
                {
                    stop();
                    outDevName = unitdlg.devName;
                    start();
                }
                channelNum = unitdlg.chanNum;
                progCount = unitdlg.progCount;
            }            
        }

        public override List<PatchPanel> getPatchPanels(PatchBox _box)
        {
            List<PatchPanel> panels = new List<PatchPanel>();
            panels.Add(new InJackPanel(_box, "input 1"));              //output unit has an input jack to act as a sink
            panels.Add(new ProgramPanel(_box, "programmer"));
            return panels;
        }

        //- operation ---------------------------------------------------------------

        public override void start()
        {
            if (!started)
            {
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
            //Console.WriteLine("sending midi msg on output {0}", name); 

            if (outDev != null)
            {
                if (msg is ChannelMessage)       //if channel msg, route to output device
                {
                    ((ChannelMessage)msg).channel = channelNum - 1;
                }
                
                outDev.sendMessage(msg.getDataBytes());
                //Console.WriteLine("sent midi msg on output {0}", name);
            }
        }

        public void sendProgramChange(int progNum)
        {
            byte b1 = (byte)(progNum & 0x7f);
            PatchChangeMessage msg = new PatchChangeMessage(channelNum, b1);
            processMidiMsg(msg);
        }

        public void sendAllNotesOff()
        {
            byte[] msg = new byte[3] { (byte)(0xB0 + channelNum), 123, 0 };
            outDev.sendMessage(msg);                
        }

        //- persistance ---------------------------------------------------------------

        public static OutputUnit loadFromConfig(EnamlData data, String path)
        {
            String name = data.getStringValue(path + ".name", "no name");
            String devicename = data.getStringValue(path + ".device-name", "no name");
            int channel = data.getIntValue(path + ".channel", 0);
            int progCount = data.getIntValue(path + ".program-count", 0);

            return new OutputUnit(name, devicename, channel, progCount);
        }

        public void saveToConfig(EnamlData data, String path)
        {
            data.setStringValue(path + ".name", name);
            data.setStringValue(path + ".device-name", outDevName);
            data.setIntValue(path + ".channel", channelNum);
            data.setIntValue(path + ".program-count", progCount);
        }
    }
}

//Console.WriteLine("There is no sun in the shadow of the wizard");
