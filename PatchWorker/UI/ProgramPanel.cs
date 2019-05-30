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
using System.Windows.Forms;
using System.Drawing;
using System.Xml;

using Transonic.Patch;
using PatchWorker.Graph;

namespace PatchWorker.UI
{
    public class ProgramPanel : PatchPanel
    {
        OutputUnit unit;                        //will need to include modifier units too
        public Programmer programmer;
        int progNum;
        public int progMax;
        public Rectangle display;

        public ProgramPanel(PatchBox box)
            : base(box)
        {
            panelType = "ProgramPanel";
            connType = CONNECTIONTYPE.NEITHER;
            PatchUnitBox ubox = (PatchUnitBox)box;
            unit = (OutputUnit)ubox.unit;
            programmer = unit.programmer;
            progMax = unit.progCount;
            progNum = 0;
            frame = new Rectangle(0, 0, 100, 40);
            display = new Rectangle(5, 5, 90, 30);
        }

        public override void setPos(int xOfs, int yOfs)
        {
            base.setPos(xOfs, yOfs);
            display.Offset(xOfs, yOfs);
        }

        public override void onRightClick(Point pos)
        {
            ContextMenuStrip cm = new ContextMenuStrip();
            cm.ItemClicked += new ToolStripItemClickedEventHandler(ProgMenuClicked);
            for (int i = 0; i < programmer.progCount; i++)
            {
                ToolStripButton item = new ToolStripButton(programmer.programs[i]);
                item.Checked = (i == progNum);
                item.Tag = i;
                cm.Items.Add(item);
            }

            cm.Show(patchbox.canvas, pos);
        }

        void ProgMenuClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripButton item = (ToolStripButton)e.ClickedItem;
            progNum = (Int32)item.Tag;
            patchbox.canvas.Invalidate();
            unit.sendProgramChange(progNum);
        }

//- painting ------------------------------------------------------------------

        public override void paint(Graphics g)
        {
            base.paint(g);

            //controls
            g.DrawRectangle(Pens.Black, frame);
            g.FillRectangle(Brushes.Black, display);

            //display
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(programmer.programs[progNum], SystemFonts.DefaultFont, Brushes.Lime, display, stringFormat);
        }

//- persistance ---------------------------------------------------------------

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
