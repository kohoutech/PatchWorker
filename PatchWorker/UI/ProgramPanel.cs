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
using System.Drawing;
using System.Xml;

using Transonic.Patch;
using PatchWorker.Graph;

namespace PatchWorker.UI
{
    public class ProgramPanel : PatchPanel
    {
        public int progNum;
        public int progMax;
        public Rectangle display;
        public Point[] leftArrow;
        public Point[] rightArrow;

        public ProgramPanel(PatchBox box)
            : base(box)
        {
            panelType = "ProgramPanel";
            connType = CONNECTIONTYPE.NEITHER;
            PatchUnitBox ubox = (PatchUnitBox)box;
            OutputUnit unit = (OutputUnit)ubox.unit;
            progNum = 1;
            progMax = unit.progCount;
            frame = new Rectangle(0, 0, 100, 40);
            display = new Rectangle(20, 10, 60, 20);
            leftArrow = new Point[] { new Point(5, 20), new Point(15, 10), new Point(15, 30) };
            rightArrow = new Point[] { new Point(85, 10), new Point(95, 20), new Point(85, 30) };
        }

        public override void setPos(int xOfs, int yOfs)
        {
            base.setPos(xOfs, yOfs);
            display.Offset(xOfs, yOfs);
            leftArrow[0].Offset(xOfs, yOfs);
            leftArrow[1].Offset(xOfs, yOfs);
            leftArrow[2].Offset(xOfs, yOfs);
            rightArrow[0].Offset(xOfs, yOfs);
            rightArrow[1].Offset(xOfs, yOfs);
            rightArrow[2].Offset(xOfs, yOfs);
        }

        public override void onClick(Point pos)
        {
            PatchUnitBox unitbox = (PatchUnitBox)patchbox;
            OutputUnit outUnit = (OutputUnit)unitbox.unit;
            int xpos = pos.X - frame.X;
            if (xpos > 80)
            {
                progNum++;
                if (progNum > progMax) progNum = 1;
                outUnit.sendProgramChange(progNum);
            }
            else if (xpos < 20)
            {
                progNum--;
                if (progNum < 1) progNum = progMax;
                outUnit.sendProgramChange(progNum);
            }
        }

        public override void paint(Graphics g)
        {
            base.paint(g);

            //controls
            g.DrawRectangle(Pens.Black, frame);
            g.FillRectangle(Brushes.Black, display);
            g.FillPolygon(Brushes.Red, leftArrow);
            g.FillPolygon(Brushes.Red, rightArrow);

            //display
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(progNum.ToString(), SystemFonts.DefaultFont, Brushes.Red, display, stringFormat);
        }

        public override void loadAttributesFromXML(XmlNode panelNode)
        {
            base.loadAttributesFromXML(panelNode);
            progNum = Convert.ToInt32(panelNode.Attributes["prognum"].Value);
        }

        public override void saveAttributesToXML(XmlWriter xmlWriter)
        {
            base.saveAttributesToXML(xmlWriter);
            xmlWriter.WriteAttributeString("prognum", progNum.ToString());            
        }
    }

//- panel loader class --------------------------------------------------------

    public class ProgramPanelLoader : PatchPanelLoader
    {
        public override PatchPanel loadFromXML(PatchBox box, XmlNode panelNode)
        {
            ProgramPanel panel = new ProgramPanel(box);
            panel.loadAttributesFromXML(panelNode);
            return panel;
        }
    }
}
