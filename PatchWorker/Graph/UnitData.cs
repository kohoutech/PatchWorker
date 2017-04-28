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
