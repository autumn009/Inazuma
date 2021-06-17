using Inazuma;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
            updateCombo();
        }

        private void updateCombo()
        {
            ComboBoxMacros.Items.Clear();
            foreach (var item in Macros.EnumMainMacroEntry())
            {
                ComboBoxMacros.Items.Add(item);
            }
            if (ComboBoxMacros.Items.Count > 0) ComboBoxMacros.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            updateWorkingDirectory();
            var r = Macros.Load();
            if (r == null)
            {
                Macros.CopyTempToMain();
                updateCombo();
            }
            else
                MessageBox.Show(this, r);

        }

        private void ComboBoxMacros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ComboBoxMacros.SelectedItem as MacroItem;
            if (item == null) return;
            TextBoxCommandLine.Text = item.CommandLine;
            TextBoxCommandLine.SelectAll();
            CheckBoxDefaultEncoding.IsChecked = item.IsDefaultEncoding;
        }

        private async Task executeAsync(string commandLine, bool useDefaultEncoding)
        {
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            if (useDefaultEncoding) encoding = System.Text.Encoding.Default;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = $"/C {commandLine}";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.StandardErrorEncoding = encoding;
            p.StartInfo.StandardOutputEncoding = encoding;
            p.StartInfo.StandardInputEncoding = encoding;
            p.EnableRaisingEvents = true;
            p.Start();
            StreamWriter myStreamWriter = p.StandardInput;
            Task taskToInput = myStreamWriter.WriteAsync(TextBoxSrc.Text);
            Task<string> taskToOutout = p.StandardOutput.ReadToEndAsync();
            Task<string> taskToError = p.StandardError.ReadToEndAsync();
            await taskToInput;
            myStreamWriter.Close();
            await p.WaitForExitAsync();
            var output = taskToOutout.Result;
            var error = taskToError.Result;
            if (error.Trim().Length > 0)
                TextBoxDst.Text = output + "\r\n\r\nError Output:\r\n" + error;
            else
                TextBoxDst.Text = output;
        }

        private async void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            var old = this.Cursor;
            this.Cursor = Cursors.Wait;
            await executeAsync(TextBoxCommandLine.Text, CheckBoxDefaultEncoding.IsChecked == true );
            this.Cursor = old;
        }

        private void updateWorkingDirectory()
        {
            LabelWorkingDir.Text = Directory.GetCurrentDirectory();
        }

        private void ButtonSetDir_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();

            // フォルダ選択ダイアログ（falseにするとファイル選択ダイアログ）
            dlg.IsFolderPicker = true;
            // タイトル
            dlg.Title = "Select Working Directory";
            // 初期ディレクトリ
            dlg.InitialDirectory = LabelWorkingDir.Text;

            if (dlg.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
            {
                Directory.SetCurrentDirectory(dlg.FileName);
                updateWorkingDirectory();
            }
        }
    }
}
