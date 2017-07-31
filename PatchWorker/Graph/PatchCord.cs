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

namespace PatchWorker.Graph
{
    public class PatchCord
    {
        public PatchUnit inUnit;
        public PatchUnit outUnit;

        public PatchCord(PatchUnit _inUnit, PatchUnit _outUnit)
        {
            inUnit = _inUnit;
            outUnit = _outUnit;
        }

        public void disconnect()
        {
            inUnit.disconnectOutUnit(outUnit);         //remove cord from input unit's cord list
            inUnit = null;
            outUnit = null;
        }

        public void processMidiMsg(Message msg)
        {
            //Console.WriteLine(" PATCH CORD: got msg from input unit" + inUnit.name + " sending it to output unit " + outUnit.name);
            outUnit.processMidiMsg(msg);                
        }
    }
}
