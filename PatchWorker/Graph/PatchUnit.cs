using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatchWorker.Graph
{
    public abstract class PatchUnit
    {

        public UnitData udata;          //unit definition
        public String uname;
        public PatchWorker patchworker;
        List<PatchCord> outUnitList;       //connections to units downstream - not used by output units

        public PatchUnit(PatchWorker _patchworker, UnitData _udata)
        {
            patchworker = _patchworker;
            udata = _udata;
            uname = udata.name;
            outUnitList = new List<PatchCord>();
        }

        public void delete()
        {
            patchworker.removeUnitFromPatch(this);
        }

        //only used by input & modifier units - not called or used by output units
        public virtual PatchCord connectOutUnit(PatchUnit outunit)
        {
            PatchCord cord = new PatchCord(this, outunit);
            outUnitList.Add(cord);
            return cord;
        }

        public virtual void disconnectOutUnit(PatchCord cord)
        {
            outUnitList.Remove(cord);
        }

        public virtual void processShortMsg(MidiShortMsg msg)
        {
            foreach (PatchCord cord in outUnitList)
            {
                Console.WriteLine("sending msg to next unit in patch");
                cord.processShortMsg(msg);
            }

        }
    }
}
