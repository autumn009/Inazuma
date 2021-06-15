using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Inazuma;

namespace InazumaWpf
{

    class MacroItem
    {
        public string Id;
        public string Name;
        public string CommandLine;
        public DateTime LastUse;
        public bool IsDefaultEncoding;
        public override string ToString()
        {
            return $"{Name} ({Id}): {CommandLine}";
        }
    }

    class Macros
    {
        private static List<MacroItem> macroItems = new List<MacroItem>();

        public static void Load()
        {
            
        }

        public static void Save()
        {
        }

        public static MacroItem AddMacroEntry()
        {
            var newmac = new MacroItem();
            newmac.Id = createNewUniqieId();
            newmac.Name = $"New Macro Item({newmac.Id})";
            newmac.CommandLine = "Echo New Macro Item (Re-Write me!)";
            macroItems.Add(newmac);
            return newmac;

            string createNewUniqieId()
            {
                for (int i = 0; ; i++)
                {
                    string id = "macid" + i.ToString();
                    if (macroItems.Any(s => s.Id == id)) continue;
                    return id;
                }
            }
        }

        public static void RemoveMacroEntry(string id)
        {

        }

        public static IEnumerable<MacroItem> EnumMacroEntry()
        {
            return macroItems;
        }




    }

    class WpfUtil
    {
        public static T GetMainWindow<T>(DependencyObject fromObject) where T : DependencyObject
        {
            var p = fromObject;
            for (; ; )
            {
                var next = VisualTreeHelper.GetParent(p);
                if (next == null) return p as T;
                p = next;
            }
        }
        //public static Editor GetMyEditor(DependencyObject fromObject)
        //{
            //var main =  GetMainWindow<MainWindow>(fromObject);
            //return main.GetMyEditor();
        //}
    }
}
