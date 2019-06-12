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
using System.Drawing;
using System.Xml;

using Transonic.Patch;
using PatchWorker.Graph;

namespace PatchWorker.UI
{
    public class PatchUnitBox : PatchBox
    {
        public PatchUnit unit;              //backing model unit

        public PatchUnitBox(PatchUnit _unit)
            : base()
        {
            unit = _unit;
            title = unit.name;
            List<PatchPanel> panelList = unit.getPatchPanels(this);         //model knows what panels to add to this patch box
            foreach (PatchPanel panel in panelList)
            {
                addPanel(panel);
            }
        }

        //- settings methods -------------------------------------------------------------

        //called by canvas when user double clicks on title bar
        public override void onTitleDoubleClick()
        {
            unit.editSettings();
            if (!title.Equals(unit.name))
            {
                title = unit.name;
                canvas.Invalidate();
            }
        }
    }
}
