using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatchWorker.Graph
{
    public class PatchCord
    {
        PatchUnit inUnit;
        PatchUnit outUnit;

        public PatchCord(PatchUnit _inUnit, PatchUnit _outUnit)
        {
            inUnit = _inUnit;
            outUnit = _outUnit;
        }

        public void disconnect()
        {
            inUnit.disconnectOutUnit(this);         //remove cord from input unit's cord list
            inUnit = null;
            outUnit = null;
        }

        public void processShortMsg(MidiShortMsg msg)
        {
            Console.WriteLine(" PATCH CORD: got msg from input unit" + inUnit.uname + " sending it to output unit " + outUnit.uname);
            outUnit.processShortMsg(msg);
        }
    }
}
