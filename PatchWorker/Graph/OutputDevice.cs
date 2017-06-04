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
using System.Runtime.InteropServices;

namespace PatchWorker.Graph
{
    public class OutputDevice
    {
        [DllImport("winmm.dll")]
        static extern MMRESULT midiOutOpen(out IntPtr lphMidiOut, int uDeviceID, IntPtr dwCallback, IntPtr dwInstance, int dwFlags);

        [DllImport("winmm.dll")]
        static extern MMRESULT midiOutClose(IntPtr hMidiOut);

        [DllImport("winmm.dll")]
        static extern MMRESULT midiOutShortMsg(IntPtr hMidiOut, uint dwMsg);

        public int devID;
        public String devName;
        public IntPtr devHandle;

        private bool opened;

        const int CALLBACK_NULL = 0x0;

        public OutputDevice(int _id, string _name)
        {
            devID = _id;
            devName = _name;
            opened = false;
        }

        public void open()
        {
            if (!opened)
            {
                MMRESULT result = midiOutOpen(out devHandle, devID, IntPtr.Zero, IntPtr.Zero, CALLBACK_NULL);
                opened = true;
                Console.WriteLine("opened device " + devName + " result = " + result);
            }
        }

        public void close()
        {
            if (opened)
            {
                MMRESULT result = midiOutClose(devHandle);
                opened = false;
                Console.WriteLine("closed device " + devName + " result = " + result);
            }
        }

        public void sendShortMsg(MidiShortMsg msg)
        {
            byte[] data = new byte[4];
            data[0] = (byte)msg.byte1;
            data[1] = (byte)msg.byte2;
            data[2] = msg.byte3;
            data[3] = 0x00;

            uint outMsg = BitConverter.ToUInt32(data, 0);
            midiOutShortMsg(devHandle, outMsg);
            Console.WriteLine("OUTPUT DEVICE: sent output msg to name = " + devName + " : " + data[0].ToString("X2") + "." + 
                data[1].ToString("X2") + "." + data[2].ToString("X2"));  
        }

    }
}
