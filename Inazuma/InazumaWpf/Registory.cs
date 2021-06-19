using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace InazumaWpf
{
    class Registory
    {
        public static int SetCodePage(int newCodePage=65001)
        {
            const string path = @"Console\%SystemRoot%_system32_cmd.exe";
            const string key = "CodePage";

            if (newCodePage == 0)
            {
                using (var m = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(path, true))
                {
                    m.DeleteValue(key);
                }
                return 0;
            }

            using (var m = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(path, true))
            {
                var oldval = m.GetValue(key);
                m.SetValue(key, newCodePage);
                if (oldval == null) return 0;
                var v = (long)oldval;
                return (int)v;
            }
        }
    }
}
