using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Graphics
{
    public class Rect : Markers
    {
        /// <summary>
        /// Рисуем линию через все точки
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(System.Drawing.Graphics graphics, Pen pen = null)
        {
            if (markers.Count < 2) return;
            graphics.DrawRectangles(pen ?? Pens.DarkMagenta, new[] { GetRectangle() });
        }

        /// <summary>
        /// Резиновый прямоугольник тянется от последней точки списка до точки позиции курсора
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="mousePosition"></param>
        /// <param name="pen"></param>
        public void DrawRibbonRect(System.Drawing.Graphics graphics, Point mousePosition, Pen pen = null)
        {
            if (markers.Count < 1) return;
            graphics.DrawRectangles(pen ?? Pens.Magenta, new[] { GetRectangle(mousePosition) });
        }

        private RectangleF GetRectangle(Point mousePosition)
        {
            var location = new PointF(Math.Min(mousePosition.X, markers[0].Location.X), Math.Min(mousePosition.Y, markers[0].Location.Y));
            var size = new SizeF(Math.Abs(mousePosition.X - markers[0].Location.X), Math.Abs(mousePosition.Y - markers[0].Location.Y));
            return new RectangleF(location, size);
        }

        private RectangleF GetRectangle()
        {
            var location = new PointF(Math.Min(markers[1].Location.X, markers[0].Location.X), Math.Min(markers[1].Location.Y, markers[0].Location.Y));
            var size = new SizeF(Math.Abs(markers[1].Location.X - markers[0].Location.X), Math.Abs(markers[1].Location.Y - markers[0].Location.Y));
            return new RectangleF(location, size);
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
                path.AddRectangle(GetRectangle());
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

        /// <summary>
        /// Удаляем ранее созданный маркер
        /// </summary>
        /// <param name="marker"></param>
        public override void Remove(Marker marker)
        {
            // удаление маркеров не используется для прямоугольника
        }
    }
}