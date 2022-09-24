using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inazuma
{
    public static class General
    {
        public static bool IsIgnoreChar(long ch)
        {
            return ch == 0x0d;
        }
        public static bool IsSkipChar(long ch)
        {
            return ch == -1;
        }
        public static bool IsEOLChar(long ch)
        {
            return ch == 0x0a;
        }
    }
}
