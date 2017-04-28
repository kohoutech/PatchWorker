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
