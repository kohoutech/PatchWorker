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

namespace PatchWorker.Graph
{
    public class OutputUnit : PatchUnit
    {
        OutputDevice outDev;
        int channelNum;

        //connect output unit to output unit
        public OutputUnit(PatchWorker _patchworker, UnitData _udata, OutputDevice _outDev)
            : base(_patchworker, _udata)
        {
            outDev = _outDev;
            if (outDev != null)
            {
                outDev.open();          //open output device to send data to
            }
            channelNum = _udata.channelNum;
        }

        //instead of sending msg to next unit in patch, we send it to the output device
        public override void processShortMsg(MidiShortMsg msg)
        {
            if (outDev != null)
            {
                Console.WriteLine("OUTPUT UNIT: sending msg to output device " + outDev.devName);
                outDev.sendShortMsg(msg);
            }
            else
            {
                Console.WriteLine("OUTPUT UNIT: no output device to send msg to");
            }
        }

        public void sendProgramChange(int progNum)
        {
            byte b1 = (byte)(0xC0 | channelNum);
            byte b2 = (byte)(progNum & 0x7f);
            MidiShortMsg msg = new MidiShortMsg(b1, b2, 0x00, 0);
            processShortMsg(msg);
        }

    }
}
