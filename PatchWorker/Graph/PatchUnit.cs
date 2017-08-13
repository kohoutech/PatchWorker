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
using System.Windows.Forms;

using Transonic.MIDI;
using Transonic.MIDI.System;
using Transonic.Patch;

namespace PatchWorker.Graph
{
    public class PatchUnit : SystemUnit
    {
        public PatchWorker patchworker;
        List<PatchCord> destList;       //connections to units downstream - not used by output units
        public ToolStripItem menuItem;

        public PatchUnit(PatchWorker _patchworker, String name) :  base(name)
        {
            patchworker = _patchworker;
            destList = new List<PatchCord>();
        }

        public virtual void editSettings()
        {
        }

        public void delete()
        {
            patchworker.removeUnitFromPatch(this);
        }

        public void setMenuItem(ToolStripItem _menuItem)
        {
            menuItem = _menuItem;
        }

        public virtual List<PatchPanel> getPatchPanels(PatchBox _box)
        {
            return null;
        }

//- operation ---------------------------------------------------------------

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

        public virtual void disconnectDest(PatchUnit dest)
        {
            PatchCord patchcord = null;
            foreach (PatchCord cord in destList)
            {
                if (cord.destUnit == dest)
                {
                    patchcord = cord;
                    break;
                }
            }
            if (patchcord != null)
            {
                patchcord.disconnect();
                destList.Remove(patchcord);
            }
        }

        public virtual void processMidiMsg(Transonic.MIDI.Message msg)
        {
            foreach (PatchCord cord in destList)
            {
                //Console.WriteLine("sending msg to next unit in patch");
                cord.processMidiMsg(msg);
            }

        }
    }

//- exceptions ----------------------------------------------------------------

    public class PatchUnitLoadException : Exception
    {
        public String unitName;

        public PatchUnitLoadException(String _name, String msg) : base(msg)
        {
            unitName = _name;
        }
    }
}
