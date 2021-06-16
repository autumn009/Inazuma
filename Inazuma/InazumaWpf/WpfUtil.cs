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
        public override string ToString() => $"{Name} ({Id}): {CommandLine}";
        public MacroItem Clone() => MemberwiseClone() as MacroItem;
    }

    class Macros
    {
        private static List<MacroItem> mainMacroItems = new List<MacroItem>();
        private static List<MacroItem> tempMacroItems = new List<MacroItem>();
        private static bool isDirty = false;

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
            newmac.Name = $"New Item";
            newmac.CommandLine = "Echo New Item (Re-Write me!)";
            tempMacroItems.Add(newmac);
            return newmac;

            string createNewUniqieId()
            {
                for (int i = 0; ; i++)
                {
                    string id = "m" + i.ToString();
                    if (tempMacroItems.Any(s => s.Id == id)) continue;
                    return id;
                }
            }
        }

        public static void SetDirty() => isDirty = true;

        public static void RemoveMacroEntry(string id)
        {

        }

        public static IEnumerable<MacroItem> EnumMacroEntry() => tempMacroItems;

        public static IEnumerable<MacroItem> EnumMainMacroEntry() => mainMacroItems;

        private static void copy(List<MacroItem> src, List<MacroItem> dst)
        {
            dst.Clear();
            foreach (var item in src)
            {
                dst.Add(item.Clone());
            }
        }

        public static void CopyMainToTemp()
        {
            copy(mainMacroItems, tempMacroItems);
        }

        public static void CopyTempToMain()
        {
            copy(tempMacroItems, mainMacroItems);
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
