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
using System.Windows.Forms;

// p/invoke calls and structs used with WINMM.DLL library taken from http://www.pinvoke.net

namespace PatchWorker.Graph
{
    public class MidiSystem
    {       
        //p/invoke imports
        [DllImport("winmm.dll", SetLastError = true)]
        static extern uint midiInGetNumDevs();

        [DllImport("winmm.dll", SetLastError = true)]
        static extern MMRESULT midiInGetDevCaps(int uDeviceID, ref MIDIINCAPS caps, int cbMidiInCaps);

        [DllImport("winmm.dll", SetLastError = true)]
        static extern uint midiOutGetNumDevs();

        [DllImport("winmm.dll", SetLastError = true)]
        static extern MMRESULT midiOutGetDevCaps(int uDeviceID, ref MIDIOUTCAPS lpMidiOutCaps, int cbMidiOutCaps);

        public List<InputDevice> inputDevices;
        public List<OutputDevice> outputDevices;

        public Label midiTest;

        public MidiSystem()
        {
            //input devices
            int deviceID;
            uint incount = midiInGetNumDevs();
            inputDevices = new List<InputDevice>();
            MIDIINCAPS inCaps = new MIDIINCAPS();
            for (deviceID = 0; deviceID < incount; deviceID++)
            {
                MMRESULT result = midiInGetDevCaps(deviceID, ref inCaps, Marshal.SizeOf(inCaps));
                InputDevice indev = new InputDevice(deviceID, inCaps.szPname);
                inputDevices.Add(indev);
            }

            //output devices
            uint outcount = midiOutGetNumDevs();
            outputDevices = new List<OutputDevice>((int)outcount);
            MIDIOUTCAPS outCaps = new MIDIOUTCAPS();
            for (deviceID = 0; deviceID < incount; deviceID++)
            {
                MMRESULT result = midiOutGetDevCaps(deviceID, ref outCaps, Marshal.SizeOf(outCaps));                 
                OutputDevice outdev = new OutputDevice(deviceID, outCaps.szPname);
                outputDevices.Add(outdev);
            }
        }

        public List<String> getInDevNameList()
        {
            List<String> nameList = new List<String>();
            foreach (InputDevice indev in inputDevices) 
            {
                nameList.Add(indev.devName);
            }
            if (nameList.Count == 0) nameList.Add("none");
            return nameList;
        }

        public InputDevice findInputDevice(String inName)
        {
            InputDevice result = null;
            foreach (InputDevice indev in inputDevices)
            {
                if (indev.devName.Equals(inName)) {
                    result = indev;
                    break;
                }
            }
            return result;
        }

        public List<String> getOutDevNameList()
        {
            List<String> nameList = new List<String>();
            foreach (OutputDevice outdev in outputDevices)
            {
                nameList.Add(outdev.devName);
            }
            if (nameList.Count == 0) nameList.Add("none");
            return nameList;
        }

        public OutputDevice findOutputDevice(String outName)
        {
            OutputDevice result = null;
            foreach (OutputDevice outdev in outputDevices)
            {
                if (outdev.devName.Equals(outName)) {
                    result = outdev;
                    break;
                }
            }
            return result;
        }
    }

    public enum MMRESULT : uint
    {
        MMSYSERR_NOERROR = 0,
        MMSYSERR_ERROR = 1,
        MMSYSERR_BADDEVICEID = 2,
        MMSYSERR_NOTENABLED = 3,
        MMSYSERR_ALLOCATED = 4,
        MMSYSERR_INVALHANDLE = 5,
        MMSYSERR_NODRIVER = 6,
        MMSYSERR_NOMEM = 7,
        MMSYSERR_NOTSUPPORTED = 8,
        MMSYSERR_BADERRNUM = 9,
        MMSYSERR_INVALFLAG = 10,
        MMSYSERR_INVALPARAM = 11,
        MMSYSERR_HANDLEBUSY = 12,
        MMSYSERR_INVALIDALIAS = 13,
        MMSYSERR_BADDB = 14,
        MMSYSERR_KEYNOTFOUND = 15,
        MMSYSERR_READERROR = 16,
        MMSYSERR_WRITEERROR = 17,
        MMSYSERR_DELETEERROR = 18,
        MMSYSERR_VALNOTFOUND = 19,
        MMSYSERR_NODRIVERCB = 20,
        WAVERR_BADFORMAT = 32,
        WAVERR_STILLPLAYING = 33,
        WAVERR_UNPREPARED = 34
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MIDIINCAPS
    {
        public ushort wMid;             //manfacturer id
        public ushort wPid;             //product id
        public uint vDriverVersion;     //device driver ver num, high byte = major ver, lo byte = minor ver
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          //product name
        public uint dwSupport;          //must be 0
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MIDIOUTCAPS
    {
        public ushort wMid;             //manfacturer id
        public ushort wPid;             //product id
        public uint vDriverVersion;     //device driver ver num, high byte = major ver, lo byte = minor ver
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          //product name
        public ushort wTechnology;      //type of midi output device
        public ushort wVoices;          //num voices if internal synth, 0 if device is a port
        public ushort wNotes;           //max num of notes synth can play, 0 if device is a port
        public ushort wChannelMask;     //internal synth's channel, -1 if device is a port
        public uint dwSupport;          //optional functionality supported by the device
    }
}
