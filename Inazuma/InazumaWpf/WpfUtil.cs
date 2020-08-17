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
        public static Editor GetMyEditor(DependencyObject fromObject)
        {
            var main =  GetMainWindow<MainWindow>(fromObject);
            return main.GetMyEditor();
        }
    }
}
