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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InazumaWpf
{
    /// <summary>
    /// MainEditControl.xaml の相互作用ロジック
    /// </summary>
    public partial class MainEditControl : UserControl
    {
        public MainEditControl()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            // drawing background and border
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Colors.LimeGreen;
            Pen myPen = new Pen(Brushes.Blue, 1);
            Rect myRect = new Rect(0, 0, ActualWidth, ActualHeight);
            drawingContext.DrawRectangle(mySolidColorBrush, myPen, myRect);

            // drawing text
            var editor = WpfUtil.GetMyEditor(this);
            var line = editor.GetCuurentLine();
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
        }
    }
}
