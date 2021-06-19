using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InazumaWpf
{
    class PredefinedMacros
    {
        public static void AddPredefinedMaros()
        {
            add("grep", "grep \"SEARCH TEXT\"", true);
            add("sort|uniq", "sort|uniq", true);
            add("Current Directory", "CD");
            add("File Association", "ASSOC");
            add("Command Search Path", "PATH");
            add("List Env Var", "SET");
            add("SORT", "SORT");
            add("System Info", "SYSTEMINFO");
            add("Task List", "TASKLIST");
            add("Directory Tree", "TREE");
            add("Windows Version", "VER");
            add("Volume Name", "VOL");
            add("Find String", "FIND \"SEARCH TEXT\"");

            void add(string name, string commandLine, bool isDefaultEncoding = true)
            {
                var item = Macros.AddMacroEntry();
                item.CommandLine = commandLine;
                item.Name = name;
                item.IsDefaultEncoding = isDefaultEncoding;
            }
        }
    }
}
