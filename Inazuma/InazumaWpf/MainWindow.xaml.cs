using Inazuma;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InazumaWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Editor myEditor = new Editor();

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            //if (State.FileName != null) Open(State.FileName);
        }

        //       internal Editor GetMyEditor() => myEditor;

        //internal void Open(string fileName)
        //{
        //myEditor.Load(fileName);
        //}

        private void ButtonEditMacros_Click(object sender, RoutedEventArgs e)
        {
            var diaglog = new MacroEditorMain();
            diaglog.ShowDialog();
        }
    }
}
