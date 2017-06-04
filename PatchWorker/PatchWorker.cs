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
using PatchWorker.Graph;

namespace PatchWorker
{
    public class PatchWorker
    {
        public PatchWindow patchwin;
        public MidiSystem midiSystem;

        public List<UnitData> unitDataList;
        List<PatchUnit> patchUnits;

        public PatchWorker(PatchWindow _patchwin)
        {
            patchwin = _patchwin;

            //get input + output device lists
            midiSystem = new MidiSystem();

            //get unit defs & add items to unit menu
            loadUnitData();

            //patch graph empty
            patchUnits = new List<PatchUnit>();

        }

//- units ---------------------------------------------------------------------

        public void loadUnitData()
        {
            //get list of saved unit defs
            unitDataList = UnitData.loadUnitData();
            foreach (UnitData udata in unitDataList)
            {
                patchwin.addUnitMenuItem(udata);
            }        
        }

        public UnitData findUnitData(String uname)
        {
            UnitData result = null;
            foreach (UnitData udata in unitDataList)
            {
                if (udata.name.Equals(uname))
                {
                    result = udata;
                    break;
                }
            }
            return result;
        }

        public void addUnitData(UnitData udata)
        {
            unitDataList.Add(udata);
            patchwin.addUnitMenuItem(udata);
        }

        public void saveUnitData()
        {
            UnitData.saveUnitData(unitDataList);
        }

        public PatchUnit addUnitToPatch(UnitData udata)
        {
            PatchUnit unit = null;

            //create new unit from unit def and match def's device name against actual device list
            switch (udata.utype)
            {
                case UNITTYPE.INPUT:
                    InputDevice indev = midiSystem.findInputDevice(udata.devName);                    
                    unit = new InputUnit(this, udata, indev);
                    udata.menuItem.Enabled = false;
                    break;
                case UNITTYPE.MODIFIER:
                    unit = new ModifierUnit(this, udata);
                    break;
                case UNITTYPE.OUTPUT:
                    OutputDevice outdev = midiSystem.findOutputDevice(udata.devName);
                    unit = new OutputUnit(this, udata, outdev);
                    udata.menuItem.Enabled = false;
                    break;
            }

            //add new unit to unit list & link to this
            Console.WriteLine(" added unit " + unit.udata.name + " to patch");
            patchUnits.Add(unit);
            return unit;
        }

        //connections should already have been removed when this is called
        public void removeUnitFromPatch(PatchUnit unit)
        {
            patchUnits.Remove(unit);                //remove the unit from the patch graph
            unit.udata.menuItem.Enabled = true;     //re-enable menu items for input and output units            
        }


    }
}
