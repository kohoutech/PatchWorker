using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PatchWorker.Graph;
using System.Xml.Serialization;

namespace PatchWorker.UI
{
    [Serializable]
    public class PatchBox
    {
        public static int boxCount = 0;
        public int boxNum;

        [XmlIgnore]         
        public PatchCanvas canvas;
        [XmlIgnore]         
        public PatchUnit unit;
        public String unitName;         //for matching units when deserializing

        [XmlIgnore]
        public List<PatchLine> inConnectors;
        [XmlIgnore]
        public List<PatchLine> outConnectors;

        [XmlIgnore]
        bool isSelected;
        [XmlIgnore]
        bool isTargeted;

        public Rectangle frame;
        public Rectangle namePanel;
        public Rectangle jackPanel;
        public Rectangle inJack;
        public Rectangle outJack;

        //box constants
        readonly Pen NORMALBORDER  = Pens.Black;
        readonly Pen SELECTEDBORDER = new Pen(Color.Black, 3.0f);
        readonly Pen TARGETEDBORDER = new Pen(Color.Blue, 3.0f);
        readonly Brush BACKGROUNDCOLOR = new SolidBrush(Color.FromArgb(111, 177, 234));
        readonly Brush NAMECOLOR = Brushes.Black;
        readonly Font NAMEFONT = SystemFonts.DefaultFont;
        readonly Pen JACKBORDER = Pens.Black;
        readonly Brush JACKCOLOR = new SolidBrush(Color.FromArgb(90, 50, 188));
        readonly Brush JACKHOLE = Brushes.Black;
        readonly int FRAMEWIDTH = 100;
        readonly int JACKSIZE = 20;

        public ProgramPanel progPanel = null;

        public PatchBox(PatchCanvas _canvas, PatchUnit _unit, Point framePos)
        {
            boxNum = ++boxCount;
            canvas = _canvas;
            unit = _unit;
            unitName = unit.uname;
            inConnectors = new List<PatchLine>();
            outConnectors = new List<PatchLine>();

            frame.Location = framePos;
            frame.Width = FRAMEWIDTH;
            frame.Height = 100;

            namePanel.Location = frame.Location;
            namePanel.Width = FRAMEWIDTH;
            namePanel.Height = 40;
            
            jackPanel.X = frame.X;
            jackPanel.Y = namePanel.Bottom;
            jackPanel.Width = FRAMEWIDTH;
            jackPanel.Height = JACKSIZE * 2;
            inJack = new Rectangle(jackPanel.Left + 15, jackPanel.Top + (JACKSIZE/2), JACKSIZE, JACKSIZE);
            outJack = new Rectangle(jackPanel.Right - (15 + JACKSIZE), jackPanel.Top + (JACKSIZE / 2), JACKSIZE, JACKSIZE);

            isSelected = false;
            isTargeted = false;

            if (unit.udata.utype == UNITTYPE.OUTPUT)
            {
                progPanel = new ProgramPanel();
                progPanel.setPos(frame.X, frame.Y + 80);
                frame.Height += progPanel.frame.Height;
                progPanel.progMax = unit.udata.progCount;
                progPanel.patchbox = this;
            }
        }

        //no param cons for serialization
        public PatchBox()
        {
            canvas = null;
            unit = null;
            unitName = "none";
            inConnectors = new List<PatchLine>();
            outConnectors = new List<PatchLine>();

            frame = new Rectangle(0, 0, 100, 100);
            namePanel = new Rectangle(0, 0, 100, 40);
            jackPanel = new Rectangle(0, 40, 100, 40);
            inJack = new Rectangle(10, 50, 20, 20);
            outJack = new Rectangle(70, 50, 20, 20);

            isSelected = false;
            isTargeted = false;
        }

        //all connections should have been deleted first
        public void delete()
        {
            unit.delete();      //remove backing unit from model
        }

        public bool hitTest(Point p)
        {
            return (frame.Contains(p));
        }

        public bool dragTest(Point p)
        {
            return (namePanel.Contains(p));
        }

        //for now, only can start connection from input/modifier box's out jack
        public bool jackTest(Point p)
        {
            return ((unit.udata.utype == UNITTYPE.INPUT || unit.udata.utype == UNITTYPE.MODIFIER) && (jackPanel.Contains(p)));
        }

        public void setSelected(bool _selected)
        {
            isSelected = _selected;
        }

        public void setTargeted(bool _targeted)
        {
            isTargeted = _targeted;
        }

        public Point getPos()
        {
            return frame.Location;
        }

        public void setPos(Point pos)
        {
            int xpos = pos.X;
            int ypos = pos.Y;
            frame.Location = pos;
            namePanel.Location = pos;
            jackPanel.X = frame.Left;
            jackPanel.Y = namePanel.Bottom;
            inJack.X = jackPanel.Left + 10;
            inJack.Y = jackPanel.Top + 10;
            outJack.X = jackPanel.Right - 30;
            outJack.Y = jackPanel.Top + 10;
            foreach (PatchLine line in inConnectors)
            {
                line.setInEndPos(inJackPos());
            }
            foreach (PatchLine line in outConnectors)
            {
                line.setOutEndPos(outJackPos());
            }
            if (progPanel != null)
            {
                progPanel.setPos(frame.X, frame.Y + 80);
            }
        }

        public Point inJackPos()
        {

            return new Point(inJack.Left + (JACKSIZE / 2), inJack.Top + (JACKSIZE / 2));
        }

        public Point outJackPos()
        {

            return new Point(outJack.Left + (JACKSIZE / 2), outJack.Top + (JACKSIZE / 2));
        }

//- connection handling -------------------------------------------------------

        public void connectInJack(PatchLine line)
        {
            inConnectors.Add(line);
        }

        public void connectOutJack(PatchLine line)
        {
            outConnectors.Add(line);
        }

        public void disconnectInJack(PatchLine line)
        {
            inConnectors.Remove(line);
        }

        public void disconnectOutJack(PatchLine line)
        {
            outConnectors.Remove(line);
        }

        //called by canvas to get all of a box's connections for deletion
        public List<PatchLine> getConnectionList()
        {
            List<PatchLine> connections = new List<PatchLine>();
            connections.AddRange(inConnectors);
            connections.AddRange(outConnectors);
            return connections;
        }

//- painting ------------------------------------------------------------------

        public void paint(Graphics g) 
        {
            //frame
            g.FillRectangle(BACKGROUNDCOLOR, frame);
            g.DrawRectangle(isSelected ? SELECTEDBORDER : (isTargeted ? TARGETEDBORDER : NORMALBORDER), frame);
            
            //name box
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(unit.udata.name, NAMEFONT, NAMECOLOR, namePanel, stringFormat);
            g.DrawLine(NORMALBORDER, namePanel.Left, namePanel.Bottom, namePanel.Right, namePanel.Bottom);

            //jack panel
            //input & modifier boxes have OUTPUT jacks
            if (unit.udata.utype == UNITTYPE.INPUT || unit.udata.utype == UNITTYPE.MODIFIER)
            {
                g.DrawEllipse(JACKBORDER, outJack);
                g.FillEllipse(JACKCOLOR, outJack);
                outJack.Inflate(-5, -5);
                g.FillEllipse(JACKHOLE, outJack);
                outJack.Inflate(5, 5);
            }

            //output & modifier boxes have INPUT jacks
            if (unit.udata.utype == UNITTYPE.OUTPUT || unit.udata.utype == UNITTYPE.MODIFIER)
            {
                g.DrawEllipse(JACKBORDER, inJack);
                g.FillEllipse(JACKCOLOR, inJack);
                inJack.Inflate(-5, -5);
                g.FillEllipse(JACKHOLE, inJack);
                inJack.Inflate(5, 5);
            }
            g.DrawLine(NORMALBORDER, jackPanel.Left, jackPanel.Bottom, jackPanel.Right, jackPanel.Bottom);

            //program panel
            if (unit.udata.utype == UNITTYPE.OUTPUT)
            {
                progPanel.paint(g);
            }
        }
    }

    [Serializable]
    public class ProgramPanel
    {
        [XmlIgnore]
        public PatchBox patchbox;
        public int progNum;
        public int progMax;
        public Rectangle frame;
        public Rectangle display;
        public Point[] leftArrow;
        public Point[] rightArrow;

        public ProgramPanel()
        {
            progNum = 1;
            progMax = 99;
            frame = new Rectangle(0, 0, 100, 40);
            display = new Rectangle(20, 10, 60, 20);
            leftArrow = new Point[] { new Point(5, 20), new Point(15, 10), new Point(15, 30) };
            rightArrow = new Point[] { new Point(85, 10), new Point(95, 20), new Point(85, 30)};
        }

        public void setPos(int newX, int newY)
        {
            int xOfs = newX - frame.X;
            int yOfs = newY - frame.Y;
            frame.Offset(xOfs, yOfs);
            display.Offset(xOfs, yOfs);
            for (int i = 0; i < 3; i++) {leftArrow[i].Offset(xOfs, yOfs); }
            for (int i = 0; i < 3; i++) {rightArrow[i].Offset(xOfs, yOfs); }
        }

        public bool hitTest(Point p)
        {
            return (frame.Contains(p));
        }

        public void handleClick(Point pos, PatchBox _box) 
        {
            patchbox = _box;                //duct tape
            int xpos = pos.X - frame.X;
            if (xpos > 80)
            {
                progNum++;
                if (progNum > progMax) progNum = 1;
                ((OutputUnit)patchbox.unit).sendProgramChange(progNum);                
            } 
            else if (xpos < 20) 
            {
                progNum--;
                if (progNum < 1) progNum = progMax;
                ((OutputUnit)patchbox.unit).sendProgramChange(progNum);
            }         
        }

        public void paint(Graphics g)
        {
            g.DrawRectangle(Pens.Black, frame);
            g.FillRectangle(Brushes.Black, display);
            g.FillPolygon(Brushes.Red, leftArrow);
            g.FillPolygon(Brushes.Red, rightArrow);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(progNum.ToString(), SystemFonts.DefaultFont, Brushes.Red, display, stringFormat);            
        }
    }
}
