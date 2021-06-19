using Inazuma;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
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
            foreach (var item in Macros.EnumMainMacroEntry().OrderByDescending(c => c.LastUse))
            {
                ComboBoxMacros.Items.Add(new MacroItemForMain(item));
            }
            if (ComboBoxMacros.Items.Count > 0) ComboBoxMacros.SelectedIndex = 0;
        }

        private void updateVersion()
        {
            AssemblyName name = Assembly.GetExecutingAssembly().GetName();
            LabelVersion.Text = $"Version {name.Version}";
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            updateVersion();
            try
            {
                Directory.SetCurrentDirectory(InazumaWpf.Properties.Settings.Default.workingDir);
            }
            catch (Exception)
            {
                // nop
            }
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
            var item = ComboBoxMacros.SelectedItem as MacroItemForMain;
            if (item == null) return;
            TextBoxCommandLine.Text = item.ReferBody.CommandLine;
            TextBoxCommandLine.SelectAll();
            CheckBoxDefaultEncoding.IsChecked = item.ReferBody.IsDefaultEncoding;
        }
        [DllImport("kernel32.dll")]
        [ResourceExposure(ResourceScope.None)]
        internal static extern int GetACP();

        private async Task executeAsync(string commandLine, bool useDefaultEncoding)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            string options = "";
            if (useDefaultEncoding)
            {
                var encoding = System.Text.Encoding.GetEncoding(GetACP());
                p.StartInfo.StandardErrorEncoding = encoding;
                p.StartInfo.StandardOutputEncoding = encoding;
                p.StartInfo.StandardInputEncoding = encoding;
            }
            else
            {
                p.StartInfo.StandardInputEncoding = System.Text.Encoding.Unicode;
                p.StartInfo.StandardOutputEncoding = System.Text.Encoding.Unicode;
                p.StartInfo.StandardErrorEncoding = System.Text.Encoding.Unicode;
                options = "/u ";
            }
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = $"{options}/C {commandLine}";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
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
            if (string.IsNullOrEmpty(TextBoxSrc.Text) && Clipboard.ContainsText())
            {
                TextBoxSrc.Text = Clipboard.GetText();
                TextBoxSrc.SelectAll();
            }
            this.Cursor = Cursors.Wait;
            await executeAsync(TextBoxCommandLine.Text, CheckBoxDefaultEncoding.IsChecked == true);
            var item = ComboBoxMacros.SelectedItem as MacroItemForMain;
            if (item != null)
            {
                item.ReferBody.LastUse = DateTime.Now;
                Macros.SetDirty();
                Macros.CopyMainToTemp();
                Macros.Save();
            }
            updateCombo();
            ComboBoxMacros.SelectedItem = item;
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
                InazumaWpf.Properties.Settings.Default.workingDir = dlg.FileName;
                InazumaWpf.Properties.Settings.Default.Save();
                updateWorkingDirectory();
            }
        }

        private void ButtonPasteSrc_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                TextBoxSrc.Text = Clipboard.GetText();
                TextBoxSrc.SelectAll();
            }
            else
                Console.Beep();
        }

        private void ButtonCopyDst_Click(object sender, RoutedEventArgs e)
        {
            var text = TextBoxDst.SelectedText;
            if (string.IsNullOrEmpty(text)) text = TextBoxDst.Text;
            if (string.IsNullOrEmpty(text))
            {
                Console.Beep();
            }
            Clipboard.Clear();
            Clipboard.SetText(text);
        }
    }
}
