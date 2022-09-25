using Inazuma;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
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
        private int yCursor;

        public void ResizeSub()
        {
            if (xCharSize == 0 || yCharSize == 0) return;
            var size = this.RenderSize;
            if (size.Width == 0 || size.Height == 0) return;
            var x = (int)(size.Width / xCharSize);
            var y = (int)(size.Height / yCharSize);
            State.VirtualVRam = new VirtualVRam(x, y, State.FileAbsotactionLayer);

            InvalidateVisual();
        }

        private void setCursorPos(int xCursor, int yCursor)
        {
            CursorRect.Margin = new Thickness(xCursor * xCharSize, yCursor * yCharSize, 0, 0);
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
                    var brush = Brushes.Black;
                    if (General.IsEOLChar(vvram[x,y]))
                    {
                        brush = Brushes.DarkRed;
                        s = "⊢";
                    }

                    FormattedText formattedText = new FormattedText(
                        s,
                        CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch),   // TBW customize
                        this.FontSize, // TBW customize
                        brush, // TBW customize
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

        private void DrawArea_SizeChanged(object sender, SizeChangedEventArgs e) => ResizeSub();

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

            ResizeSub();
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
                    CursorUp();
                    break;
                case Key.Left:
                    break;
                case Key.Right:
                    CursorRight();
                    break;
                case Key.PageUp:
                    CursorUp(State.VirtualVRam.VVRam.GetLength(1));
                    break;
                case Key.PageDown:
                    CursorDown(State.VirtualVRam.VVRam.GetLength(1));
                    break;
                default:
                    return; // 後処理をすっ飛ばす
            }
            e.Handled = true;
            //System.Diagnostics.Debug.WriteLine("KD"+e.Key.ToString());
        }

        private long seekLineTop(long p)
        {
            var block = State.FileAbsotactionLayer.GetBlock(p);
            if (block == null) return -1;
            p--;    // skip current char
            for (; ; )
            {
                if (p <= 0)
                {
                    return 0;   // line top is text top
                }
                if (p < block.From)
                {
                    block = State.FileAbsotactionLayer.GetBlock(p);
                    if (block == null) return -1;
                }
                var ch = block.Image[p-- - block.From];
                if (General.IsEOLChar(ch)) break;
            }
            p++;    // skip current char
            return p;
        }

        private long[,] analizeLineToVVRamFormat(int xSize, long p)
        {
            p = seekLineTop(p);
            List<long[]> list = new List<long[]>();
            bool exit = false;
            for(; ; )
            {
                var line = Enumerable.Repeat(-1, xSize).Cast<long>().ToArray();
                for (int i = 0; i < line.Length; i++)
                {
                    // TBW マルチバイト対応
                    var ch = State.FileAbsotactionLayer.GetByte(p++);
                    if (General.IsEOLChar(ch))
                    {
                        exit = true;
                        break;
                    }
                    line[i] = ch;
                }
                list.Add(line);
                if (exit) break;
            }
            var vvram = new long[xSize, list.Count];
            for (int y = 0; y < vvram.GetLength(0); y++)
            {
                for (int x = 0; x < vvram.GetLength(1); x++)
                {
                    vvram[x, y] = list[y][x];
                }
            }
            return vvram;
        }

        private int getCurrentCursorX(int xSize, long pTarget)
        {
            var p = seekLineTop(pTarget);
            for (; ; )
            {
                for (int i = 0; i < xSize; i++)
                {
                    if (p == pTarget) return i;
                    // TBW マルチバイト対応
                    var ch = State.FileAbsotactionLayer.GetByte(p++);
                    if (General.IsEOLChar(ch))
                    {
                        return i;
                    }
                }
            }
        }

        private void CursorUp(int count = 1)
        {
            var p = State.MasterPointer1;
            if (p <= 0) return;

            for (int i = 0; i < count; i++)
            {
                var block = State.FileAbsotactionLayer.GetBlock(p);
                if (block == null) return;
                p--;    // skip top of current line
                p--;    // skip EOL in prev line
                for (; ; )
                {
                    if (p <= 0)
                    {
                        p = 0;
                        goto direct;
                    }
                    if (p < block.From)
                    {
                        block = State.FileAbsotactionLayer.GetBlock(p);
                        if (block == null) return;
                    }
                    var ch = block.Image[p-- - block.From];
                    if (General.IsEOLChar(ch)) break;
                }
                p++;    // skip last char of prev line
                p++;    // go to top of next line
            }
            direct:
            State.MasterPointer1 = p;
            State.VirtualVRam.RecreateVRam(State.MasterPointer1);
            InvalidateVisual();
        }

        private void CursorRight()
        {
            for (; ; )
            {
                State.MasterPointer1++;
                long ch = State.FileAbsotactionLayer.GetByte(State.MasterPointer1);
                if (!General.IsSkipChar(ch)) break;
            }
            int xCursor = getCurrentCursorX(State.VirtualVRam.VVRam.GetLength(0), State.MasterPointer1);
            setCursorPos(xCursor, yCursor);
        }

        private void CursorDown(int count = 1)
        {
            var p = State.MasterPointer1;
            for (int i = 0; i < count; i++)
            {
                var block = State.FileAbsotactionLayer.GetBlock(p);
                if (block == null) return;
                int xCursor = getCurrentCursorX(State.VirtualVRam.VVRam.GetLength(0), State.MasterPointer1);
                bool exceedLine = false;
                for (int x = 0; x < State.VirtualVRam.VVRam.GetLength(0); x++)
                {
                    if (p >= block.From + block.Image.LongLength)
                    {
                        block = State.FileAbsotactionLayer.GetBlock(p);
                        if (block == null) return;
                    }
                    var ch = block.Image[p++ - block.From];
                    if (General.IsEOLChar(ch))
                    {
                        exceedLine = true;
                        break;
                    }
                }
                if (exceedLine)
                {
                    for (int j = 0; j < State.VirtualVRam.VVRam.GetLength(0); j++)
                    {
                        if (j == xCursor) break;
                        long ch = State.FileAbsotactionLayer.GetByte(p++);
                        if (General.IsEOLChar(ch)) break;
                    }
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
