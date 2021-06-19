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
            add("sed", "sed -e 's/SEARCH_FOR/REPlACE_WITH/g'", true);
            add("awk", "awk '/SEARCH_FOR/ { print $0 }'", true);
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
            add("Welcome", "echo Welcome to Inazuma Procedural Text Editor, please re-write this command to learn how to use.");

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
