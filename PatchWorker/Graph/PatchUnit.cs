/* ----------------------------------------------------------------------------
Patchworker : a midi patchbay
Copyright (C) 1995-2019  George E Greaney

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
using Transonic.MIDI.System;
using Transonic.Patch;

//patch graph unit base class - the graph is composed of subclasses of patch units (input/output/modifiers)

namespace PatchWorker.Graph
{
    public class PatchUnit : SystemUnit
    {
        public PatchWork patchWork;
        List<PatchCord> destList;           //connections to units downstream - not used by output units

        public Programmer programmer;       //used by modifier & output units
        public int progCount;

        public bool enabled;
        public PaletteItem paletteItem;     //to let the palette know when to enable/disable unit in palette items

        public PatchUnit(String name) :  base(name)
        {
            patchWork = null;
            destList = new List<PatchCord>();

            programmer = null;
            progCount = 0;
            enabled = true;
            paletteItem = null;
        }

        public virtual void editSettings()
        {
        }

        //perform any needed clean-up
        public virtual void delete()
        {
        }

        //subclasses will return a list of patch panels (depending on type) that will be displayed in the patch box on the canvas
        public virtual List<PatchPanel> getPatchPanels(PatchBox _box)
        {
            return null;
        }

//- operation ---------------------------------------------------------------

        //start & stop processing midi messages
        public virtual void start()
        {
        }

        public virtual void stop()
        {
        }

        //only used by input & modifier units - not called or used by output units
        public virtual PatchCord connectDest(PatchUnit dest)
        {
            PatchCord cord = new PatchCord(this, dest);      //create cord & connect it between source & dest units
            destList.Add(cord);
            return cord;
        }

        public virtual void disconnectCord(PatchCord cord)
        {
            cord.disconnect();              //disconnect cord from dest
            destList.Remove(cord);          //disconnect cord from source
        }

        public virtual void processMidiMsg(Message _msg)
        {
            foreach (PatchCord cord in destList)
            {
                Message msg = _msg.copy();          //make a new copy of message for each cord
                cord.processMidiMsg(msg);
            }
        }
    }
}

//Console.WriteLine("there's no sun in the shadow of the Wizard");
