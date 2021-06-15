using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InazumaWpf
{
    /// <summary>
    /// MacroEditorMain.xaml の相互作用ロジック
    /// </summary>
    public partial class MacroEditorMain : Window
    {
        public MacroEditorMain()
        {
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAddNew_Click(object sender, RoutedEventArgs e)
        {
            var newitem = Macros.AddMacroEntry();
            int index = ListBoxMacros.Items.Add(newitem);
            if (index < 0) return;
            ListBoxMacros.SelectedIndex = index;    // select created item
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
