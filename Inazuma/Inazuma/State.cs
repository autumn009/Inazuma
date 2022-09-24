﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inazuma
{
    public static class State
    {
        public static string FileName { get; set; }
        public static FileAbsotactionLayer FileAbsotactionLayer { get; set; }
        public static VirtualVRam VirtualVRam { get; set; }
        public static long MasterPointer1 { get; set; }
        public static long MasterPointer2 { get; set; }
        public static long SelectionPointer { get; set; }


    }
}
