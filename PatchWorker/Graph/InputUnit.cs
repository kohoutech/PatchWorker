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

using Transonic.MIDI;
using Transonic.MIDI.Engine;

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

        public override void receiveMessage(byte[] data)
        {
            Message msg = Message.getMessage(data, 0);
            processMidiMsg(msg);
        }

        public override void processMidiMsg(Message msg)
        {
            Console.WriteLine("INPUT UNIT: got msg from input device" + indev.devName);
            base.processMidiMsg(msg);
        }
    }
}
