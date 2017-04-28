using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatchWorker.Graph
{
    public class ModifierUnit : PatchUnit
    {
        //no special cons here
        public ModifierUnit(PatchWorker _patchworker, UnitData _udata)
            : base(_patchworker, _udata)
        {            
        }

        //just call parent method - for now
        public override void processShortMsg(MidiShortMsg msg)
        {
            base.processShortMsg(msg);
        }

    }
}
