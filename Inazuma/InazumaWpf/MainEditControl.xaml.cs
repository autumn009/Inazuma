using Inazuma;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;
using Point = System.Windows.Point;

namespace InazumaWpf
{
    /// <summary>
    /// MainEditControl.xaml の相互作用ロジック
    /// </summary>
    public partial class MainEditControl : UserControl
    {
        private int xCharSize;
        private int yCharSize;

        private void resizeSub()
        {
            if (xCharSize == 0 || yCharSize == 0) return;
            var size = this.RenderSize;
            if (size.Width == 0 || size.Height == 0) return;
            var x = (int)(size.Width / xCharSize);
            var y = (int)(size.Height / yCharSize);
            State.VirtualVRam = new VirtualVRam(x, y, State.FileAbsotactionLayer);

            InvalidateVisual();
        }

        public MainEditControl()
        {
            InitializeComponent();
            AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(DrawArea_MouseDown), true);
            AddHandler(Control.MouseDoubleClickEvent, new MouseButtonEventHandler(ContentControl_MouseDoubleClick), true);
        }

        public void SelectAll()
        {
            // TBW
        }

        public string Text { get; set; }    // TBW


        private DispatcherTimer _timer;

        private void MyTimerMethod(object sender, EventArgs e)
        {
            IInputElement focusedControl = Keyboard.FocusedElement;
            if (DrawArea == focusedControl)
            {
                CursorRect.Visibility = (CursorRect.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                CursorRect.Visibility = Visibility.Collapsed;
            }
        }

        private void startTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 500, 0);
            _timer.Tick += new EventHandler(MyTimerMethod);
            _timer.Start();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (State.VirtualVRam == null) return;

            // drawing background and border
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Colors.LimeGreen;
            Pen myPen = new Pen(Brushes.Blue, 1);
            Rect myRect = new Rect(0, 0, ActualWidth, ActualHeight);
            drawingContext.DrawRectangle(mySolidColorBrush, myPen, myRect);

            // drawing text
            var vvram = State.VirtualVRam.VVRam;
            for (int x = 0; x < vvram.GetLength(0); x++)
            {
                for (int y = 0; y < vvram.GetLength(1); y++)
                {
                    if (vvram[x, y] == -1) continue;
                    string s = "";
                    if (vvram[x, y] > 65535)
                    {
                        // TBW
                    }
                    else
                    {
                        s = ((char)vvram[x, y]).ToString();
                    }
                    FormattedText formattedText = new FormattedText(
                        s,
                        CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch),   // TBW customize
                        this.FontSize, // TBW customize
                        Brushes.Black, // TBW customize
                        1.0);
                    drawingContext.DrawText(formattedText, new Point(x * xCharSize, y * yCharSize));
                }
            }

            // drawing cursor
            //SolidColorBrush myCursorSolidColorBrush = new SolidColorBrush();
            //myCursorSolidColorBrush.Color = Colors.Black;
            //Rect myCursorRect = new Rect(0, 0, Math.Max(2,xCharSize/8), yCharSize);
            //drawingContext.DrawRectangle(myCursorSolidColorBrush, null, myCursorRect);




#if false
            //var editor = WpfUtil.GetMyEditor(this);
            //var line = editor.GetCuurentLine();
            double y = 0;
            for (; ; )
            {
                if (line == null) break;
                FormattedText formattedText = new FormattedText(
                    line.Value.ToString(),
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch),   // TBW customize
                    this.FontSize, // TBW customize
                    Brushes.Black, // TBW customize
                    1.0);
                drawingContext.DrawText(formattedText, new Point(0, y));
                y += formattedText.Height;
                line = line.Next;
            }
#endif
        }

        protected override void OnGotFocus(System.Windows.RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            DrawArea.Focus();
        }

        private void DrawArea_SizeChanged(object sender, SizeChangedEventArgs e) => resizeSub();

        private void DrawArea_Loaded(object sender, RoutedEventArgs e)
        {
            FormattedText formattedText = new FormattedText("A",
                        CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch),   // TBW customize
                        this.FontSize, // TBW customize
                        Brushes.Black, // TBW customize
                        1.0);
            xCharSize = (int)formattedText.Width;
            yCharSize = (int)formattedText.Height;
            CursorRect.Width = Math.Max(2, xCharSize / 8);
            CursorRect.Height = yCharSize;

            resizeSub();
            startTimer();
        }

        private void DrawArea_Unloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        private void DrawArea_TextInput(object sender, TextCompositionEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("TI:" + e.Text);
        }

        private void DrawArea_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    CursorDown();
                    break;
                case Key.Up:
                case Key.Left:
                case Key.Right:
                case Key.PageUp:
                case Key.PageDown:
                    CursorDown(State.VirtualVRam.VVRam.GetLength(1));
                    break;
            }
            e.Handled = true;
            //System.Diagnostics.Debug.WriteLine("KD"+e.Key.ToString());
        }

        private void CursorDown(int count=1)
        {
            var p = State.MasterPointer1;
            for (int i = 0; i < count; i++)
            {
                var block = State.FileAbsotactionLayer.GetBlock(p);
                if (block == null) return;
                for (; ; )
                {
                    if (p >= block.From + block.Image.LongLength)
                    {
                        block = State.FileAbsotactionLayer.GetBlock(p);
                        if (block == null) return;
                    }
                    var ch = block.Image[p++ - block.From];
                    if (General.IsEOLChar(ch)) break;
                }
            }
            State.MasterPointer1 = p;
            State.VirtualVRam.RecreateVRam(State.MasterPointer1);
            InvalidateVisual();
        }

        private void DrawArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DrawArea.Focus();
        }

        private void ContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
