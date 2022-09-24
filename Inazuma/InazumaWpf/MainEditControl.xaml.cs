using Inazuma;
using System;
using System.Collections.Generic;
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
        }

        public void SelectAll()
        { 
            // TBW
        }

        public string Text { get; set; }    // TBW

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
                    if(vvram[x, y] > 65535)
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
                    drawingContext.DrawText(formattedText, new Point(x*xCharSize, y * yCharSize));
                }
            }
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
            resizeSub();
        }
    }
}
