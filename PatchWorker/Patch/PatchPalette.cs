/* ----------------------------------------------------------------------------
Transonic Patch Library
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
using System.Xml.Serialization;
using System.Drawing.Drawing2D;

namespace Transonic.Patch
{
    public class PatchPalette : Control
    {
        const int PALLETEWIDTH = 150;
        public Color PALETTECOLOR = Color.Ivory;
        public Color DISABLEDCOLOR = Color.LightGray;

        public PatchCanvas canvas;
        public Button btnOpen;
        private VScrollBar scrollbar;
        private ToolTip paletteToolTip;
        private Panel panelSpace;

        public int buttonWidth;
        bool isOpen;

        public List<PaletteItem> items;

        public PatchPalette(PatchCanvas _canvas)
        {
            canvas = _canvas;
            this.Size = new Size(PALLETEWIDTH, 100);        //the width is constant, the height changes
            this.BackColor = PALETTECOLOR;

            //ui
            btnOpen = new Button();
            btnOpen.FlatStyle = FlatStyle.System;
            btnOpen.Text = "<";
            btnOpen.Click += new System.EventHandler(btnOpen_Click);
            this.Controls.Add(btnOpen);

            paletteToolTip = new ToolTip();
            paletteToolTip.SetToolTip(btnOpen, "open / close palette");
            
            scrollbar = new VScrollBar();
            scrollbar.Minimum = 0;
            scrollbar.Dock = DockStyle.Right;
            scrollbar.ValueChanged += new EventHandler(scrollbar_ValueChanged);
            this.Controls.Add(scrollbar);

            btnOpen.Size = new System.Drawing.Size(scrollbar.Width, scrollbar.Width);
            btnOpen.Location = new System.Drawing.Point(this.Width - scrollbar.Width, 0);
            buttonWidth = btnOpen.Width;
            isOpen = true;

            panelSpace = new Panel();
            panelSpace.BackColor = PALETTECOLOR;
            panelSpace.Location = new Point(0, 0);
            panelSpace.Size = new Size(0, 0);
            this.Controls.Add(panelSpace);

            items = new List<PaletteItem>();        //empty palette list
            setItems(items);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            updateScrollBar();
        }

        public void setItems(List<PaletteItem> _items)
        {
            items = _items;
            panelSpace.Controls.Clear();

            int ypos = 0;
            int itemWidth = this.Width - buttonWidth;
            foreach (PaletteItem item in items)
            {
                Label itemBox = new Label();
                itemBox.Text = item.name;
                itemBox.TextAlign = ContentAlignment.MiddleCenter;
                itemBox.BorderStyle = BorderStyle.FixedSingle;
                if (!item.enabled)
                {
                    itemBox.BackColor = DISABLEDCOLOR;
                    itemBox.Enabled = false;
                }
                itemBox.Location = new Point(0, ypos);
                itemBox.Size = new Size(itemWidth, 25);                 //needs to have box height calculated
                itemBox.Tag = item;
                itemBox.DoubleClick += new EventHandler(itemBox_DoubleClick);
                panelSpace.Controls.Add(itemBox);
                ypos += itemBox.Height;
            }

            panelSpace.Size = new Size(itemWidth, ypos);
            updateScrollBar();
        }

        //enable or disable palette item w/o rebuilding entire list
        public void enableItem(PaletteItem item)
        {
            if (item.itembox != null)
            {
                item.itembox.BackColor = item.enabled ? PALETTECOLOR : DISABLEDCOLOR;
                item.itembox.Enabled = item.enabled;
                item.itembox.Invalidate();
            }
        }

        //- event handlers ----------------------------------------------------

        void scrollbar_ValueChanged(object sender, EventArgs e)
        {
            panelSpace.Location = new Point(0, -scrollbar.Value);
        }

        //compensate for the fact that if the scrollbar max = 50, the greatest value will be 50 - THUMBWIDTH (value determined experimentally)
        const int THUMBWIDTH = 9;

        //called when the canvas space or item list changes
        void updateScrollBar()
        {
            if (scrollbar != null)
            {
                scrollbar.Maximum = ((this.Height) < panelSpace.Height) ? (panelSpace.Height - this.Height + THUMBWIDTH) : 0;
                if ((scrollbar.Maximum > THUMBWIDTH) && (scrollbar.Maximum - scrollbar.Value < THUMBWIDTH))
                {
                    scrollbar.Value = scrollbar.Maximum - THUMBWIDTH;
                }
            }
        }

        //when any palette item is double clicked
        void itemBox_DoubleClick(object sender, EventArgs e)
        {
            PaletteItem item = (PaletteItem)((Label)sender).Tag;
            if (item.enabled)
            {
                canvas.handlePaletteItemDoubleClick(item);
            }
        }

        //tells the canvas to open or close the palette
        public void btnOpen_Click(object sender, EventArgs e)
        {
            isOpen = !isOpen;
            btnOpen.Text = isOpen ? "<" : ">";
            String tooltiptext = isOpen ? "close palette" : "open palette";
            paletteToolTip.SetToolTip(btnOpen, tooltiptext);
            scrollbar.Visible = isOpen;
            canvas.openPalette(isOpen);
        }

        //public void addInputUnitToMenu(PatchUnit unit)
        //{
        //    ToolStripItem inputItem = new ToolStripMenuItem(unit.name);
        //    inputItem.Click += new EventHandler(unitSelectMenuItem_Click);
        //    inputItem.Tag = unit;
        //    inputItem.Enabled = unit.enabled;
        //    unit.setMenuItem(inputItem);

        //    if (inputUnitMenuItems == 0)
        //    {
        //        unitMenuItem.DropDownItems.Add(new ToolStripSeparator());
        //        inputUnitMenuItems++;
        //    }
        //    unitMenuItem.DropDownItems.Insert((3 + inputUnitMenuItems), inputItem);
        //    inputUnitMenuItems++;
        //}

        //public void addOutputUnitToMenu(PatchUnit unit)
        //{
        //    ToolStripItem outputItem = new ToolStripMenuItem(unit.name);
        //    outputItem.Click += new EventHandler(unitSelectMenuItem_Click);
        //    outputItem.Tag = unit;
        //    outputItem.Enabled = unit.enabled;
        //    unit.setMenuItem(outputItem);

        //    if (outputUnitMenuItems == 0)
        //    {
        //        unitMenuItem.DropDownItems.Add(new ToolStripSeparator());
        //        outputUnitMenuItems++;
        //    }
        //    unitMenuItem.DropDownItems.Insert((3 + inputUnitMenuItems + outputUnitMenuItems), outputItem);
        //    outputUnitMenuItems++;
        //}

        ////handler for all unit menu items
        //private void unitSelectMenuItem_Click(object sender, EventArgs e)
        //{
        //    ToolStripItem item = (ToolStripItem)sender;
        //    PatchUnit unit = (PatchUnit)item.Tag;                      //get patch unit obj from menu item

        //    addUnitToPatch(unit);               //add patch unit to graph
        //    PatchUnitBox box = new PatchUnitBox(unit);      //create new patch box from unit
        //    canvas.addPatchBox(box);                        //and add it to canvas
        //}

        //- units ---------------------------------------------------------------------

        //public PatchUnit findUnit(String unitName)
        //{
        //    PatchUnit result = null;
        //    //foreach (PatchUnit unit in allUnitList)
        //    //{
        //    //    if (unit.name.Equals(unitName))
        //    //    {
        //    //        result = unit;
        //    //        break;
        //    //    }
        //    //}
        //    return result;
        //}

        ////add new unit to unit list & start it up
        //public void addUnitToPatch(PatchUnit unit)
        //{
        //    //patchUnits.Add(unit);
        //    //if (unit is InputUnit || unit is OutputUnit)             //input & output units can only be added once
        //    //{
        //    //    unit.menuItem.Enabled = false;                       //so we disable menu item while unit is in graph
        //    //}

        //    //unit.start();
        //}

        ////connections should already have been removed when this is called
        //public void removeUnitFromPatch(PatchUnit unit)
        //{
        //    //patchUnits.Remove(unit);            //remove the unit from the patch graph
        //    //unit.menuItem.Enabled = true;       //re-enable menu items for input and output units            
        //}

    }

    //-------------------------------------------------------------------------

    //for future organization
    public class PaletteGroup
    {
        public String name;
        public List<PaletteItem> items;

        public PaletteGroup(String _name)
        {
            name = _name;
            items = new List<PaletteItem>();
        }

        public void addItem(PaletteItem item)
        {
            items.Add(item);
        }
    }

    public class PaletteItem
    {
        public String name;
        public bool enabled;
        public Label itembox;
        public object tag;

        public PaletteItem(String _name)
        {
            name = _name;
            enabled = true;
            itembox = null;
            tag = null;
        }
    }
}
