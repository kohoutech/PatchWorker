/* ----------------------------------------------------------------------------
Patchworker : a midi patchbay
Copyright (C) 1995-2017  George E Greaney

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
using System.Drawing;
using System.Xml;

using Transonic.Patch;
using PatchWorker.Graph;

namespace PatchWorker.UI
{
    public class OutJackPanel : PatchPanel
    {
        public Rectangle outJack;

        readonly Pen JACKBORDER = Pens.Black;
        readonly Brush JACKCOLOR = new SolidBrush(Color.FromArgb(90, 50, 188));
        readonly Brush JACKHOLE = Brushes.Black;
        readonly int JACKSIZE = 20;

        public OutJackPanel(PatchBox _box)
            : base(_box)
        {
            panelType = "OutJackPanel";
            connType = CONNECTIONTYPE.SOURCE;
            outJack = new Rectangle(frame.Right - (15 + JACKSIZE), frame.Top + (JACKSIZE / 2), JACKSIZE, JACKSIZE);
        }

        public override void setPos(int xOfs, int yOfs)
        {
            base.setPos(xOfs, yOfs);
            outJack.Offset(xOfs, yOfs);
        }

//- connections ---------------------------------------------------------------

        public override Point getConnectionPoint()
        {
            return new Point(outJack.Left + outJack.Width / 2, outJack.Top + outJack.Height / 2);
        }

        public override iPatchConnector makeConnection(PatchPanel destPanel)
        {
            PatchUnitBox srcbox = (PatchUnitBox)patchbox;
            PatchUnit source = srcbox.unit;
            PatchUnitBox destbox = (PatchUnitBox)destPanel.patchbox;
            PatchUnit dest = destbox.unit;
            iPatchConnector patchCord = source.connectDest(dest);
            return patchCord;
        }

        public override void breakConnection(PatchPanel destPanel)
        {
            if (destPanel != null)
            {
                PatchUnitBox srcbox = (PatchUnitBox)patchbox;
                PatchUnit source = srcbox.unit;
                PatchUnitBox destbox = (PatchUnitBox)destPanel.patchbox;
                PatchUnit dest = destbox.unit;
                source.disconnectDest(dest);
            }
        }

        public override void onClick(Point pos)
        {            
        }


//- painting ------------------------------------------------------------------

        public override void paint(Graphics g)
        {
            base.paint(g);

            //jack
            g.DrawEllipse(JACKBORDER, outJack);
            g.FillEllipse(JACKCOLOR, outJack);
            outJack.Inflate(-5, -5);
            g.FillEllipse(JACKHOLE, outJack);
            outJack.Inflate(5, 5);
        }

        public override void loadAttributesFromXML(XmlNode panelNode)
        {
            base.loadAttributesFromXML(panelNode);
        }

        public override void saveAttributesToXML(XmlWriter xmlWriter)
        {
            base.saveAttributesToXML(xmlWriter);
        }
    }

//- panel loader class --------------------------------------------------------

    public class OutJackPanelLoader : PatchPanelLoader
    {
        public override PatchPanel loadFromXML(PatchBox box, XmlNode panelNode)
        {
            OutJackPanel panel = new OutJackPanel(box);
            panel.loadAttributesFromXML(panelNode);
            return panel;
        }
    }

}
