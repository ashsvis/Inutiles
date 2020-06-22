using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Graphics
{
    public class Markers
    {
        private List<Marker> markers = new List<Marker>();

        public void Add(Marker marker)
        {
            markers.Add(marker);
        }

        public void Add(PointF point)
        {
            Add(new Marker() { Location = point });
        }

        public void Remove(Marker marker)
        {
            markers.Remove(marker);
        }

        public void DrawMarkers(System.Drawing.Graphics graphics)
        {
            foreach (var marker in markers)
                graphics.DrawRectangles(Pens.Blue, markers.Select(x => x.Bounds).ToArray());
        }
    }

    public class Marker
    {
        public PointF Location { get; internal set; }
        public SizeF Size { get => new SizeF(6, 6); }

        public RectangleF Bounds => new RectangleF(PointF.Subtract(Location, new SizeF(Size.Width / 2, Size.Height / 2)), Size);
    }
}