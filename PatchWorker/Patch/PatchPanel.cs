﻿/* ----------------------------------------------------------------------------
Transonic Patch Library
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
using System.Windows.Forms;
using System.Drawing;
using System.Xml;

namespace Transonic.Patch
{
    public class PatchPanel
    {
        public enum CONNECTIONTYPE 
        {
            SOURCE,
            DEST,
            NEITHER
        }

        public String panelType;
        public static int panelCount = 0;
        public int panelNum;

        public PatchBox patchbox;
        public Rectangle frame;
        public PatchLine connector;
        public CONNECTIONTYPE connType;

        public PatchPanel(PatchBox box)
        {
            panelType = "PatchPanel";
            panelNum = ++panelCount;
            patchbox = box;

            frame = new Rectangle(0, 0, patchbox.frame.Width, 40);
            connector = null;
            connType = CONNECTIONTYPE.NEITHER;
        }

        public virtual void setPos(int xOfs, int yOfs)
        {
            frame.Offset(xOfs, yOfs);
            if (connector != null)
            {
                if (connType == CONNECTIONTYPE.SOURCE)
                {
                    connector.setSourceEndPos(getConnectionPoint());
                }
                if (connType == CONNECTIONTYPE.DEST)
                {
                    connector.setDestEndPos(getConnectionPoint());
                }
            }
        }

        public bool hitTest(Point p)
        {
            return (frame.Contains(p));
        }

//- connections ---------------------------------------------------------------

        public bool canConnectIn()
        {
            return (connType == CONNECTIONTYPE.DEST);
        }

        public bool canConnectOut()
        {
            return (connType == CONNECTIONTYPE.SOURCE);
        }

        //default connection point - dead center of the frame
        public virtual Point getConnectionPoint()
        {
            return new Point(frame.Left + frame.Width / 2, frame.Top + frame.Height / 2);
        }

        public virtual void connectLine(PatchLine line)
        {
            connector = line;
        }

        public virtual void disconnectLine(PatchLine line)
        {
            connector = null;
        }

        public bool isConnected()
        {
            return (connector != null);
        }

        //called on source panel when a patch line connects two panels, so matching connection can be made in the backing model
        public virtual void makeConnection(PatchPanel destPanel)
        {            
        }

        //called on source panel when two panels are disconnected, so matching connection can be ended in the backing model
        public virtual void breakConnection(PatchPanel destPanel)
        {
        }

//- user input ----------------------------------------------------------------

        public virtual void onClick(MouseEventArgs e)
        {
        }

        public virtual void onDoubleClick(Point pos)
        {
        }

        public virtual void onRightClick(Point pos)
        {
        }

//- painting ------------------------------------------------------------------

        public virtual void paint(Graphics g)
        {
            g.DrawRectangle(Pens.Black, frame);
        }

//- persistance ---------------------------------------------------------------

        static Dictionary<String, PatchPanelLoader> panelTypeList = new Dictionary<String, PatchPanelLoader>();

        public static void registerPanelType(String panelName, PatchPanelLoader loader)
        {
            panelTypeList.Add(panelName, loader);
        }

        //loading
        public static PatchPanel loadFromXML(PatchBox box, XmlNode panelNode)
        {
            PatchPanel panel = null;
            String panelName = panelNode.Attributes["paneltype"].Value;
            PatchPanelLoader loader = panelTypeList[panelName];
            if (loader != null)
            {
                panel = loader.loadFromXML(box, panelNode);
            }
            return panel;
        }

        public virtual void loadAttributesFromXML(XmlNode panelNode)
        {
            panelNum = Convert.ToInt32(panelNode.Attributes["number"].Value);            
        }
        
        //saving
        public void saveToXML(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("panel");
            saveAttributesToXML(xmlWriter);
            xmlWriter.WriteEndElement();
        }

        public virtual void saveAttributesToXML(XmlWriter xmlWriter)
        {
            xmlWriter.WriteAttributeString("paneltype", panelType);
            xmlWriter.WriteAttributeString("number", panelNum.ToString());            
        }
    }

//- panel loader class --------------------------------------------------------

    //subclassed by descendants so correct class will be loaded from XML file
    public class PatchPanelLoader
    {
        public virtual PatchPanel loadFromXML(PatchBox box, XmlNode panelNode)
        {
            PatchPanel panel = new PatchPanel(box);
            panel.loadAttributesFromXML(panelNode);
            return panel;
        }
    }
}

//Console.WriteLine("there's no sun in the shadow of the Wizard");
