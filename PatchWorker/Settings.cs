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

using PatchWorker.UI;
using PatchWorker.Graph;
using Transonic.MIDI.System;
using Origami.ENAML;

namespace PatchWorker
{
    public class Settings
    {
        public static String VERSION = "1.3.0";
        const String CONFIGFILENAME = "patchworker.cfg";

        PatchWindow patchWnd;

        //global settings
        public int patchWndX;
        public int patchWndY;
        public int patchWndHeight;
        public int patchWndWidth;
        public String patchFolder;

        public Settings(PatchWindow _patchWnd)
        {
            patchWnd = _patchWnd;

            EnamlData data = new EnamlData(CONFIGFILENAME);

            string version = data.getStringValue("version", VERSION);
            patchWndX = data.getIntValue("global-settings.patch-window.x", 100);
            patchWndY = data.getIntValue("global-settings.patch-window.y", 100);
            patchWndWidth = data.getIntValue("global-settings.patch-window.width", 400);
            patchWndHeight = data.getIntValue("global-settings.patch-window.height", 400);
            patchFolder = data.getStringValue("global-settings.patch-window.patch-folder", Application.StartupPath);

            patchWnd.patchWork.loadUnits(data);
        }

        public void save()
        {
            EnamlData data = new EnamlData(CONFIGFILENAME);

            data.setStringValue("version", VERSION);
            data.setIntValue("global-settings.patch-window.x", patchWndX);
            data.setIntValue("global-settings.patch-window.y", patchWndY);
            data.setIntValue("global-settings.patch-window.width", patchWndWidth);
            data.setIntValue("global-settings.patch-window.height", patchWndHeight);
            data.setStringValue("global-settings.patch-window.patch-folder", patchFolder);

            patchWnd.patchWork.saveUnits(data);

            data.saveToFile();
        }
    }
}
