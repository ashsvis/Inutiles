using System.Drawing;
using System.Linq;

namespace Graphics
{
    public class Polyline : Markers
    {
        /// <summary>
        /// Рисуем линию через все точки
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(System.Drawing.Graphics graphics)
        {
            if (markers.Count < 2) return;
            graphics.DrawLines(Pens.DarkMagenta, markers.Select(x => x.Location).ToArray());
        }

        public void DrawRibbonLine(System.Drawing.Graphics graphics, Point mousePosition)
        {
            if (markers.Count < 1) return;
            graphics.DrawLine(Pens.Magenta, markers.Last().Location, mousePosition);
        }

    }
}