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

namespace PatchWorker.Graph
{
    public class Programmer
    {
        public PatchUnit unit;
        public int progCount;
        public List<String> programs;

        public Programmer(PatchUnit _unit)
        {
            unit = _unit;
            progCount = unit.progCount;
            programs = new List<string>(progCount);
            for (int i = 0; i < progCount; i++)
            {
                programs.Add("program " + (i + 1).ToString());
            }
        }

        //public static Programmer loadFromXML(PatchUnit _unit, XmlNode programmerNode)
        //{
        //    Programmer programmer = new Programmer(_unit);
        //    programmer.programs.Clear();
        //    foreach (XmlNode progNode in programmerNode.ChildNodes)
        //    {
        //        if (progNode.Name.Equals("program"))
        //        {
        //            String name = progNode.Attributes["name"].Value;
        //            programmer.programs.Add(name);
        //        }
        //    }
        //    return programmer;            
        //}

        //public void saveToXML(XmlWriter xmlWriter)
        //{
        //    xmlWriter.WriteStartElement("programmer");

        //    foreach (String progName in programs)
        //    {
        //        xmlWriter.WriteStartElement("program");
        //        xmlWriter.WriteAttributeString("name", progName);
        //        xmlWriter.WriteEndElement();
        //    }
        //    xmlWriter.WriteEndElement();
        //}
    }
}
