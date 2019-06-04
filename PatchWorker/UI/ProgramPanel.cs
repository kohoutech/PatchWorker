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
        public int progNum;
        public int progMax;

        const int PANELHEIGHT = 30;
        public Rectangle display;

        public ProgramPanel(PatchBox box, String name)
            : base(box, name)
        {
            connType = CONNECTIONTYPE.NEITHER;

            PatchUnitBox ubox = (PatchUnitBox)box;
            unit = (OutputUnit)ubox.unit;
            programmer = unit.programmer;
            progMax = unit.progCount - 1;           //max program num one less than number of programs (ie 0 - 19)
            progNum = 0;

            updateFrame(patchbox.frame.Width, PANELHEIGHT);

            display = new Rectangle(10, 5, frame.Width - 20, frame.Height - 10);
        }

        public override void setPos(int xOfs, int yOfs)
        {
            base.setPos(xOfs, yOfs);
            display.Offset(xOfs, yOfs);
        }

        public override void onClick(Point pos)
        {
            if (pos.X < display.Left)
            {
                if (progNum > 0)
                {
                    progNum--;
                    patchbox.canvas.Invalidate();
                    unit.sendProgramChange(progNum);
                }
            }
            else if (pos.X > display.Right)
            {
                if (progNum < progMax)
                {
                    progNum++;
                    patchbox.canvas.Invalidate();
                    unit.sendProgramChange(progNum);
                }
            }
            else
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
        }

        public void setProgram(int _progNum) 
        {
            progNum = _progNum;
            if (patchbox.canvas != null)        //may not have been added to canvas yet, if loading from a patch file
            {
                patchbox.canvas.Invalidate();
            }
            unit.sendProgramChange(progNum);
        }

        void ProgMenuClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripButton item = (ToolStripButton)e.ClickedItem;
            setProgram((Int32)item.Tag);
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

            //prev prog arrow
            Point[] leftArrow = new Point[]{new Point(frame.Left, (display.Top + display.Height / 2)), 
            new Point(display.Left, display.Top), new Point(display.Left, display.Bottom)};
            g.FillPolygon(Brushes.Red, leftArrow);

            //next prog arrow
            Point[] rightArrow = new Point[]{new Point(frame.Right, (display.Top + display.Height / 2)), 
            new Point(display.Right, display.Top), new Point(display.Right, display.Bottom)};
            g.FillPolygon(Brushes.Red, rightArrow);
        }
    }
}
