using System.Drawing;
using System.Drawing.Drawing2D;
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

        /// <summary>
        /// Определяем попадание точки на контур фигуры
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
        public override bool Contained(Point mousePosition)
        {
            using (var path = new GraphicsPath())
            {
                // добавляем в путь контур полилинии
                path.AddLines(markers.Select(x => x.Location).ToArray());
                path.AddRectangles(markers.Select(x => x.Bounds).ToArray());
                // ширина линии для поиска попадания равна размеру маркера
                using (var pen = new Pen(Color.Black, 5f))
                {
                    pen.LineJoin = LineJoin.Round;
                    pen.StartCap = LineCap.RoundAnchor;
                    pen.EndCap = LineCap.RoundAnchor;
                    return path.IsOutlineVisible(mousePosition, pen);
                }
            }
        }
    }
}