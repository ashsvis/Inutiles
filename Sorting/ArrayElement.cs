using System.Drawing;

namespace Sorting
{
    internal class ArrayElement : Element
    {
        public PointF Location { get; set; }

        public SizeF Size { get; set; }

        public int Value { get; set; }

        public override void DrawAt(Graphics graphics)
        {
            var rect = new RectangleF(Location, Size);
            graphics.FillRectangle(Brushes.White, rect);
            graphics.DrawRectangles(Pens.Black, new[] { rect });
            using (var format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                graphics.DrawString($"{Value}", SystemFonts.DefaultFont, Brushes.Black, rect, format);
            }
        }
    }

}