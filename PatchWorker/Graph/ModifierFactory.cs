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
using System.Reflection;

using PatchWorker.Plugin;
using Origami.ENAML;

namespace PatchWorker.Graph
{
    public class ModifierFactory
    {
        public PatchWork patchWork;
        public String plugPath;
        public String plugName;
        public Type pluginType;
        public bool enabled;
        public int plugCount;

        public ModifierFactory(string filename)
        {
            patchWork = null;
            plugPath = filename;
            plugName = "";
            pluginType = null;
            enabled = false;
            plugCount = 0;

            try
            {
                Assembly assembly = Assembly.LoadFrom(plugPath);
                Type[] pluginTypes = assembly.GetTypes();
                foreach (Type type in pluginTypes)
                {
                    Type interfaceType = type.GetInterface("IPatchPlugin");
                    if (interfaceType != null)
                    {
                        //get plugin name for displaying in patch palette
                        //if this works, we know that the plugin .dll is valid & can enable the factory
                        IPatchPlugin plugin = (IPatchPlugin)Activator.CreateInstance(type);
                        plugName = plugin.getName();
                        pluginType = type;
                        enabled = true;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                //if any of the above steps fails, factory will be disbaled
                //Console.WriteLine("mod factory load failed: " + e.Message);
            }
        }

        //create a new modifier unit when the modifier factory entry on the patch palette is clicked
        public ModifierUnit newModifierUnit()
        {
            IPatchPlugin plugin = (IPatchPlugin)Activator.CreateInstance(pluginType);       //create plugin from type info

            ModifierUnit modUnit = new ModifierUnit(this, plugin, ++plugCount);

            plugin.setModifier(modUnit);
            modUnit.patchWork = patchWork;
            return modUnit;
        }

        //unlike input/output units, which are singletons and loaded/saved to/from the config file
        //there can be many modifiers in a patch of the same type, so we load them from the patch
        //and use the modNum val to distinguish between them
        public ModifierUnit loadUnitFromPatch(EnamlData data, string dataPath, int modNum)
        {
            IPatchPlugin plugin = (IPatchPlugin)Activator.CreateInstance(pluginType);
            plugin.loadFromPatch(data, dataPath);

            if (modNum > plugCount) plugCount = modNum;     //so when we start adding more modifiers, 
            //their numbers won't conflict with the ones loaded from the patch

            ModifierUnit modUnit = new ModifierUnit(this, plugin, modNum);
            plugin.setModifier(modUnit);
            modUnit.patchWork = patchWork;
            return modUnit;
        }

        //- persistance -------------------------------------------------------

        public static ModifierFactory loadFromConfig(EnamlData data, string modPath)
        {
            String pluginName = data.getStringValue(modPath + ".name", "");
            String pluginPath = data.getStringValue(modPath + ".plugin-path", "");

            ModifierFactory modFact = new ModifierFactory(pluginPath);      //load plugin .dll from config file path
            if (!modFact.enabled)
            {
                modFact.plugName = pluginName;      //if plugin didn't load this time, use the name from last time it did
            }
            return modFact;
        }

        public void saveToConfig(EnamlData data, string path)
        {
            data.setStringValue(path + ".name", plugName);
            data.setStringValue(path + ".plugin-path", plugPath);
        }
    }
}
