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

using PatchWorker.Dialogs;
using Transonic.Patch;
using Transonic.MIDI;

namespace PatchWorker.Graph
{
    public class PatchCord 
    {
        public PatchUnit srcUnit;
        public PatchUnit destUnit;
        //public PatchWire line;
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

        //public void setLine(PatchWire _line)
        //{
        //    line = _line;
        //}

//- user input ----------------------------------------------------------------

        public void setPatchCordValues(PatchCordDialog patchdlg)
        {
            transpose = patchdlg.transpose;
            loRange = patchdlg.loRange;
            hiRange = patchdlg.hiRange;

            //Console.WriteLine(" PATCH CORD: range = " + loRange + " to " + hiRange + " transpose = " + transpose);
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
    }
}

//Console.WriteLine("There's no sun in the shadow of the Wizard");
