using Microsoft.Win32;
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

        MacroItem Selection = null;

        private void updateItem()
        {
            if (Selection != null)
            {
                if (Selection.Name != TextBoxName.Text
                || Selection.CommandLine != TextBoxCommandLine.Text
                || Selection.IsDefaultEncoding != (CheckBoxIsDefaultEncoding.IsChecked == true))
                {
                    Selection.Name = TextBoxName.Text;
                    Selection.CommandLine = TextBoxCommandLine.Text;
                    Selection.IsDefaultEncoding = CheckBoxIsDefaultEncoding.IsChecked == true;
                    Macros.SetDirty();
                    ListBoxMacros.Items.Refresh();
                }
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            updateItem();
            Macros.Save();
            Macros.CopyTempToMain();
            Close();
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
            var selectedIndex = ListBoxMacros.SelectedIndex;
            var selectedItem = ListBoxMacros.SelectedItem as MacroItem;
            if (selectedItem == null) return;
            Selection = null;
            ListBoxMacros.Items.Remove(selectedItem);
            Macros.RemoveMacroEntry(selectedItem.Id);
            if (selectedIndex < ListBoxMacros.Items.Count) ListBoxMacros.SelectedIndex = selectedIndex;
        }

        private void ListBoxMacros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateItem();
            MacroItem item = ListBoxMacros.SelectedItem as MacroItem;
            if (item != null)
            {
                Selection = item;
                TextBoxName.Text = item.Name;
                TextBoxCommandLine.Text = item.CommandLine;
                CheckBoxIsDefaultEncoding.IsChecked = item.IsDefaultEncoding;
            }
            else
            {
                Selection = null;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Selection = null;
            TextBoxName.Text = "";
            TextBoxCommandLine.Text = "";
            CheckBoxIsDefaultEncoding.IsChecked = false;

            Macros.CopyMainToTemp();
            ListBoxMacros.Items.Clear();
            foreach (var item in Macros.EnumMacroEntry())
            {
                ListBoxMacros.Items.Add(item);
            }
            if(ListBoxMacros.Items.Count>0)
            {
                ListBoxMacros.SelectedIndex = 0;
            }
        }
    }
}
