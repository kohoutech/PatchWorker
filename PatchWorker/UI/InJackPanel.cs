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

namespace PatchWorker.UI
{
    public class InJackPanel : PatchPanel
    {
        public Point connectionPoint;
        public Point[] jackShape;

        const int PANELHEIGHT = 30;
        readonly Brush JACKCOLOR = new SolidBrush(Color.FromArgb(90, 50, 188));

        public InJackPanel(PatchBox box, String jackName)
            : base(box, jackName)
        {
            connType = CONNECTIONTYPE.DEST;

            updateFrame(patchbox.frame.Width, PANELHEIGHT);
            connectionPoint = new Point(frame.Left, frame.Top + (frameHeight / 2));
            updateJack();
        }

        public override void setPos(int xOfs, int yOfs)
        {
            base.setPos(xOfs, yOfs);
            connectionPoint.Offset(xOfs, yOfs);
            updateJack();
        }

        private void updateJack()
        {
            jackShape = new Point[]{ new Point(connectionPoint.X, connectionPoint.Y), 
                                     new Point(connectionPoint.X + 10, connectionPoint.Y + 10), 
                                     new Point(connectionPoint.X + 10, connectionPoint.Y - 10) };
        }

        public override Point ConnectionPoint
        {
            get{ return connectionPoint;}
        }

        public override void paint(Graphics g)
        {
            base.paint(g);

            //in jack
            g.FillPolygon(JACKCOLOR, jackShape);
        }
    }
}
