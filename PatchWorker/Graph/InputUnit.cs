using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatchWorker.Graph
{
    public class InputUnit : PatchUnit
    {
        InputDevice indev;

        //connect input device to input unit
        public InputUnit(PatchWorker _patchworker, UnitData _udata, InputDevice _indev)
            : base(_patchworker, _udata)
        {
            indev = _indev;
            if (indev != null)
            {
                indev.connectUnit(this);
                indev.open();
                indev.start();              //open device & start receiving input
            }            
        }

        public override void processShortMsg(MidiShortMsg msg)
        {
            Console.WriteLine("INPUT UNIT: got msg from input device" + indev.devName);
            base.processShortMsg(msg);
        }
    }
}
