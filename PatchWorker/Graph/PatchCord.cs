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
using System.Drawing;
using System.Xml;

using Transonic.Patch;
using Transonic.MIDI;
using PatchWorker.Dialogs;

namespace PatchWorker.Graph
{
    public class PatchCord : iPatchConnector
    {
        public PatchUnit srcUnit;
        public PatchUnit destUnit;
        public PatchLine line;
        public int transpose;
        public int loRange;
        public int hiRange;

        public PatchCord(PatchUnit _srcUnit, PatchUnit _destUnit)
        {
            srcUnit = _srcUnit;
            destUnit = _destUnit;

            transpose = 0;
            loRange = 0;
            hiRange = 127;
        }

        public void disconnect()
        {
            srcUnit = null;
            destUnit = null;
        }

        public void setLine(PatchLine _line)
        {
            line = _line;
        }

//- user input ----------------------------------------------------------------

        public void onDoubleClick(Point pos)
        {
            PatchCordDialog patchdlg = new PatchCordDialog(this);
            patchdlg.setTitle(srcUnit.name, destUnit.name);
            patchdlg.initDialogValues(transpose, loRange, hiRange);
            patchdlg.Show(line.canvas);
        }

        public void setPatchCordValues(PatchCordDialog patchdlg)
        {
            transpose = patchdlg.transpose;
            loRange = patchdlg.loRange;
            hiRange = patchdlg.hiRange;

            //Console.WriteLine(" PATCH CORD: range = " + loRange + " to " + hiRange + " transpose = " + transpose);
        }

        public void onRightClick(Point pos)
        {
        }


//- processing ----------------------------------------------------------------

        public void processMidiMsg(Message msg)
        {
            //filter on msgs with note numbers - note on / note off / aftertouch
            if (msg is NoteOnMessage)
            {
                NoteOnMessage noteMsg = (NoteOnMessage)msg;
                if ((noteMsg.noteNumber >= loRange) && (noteMsg.noteNumber <= hiRange))
                {
                    noteMsg.noteNumber += transpose;
                    destUnit.processMidiMsg(noteMsg);
                }
            }
            else if (msg is NoteOffMessage)
            {
                NoteOffMessage noteMsg = (NoteOffMessage)msg;
                if ((noteMsg.noteNumber >= loRange) && (noteMsg.noteNumber <= hiRange))
                {
                    noteMsg.noteNumber += transpose;
                    destUnit.processMidiMsg(noteMsg);
                }
            }
            else if (msg is AftertouchMessage)
            {
                AftertouchMessage noteMsg = (AftertouchMessage)msg;
                if ((noteMsg.noteNumber >= loRange) && (noteMsg.noteNumber <= hiRange))
                {
                    noteMsg.noteNumber += transpose;
                    destUnit.processMidiMsg(noteMsg);
                }
            }
            else
            {
                destUnit.processMidiMsg(msg);
            }
        }

//- persistance ---------------------------------------------------------------

        public void loadFromXML(XmlNode lineNode)
        {
            foreach (XmlNode cordNode in lineNode.ChildNodes)
            {
                if (cordNode.Name.Equals("patchcord"))
                {
                    transpose = Convert.ToInt32(cordNode.Attributes["transpose"].Value);
                    loRange = Convert.ToInt32(cordNode.Attributes["lorange"].Value);
                    hiRange = Convert.ToInt32(cordNode.Attributes["hirange"].Value);
                }
            }
        }

        public void saveToXML(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("patchcord");
            xmlWriter.WriteAttributeString("transpose", transpose.ToString());
            xmlWriter.WriteAttributeString("lorange", loRange.ToString());
            xmlWriter.WriteAttributeString("hirange",hiRange.ToString());            
            xmlWriter.WriteEndElement();
        }

    }
}

//Console.WriteLine("There's no sun in the shadow of the Wizard");
