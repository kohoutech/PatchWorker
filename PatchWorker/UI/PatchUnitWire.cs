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

using Transonic.Patch;
using PatchWorker.Graph;
using PatchWorker.Dialogs;

namespace PatchWorker.UI
{
    class PatchUnitWire : PatchWire
    {
        public PatchCord patchCord;            //connector in the backing model

        public PatchUnitWire(PatchPanel srcPanel, PatchPanel destPanel, PatchCord _patchCord)
            : base(srcPanel, destPanel)
        {
            patchCord = _patchCord;
        }

        //public void connectDestJack(PatchPanel _destPanel)
        //{
        //    destPanel = _destPanel;
        //    destEnd = destPanel.ConnectionPoint;
        //    destPanel.connectLine(this);                            //connect line & dest panel in view

        //    connector = srcPanel.makeConnection(destPanel);         //connect panels in model, get model connector
        //    if (connector != null)
        //    {
        //        connector.setLine(this);                                //and set this as connector's view
        //    }
        //}

        //public void disconnect()
        //{
        //    if (srcPanel != null)
        //    {
        //        srcPanel.breakConnection(destPanel);                 //disconnect panels in model
        //        srcPanel.disconnectLine(this);
        //        srcPanel = null;
        //    }

        //    if (destPanel != null)
        //    {
        //        destPanel.disconnectLine(this);
        //        destPanel = null;                                           //disconnect line from both panels in view
        //    }
        //    path = null;
        //}


        public override void onDoubleClick(Point pos)
        {
            PatchCordDialog patchdlg = new PatchCordDialog(patchCord);
            patchdlg.setTitle(patchCord.srcUnit.name, patchCord.destUnit.name);
            patchdlg.initDialogValues(patchCord);
            //patchdlg.Icon = canvas.patchwin.
            patchdlg.Show(canvas);
        }
    }
}
