/* ----------------------------------------------------------------------------
Patchworker : a midi patchbay
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
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace PatchWorker.Graph
{
    public enum UNITTYPE : uint
    {
        INPUT = 0,
        MODIFIER,
        OUTPUT
    }

    public class UnitData
    {
        public String name;
        public UNITTYPE utype;
        public String devName;
        public int channelNum;
        public int progCount;

        [XmlIgnore] 
        public ToolStripItem menuItem;

        //dummy cons for serializing
        public UnitData()
        {
            name = null;
            utype = UNITTYPE.INPUT;
            devName = null;
            channelNum = -1;
            progCount = 0;
            menuItem = null;
        }

        public UnitData(String _name, UNITTYPE _utype)
        {
            name = _name;
            utype = _utype;
            devName = null;
            channelNum = -1;
            progCount = 0;
            menuItem = null;
        }
        
        public void setMenuItem(ToolStripItem _menuItem)
        {
            menuItem = _menuItem;
        }

//- persistance ---------------------------------------------------------------

        public static List<UnitData> loadUnitData()
        {
            List<UnitData> ulist = null;

            if (File.Exists("patchworker.ini"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<UnitData>));
                using (XmlReader reader = XmlReader.Create("patchworker.ini"))
                {
                    ulist = (List<UnitData>)xs.Deserialize(reader);
                }
            }
            else
            {
                ulist = new List<UnitData>();
            }
            return ulist;
        }

        public static void saveUnitData(List<UnitData> ulist)
        {
            XmlSerializer xs = new XmlSerializer(ulist.GetType());
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            using (XmlWriter writer = XmlWriter.Create("patchworker.ini", settings))
            {
                xs.Serialize(writer, ulist);
            }            
        }
    }
}
