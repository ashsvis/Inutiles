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
        public override void Draw(System.Drawing.Graphics graphics, Pen pen = null)
        {
            if (markers.Count < 2) return;
            graphics.DrawLines(pen ?? Pens.DarkMagenta, markers.Select(x => x.Location).ToArray());
        }

        /// <summary>
        /// Резиновая линия тянется от последней точки списка до точки позиции курсора
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="mousePosition">точка позиции курсора</param>
        public void DrawRibbonLine(System.Drawing.Graphics graphics, Point mousePosition, Pen pen = null)
        {
            if (markers.Count < 1) return;
            graphics.DrawLine(pen ?? Pens.Magenta, markers.Last().Location, mousePosition);
        }

    }
}