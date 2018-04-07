/* ----------------------------------------------------------------------------
Patchworker : a midi patchbay
Copyright (C) 2005-2018  George E Greaney

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

namespace PatchWorker.UI
{
    public class InJackPanel : PatchPanel
    {
        public Rectangle inJack;

        readonly Pen JACKBORDER = Pens.Black;
        readonly Brush JACKCOLOR = new SolidBrush(Color.FromArgb(90, 50, 188));
        readonly Brush JACKHOLE = Brushes.Black;
        readonly int JACKSIZE = 20;

        public InJackPanel(PatchBox _box)
            : base(_box)
        {
            panelType = "InJackPanel";
            connType = CONNECTIONTYPE.DEST;
            frame = new Rectangle(0, 0, 100, 40);
            inJack = new Rectangle(frame.Left + 15, frame.Top + (JACKSIZE / 2), JACKSIZE, JACKSIZE);
        }

        public override void setPos(int xOfs, int yOfs)
        {
            base.setPos(xOfs, yOfs);
            inJack.Offset(xOfs, yOfs);
        }

        public override Point ConnectionPoint
        {
            get {return new Point(inJack.Left + inJack.Width / 2, inJack.Top + inJack.Height / 2);}
        }

        public override void paint(Graphics g)
        {
            base.paint(g);

            //jack
            g.DrawEllipse(JACKBORDER, inJack);
            g.FillEllipse(JACKCOLOR, inJack);
            inJack.Inflate(-5, -5);
            g.FillEllipse(JACKHOLE, inJack);
            inJack.Inflate(5, 5);
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

    public class InJackPanelLoader : PatchPanelLoader
    {
        public override PatchPanel loadFromXML(PatchBox box, XmlNode panelNode)
        {
            InJackPanel panel = new InJackPanel(box);
            panel.loadAttributesFromXML(panelNode);
            return panel;

        }
    }
}
