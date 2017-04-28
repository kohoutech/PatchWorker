using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using PatchWorker.Graph;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;

namespace PatchWorker.UI
{
    public class PatchCanvas : Control
    {
        [XmlIgnore]
        public PatchWindow patchwin;
        List<PatchBox> boxList;
        List<PatchLine> lineList;

        [XmlIgnore]
        PatchBox selectedBox;
        [XmlIgnore]
        PatchBox targetBox;

        [XmlIgnore]
        Point newBoxOrg;

        [XmlIgnore]
        bool dragging;
        [XmlIgnore]
        Point dragOrg;
        [XmlIgnore]
        Point dragOfs;

        [XmlIgnore]
        bool connecting;
        [XmlIgnore]
        PatchLine connectLine;

        //cons
        public PatchCanvas(PatchWindow _patchwin)
        {
            patchwin = _patchwin;
            boxList = new List<PatchBox>();
            lineList = new List<PatchLine>();

            newBoxOrg = new Point(50, 50);

            this.BackColor = Color.FromArgb(0xF2, 0x85, 0x00);
            this.DoubleBuffered = true;

            dragging = false;
            connecting = false;
            selectedBox = null;
            targetBox = null;
        }

        public void clearPatch()
        {
            List<PatchLine> dellineList = new List<PatchLine>(lineList);
            foreach (PatchLine line in dellineList)
            {
                removePatchLine(line);
            }
            PatchBox.boxCount = 0;
            List<PatchBox> delboxList = new List<PatchBox>(boxList);
            foreach (PatchBox box in delboxList)
            {
                removePatchBox(box);
            }
            newBoxOrg = new Point(50, 50);
        }

        public void savePatch(String patchFileName)
        {
            CanvasArchive arch = new CanvasArchive(boxList, lineList);

            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            
            XmlSerializer xs = new XmlSerializer(arch.GetType());
            using (XmlWriter writer = XmlWriter.Create(patchFileName, settings))
            {
                xs.Serialize(writer, arch);
            }
        }

        public void loadPatch(String patchFileName)
        {
            clearPatch();       //start with a clean slate

            CanvasArchive arch = null;
            XmlSerializer xs = new XmlSerializer(typeof(CanvasArchive));
            using (XmlReader reader = XmlReader.Create(patchFileName))
            {
                arch = (CanvasArchive)xs.Deserialize(reader);
            }
            int maxBoxNum = 0;
            foreach (PatchBox box in arch.boxList)
            {
                loadPatchBox(box);
                if (box.boxNum > maxBoxNum) maxBoxNum = box.boxNum;
            }
            PatchBox.boxCount = maxBoxNum;                  //start num new boxes after the nighest box num from loaded patch
            foreach (PatchLine line in arch.lineList)
            {
                loadPatchLine(line);
            }
            Invalidate();
        }

//- box methods ---------------------------------------------------------------

        public void addPatchBox(PatchUnit unit)
        {
            PatchBox box = new PatchBox(this, unit, newBoxOrg);
            newBoxOrg.Offset(20, 20);
            if (!this.ClientRectangle.Contains(newBoxOrg))
            {
                newBoxOrg = new Point(50, 50);      //if we've gone outside the canvas, reset to original new box pos
            }
            boxList.Add(box);
            Invalidate();
        }

        public void loadPatchBox(PatchBox box)
        {
            box.canvas = this;
            UnitData udata = patchwin.patchworker.findUnitData(box.unitName);
            box.unit = patchwin.patchworker.addUnitToPatch(udata);
            boxList.Add(box);
        }

        public void removePatchBox(PatchBox box)
        {
            List<PatchLine> connections = box.getConnectionList();
            foreach (PatchLine line in connections)
            {
                removePatchLine(line);                  //remove all connections first
            }
            boxList.Remove(box);                //and remove box from canvas            
            box.delete();
            Invalidate();            
        }

        public void selectPatchBox(PatchBox box)
        {
            if (selectedBox != null)
            {
                selectedBox.setSelected(false);     //deselect current selection, if there is one
            }
            boxList.Remove(box);                //remove the box from its place in z-order
            boxList.Add(box);                   //and add to end of list, making it topmost
            selectedBox = box;                  //mark box as selected for future operations
            selectedBox.setSelected(true);      //and let it know it
            Invalidate();
        }

        public PatchBox findPatchBox(int boxNum)
        {
            PatchBox result = null;
            foreach (PatchBox box in boxList)
            {
                if (box.boxNum == boxNum)
                {
                    result = box;
                    break;
                }
            }
            return result;
        }

//- line methods ---------------------------------------------------------------

        public void loadPatchLine(PatchLine line)
        {
            line.canvas = this;
            PatchBox outBox = findPatchBox(line.outNum);        //find boxes from box nums in line
            PatchBox inBox = findPatchBox(line.inNum);
            line.connectOutputJack(outBox);                     //connect out jack first
            line.connectInputJack(inBox);                       //then in jack - and connect backing units in model
            lineList.Add(line);
        }

        //patch line will be connected to output jack, but may or may not be connected to input jack
        public void removePatchLine(PatchLine line)
        {
            line.disconnect();
            lineList.Remove(line);
        }

//- mouse handling ------------------------------------------------------------

        //dragging & connecting are handled while mouse button is pressed and end when it it let up
        //all other ops are handled with mouse clicks
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            bool handled = false;

            //go in reverse z-order so we check topmost first
            for (int i = boxList.Count - 1; i >= 0; i--)
            {
                PatchBox box = boxList[i];
                if (box.hitTest(e.Location))
                {
                    selectPatchBox(box);
                    handled = true;
                    break;
                }
            }

            if (handled)        //we clicked somewhere inside a patchbox (and selected it), check if dragging or connecting
            {
                if (selectedBox.dragTest(e.Location))           //if we clicked on name panel
                {
                    startDrag(e.Location);
                }
                else if (selectedBox.jackTest(e.Location))      //if we clicked on jack panel
                {
                    startConnection(e.Location);
                }

            }
            else            //we clicked on a blank area of the canvas - deselect current selection if there is one
            {
                if (selectedBox != null)
                {
                    selectedBox.setSelected(false);
                    Invalidate();
                }
                selectedBox = null;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (dragging)
            {
                drag(e.Location);
            }

            if (connecting)
            {
                moveConnection(e.Location);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (dragging)
            {
                endDrag(e.Location);
            }

            if (connecting)
            {
                finishConnection(e.Location);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (selectedBox != null && selectedBox.progPanel != null && selectedBox.progPanel.hitTest(e.Location))                
            {
                selectedBox.progPanel.handleClick(e.Location, selectedBox);
            }
            Invalidate();
        }

//- keyboard handling ---------------------------------------------------------

        //delete key removes currently selected box & any connections to other boxes from canvas
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (selectedBox != null)
                {
                    removePatchBox(selectedBox);
                    selectedBox = null;
                }
            }
        }

//- dragging ------------------------------------------------------------------

        //track diff between pos when mouse button was pressed and where it is now, and move box by the same offset
        public void startDrag(Point p)
        {
            dragging = true;
            dragOrg = selectedBox.getPos();
            dragOfs = p;
        }

        public void drag(Point p)
        {
            int newX = p.X - dragOfs.X;
            int newY = p.Y - dragOfs.Y;
            selectedBox.setPos(new Point(dragOrg.X + newX, dragOrg.Y + newY));
            Invalidate();
        }

        public void endDrag(Point p)
        {
            dragging = false;
        }

//- connecting ------------------------------------------------------------------

        //for now, connections start from selected box's output jack
        //if it doesn't have output jack, it would fail jack hit test & not get here
        public void startConnection(Point p)
        {
            connecting = true;
            connectLine = new PatchLine(this, selectedBox, p);      //create new line & connect output end to selected box
            lineList.Add(connectLine);                              //add to canvas
            targetBox = null;
        }

        public void moveConnection(Point p)
        {
            connectLine.setInEndPos(p);         //move line
            
            //check if currently over a possible target box
            bool handled = false;
            for (int i = boxList.Count - 1; i >= 0; i--)
            {
                PatchBox box = boxList[i];
                if (box.hitTest(p))
                {
                    if (box != selectedBox)         //check selected box in case another box is under it, but don't connect to itself
                    {
                        if (targetBox != null)
                        {
                            targetBox.setTargeted(false);     //deselect current target, if there is one
                        }
                        targetBox = box;                      //mark box as current target, if we drop connection on it
                        targetBox.setTargeted(true);          //and let the box know it
                        handled = true;
                    }
                    break;
                }
            }

            //if we aren't currently over any targets, unset prev target, if one
            if ((!handled) && (targetBox != null))
            {
                targetBox.setTargeted(false);
                targetBox = null;
            }

            Invalidate();
        }

        public void finishConnection(Point p)
        {
            if (targetBox != null)                              //drop connection on target box we are currently over
            {
                connectLine.connectInputJack(targetBox);        //connect line to tagret box and input & output units in model
                targetBox.setTargeted(false);
                targetBox = null;
            }
            else            //not over a target box, delete patch line
            {
                removePatchLine(connectLine);                
            }
            connecting = false;
            Invalidate();
        }
        
//- painting ------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //z-order is front to back - the last one in list is topmost
            for (int i = 0; i < boxList.Count; i++)
            {
                boxList[i].paint(g);
            }

            for (int i = 0; i < lineList.Count; i++)
            {
                lineList[i].paint(g);
            }
        }
    }

//-----------------------------------------------------------------------------

    [Serializable]
    public class CanvasArchive
    {
        public List<PatchBox> boxList;
        public List<PatchLine> lineList;

        public CanvasArchive()
        {
            boxList = new List<PatchBox>();
            lineList = new List<PatchLine>();
        }

        public CanvasArchive(List<PatchBox> _boxList, List<PatchLine> _lineList)
        {
            boxList = _boxList;
            lineList = _lineList;
        }
    }
}

//Console.WriteLine(" not over a connection target");
