using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatchWorker
{
    public class MidiShortMsg
    {
        public byte byte1;
        public byte byte2;
        public byte byte3;
        public int timestamp;

        public MidiShortMsg(byte b1, byte b2, byte b3, int ts) 
        {
            byte1 = b1;
            byte2 = b2;
            byte3 = b3;
            timestamp = ts;
        }
    }
}
