using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

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

        public static Programmer loadFromXML(PatchUnit _unit, XmlNode programmerNode)
        {
            Programmer programmer = new Programmer(_unit);
            programmer.programs.Clear();
            foreach (XmlNode progNode in programmerNode.ChildNodes)
            {
                if (progNode.Name.Equals("program"))
                {
                    String name = progNode.Attributes["name"].Value;
                    programmer.programs.Add(name);
                }
            }
            return programmer;            
        }

        public void saveToXML(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("programmer");

            foreach (String progName in programs)
            {
                xmlWriter.WriteStartElement("program");
                xmlWriter.WriteAttributeString("name", progName);
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
        }
    }
}
